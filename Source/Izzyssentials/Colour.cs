using Verse;

namespace Izzyssentials;

public struct Colour(string a, ColorInt b)
{
    public readonly string colourName = a;
    public ColorInt ColourValue = b;
}