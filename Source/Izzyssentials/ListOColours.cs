using System.Collections.Generic;
using Verse;

namespace Izzyssentials;

public static class ListOColours //17/07/16-R
{
    public static readonly List<Colour> colourList =
    [
        new Colour("white", new ColorInt(217, 217, 217, 0)),

        new Colour("red", new ColorInt(255, 0, 0, 0)),
        new Colour("vermilion", new ColorInt(255, 63, 0, 0)),
        new Colour("orange", new ColorInt(255, 127, 0, 0)),
        new Colour("amber", new ColorInt(255, 191, 0, 0)),

        new Colour("yellow", new ColorInt(255, 255, 0, 0)),
        new Colour("lime", new ColorInt(191, 255, 0, 0)),
        new Colour("chartreuse", new ColorInt(127, 255, 0, 0)),
        new Colour("harlequin", new ColorInt(63, 255, 0, 0)),

        new Colour("green", new ColorInt(0, 255, 0, 0)),
        new Colour("malachite", new ColorInt(0, 255, 63, 0)),
        new Colour("spring", new ColorInt(0, 255, 127, 0)),
        new Colour("turquoise", new ColorInt(0, 255, 191, 0)),

        new Colour("cyan", new ColorInt(0, 255, 255, 0)),
        new Colour("cerulean", new ColorInt(0, 191, 255, 0)),
        new Colour("azure", new ColorInt(0, 127, 255, 0)),
        new Colour("sapphire", new ColorInt(0, 63, 255, 0)),

        new Colour("blue", new ColorInt(0, 0, 255, 0)),
        new Colour("indigo", new ColorInt(63, 0, 255, 0)),
        new Colour("violet", new ColorInt(127, 0, 255, 0)),
        new Colour("purple", new ColorInt(191, 0, 255, 0)),

        new Colour("magenta", new ColorInt(255, 0, 255, 0)),
        new Colour("fushia", new ColorInt(255, 0, 191, 0)),
        new Colour("rose", new ColorInt(255, 0, 127, 0)),
        new Colour("crimson", new ColorInt(255, 0, 63, 0))
    ];

    public static ColorInt Red = colourList[1].ColourValue;

    public static ColorInt Green = colourList[9].ColourValue;

    public static ColorInt Blue = colourList[17].ColourValue;

    public static ColorInt Blackout = new ColorInt(100, 100, 100, 255);
}