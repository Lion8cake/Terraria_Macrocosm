using Macrocosm.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Macrocosm.Content.Walls;

public class HeveaFence : ModWall
{
    public override void SetStaticDefaults()
    {
        Main.wallHouse[Type] = true;
        Main.wallLight[Type] = true;

        AddMapEntry(new Color(90, 44, 30));

        DustType = ModContent.DustType<HeveaDust>();
    }
}
