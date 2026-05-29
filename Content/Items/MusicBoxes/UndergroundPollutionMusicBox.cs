using Terraria.ID;
using Terraria.ModLoader;

namespace Macrocosm.Content.Items.MusicBoxes;

public class UndergroundPollutionMusicBox : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.CanGetPrefixes[Type] = false;
        ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.MusicBox;
        MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Music/UndergroundPollution"), ModContent.ItemType<UndergroundPollutionMusicBox>(), ModContent.TileType<Tiles.MusicBoxes.UndergroundPollutionMusicBox>());
    }

    public override void SetDefaults()
    {
        Item.DefaultToMusicBox(ModContent.TileType<Tiles.MusicBoxes.UndergroundPollutionMusicBox>(), 0);
    }
}
