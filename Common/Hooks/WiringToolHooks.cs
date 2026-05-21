using Macrocosm.Common.Systems.Connectors;
using Macrocosm.Common.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Macrocosm.Common.Hooks;

public class WiringToolHooks : ILoadable
{
    public void Load(Mod mod)
    {
        On_Player.ItemCheck_UseWiringTools += On_Player_ItemCheck_UseWiringTools;
    }

    public void Unload()
    {
        On_Player.ItemCheck_UseWiringTools -= On_Player_ItemCheck_UseWiringTools;
    }

    // Can't use UseItem here as it doesn't fail early (calls every hook & can't skip vanilla logic)
    private void On_Player_ItemCheck_UseWiringTools(On_Player.orig_ItemCheck_UseWiringTools orig, Player player, Item item)
    {
        if (player.whoAmI == Main.myPlayer && player.ItemInTileReach(item) && player.CanDoWireStuffHere(Player.tileTargetX, Player.tileTargetY) && player.itemAnimation > 0 && player.ItemTimeIsZero && player.controlUseItem)
        {
            Point pos = player.TargetCoords();
            if (item.type == ItemID.WireCutter)
            {
                // Run default Wire Cutter logic first, then remove conveyors if vanilla did not spend the use
                orig(player, item);

                if (player.ItemTimeIsZero && ConveyorSystem.Remove(pos))
                    ApplyWiringToolTime(player, item);

                return;
            }
        }

        orig(player, item);
    }

    private static void ApplyWiringToolTime(Player player, Item item)
    {
        player.ApplyItemTime(item);
        player.toolTime = item.useTime;
    }
}
