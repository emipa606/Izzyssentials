using System.Collections.Generic;
using Verse;

namespace Izzyssentials;

public static class ListOColours //17/07/16-R
{
    public static readonly List<Colour> colourList =
    [
        new("white", new ColorInt(217, 217, 217, 0)),

        new("red", new ColorInt(255, 0, 0, 0)),
        new("vermilion", new ColorInt(255, 63, 0, 0)),
        new("orange", new ColorInt(255, 127, 0, 0)),
        new("amber", new ColorInt(255, 191, 0, 0)),

        new("yellow", new ColorInt(255, 255, 0, 0)),
        new("lime", new ColorInt(191, 255, 0, 0)),
        new("chartreuse", new ColorInt(127, 255, 0, 0)),
        new("harlequin", new ColorInt(63, 255, 0, 0)),

        new("green", new ColorInt(0, 255, 0, 0)),
        new("malachite", new ColorInt(0, 255, 63, 0)),
        new("spring", new ColorInt(0, 255, 127, 0)),
        new("turquoise", new ColorInt(0, 255, 191, 0)),

        new("cyan", new ColorInt(0, 255, 255, 0)),
        new("cerulean", new ColorInt(0, 191, 255, 0)),
        new("azure", new ColorInt(0, 127, 255, 0)),
        new("sapphire", new ColorInt(0, 63, 255, 0)),

        new("blue", new ColorInt(0, 0, 255, 0)),
        new("indigo", new ColorInt(63, 0, 255, 0)),
        new("violet", new ColorInt(127, 0, 255, 0)),
        new("purple", new ColorInt(191, 0, 255, 0)),

        new("magenta", new ColorInt(255, 0, 255, 0)),
        new("fushia", new ColorInt(255, 0, 191, 0)),
        new("rose", new ColorInt(255, 0, 127, 0)),
        new("crimson", new ColorInt(255, 0, 63, 0))
    ];

    public static ColorInt Red = colourList[1].ColourValue;

    public static ColorInt Green = colourList[9].ColourValue;

    public static ColorInt Blue = colourList[17].ColourValue;

    public static ColorInt Blackout = new(100, 100, 100, 255);
}