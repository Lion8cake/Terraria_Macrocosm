using Macrocosm.Content.Items.Plants;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Macrocosm.Content.Items.Walls;

public class HeveaFence : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 400;
    }

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableWall(ModContent.WallType<Content.Walls.HeveaFence>());
        Item.width = 30;
        Item.height = 30;
    }

    public override void AddRecipes()
    {
        CreateRecipe(4)
            .AddIngredient<HeveaWood>()
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
