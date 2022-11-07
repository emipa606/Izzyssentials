using System.Collections.Generic;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace Izzyssentials;

public class CompGlowerColour : ThingComp //17/07/16-R
{
    public int ActiveColour;

    public CompGlower thisGlower;


    private CompPowerTrader GetParent_CompPowerTrader => parent.TryGetComp<CompPowerTrader>();

    private CompGlower GetParent_CompGlower => parent.TryGetComp<CompGlower>();


    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Values.Look(ref ActiveColour, "ActiveColour");
    }


    public override void PostSpawnSetup(bool respawningAfterLoad)
    {
        base.PostSpawnSetup(respawningAfterLoad);
        ChangeColour();
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

        if (!DefDatabase<ResearchProjectDef>.GetNamed("ColoredLights").IsFinished)
        {
            yield break;
        }

        yield return Btn_Previous();
        yield return Btn_Reset();
        yield return Btn_Next();
    }

    public override string CompInspectStringExtra()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"Current colour is : {ListOColours.colourList[ActiveColour].colourName}");
        return stringBuilder.ToString();
    }

    private void CycleColours_RetValue(int I_typeOfAction)
    {
        ActiveColour = GetColourIndex(I_typeOfAction);
    }

    private int GetColourIndex(int dirCounter)
    {
        var tempIndex = ActiveColour + dirCounter;
        if (dirCounter == 0 || tempIndex == ListOColours.colourList.Count)
        {
            return 0;
        }

        if (tempIndex == -1)
        {
            return ListOColours.colourList.Count - 1;
        }

        return tempIndex;
    }


    private void Actionstart(int I_typeOfAction)
    {
        CycleColours_RetValue(I_typeOfAction);
        ChangeColour();
    }

    private Command_Action Btn_Previous()
    {
        var command_Action = new Command_Action
        {
            defaultLabel = "Previous colour", defaultDesc = "Change light colour"
        };
        var path = $"UI/Commands/Lights/PT_left_{GetColourIndex(-1)}";
        command_Action.icon = ContentFinder<Texture2D>.Get(path);
        command_Action.action = delegate { Actionstart(-1); };
        command_Action.activateSound = SoundDef.Named("Click");
        return command_Action;
    }

    private Command_Action Btn_Reset()
    {
        var command_Action = new Command_Action
        {
            defaultLabel = "Reset",
            defaultDesc = "Reset light colour",
            icon = ContentFinder<Texture2D>.Get("UI/Commands/Lights/C_Reset"),
            action = delegate { Actionstart(0); },
            activateSound = SoundDef.Named("Click")
        };
        return command_Action;
    }

    private Command_Action Btn_Next()
    {
        var command_Action = new Command_Action { defaultLabel = "Next Colour", defaultDesc = "Change light colour" };
        var path = $"UI/Commands/Lights/PT_right_{GetColourIndex(1)}";
        command_Action.icon = ContentFinder<Texture2D>.Get(path);
        command_Action.action = delegate { Actionstart(1); };
        command_Action.activateSound = SoundDef.Named("Click");
        return command_Action;
    }

    private void ChangeColour()
    {
        Util.DestroyNCreateGlower(parent, ListOColours.colourList[ActiveColour].ColourValue, 14f,
            parent.Map);
    }
}