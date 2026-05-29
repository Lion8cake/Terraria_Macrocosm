using Terraria.ID;
using Terraria.ModLoader;

namespace Macrocosm.Content.Items.MusicBoxes;

[LegacyName("DeadworldMusicBox")]
public class MoonDayMusicBox : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.CanGetPrefixes[Type] = false;
        ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.MusicBox;
        MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Music/MoonDay"), ModContent.ItemType<MoonDayMusicBox>(), ModContent.TileType<Tiles.MusicBoxes.MoonDayMusicBox>());
    }

    public override void SetDefaults()
    {
        Item.DefaultToMusicBox(ModContent.TileType<Tiles.MusicBoxes.MoonDayMusicBox>(), 0);
    }
}
