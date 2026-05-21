using Macrocosm.Common.Systems.Power;
using Macrocosm.Content.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using Terraria.ModLoader;

namespace Macrocosm.Content.Machines.Consumers.Drills;

public class OreExcavatorTE : BaseDrillTE
{
    private SlotId activeSoundSlot = SlotId.Invalid;

    public override MachineTile MachineTile => ModContent.GetInstance<OreExcavator>();
    protected override float ExcavateRate => 60;
    public override int InventorySize => 50;

    // Sample area: matches machine footprint width (7), 10 rows deep
    public override int SampleGridWidth  => 7;
    public override int SampleGridHeight => 10;

    public override void MachineUpdate()
    {
        MinPower = 5f;
        MaxPower = 300f;
        base.MachineUpdate();
    }

    protected override void UpdateActiveSounds()
    {
        UpdateLoopedActiveSound(
            ref activeSoundSlot,
            SFX.DrillLoop,
            nameof(OreExcavatorTE),
            () => MathHelper.Lerp(0.65f, 0.85f, ActiveSoundPowerProgress),
            () => MathHelper.Lerp(-0.15f, 0.1f, ActiveSoundPowerProgress)
        );
    }

    protected override void StopActiveSounds()
    {
        StopLoopedActiveSound(ref activeSoundSlot);
    }

    public override void DrawMachinePowerInfo(SpriteBatch spriteBatch, Vector2 basePosition, Color lightColor)
    {
        basePosition.X -= 12;
        base.DrawMachinePowerInfo(spriteBatch, basePosition, lightColor);
    }
}
