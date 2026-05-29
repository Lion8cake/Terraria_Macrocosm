using Terraria;
using Terraria.Localization;

namespace Macrocosm.Common.DataStructures;

/// <summary>
/// Per-tile drill metadata: the item dropped when mined and an optional world-state
/// <see cref="Condition"/> that must be satisfied for the tile to be drillable at all.
/// </summary>
public struct DrillDropData
{
    /// <summary> Item ID dropped when this tile is drilled. -1 = not drillable. </summary>
    public int ItemDrop;

    /// <summary>
    /// Optional world-state condition that must be met before this tile can be drilled.
    /// <see langword="null"/> means always drillable (no restriction).
    /// </summary>
    public Condition Condition;

    public DrillDropData(int itemDrop, Condition condition = null)
    {
        ItemDrop  = itemDrop;
        Condition = condition;
    }

    /// <summary> Sentinel default for unregistered tiles: not drillable. </summary>
    public static readonly DrillDropData None = new() { ItemDrop = -1 };
}
