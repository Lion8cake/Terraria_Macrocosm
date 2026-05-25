using Macrocosm.Content.Items.Plants;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Macrocosm.Content.Items.Furniture.Hevea;

public class HeveaBookcase : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Hevea.HeveaBookcase>());
        Item.width = 24;
        Item.height = 32;
        Item.value = 150;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient<HeveaWood>(20)
            .AddIngredient(ItemID.Book, 10)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
