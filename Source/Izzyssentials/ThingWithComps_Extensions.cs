using System.Collections.Generic;
using System.Reflection;
using Verse;

namespace RimWorld;

public static class ThingWithComps_Extensions //17/07/16-R
{
    public static void SetCompsIzzy(this ThingWithComps thingWithComps, List<ThingComp> comps)
    {
        // Hate this bit of code. thank fuck for extentions
        typeof(ThingWithComps).GetField("comps", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.SetValue(thingWithComps, comps);
    }


    public static List<ThingComp> GetCompsIzzy(this ThingWithComps thingWithComps)
    {
        // useless bit... but now I have a nice looking Get and Set ...
        return thingWithComps.AllComps;
    }
}