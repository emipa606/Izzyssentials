using System.Collections.Generic;
using RimWorld;
using Verse;

namespace Izzyssentials;

//works... stop touching it! 
public class Building_PlantBetterGrower : Building_PlantGrower, IPlantToGrowSettable //  17/07/16-R
{
    public new ThingDef GetPlantDefToGrow()
    {
        return base.GetPlantDefToGrow();
    }

    public new void SetPlantDefToGrow(ThingDef plantDef)
    {
        base.SetPlantDefToGrow(plantDef);
        PlantDefChanger("spread");
    }

    public override void SpawnSetup(Map map, bool respawningAfterLoad)
    {
        base.SpawnSetup(map, respawningAfterLoad);
        PlantDefChanger("inherit");
    }

    // merged spread and inherit
    private void PlantDefChanger(string Command) // inherit or spread
    {
        var q = new IntVec3(Position.x + 1, Position.y, Position.z + 0);
        var r = new IntVec3(Position.x + 0, Position.y, Position.z + 1);
        var s = new IntVec3(Position.x - 1, Position.y, Position.z + 0);
        var t = new IntVec3(Position.x + 0, Position.y, Position.z - 1);
        IList<IntVec3> dirList = new List<IntVec3> { q, r, s, t };

        foreach (var var in dirList)
        {
            var list = var.GetThingList(Map);
            if (list == null)
            {
                continue;
            }

            foreach (var curThing in list)
            {
                if (curThing is not Building_PlantBetterGrower grower)
                {
                    continue;
                }

                switch (Command)
                {
                    case "inherit" when grower.GetPlantDefToGrow() !=
                                        base.GetPlantDefToGrow():
                        SetPlantDefToGrow(grower.GetPlantDefToGrow());
                        break;
                    case "spread" when grower.GetPlantDefToGrow() !=
                                       base.GetPlantDefToGrow():
                        grower.SetPlantDefToGrow(base.GetPlantDefToGrow());
                        break;
                }
            }
        }
    }
}