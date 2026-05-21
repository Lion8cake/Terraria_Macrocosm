using Macrocosm.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Macrocosm.Content.Tiles.Blocks.Sands;

public class Bitumen : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
        Main.tileBlockLight[Type] = true;
        Main.tileBrick[Type] = true;
        Main.tileMergeDirt[Type] = true;

        TileID.Sets.ChecksForMerge[Type] = true;
        TileID.Sets.CanBeClearedDuringOreRunner[Type] = true;

        MineResist = 0.5f;
        DustType = ModContent.DustType<BitumenDust>();

        AddMapEntry(new Color(42, 41, 47));
    }

    public override bool HasWalkDust() => true;

    public override void WalkDust(ref int dustType, ref bool makeDust, ref Color color)
    {
        dustType = ModContent.DustType<BitumenDust>();
    }
}
