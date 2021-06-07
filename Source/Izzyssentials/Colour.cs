using Verse;

namespace Izzyssentials
{
    public struct Colour
    {
        public readonly string colourName;
        public ColorInt ColourValue;

        public Colour(string a, ColorInt b)
        {
            colourName = a;
            ColourValue = b;
        }
    }
}