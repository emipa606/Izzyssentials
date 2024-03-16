using System.Collections.Generic;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace Izzyssentials;

public class Building_HRC_Switch : Building
{
    private CompFlickable flickableComp; //A13

    private ColorInt LedState;

    private CompSensor sensorComp;

    private bool switchOnOld = true;

    public override bool TransmitsPowerNow => flickableComp.SwitchIsOn; //A13

    //return this.switchOn;
    public override Color DrawColor => def.MadeFromStuff ? base.DrawColor : DrawColorTwo;

    public override Color DrawColorTwo => Util.IntToColour(LedState);


    public override void SpawnSetup(Map map, bool respawningAfterLoad)
    {
        base.SpawnSetup(map, respawningAfterLoad);
        flickableComp = GetComp<CompFlickable>(); //A13
        sensorComp = GetComp<CompSensor>();
        LedState = ListOColours.Blackout;
    }


    public override void ExposeData()
    {
        base.ExposeData();
        if (Scribe.mode != LoadSaveMode.PostLoadInit)
        {
            return;
        }

        if (flickableComp == null)
        {
            flickableComp = GetComp<CompFlickable>();
        }

        switchOnOld = !flickableComp.SwitchIsOn;
        UpdatePowerGrid();
    }

    //moved to Comp
    public override IEnumerable<Gizmo> GetGizmos()
    {
        foreach (var g in base.GetGizmos())
        {
            if (typeof(Command_Toggle) != g.GetType())
            {
                yield return g;
            }
            else
            {
                if (!sensorComp.auto)
                {
                    yield return g;
                }
            }
        }
    }


    public override string GetInspectString() // fuck these comments to hell and back.
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append(base.GetInspectString());
        stringBuilder.AppendLine();
        stringBuilder.Append("PowerSwitch_Power".Translate() + ": ");
        stringBuilder.Append(flickableComp.SwitchIsOn ? "On".Translate() : "Off".Translate());

        return stringBuilder.ToString();
    }

    protected override void ReceiveCompSignal(string signal) //A13
    {
        if (signal is "FlickedOff" or "FlickedOn")
        {
            UpdatePowerGrid();
        }
    }

    private void UpdatePowerGrid()
    {
        if (flickableComp.SwitchIsOn == switchOnOld)
        {
            return;
        }

        if (Spawned)
        {
            Map.powerNetManager.Notfiy_TransmitterTransmitsPowerNowChanged(PowerComp);
        }

        switchOnOld = flickableComp.SwitchIsOn;
    }

    public override void DrawExtraSelectionOverlays()
    {
        if (sensorComp == null)
        {
            return;
        }

        if (sensorComp.auto)
        {
            GenDraw.DrawRadiusRing(Position, sensorComp.setRadius);
        }
    }

    protected override void DrawAt(Vector3 drawLoc, bool flip = false)
    {
        base.DrawAt(drawLoc, flip);
        if (flickableComp.SwitchIsOn)
        {
            if (PowerComp.PowerNet.CurrentEnergyGainRate() == 0)
            {
                if (LedState != ListOColours.Red)
                {
                    UpdateTex(ListOColours.Red);
                }
            }
            else
            {
                if (LedState != ListOColours.Green)
                {
                    UpdateTex(ListOColours.Green);
                }
            }
        }
        else
        {
            if (LedState != ListOColours.Blackout)
            {
                UpdateTex(ListOColours.Blackout);
            }
        }
    }

    private void UpdateTex(ColorInt col)
    {
        LedState = col;
        Notify_ColorChanged();
        Util.updateMap(Position, Map);
    }
}