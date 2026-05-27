using Macrocosm.Common.Subworlds;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace Macrocosm.Content.Rockets;

public class RocketFuelLookup : ILoadable
{
    public enum RocketFuelRouteKind
    {
        Intraplanetary,
        Orbital,
        Interbody
    }

    public readonly record struct RocketFuelRoute(
        string Location,
        string Destination,
        string LocationParent,
        string DestinationParent,
        bool LocationIsOrbit,
        bool DestinationIsOrbit
    )
    {
        public RocketFuelRouteKind Kind
        {
            get
            {
                if (LocationParent != DestinationParent)
                    return RocketFuelRouteKind.Interbody;

                if (LocationIsOrbit || DestinationIsOrbit)
                    return RocketFuelRouteKind.Orbital;

                return RocketFuelRouteKind.Intraplanetary;
            }
        }
    }

    private static Dictionary<(string Location, string Destination), float> interbodyFuelLookup;
    public const float IntraplanetaryLaunchCost = 100f;
    public const float OrbitalLaunchCost = 125f;

    public void Load(Mod mod)
    {
        interbodyFuelLookup = new();
        PopulateTable();
    }

    public void Unload()
    {
        interbodyFuelLookup = null;
    }

    public static RocketFuelRoute ResolveRoute(string location, string destination)
    {
        bool locationIsOrbit = OrbitSubworld.IsOrbitSubworld(location);
        bool destinationIsOrbit = OrbitSubworld.IsOrbitSubworld(destination);

        string locationParent = MacrocosmSubworld.SanitizeID(OrbitSubworld.GetParentID(location), out _);
        string destinationParent = MacrocosmSubworld.SanitizeID(OrbitSubworld.GetParentID(destination), out _);

        return new(location, destination, locationParent, destinationParent, locationIsOrbit, destinationIsOrbit);
    }

    public static float GetFuelCost(string location, string destination)
    {
        RocketFuelRoute route = ResolveRoute(location, destination);

        if (route.Kind == RocketFuelRouteKind.Intraplanetary)
            return IntraplanetaryLaunchCost;

        if (route.Kind == RocketFuelRouteKind.Orbital)
            return OrbitalLaunchCost;

        if (interbodyFuelLookup.TryGetValue((route.LocationParent, route.DestinationParent), out float value))
            return value;

        return float.MaxValue;
    }

    private static void Add(string locationKey, string destinationKey, float value)
        => interbodyFuelLookup.Add((locationKey, destinationKey), value);

    private static void PopulateTable()
    {
        // Earth 
        Add("Earth", "Moon", 725f);
        Add("Earth", "Sun", 5900f);
        Add("Earth", "Mercury", 3200f);
        Add("Earth", "Venus", 1900f);
        Add("Earth", "Mars", 1300f);
        Add("Earth", "Phobos", 1250f);
        Add("Earth", "Deimos", 1250f);
        Add("Earth", "AsteroidBelt", 1800f);
        Add("Earth", "Ceres", 1900f);
        Add("Earth", "Jupiter", 2800f);
        Add("Earth", "Io", 2900f);
        Add("Earth", "Europa", 2800f);
        Add("Earth", "Saturn", 3700f);
        Add("Earth", "Titan", 3800f);
        Add("Earth", "Ouranos", 4700f);
        Add("Earth", "Miranda", 4800f);
        Add("Earth", "Neptune", 5700f);
        Add("Earth", "Triton", 5800f);
        Add("Earth", "Pluto", 6700f);
        Add("Earth", "Charon", 6800f);
        Add("Earth", "Eris", 7700f);

        // Moon 
        Add("Moon", "Earth", 225f);
        Add("Moon", "Sun", 5800f);
        Add("Moon", "Mercury", 3100f);
        Add("Moon", "Venus", 2000f);
        Add("Moon", "Mars", 1500f);
        Add("Moon", "Phobos", 1450f);
        Add("Moon", "Deimos", 1450f);
        Add("Moon", "AsteroidBelt", 1900f);
        Add("Moon", "Ceres", 2000f);
        Add("Moon", "Jupiter", 2800f);
        Add("Moon", "Io", 2900f);
        Add("Moon", "Europa", 2800f);
        Add("Moon", "Saturn", 3700f);
        Add("Moon", "Titan", 3800f);
        Add("Moon", "Ouranos", 4700f);
        Add("Moon", "Miranda", 4800f);
        Add("Moon", "Neptune", 5700f);
        Add("Moon", "Triton", 5800f);
        Add("Moon", "Pluto", 6700f);
        Add("Moon", "Charon", 6800f);
        Add("Moon", "Eris", 7700f);

    }
}
