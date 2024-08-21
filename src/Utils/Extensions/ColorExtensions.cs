using UnityEngine;

namespace GoblinShared
{
    public static partial class Extensions
    {
        public static Color SetRed(this Color c, float red) => new Color(red, c.g, c.b, c.a);
        public static Color SetGreen(this Color c, float green) => new Color(c.r, green, c.b, c.a);
        public static Color SetBlue(this Color c, float blue) => new Color(c.r, c.g, blue, c.a);
        public static Color SetAlpha(this Color c, float alpha) => new Color(c.r, c.g, c.b, alpha);
    }
}
