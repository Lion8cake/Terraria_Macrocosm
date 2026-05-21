using Macrocosm.Common.Systems.Power;
using Macrocosm.Content.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using Terraria.ModLoader;

namespace Macrocosm.Content.Machines.Consumers.Drills;

public class OreDrillTE : BaseDrillTE
{
    private SlotId activeSoundSlot = SlotId.Invalid;

    public override MachineTile MachineTile => ModContent.GetInstance<OreDrill>();
    protected override float ExcavateRate => 120;
    public override int InventorySize => 30;

    // Sample area: matches machine footprint width (4), 5 rows deep
    public override int SampleGridWidth  => 4;
    public override int SampleGridHeight => 5;

    public override void MachineUpdate()
    {
        MinPower = 1f;
        MaxPower = 80f;
        base.MachineUpdate();
    }

    protected override void UpdateActiveSounds()
    {
        UpdateLoopedActiveSound(
            ref activeSoundSlot,
            SFX.DrillLoop,
            nameof(OreDrillTE),
            () => MathHelper.Lerp(0.55f, 0.75f, ActiveSoundPowerProgress),
            () => MathHelper.Lerp(0f, 0.25f, ActiveSoundPowerProgress)
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
