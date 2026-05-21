using Macrocosm.Common.Systems.Power;
using Terraria.ModLoader;

namespace Macrocosm.Content.Machines.Generators.Steam;

public class SteamEngineTE : GeneratorTE
{
    public override MachineTile MachineTile => ModContent.GetInstance<SteamEngine>();

    public override void MachineUpdate()
    {
        MaxGeneratedPower = 200f; // simple constant output
        GeneratedPower = IsOnFrame ? MaxGeneratedPower : 0f; // no auto turn on
    }
}

