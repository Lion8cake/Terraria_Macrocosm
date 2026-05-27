using Macrocosm.Common.Systems.Power;
using Macrocosm.Common.Utils;
using System;
using Terraria;

namespace Macrocosm.Content.Machines.Generators.Wind;

public abstract class WindTurbineTEBase : GeneratorTE
{
    protected abstract float BaseGeneratedPower { get; }
    protected virtual int WindCheckHeight => MachineTile.Height - 1;

    public override bool IsRunning => GetWindEfficiency() > 0f && WorldGen.InAPlaceWithWind(Position.X, Position.Y, MachineTile.Width, WindCheckHeight);

    public override void MachineUpdate()
    {
        MaxGeneratedPower = BaseGeneratedPower;
        GeneratedPower = IsRunning ? MaxGeneratedPower * GetWindEfficiency() : 0f;
    }

    protected float GetWindEfficiency()
    {
        float windSpeed = Math.Clamp(Math.Abs(Utility.WindSpeedScaled) * 100f, 0f, 100f);
        if (windSpeed <= 0f)
            return 0f;

        float efficiencyPercent = 100f / (1f + 100f * MathF.Exp(-2f * windSpeed / 3f));
        return Math.Clamp(efficiencyPercent / 100f, 0f, 1f);
    }
}
