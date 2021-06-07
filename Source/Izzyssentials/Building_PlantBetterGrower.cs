using System.Collections.Generic;
using RimWorld;
using Verse;

namespace Izzyssentials
{
    //works... stop touching it! 
    public class Building_PlantBetterGrower : Building_PlantGrower, IPlantToGrowSettable //  17/07/16-R
    {
        public new ThingDef GetPlantDefToGrow()
        {
            return base.GetPlantDefToGrow(); //this.plantDefToGrow
        }

        public new void SetPlantDefToGrow(ThingDef plantDef)
        {
            base.SetPlantDefToGrow(plantDef); //this.plantDefToGrow
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
            IList<IntVec3> dirList = new List<IntVec3> {q, r, s, t};

            foreach (var var in dirList)
            {
                var list = var.GetThingList(Map);
                if (list == null)
                {
                    continue;
                }

                foreach (var curThing in list)
                {
                    if (curThing is not Building_PlantBetterGrower)
                    {
                        continue;
                    }

                    if (Command == "inherit" && ((Building_PlantBetterGrower) curThing).GetPlantDefToGrow() !=
                        base.GetPlantDefToGrow())
                    {
                        SetPlantDefToGrow(((Building_PlantBetterGrower) curThing).GetPlantDefToGrow());
                    }

                    if (Command == "spread" && ((Building_PlantBetterGrower) curThing).GetPlantDefToGrow() !=
                        base.GetPlantDefToGrow())
                    {
                        ((Building_PlantBetterGrower) curThing).SetPlantDefToGrow(base.GetPlantDefToGrow());
                    }
                }
            }
        }
    }
}