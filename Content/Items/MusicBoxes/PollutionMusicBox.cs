using Terraria.ID;
using Terraria.ModLoader;

namespace Macrocosm.Content.Items.MusicBoxes;

public class PollutionMusicBox : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.CanGetPrefixes[Type] = false;
        ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.MusicBox;
        MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Music/Pollution"), ModContent.ItemType<PollutionMusicBox>(), ModContent.TileType<Tiles.MusicBoxes.PollutionMusicBox>());
    }

    public override void SetDefaults()
    {
        Item.DefaultToMusicBox(ModContent.TileType<Tiles.MusicBoxes.PollutionMusicBox>(), 0);
    }
}
