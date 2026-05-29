using Terraria.ID;
using Terraria.ModLoader;

namespace Macrocosm.Content.Items.MusicBoxes;

[LegacyName("IntoTheUnknownMusicBox")]
public class MenuMusicBox : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.CanGetPrefixes[Type] = false;
        ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.MusicBox;
        MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Music/Menu"), ModContent.ItemType<MenuMusicBox>(), ModContent.TileType<Tiles.MusicBoxes.MenuMusicBox>());
    }

    public override void SetDefaults()
    {
        Item.DefaultToMusicBox(ModContent.TileType<Tiles.MusicBoxes.MenuMusicBox>(), 0);
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient<MoonDayMusicBox>()
            .AddIngredient<MoonNightMusicBox>()
            .AddIngredient<UndergroundMoonMusicBox>()
            .AddIngredient<CraterDemonMusicBox>()
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
}
