using UnityEngine;

namespace GoblinShared
{
	public static partial class Extensions
	{
		public static Color SetRed(this Color c, float red) => new Color(red, c.g, c.b, c.a);
		public static Color SetGreen(this Color c, float green) => new Color(c.r, green, c.b, c.a);
		public static Color SetBlue(this Color c, float blue) => new Color(c.r, c.g, blue, c.a);
		public static Color SetAlpha(this Color c, float alpha) => new Color(c.r, c.g, c.b, alpha);

		public static Color Saturate(this Color c, float saturate)
		{
			Color.RGBToHSV(c, out var h, out var s, out var v);
			s *= saturate;
			return Color.HSVToRGB(h, s, v);
		}
		public static Color HueShift(this Color c, float hueShift)
		{
			Color.RGBToHSV(c, out var h, out var s, out var v);
			h += hueShift;
			return Color.HSVToRGB(h, s, v);
		}
		public static Color ValueMult(this Color c, float valueMult)
		{
			Color.RGBToHSV(c, out var h, out var s, out var v);
			v *= valueMult;
			return Color.HSVToRGB(h, s, v);
		}
		public static Color ValueClamp(this Color c, float minValue, float maxValue)
		{
			Color.RGBToHSV(c, out var h, out var s, out var v);
			v = Mathf.Clamp(v, minValue, maxValue);
			return Color.HSVToRGB(h, s, v);
		}
		public static Color SetR(this Color c, float r) => new Color(r, c.g, c.b, c.a);
		public static Color SetG(this Color c, float g) => new Color(c.r, g, c.b, c.a);
		public static Color SetB(this Color c, float b) => new Color(c.r, c.g, b, c.a);
		public static Color SetA(this Color c, float a) => new Color(c.r, c.g, c.b, a);
	}
}
