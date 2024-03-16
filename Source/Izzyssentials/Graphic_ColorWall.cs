using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace Izzyssentials;

public class Graphic_ColorWall : Graphic //17/07/16-R
{
    private List<StuffAppearanceDef> stuffAppearanceDefs;

    //Graphic_collection
    private Graphic[] subGraphics;


    //Graphic_Appearances
    public override Material MatSingle => subGraphics[0].MatSingle;

    //Graphic_Appearances merged Graphic_collection
    public override void Init(GraphicRequest req)
    {
        if (req.path.NullOrEmpty())
        {
            return;
        }

        if (req.shader == null)
        {
            return;
        }

        path = req.path;
        color = req.color;
        colorTwo = req.colorTwo; //Multi
        drawSize = req.drawSize;

        var list = ContentFinder<Texture2D>.GetAllInFolder(req.path).ToList();

        if (list.NullOrEmpty()) // Major graphic issues, had to
        {
            Log.Error($"Collection cannot init: No textures found at path {req.path}");
            subGraphics =
            [
                BaseContent.BadGraphic
            ];
            return;
        }

        //gets every texture file into subGraphics
        subGraphics = new Graphic[list.Count / 2];
        var k = 0;
        foreach (var texture2D in list)
        {
            var texture2DName = $"{req.path}/{texture2D.name}";
            if (texture2DName.EndsWith("m"))
            {
                continue;
            }

            subGraphics[k] =
                GraphicDatabase.Get<Graphic_Single>(texture2DName, req.shader, drawSize, color,
                    colorTwo); // this.shader
            k++;
        }

        //Copies every graphic in subgraphics into an array
        var array = new Graphic[subGraphics.Length];
        for (var i = 0; i < subGraphics.Length; i++)
        {
            array[i] = subGraphics[i];
        }

        //resets subgraphics and makes it the lenght of stuffappearances
        var allStuffAppearanceDefs = DefDatabase<StuffAppearanceDef>.AllDefsListForReading;
        subGraphics = new Graphic[allStuffAppearanceDefs.Count];
        stuffAppearanceDefs = [];

        //Creates enu and gets all the values of StuffAppearance Smooth,Planks,Bricks
        var counter = 0;
        foreach (var stuffAppearance in allStuffAppearanceDefs)
        {
            var graphic = BaseContent.BadGraphic;

            foreach (var graphic2 in array)
            {
                var array2 = graphic2?.MatSingle?.name?.Split('_');
                var a = array2?[array2.Length - 1];
                if (a == stuffAppearance?.ToString())
                {
                    graphic = graphic2;
                    break;
                }

                if (graphic == null && a == StuffAppearanceDefOf.Smooth.ToString())
                {
                    graphic = graphic2;
                }
            }

            //eventually picks wich type (Smooth,Planks,Bricks) needs to be picked.
            subGraphics[counter] = graphic;
            stuffAppearanceDefs.Add(stuffAppearance);
            counter++;
        }
    }

    //Graphic_Appearances
    public override Graphic GetColoredVersion(Shader newShader, Color newColor, Color newColorTwo)
    {
        return GraphicDatabase.Get<Graphic_ColorWall>(path, newShader, drawSize, newColor, newColorTwo);
    }

    //Graphic_Appearances
    public override Material MatSingleFor(Thing thing)
    {
        var stuffAppearance = StuffAppearanceDefOf.Smooth;
        if (thing?.Stuff != null)
        {
            stuffAppearance = thing.Stuff.stuffProps.appearance;
        }

        var graphic = subGraphics[stuffAppearanceDefs.IndexOf(stuffAppearance)];
        //graphic.ma
        return graphic.MatSingleFor(thing);
    }

    //Graphic_Appearances
    public override void DrawWorker(Vector3 loc, Rot4 rot, ThingDef thingDef, Thing thing, float extraRotation)
    {
        var stuffAppearance = StuffAppearanceDefOf.Smooth;
        if (thing?.Stuff != null)
        {
            stuffAppearance = thing.Stuff.stuffProps.appearance;
        }

        var graphic = subGraphics[stuffAppearanceDefs.IndexOf(stuffAppearance)];
        graphic.DrawWorker(loc, rot, thingDef, thing, extraRotation);
    }

    //Graphic_Appearances
    public override string ToString()
    {
        return $"Appearance(path={path}, color={color}, colorTwo{colorTwo}";
    }
}