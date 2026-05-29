using Terraria.ID;
using Terraria.ModLoader;

namespace Macrocosm.Content.Items.MusicBoxes;

[LegacyName("SpaceInvaderMusicBox")]
public class CraterDemonMusicBox : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.CanGetPrefixes[Type] = false;
        ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.MusicBox;
        MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Music/CraterDemon"), ModContent.ItemType<CraterDemonMusicBox>(), ModContent.TileType<Tiles.MusicBoxes.CraterDemonMusicBox>());
    }

    public override void SetDefaults()
    {
        Item.DefaultToMusicBox(ModContent.TileType<Tiles.MusicBoxes.CraterDemonMusicBox>(), 0);
    }
}
