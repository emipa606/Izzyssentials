using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace Izzyssentials;

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
        map.mapDrawer.MapMeshDirty(pos, MapMeshFlagDefOf.Things);
        map.mapDrawer.MapMeshDirty(pos, MapMeshFlagDefOf.GroundGlow);
        map.glowGrid.DirtyCache(pos);
    }

    //provides a new Compglower
    private static CompGlower newCompGlower(ThingWithComps parent, ColorInt glowColour, float glowRadius)
    {
        var Comp_NewGlower = new CompGlower { parent = parent };

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

        var list = parent.GetCompsIzzy();

        foreach (var comp in list)
        {
            if (typeof(CompGlower) == comp.GetType())
            {
                oldGlower = (CompGlower)comp;
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

        var newGlower = newCompGlower(parent, glowColour, glowRadius);
        list.Add(newGlower);

        //replaced with an extention to thingWithComps... doesn't feel safe but hey
        parent.SetCompsIzzy(list);

        newGlower.UpdateLit(map);
        updateMap(parent.Position, map);
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
}