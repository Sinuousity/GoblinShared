using UnityEngine;

namespace GoblinShared
{
	[System.Serializable]
	public class GoblinDataBlob
	{
		public string platform_user_id = string.Empty;
		public string customisationJson = string.Empty;
		public string statisticsJson = string.Empty;

		public GoblinDataBlob(string platform_user_id)
		{
			this.platform_user_id = platform_user_id;
			customisationJson = "{}";
			statisticsJson = "{}";
		}

		public GoblinDataBlob(GoblinData data)
		{
			platform_user_id = data.platform_user_id;
			customisationJson = data.customisation == null ? "{}" : JsonUtility.ToJson(data.customisation);
			statisticsJson = data.statistics == null ? "{}" : JsonUtility.ToJson(data.statistics);
		}

		public GoblinCustomisation ParseCustomisation()
		{
			try { return JsonUtility.FromJson<GoblinCustomisation>(customisationJson); }
			catch { return new GoblinCustomisation(); }
		}

		public GoblinStatistics ParseStatistics()
		{
			try { return JsonUtility.FromJson<GoblinStatistics>(statisticsJson); }
			catch { return new GoblinStatistics(); }
		}
	}
}
