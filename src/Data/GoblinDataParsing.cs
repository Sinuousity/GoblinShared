using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GoblinShared
{
	public static class GoblinDataParsing
	{
		public static readonly string rgx_userdata_itemlist = @"\,?\""Items\""\:\[(.+)\]\,?";
		public static readonly string rgx_userdata_items = @"\{(\"".+?\})\}(?=(?:\,\{\"")|$)";
		public static readonly string rgx_userdata_attribute_list = @"\""(.+?)\""\:\{\""(\w{1,5})\""\:\""(.+?)\""\}(?=$|(?:\,\""))";

		public static string TrimBrackets(string s) => TrimEnds(s, "{", "}");
		public static string TrimCommas(string s) => TrimEnds(s, ",", ",");
		public static string TrimEnds(string s, string trimStart, string trimEnd)
		{
			if (s.StartsWith(trimStart)) s = s.Substring(trimStart.Length, s.Length - trimStart.Length);
			if (s.EndsWith(trimEnd)) s = s.Substring(0, s.Length - trimEnd.Length);
			return s;
		}

		public static void Log(string s) => UnityEngine.Debug.Log(s);
		public static void LogError(string s) => UnityEngine.Debug.LogError(s);

		public static bool ParseOne(string itemString, out GoblinData data)
		{
			data = null;
			var itemAttributeListMatches = Regex.Matches(TrimBrackets(itemString), rgx_userdata_attribute_list);
			// string of one item attribute -> "NAME":{"S":"VALUE"}
			if (itemAttributeListMatches.Count < 1)
			{
				LogError("invalid goblin data: itemAttributeListMatches.Count < 1");
				return false;
			}

			var itemBlob = new GoblinDataBlob(string.Empty);
			var itemDebug = itemAttributeListMatches.Count + " Attrs:";
			foreach (Match itemAttributeListMatch in itemAttributeListMatches)
			{
				var attributeName = itemAttributeListMatch.Groups[1].Value;
				//var attributeValueTypeString = itemAttributeListMatch.Groups[2].Value;
				var attributeValueString = itemAttributeListMatch.Groups[3].Value;
				attributeValueString = attributeValueString.Replace("\\\"", "\"");

				switch (attributeName)
				{
					case "platform_user_id": itemBlob.platform_user_id = attributeValueString; break;
					case "customisation": itemBlob.customisationJson = attributeValueString; break;
					case "statistics": itemBlob.statisticsJson = attributeValueString; break;
					default:
						LogError("invalid goblin data attribute: " + attributeName);
						continue;
				}

				itemDebug += $"+attr:[{attributeName}]";
			}

			if (string.IsNullOrWhiteSpace(itemBlob.platform_user_id))
			{
				LogError("invalid goblin data: no platform_user_id");
				return false;
			}
			data = new GoblinData(itemBlob);
			Log("valid goblin data: " + itemBlob.platform_user_id + " : " + itemDebug);
			return true;
		}
	}
}
