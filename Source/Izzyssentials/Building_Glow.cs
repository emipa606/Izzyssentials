using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace Izzyssentials;

public class Building_Glow : Building //17/07/16-R
{
    private ColorInt Blackout;
    private List<ThingComp> comps = [];

    private CompGlowerColour glowerColour;
    private ColorInt LedState;
    private CompPowerTrader powerTrader;

    private bool HasRoof
    {
        get
        {
            var num = 0;
            var num2 = 0;
            foreach (var current in this.OccupiedRect())
            {
                num++;
                if (Map.roofGrid.Roofed(current))
                {
                    num2++;
                }
            }

            return (num - num2) / (float)num < 1;
        }
    }


    public override Color DrawColor => def.MadeFromStuff ? base.DrawColor : DrawColorTwo;

    //Changes the colour of the current led
    public override Color DrawColorTwo => Util.IntToColour(LedState);


    public override void SpawnSetup(Map map, bool respawningAfterLoad)
    {
        base.SpawnSetup(map, respawningAfterLoad);

        powerTrader = GetComp<CompPowerTrader>();
        glowerColour = GetComp<CompGlowerColour>();

        Blackout = new ColorInt(100, 100, 100, 255);
        LedState = Blackout;
    }

    protected override void DrawAt(Vector3 drawLoc, bool flip = false)
    {
        base.DrawAt(drawLoc, flip);
        //changing the actual colour
        if (powerTrader.PowerOn)
        {
            if (LedState != activeColour())
            {
                UpdateTex(activeColour());
            }
        }
        else
        {
            if (LedState != Blackout)
            {
                UpdateTex(Blackout);
            }
        }
    }

    public override void Tick()
    {
        base.Tick();
        if (def.defName != "HRC_CeilingLamp")
        {
            return;
        }

        if (!HasRoof)
        {
            Destroy();
        }
    }

    //just to ease my eyes from the otherwise long string of text
    private ColorInt activeColour()
    {
        return ListOColours.colourList[glowerColour.ActiveColour].ColourValue;
    }

    private void UpdateTex(ColorInt col)
    {
        LedState = col;
        Notify_ColorChanged();
        Util.updateMap(Position, Map);
    }
}