using Verse;

namespace Izzyssentials;

public class PlaceWorker_CeilingLamp : PlaceWorker
{
    public override AcceptanceReport AllowsPlacing(BuildableDef def, IntVec3 center, Rot4 rot,
        Map map, Thing thingToIgnore = null, Thing thing = null)
    {
        if (!map.roofGrid.Roofed(center))
        {
            return "Must be placed under a ceiling.";
        }

        return true;
    }
}