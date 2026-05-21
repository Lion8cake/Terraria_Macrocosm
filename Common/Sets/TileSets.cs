using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace Macrocosm.Common.Sets;

/// <summary> Tile Sets for special behavior of some Tiles, useful for crossmod. </summary>
[ReinitializeDuringResizeArrays]
internal class TileSets
{
    /// <summary> Tile types that count for the Graveyard biome. </summary>
    public static bool[] CountsForGraveyard { get; } = TileID.Sets.Factory.CreateNamedSet(nameof(CountsForGraveyard)).Description("Tile types that count for the Graveyard biome.").RegisterBoolSet();

    /// <summary> The Tree Tile type, different from <see cref="TileID.Trees"/>, that this tile grows into. Used for custom saplings. </summary>
    public static int[] SaplingTreeGrowthType { get; } = TileID.Sets.Factory.CreateNamedSet(nameof(SaplingTreeGrowthType)).Description("The Tree Tile type, different from TileID.Trees, that this tile grows into. Used for custom saplings.").RegisterIntSet(defaultState: -1);

    /// <summary> Tile types that are containers of Width and Height different from Chests or Dressers. </summary>
    // TODO: add support for net serializaton (and proper unloading but this needs to be in tML directly) 
    public static bool[] CustomContainer { get; } = TileID.Sets.Factory.CreateNamedSet(nameof(CustomContainer)).Description("Tile types that are containers of Width and Height different from Chests or Dressers.").RegisterBoolSet();

    /// <summary> Tile types with custom <see cref="TreePaintingSettings"/> </summary>
    public static TreePaintingSettings[] PaintingSettings { get; } = TileID.Sets.Factory.CreateNamedSet(nameof(PaintingSettings)).Description("Tile types with custom TreePaintingSettings").RegisterCustomSet<TreePaintingSettings>(defaultState: null);

    /// <summary> Tile types that allow liquids to flow through them. You cannot place liquids on them directly. Only works for <see cref="Main.tileSolid"/> tiles.</summary>
    public static bool[] AllowLiquids { get; } = TileID.Sets.Factory.CreateNamedSet(nameof(AllowLiquids)).Description("Tile types that allow liquids to flow through them. You cannot place liquids on them directly. Only works for Main.tileSolid tiles.").RegisterBoolSet();

    // TODO: this needs a rework
    public static int[] RandomStyles { get; } = TileID.Sets.Factory.CreateIntSet(defaultState: 1);

    /// <summary>
    /// Maps tile types to the item ID they yield when mined by a drill/excavator.
    /// -1 (default) means the tile is not drillable.
    /// Vanilla entries are registered inline below; modded ore tiles register themselves in their <c>SetStaticDefaults</c>.
    /// </summary>
    public static int[] DrillItemDrop { get; } = TileID.Sets.Factory.CreateNamedSet(nameof(DrillItemDrop)).Description("Maps tile types to the item they yield when drilled. -1 = not drillable.").RegisterIntSet(defaultState: -1,
        // Terrain
        TileID.Stone,        ItemID.StoneBlock,
        // Tier 1 ores
        TileID.Copper,       ItemID.CopperOre,
        TileID.Tin,          ItemID.TinOre,
        TileID.Iron,         ItemID.IronOre,
        TileID.Lead,         ItemID.LeadOre,
        TileID.Silver,       ItemID.SilverOre,
        TileID.Tungsten,     ItemID.TungstenOre,
        TileID.Gold,         ItemID.GoldOre,
        TileID.Platinum,     ItemID.PlatinumOre,
        // Evil ores
        TileID.Demonite,     ItemID.DemoniteOre,
        TileID.Crimtane,     ItemID.CrimtaneOre,
        // Special
        TileID.Meteorite,    ItemID.Meteorite,
        TileID.Hellstone,    ItemID.Hellstone,
        // Hardmode ores
        TileID.Cobalt,       ItemID.CobaltOre,
        TileID.Palladium,    ItemID.PalladiumOre,
        TileID.Mythril,      ItemID.MythrilOre,
        TileID.Orichalcum,   ItemID.OrichalcumOre,
        TileID.Adamantite,   ItemID.AdamantiteOre,
        TileID.Titanium,     ItemID.TitaniumOre,
        // Post-HM
        TileID.Chlorophyte,  ItemID.ChlorophyteOre,
        TileID.LunarOre,     ItemID.LunarOre
    );
}
