using Macrocosm.Content.Items.Plants;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Macrocosm.Content.Items.Furniture.Hevea;

public class HeveaChair : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Hevea.HeveaChair>());
        Item.width = 16;
        Item.height = 34;
        Item.value = 150;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient<HeveaWood>(4)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
