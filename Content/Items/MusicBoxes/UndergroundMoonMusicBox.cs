using Terraria.ID;
using Terraria.ModLoader;

namespace Macrocosm.Content.Items.MusicBoxes;

[LegacyName("StygiaMusicBox")]
public class UndergroundMoonMusicBox : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.CanGetPrefixes[Type] = false;
        ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.MusicBox;
        MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Music/UndergroundMoon"), ModContent.ItemType<UndergroundMoonMusicBox>(), ModContent.TileType<Tiles.MusicBoxes.UndergroundMoonMusicBox>());
    }

    public override void SetDefaults()
    {
        Item.DefaultToMusicBox(ModContent.TileType<Tiles.MusicBoxes.UndergroundMoonMusicBox>(), 0);
    }
}
