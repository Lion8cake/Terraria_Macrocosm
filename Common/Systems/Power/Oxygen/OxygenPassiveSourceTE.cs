using System;
using System.Linq;

namespace Macrocosm.Common.Systems.Power.Oxygen;

public abstract class OxygenPassiveSourceTE : MachineTE, IOxygenPassiveSource
{
    public virtual bool IsProvidingOxygen { get; private set; }
    public abstract int MaxRoomSize { get; }

    public override void UpdatePowerState()
    {
        if (!IsOnFrame && IsProvidingOxygen)
            TurnOn(automatic: true);
        else if (IsOnFrame && !IsProvidingOxygen)
            TurnOff(automatic: true);
    }

    public virtual void TryActivateOxygen()
    {
        IsProvidingOxygen = false;
        if (Circuit is null)
            return;

        foreach (var node in Circuit)
        {
            if (node is IOxygenActiveSource oxyGen && oxyGen.IsProvidingOxygen)
            {
                int linked = Circuit.OfType<OxygenPassiveSourceTE>().Where(src => src != this).Count(other => other.IsProvidingOxygen);
                if (linked < oxyGen.MaxPassiveSources)
                {
                    IsProvidingOxygen = true;
                    return;
                }
            }
        }
    }
}
