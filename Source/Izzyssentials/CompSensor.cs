using System.Collections.Generic;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace Izzyssentials;

public class CompSensor : ThingComp //17/07/16-R
{
    private readonly float maxRadius = 30f;
    public bool auto;
    private string detectTarget = "friend";
    public float setRadius = 10f;


    private CompPowerTrader GetParent_CompPowerTrader => parent.TryGetComp<CompPowerTrader>();

    private CompFlickable GetParent_CompFlickable => parent.TryGetComp<CompFlickable>();

    private IntVec3 GetParent_Pos => parent.Position;


    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Values.Look(ref setRadius, "setRadius", 10f, true);
        Scribe_Values.Look(ref detectTarget, "detectTarget", "friend", true);
        Scribe_Values.Look(ref auto, "auto", false, true);
    }

    public override void CompTick()
    {
        if (Find.TickManager.TicksGame % 45 != 0)
        {
            return;
        }

        if (!auto || GetParent_CompFlickable == null)
        {
            return;
        }

        var PinRadius = Util.RadiusPawns(detectTarget, GetParent_Pos, parent.Map, setRadius);
        if (PinRadius.Count == 0)
        {
            if (GetParent_CompFlickable.SwitchIsOn)
            {
                GetParent_CompFlickable.DoFlick();
            }
        }
        else
        {
            if (!GetParent_CompFlickable.SwitchIsOn)
            {
                GetParent_CompFlickable.DoFlick();
            }
        }
    }


    public override IEnumerable<Gizmo> CompGetGizmosExtra()
    {
        using var enumerator = base.CompGetGizmosExtra().GetEnumerator();
        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;
            yield return current;
        }

        if (parent.Faction != Faction.OfPlayer)
        {
            yield break;
        }

        if (!DefDatabase<ResearchProjectDef>.GetNamed("Sensor").IsFinished)
        {
            yield break;
        }

        yield return Btn_Auto();
        if (!auto)
        {
            yield break;
        }

        yield return Btn_Previous();
        yield return Btn_Reset();
        yield return Btn_Next();
    }

    public override string CompInspectStringExtra()
    {
        if (!auto)
        {
            return "";
        }

        var stringBuilder = new StringBuilder();
        stringBuilder.Append(detectTarget == "friend"
            ? "Currently targets colonists"
            : "Currently targets enemies");

        stringBuilder.AppendLine();
        stringBuilder.Append($"Current radius: {setRadius}");
        return stringBuilder.ToString();
    }

    private void Actionstart(int I_typeOfAction)
    {
        switch (I_typeOfAction)
        {
            case -1:

                detectTarget = detectTarget == "friend" ? "enemy" : "friend";

                break;
            case 0:
                if (setRadius != maxRadius)
                {
                    setRadius += 1f;
                }

                break;
            case 1:
                if (setRadius != 1)
                {
                    setRadius -= 1f;
                }

                break;
        }
    }

    private Command_Action Btn_Auto()
    {
        var command_Action = new Command_Action { defaultLabel = "Function" };
        if (auto)
        {
            command_Action.defaultDesc = "Change to manual";
            command_Action.icon = ContentFinder<Texture2D>.Get("UI/Commands/Switch/SensorRadar_A");
        }
        else
        {
            command_Action.defaultDesc = "Change to automatic";
            command_Action.icon = ContentFinder<Texture2D>.Get("UI/Commands/Switch/SensorRadar_M");
        }

        command_Action.action = delegate { auto = !auto; };
        command_Action.activateSound = SoundDef.Named("Click");
        return command_Action;
    }


    private Command_Action Btn_Previous()
    {
        var command_Action = new Command_Action
        {
            defaultLabel = "Target",
            defaultDesc = "Change target",
            icon = ContentFinder<Texture2D>.Get(detectTarget == "friend"
                ? "UI/Commands/Switch/SensorRadar_Friendly"
                : "UI/Commands/Switch/SensorRadar_Enemy"),
            action = delegate { Actionstart(-1); },
            activateSound = SoundDef.Named("Click")
        };

        return command_Action;
    }

    private Command_Action Btn_Reset()
    {
        var command_Action = new Command_Action
        {
            defaultLabel = "Radius+",
            defaultDesc = "Make the detection radius bigger",
            icon = ContentFinder<Texture2D>.Get("UI/Commands/Switch/SensorRadar_inc"),
            action = delegate { Actionstart(0); },
            activateSound = SoundDef.Named("Click")
        };
        return command_Action;
    }

    private Command_Action Btn_Next()
    {
        var command_Action = new Command_Action
        {
            defaultLabel = "Radius-",
            defaultDesc = "Make the detection radius smaller",
            icon = ContentFinder<Texture2D>.Get("UI/Commands/Switch/SensorRadar_dec"),
            action = delegate { Actionstart(1); },
            activateSound = SoundDef.Named("Click")
        };
        return command_Action;
    }
}