namespace GoblinShared
{
	public static partial class AWSUtils
	{
		public const string name_table_goblins_userdata = "goblins-userdata";
		public const string arn_table_goblins_userdata = "arn:aws:dynamodb:us-east-1:200907845387:table/" + name_table_goblins_userdata;

		private const string str_service_dynamo = "dynamodb";
		private const string str_amztarget_PutItem = "DynamoDB_20120810.PutItem";
		private const string str_amztarget_GetItem = "DynamoDB_20120810.GetItem";
		private const string str_amztarget_Scan = "DynamoDB_20120810.Scan";

		public static string requestBody = "";
		public static string responseBody = "";

		const string s_hex_sym = "#";
		public static string AsHtmlColorString(this string hex) => hex.StartsWith(s_hex_sym) ? hex : (s_hex_sym + hex);
		public static bool TryParseHtmlColor(this string colorString, out UnityEngine.Color c) => UnityEngine.ColorUtility.TryParseHtmlString(colorString, out c);
		public static bool TryGetHexColor(this string hex, out UnityEngine.Color c)
		{
			c = UnityEngine.Color.gray;
			if (string.IsNullOrWhiteSpace(hex)) return false;
			return hex.AsHtmlColorString().TryParseHtmlColor(out c);
		}
	}
}
