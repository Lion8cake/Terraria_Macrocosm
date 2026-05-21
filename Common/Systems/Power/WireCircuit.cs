using Macrocosm.Common.DataStructures;
using Macrocosm.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.Localization;

namespace Macrocosm.Common.Systems.Power;

public class WireCircuit : Circuit<MachineTE>
{
    public WireType WireType { get; }

    public WireCircuit(WireType wireType)
    {
        WireType = wireType;
    }

    public override void Merge(Circuit<MachineTE> other)
    {
        if (other is WireCircuit wireOther && wireOther.WireType == WireType)
        {
            foreach (var node in wireOther.nodes)
            {
                Add(node);
                if (node is MachineTE machine)
                {
                    machine.Circuit = this;
                }
            }
            other.Clear();
        }
    }

    public override void Solve(int updateRate)
    {
        float deltaTime = GetDeltaTime(updateRate);
        float totalGeneratorOutput = 0f;
        float totalConsumerDemand = 0f;
        float totalBatteryStoredEnergy = 0f;

        var generators = new List<GeneratorTE>();
        var consumers = new List<ConsumerTE>();
        var batteries = new List<BatteryTE>();

        foreach (var node in nodes)
        {
            if (node is GeneratorTE generator)
            {
                generators.Add(generator);
                totalGeneratorOutput += generator.GeneratedPower;
            }
            else if (node is ConsumerTE consumer)
            {
                consumers.Add(consumer);
                totalConsumerDemand += consumer.PowerDemand;
            }
            else if (node is BatteryTE battery)
            {
                batteries.Add(battery);
                battery.PowerFlow = 0f;
                totalBatteryStoredEnergy += battery.StoredEnergy;
            }
        }

        float netPower = totalGeneratorOutput - totalConsumerDemand;

        if (netPower >= 0f)
        {
            DistributePowerToConsumers(consumers, 1f);
            float excessPower = netPower;
            StoreExcessPowerInBatteries(batteries, excessPower, updateRate);
        }
        else
        {
            float powerNeeded = -netPower;
            float energyNeeded = powerNeeded * deltaTime;

            if (totalBatteryStoredEnergy >= energyNeeded)
            {
                DistributePowerToConsumers(consumers, 1f);
                DrawPowerFromBatteries(batteries, energyNeeded, deltaTime);
            }
            else
            {
                float totalAvailablePower = totalGeneratorOutput + totalBatteryStoredEnergy / deltaTime;
                float circuitPowerFactor = totalAvailablePower / totalConsumerDemand;

                DistributePowerToConsumers(consumers, circuitPowerFactor);
                DrawPowerFromBatteries(batteries, totalBatteryStoredEnergy, deltaTime);
            }
        }
    }


    private void DistributePowerToConsumers(List<ConsumerTE> consumers, float circuitPowerFactor)
    {
        foreach (var consumer in consumers)
        {
            consumer.InputPower = consumer.PowerDemand * circuitPowerFactor;
        }
    }

    private void StoreExcessPowerInBatteries(List<BatteryTE> batteries, float excessPower, int updateRate)
    {
        float deltaTime = GetDeltaTime(updateRate);
        float totalEnergyToStore = excessPower * deltaTime; // kW * s = kJ

        var availableBatteries = batteries.Where(b => b.StoredEnergy < b.EnergyCapacity).ToList();

        while (totalEnergyToStore > 0f && availableBatteries.Count > 0)
        {
            float energyPerBattery = totalEnergyToStore / availableBatteries.Count;
            bool anyStored = false;

            foreach (var battery in availableBatteries.ToList())
            {
                float capacityLeft = battery.EnergyCapacity - battery.StoredEnergy;
                float energyToStore = Math.Min(energyPerBattery, capacityLeft);

                if (energyToStore > 0f)
                {
                    battery.PowerFlow += energyToStore / deltaTime;
                    battery.StoredEnergy += energyToStore;
                    totalEnergyToStore -= energyToStore;
                    anyStored = true;

                    if (battery.StoredEnergy >= battery.EnergyCapacity)
                        availableBatteries.Remove(battery);
                }
            }

            if (!anyStored)
                break;
        }
    }

    private void DrawPowerFromBatteries(List<BatteryTE> batteries, float totalEnergyNeeded, float deltaTime)
    {
        var availableBatteries = batteries.Where(b => b.StoredEnergy > 0f).ToList();

        while (totalEnergyNeeded > 0f && availableBatteries.Count > 0)
        {
            float energyPerBattery = totalEnergyNeeded / availableBatteries.Count;
            bool anyDrawn = false;

            foreach (var battery in availableBatteries.ToList())
            {
                float energyAvailable = battery.StoredEnergy;
                float energyToDraw = Math.Min(energyPerBattery, energyAvailable);

                if (energyToDraw > 0f)
                {
                    battery.PowerFlow -= energyToDraw / deltaTime;
                    battery.StoredEnergy -= energyToDraw;
                    totalEnergyNeeded -= energyToDraw;
                    anyDrawn = true;

                    if (battery.StoredEnergy <= 0f)
                        availableBatteries.Remove(battery);
                }
            }

            if (!anyDrawn)
                break;
        }
    }

    private static float GetDeltaTime(int updateRate) => MathF.Max(1, updateRate) / 60f;

    public string GetPowerInfo()
    {
        float totalGeneratorOutput = 0f;
        float totalConsumerDemand = 0f;
        float totalBatteryStoredEnergy = 0f;
        float totalBatteryCapacity = 0f;
        float totalBatteryFlow = 0f;

        foreach (var node in nodes)
        {
            switch (node)
            {
                case GeneratorTE generator:
                    totalGeneratorOutput += generator.GeneratedPower;
                    break;
                case ConsumerTE consumer:
                    totalConsumerDemand += consumer.PowerDemand;
                    break;
                case BatteryTE battery:
                    totalBatteryStoredEnergy += battery.StoredEnergy;
                    totalBatteryCapacity += battery.EnergyCapacity;
                    totalBatteryFlow += battery.PowerFlow;
                    break;
            }
        }

        return Language.GetText($"Mods.Macrocosm.Machines.Common.PowerInfo.Circuit").Format(
            $"{totalGeneratorOutput:F2}",
            $"{totalConsumerDemand:F2}",
            $"{totalBatteryStoredEnergy:F2}",
            $"{totalBatteryCapacity:F2}",
            $"{totalBatteryFlow:+0.00;-0.00;0.00}"
        );
    }
}
