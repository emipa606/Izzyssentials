using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace Izzyssentials
{
    public class Util //17/07/16-R
    {
        public static Color IntToColour(ColorInt col)
        {
            var r = col.r / 255f;
            var g = col.g / 255f;
            var b = col.b / 255f;
            var a = 1f;

            return new Color(r, g, b, a);
        }

        public static void updateMap(IntVec3 pos, Map map)
        {
            map.mapDrawer.MapMeshDirty(pos, MapMeshFlag.Things);
            map.mapDrawer.MapMeshDirty(pos, MapMeshFlag.GroundGlow);
            map.glowGrid.MarkGlowGridDirty(pos);
        }

        //provides a new Compglower
        private static CompGlower newCompGlower(ThingWithComps parent, ColorInt glowColour, float glowRadius)
        {
            var Comp_NewGlower = new CompGlower {parent = parent};


            //CompProperties CompProp_New = new CompProperties();


            var CompProp_New = new CompProperties_Glower
            {
                compClass = typeof(CompGlower), glowColor = glowColour, glowRadius = glowRadius
            };

            Comp_NewGlower.Initialize(CompProp_New);

            return Comp_NewGlower;
        }


        //moved it to util for easier implementation with MAD
        public static void DestroyNCreateGlower(ThingWithComps parent, ColorInt glowColour, float glowRadius, Map map)
        {
            CompGlower oldGlower = null;
            CompGlower CCL = null;
            //bool CCL = false;

            var list = parent.GetCompsIzzy();

            foreach (var comp in list)
            {
                if (typeof(CompGlower) == comp.GetType())
                {
                    oldGlower = (CompGlower) comp;
                }

                //CCL catch
                if (comp is CompGlower glower && oldGlower == null)
                {
                    CCL = glower;
                }

                if (typeof(CompPowerTrader) == comp.GetType())
                {
                }
            }

            if (CCL != null)
            {
                map.glowGrid.DeRegisterGlower(CCL);
                list.Remove(CCL);
            }
            else
            {
                map.glowGrid.DeRegisterGlower(oldGlower);
                list.Remove(oldGlower);
            }


            //    if (oldGlower != null )
            //{

            //Boolean isLit = oldGlower.;
            //oldGlower.Lit = false;
            //parent.BroadcastCompSignal("FlickedOff");

            //oldGlower.PostDeSpawn();

            var newGlower = newCompGlower(parent, glowColour, glowRadius);
            list.Add(newGlower);
            //Find.GlowGrid.RegisterGlower(newGlower);

            //(CompGlower)oldGlower.


            //replaced with an extention to thingWithComps... doesnt feel safe but hey
            //typeof(BuildingGlowTnC).GetField("comps", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(parent, List_AllThingComps);
            parent.SetCompsIzzy(list);
            //parent.BroadcastCompSignal("FlickedOn");

            newGlower.UpdateLit(map);
            //newGlower.Lit = false;
            updateMap(parent.Position, map);


            //if (pwrTrader != null)
            //{
            //    if (isLit && pwrTrader.PowerOn)
            //    {
            //        newGlower.Lit = true;
            //    }
            //}
            //}
        }

        private static bool CommandAction(string command, Pawn potentialpawn)
        {
            switch (command)
            {
                case "friend":
                    return potentialpawn.IsColonist;
                case "enemy":
                    return potentialpawn.HostileTo(Faction.OfPlayer);
            }

            return false;
        }

        //public static bool CheckPawnInRad(string command,IntVec3 center, float maxDistance = 99999)
        //{
        //    IEnumerable<IntVec3> t = GenRadial.RadialCellsAround(center, maxDistance, true);

        //    List<Pawn> PinRadius = new List<Pawn>();

        //    foreach (IntVec3 var in t)
        //    {

        //        List<Thing> list = var.GetThingList();
        //        if (list != null)
        //        {
        //            foreach (Pawn curThing in list)
        //            {
        //                if (CommandAction(command, curThing))
        //                    return true;
        //            }
        //        }
        //    }
        //    return false;
        //    //foreach (Pawn potentialpawn in Find.MapPawns.AllPawns)
        //    //{
        //    //    if (CommandAction(command,potentialpawn))
        //    //    {
        //    //        float lengthHorizontalSquared = (center - potentialpawn.Position).LengthHorizontalSquared;
        //    //        if (lengthHorizontalSquared < num && lengthHorizontalSquared <= num2)
        //    //        {
        //    //            PinRadius.Add(potentialpawn);
        //    //        }
        //    //    }

        //    //}


        //}

        //taken from how pawns find a target within a radius
        public static List<Pawn> RadiusPawns(string command, IntVec3 center, Map map, float maxDistance = 99999)
        {
            var num = 2.14748365E+09f;
            var num2 = maxDistance * maxDistance;
            var PinRadius = new List<Pawn>();

            foreach (var potentialpawn in map.mapPawns.AllPawns)
            {
                if (!CommandAction(command, potentialpawn))
                {
                    continue;
                }

                float lengthHorizontalSquared = (center - potentialpawn.Position).LengthHorizontalSquared;
                if (lengthHorizontalSquared < num && lengthHorizontalSquared <= num2)
                {
                    PinRadius.Add(potentialpawn);
                }
            }

            return PinRadius;
        }

        //public static void AddFlik(ThingWithComps parent)
        //{
        //    CompFlickable flik = new CompFlickable();

        //    List<ThingComp> list = parent.GetCompsIzzy();

        //    list.Add(flik);
        //    parent.SetCompsIzzy(list);
        //    updateMap(parent.Position);
        //}
        //public static void RemoveFlik(ThingWithComps parent)
        //{
        //    CompFlickable flik = null;

        //    List<ThingComp> list = parent.GetCompsIzzy();

        //    foreach (ThingComp comp in list)
        //    {
        //        if (typeof(CompFlickable) == comp.GetType())
        //        {
        //            flik = (CompFlickable)comp;
        //        }
        //    }

        //    list.Remove(flik);
        //    parent.SetCompsIzzy(list);
        //    updateMap(parent.Position);
        //}
    }
}


////Boolean isLit = oldGlower.Lit;
//              //oldGlower.Lit = false;
//              parent.BroadcastCompSignal("FlickedOff");
//              CompGlower newGlower = Util.newCompGlower(parent, glowColour, glowRadius);
//              list.Remove(oldGlower);
//              list.Add(newGlower);

//              //replaced with an extention to thingWithComps... doesnt feel safe but hey
//              //typeof(BuildingGlowTnC).GetField("comps", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(parent, List_AllThingComps);
//              parent.SetComps(list);
//              parent.BroadcastCompSignal("FlickedOn");
//              //newGlower.Lit = false;
//              updateMap(parent.Position);


//              //if (pwrTrader != null)
//              //{
//              //    if (isLit && pwrTrader.PowerOn)
//              //    {
//              //        newGlower.Lit = true;
//              //    }
//              //}