using Verse;

namespace Izzyssentials;

public class PlaceWorker_CeilingLamp : PlaceWorker
{
    public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot,
        Map map, Thing thingToIgnore = null, Thing thing = null)
    {
        if (!map.roofGrid.Roofed(loc))
        {
            return "Must be placed under a ceiling.";
        }

        return true;
    }
}