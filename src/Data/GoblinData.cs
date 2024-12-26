namespace GoblinShared
{
	/// <summary>
	/// This is the actual data container object that is stored remotely for each goblin.
	/// Contains a viewer's platform-specific ID as well as their customisation and statistics data.
	/// </summary>
	[System.Serializable]
	public class GoblinData
	{
		/// <summary> The viewer's platform-specific ID, typically 'platformName.platformUserId'. Default = string.Empty </summary>
		public string platform_user_id = string.Empty;

		/// <summary> The customisation data for this goblin. Default = null </summary>
		public GoblinCustomisation customisation = null;

		/// <summary> The statistics data for this goblin. Default = null </summary>
		public GoblinStatistics statistics = null;

		/// <summary> Create a new data container to hold a viewer's customisation data. </summary>
		/// <param name="id">The viewer's platform-specific ID, typically 'platformName.platformUserId'</param>
		/// <param name="customisation">The customisation data for this goblin</param>
		/// <param name="statistics">The statistics data for this goblin</param>
		public GoblinData(string id, GoblinCustomisation customisation, GoblinStatistics statistics)
		{
			platform_user_id = id;
			this.customisation = customisation;
			this.statistics = statistics;
		}

		/// <summary> Create a new data container to hold a viewer's customisation data. </summary>
		public GoblinData(GoblinDataBlob blob)
		{
			platform_user_id = blob.platform_user_id;
			customisation = blob.ParseCustomisation();
			statistics = blob.ParseStatistics();
		}
	}
}
