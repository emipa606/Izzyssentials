using RimWorld;
using UnityEngine;
using Verse;

namespace Izzyssentials;

public class CompPowerPlantSolarCon : CompPowerPlant //17/07/16-R
{
    private float PowerOutputD => Mathf.Lerp(0f, 100f, parent.Map.skyManager.CurSkyGlow);

    protected override float DesiredPowerOutput => PowerOutputD * RoofedPowerOutputFactor;

    private float RoofedPowerOutputFactor
    {
        get
        {
            var num = 0;
            var num2 = 0;
            foreach (var current in parent.OccupiedRect())
            {
                num++;
                if (parent.Map.roofGrid.Roofed(current))
                {
                    num2++;
                }
            }

            return (num - num2) / (float)num;
        }
    }

    public override void PostDraw()
    {
        var f1 = PowerOutputD;

        base.PostDraw();
        var r = default(DrawHelper.RectangleRequest);
        r.center = parent.DrawPos;
        r.size = new Vector2(0.16f, 0.16f);
        r.mat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(
            (115f - (f1 * 58f / 110f)) / 255f,
            (130f - (f1 * 41f / 110f)) / 255f,
            (140f - (f1 * 28f / 110f)) / 255f
        ));
        r.rotation = parent.Rotation;
        DrawHelper.DrawRectangle(r);
    }
}