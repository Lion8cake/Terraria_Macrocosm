using Macrocosm.Content.Items.Plants;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Macrocosm.Content.Items.Furniture.Hevea;

public class HeveaBathtub : ModItem
{
    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.Hevea.HeveaBathtub>());
        Item.width = 32;
        Item.height = 22;
        Item.value = 150;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient<HeveaWood>(14)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
