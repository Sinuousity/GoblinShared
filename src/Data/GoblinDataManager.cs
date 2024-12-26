using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;

namespace GoblinShared
{
	public static class GoblinDataManager
	{
		public static Dictionary<string, GoblinData> loadedData { get; private set; } = new Dictionary<string, GoblinData>();

		public static System.Action afterDataLoaded;

		public static List<string> debugOutput = new List<string>();

		const string rgx_userdata_itemlist = @"\,?\""Items\""\:\[(.+)\]\,?";
		const string rgx_userdata_items = @"\{(\"".+?\})\}(?=(?:\,\{\"")|$)";
		//static readonly string rgx_userdata_attribute_list = @"\""(.+?)\""\:\{\""(\w{1,5})\""\:\""(.+?)\""\}(?=$|(?:\,\""))";
		//static readonly string rgx_userdata_attribute_data = @"(?:\""(.+?)\"")\:(?:\{\""(?:\w+)\""\:\""(.+)\""\})";
		//static string TrimBrackets(string s) => TrimEnds(s, "{", "}");
		//static string TrimCommas(string s) => TrimEnds(s, ",", ",");

		static string TrimEnds(string s, string trimStart, string trimEnd)
		{
			if (s.StartsWith(trimStart)) s = s.Substring(trimStart.Length, s.Length - trimStart.Length);
			if (s.EndsWith(trimEnd)) s = s.Substring(0, s.Length - trimEnd.Length);
			return s;
		}

		public static AWSUtils.DBReqPutItem putItemRequester = new AWSUtils.DBReqPutItem();
		public static AWSUtils.DBReqGetItem getItemRequester = new AWSUtils.DBReqGetItem();
		public static AWSUtils.DBReqScan scanRequester = new AWSUtils.DBReqScan();

		public const string tableName_GoblinsUserData = "goblins-userdata";
		public const string tableName_GoblinsCustomisation = "goblins-customisation";

		public static IEnumerator LoadOne(string platformUserId, System.Action<GoblinData> onParsed)
		{
			debugOutput.Clear();
			debugOutput.Add("Loading One Goblin User Data: " + platformUserId);
			loadedData.Clear();

			debugOutput.Add("Sending database request...");
			yield return getItemRequester.SendRequest(
				new AWSUtils.DBReqGetItemOptions()
				{
					tableName = tableName_GoblinsUserData,
					itemName = platformUserId
				},
				s => onParsed?.Invoke(ParseLoadOne(s)),
				e => UnityEngine.Debug.LogError("ERROR in LoadOne(): " + e)
			);
		}

		public static GoblinData ParseLoadOne(string response)
		{
			const string str_item_leading = @"""Item"":{";
			const string str_item_trailing = @"}";

			var validStart = response.StartsWith(str_item_leading);
			var validEnd = response.EndsWith(str_item_trailing);

			if (!validStart || !validEnd) throw new System.ArgumentException("ParseLoadOne response malformed: (!validStart || !validEnd)");

			var jsonIndex0 = str_item_leading.Length;
			var jsonIndex1 = System.Math.Max(0, response.Length - (jsonIndex0 + str_item_trailing.Length));
			response = response.Substring(jsonIndex0, jsonIndex1);

			if (GoblinDataParsing.ParseOne(response, out var loadedGoblin))
			{
				debugOutput.Add("Goblin User Data Loaded");
				return loadedGoblin;
			}
			throw new System.ArgumentException("ParseLoadOne failed inside GoblinDataParsing.ParseOne");
		}

		public static IEnumerator LoadAll()
		{
			debugOutput.Clear();
			debugOutput.Add("Loading All Goblin User Data");
			loadedData.Clear();

			debugOutput.Add("Sending database request...");
			yield return scanRequester.SendRequest(
				new AWSUtils.DBReqScanOptions() { tableName = tableName_GoblinsUserData },
				s => ParseLoadAll(s),
				e => UnityEngine.Debug.LogError("ERROR in LoadAll(): " + e)
			);
		}

		public static void ParseLoadAll(string resultJson)
		{
			try
			{
				debugOutput.Add("  ...got: " + resultJson);

				var itemListMatches = Regex.Matches(resultJson, rgx_userdata_itemlist);
				// string with list of table items -> ITEM,ITEM,ITEM,...
				if (itemListMatches.Count < 1)
				{
					debugOutput.Add("  skipping LoadAll... itemListMatches.Count < 1");
					return;
				}
				foreach (Match itemListMatch in itemListMatches)
				{
					var itemListString = itemListMatch.Groups[1].Value;
					var itemStringMatches = Regex.Matches(itemListString, rgx_userdata_items);
					debugOutput.Add("Matched " + itemStringMatches.Count + " item strings");
					// string of one table item's attributes -> ATTR,ATTR,ATTR,...
					if (itemStringMatches.Count < 1)
					{
						debugOutput.Add("  skipping item... itemStringMatches.Count < 1");
						continue;
					}
					foreach (Match itemStringMatch in itemStringMatches)
					{
						if (GoblinDataParsing.ParseOne(itemStringMatch.Value, out var data))
							loadedData.Add(data.platform_user_id, data);
					}
				}

				afterDataLoaded?.Invoke();
			}
			catch (System.Exception e)
			{
				debugOutput.Add("ERROR: " + e.Message);
			}
		}
	}
}
