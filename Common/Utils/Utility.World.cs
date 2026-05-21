using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;

namespace Macrocosm.Common.Utils;

public static partial class Utility
{
    public static bool BossActive => Main.CurrentFrameFlags.AnyActiveBossNPC;

    public static bool InvastionActive => Main.invasionType > 0 || Main.snowMoon || Main.pumpkinMoon || DD2Event.Ongoing;

    public static bool BloodMoonActive => Main.bloodMoon;

    public static bool MoonLordIncoming => NPC.MoonLordCountdown > 0;

    public static bool PillarsActive => Main.npc.Any(npc => npc.type is NPCID.LunarTowerVortex or NPCID.LunarTowerStardust or NPCID.LunarTowerNebula or NPCID.LunarTowerSolar);

    public static bool MartianProbeActive => Main.npc.Any(npc => npc.type is NPCID.MartianProbe);

    public static bool InUnderworldHeight(Vector2 worldPosition) => InUnderworldHeight(worldPosition.ToTileCoordinates().Y);
    public static bool InUnderworldHeight(int tileY) => tileY > Main.UnderworldLayer;

    public static bool InRockLayerHeight(Vector2 worldPosition) => InRockLayerHeight(worldPosition.ToTileCoordinates().Y);
    public static bool InRockLayerHeight(int tileY) => tileY <= Main.UnderworldLayer && tileY > Main.rockLayer;

    public static bool InDirtLayerHeight(Vector2 worldPosition) => InDirtLayerHeight(worldPosition.ToTileCoordinates().Y);
    public static bool InDirtLayerHeight(int tileY) => tileY <= Main.rockLayer && tileY > Main.worldSurface;

    public static bool InOverworldHeight(Vector2 worldPosition) => InOverworldHeight(worldPosition.ToTileCoordinates().Y);
    public static bool InOverworldHeight(int tileY) => tileY <= Main.worldSurface && tileY > Main.worldSurface * 0.3499999940395355;

    public static bool InSkyHeight(Vector2 worldPosition) => InSkyHeight(worldPosition.ToTileCoordinates().Y);
    public static bool InSkyHeight(int tileY) => tileY <= Main.worldSurface * 0.3499999940395355;

    public static void WorldGen_ShakeTree(int i, int j) => typeof(WorldGen).InvokeMethod("ShakeTree", parameters: [i, j]);

    public static int WorldGen_numTreeShakes
    {
        get => typeof(WorldGen).GetFieldValue<int>("numTreeShakes");
        set => typeof(WorldGen).SetFieldValue("numTreeShakes", value);
    }

    public static int WorldGen_maxTreeShakes
    {
        get => typeof(WorldGen).GetFieldValue<int>("maxTreeShakes");
        set => typeof(WorldGen).SetFieldValue("maxTreeShakes", value);
    }

    public static (int[] treeShakeX, int[] treeShakeY) WorldGen_treeShakeXY
        => (typeof(WorldGen).GetFieldValue<int[]>("treeShakeX"), typeof(WorldGen).GetFieldValue<int[]>("treeShakeY"));
}
