namespace GoblinShared
{
	public static partial class Extensions
	{
		public static string EscapeQuotes(this string s) => s.Replace("\"", "\\\"");
	}
}
