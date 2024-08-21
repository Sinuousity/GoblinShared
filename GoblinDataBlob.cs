namespace GoblinShared
{
	/// <summary>
	/// This is the actual data container object that is stored remotely for each goblin.
	/// Contains a viewer's platform-specific ID as well as their customisation data.
	/// </summary>
	[System.Serializable]
	public class GoblinDataBlob
	{
		/// <summary> The viewer's platform-specific ID, typically 'platformName.platformUserId'. Default = string.Empty </summary>
		public string id = string.Empty;

		/// <summary> The customisation data for this goblin. Default = null </summary>
		public GoblinCustomisation customisation = null;

		/// <summary> Create a new data container to hold a viewer's customisation data. </summary>
		/// <param name="id">The viewer's platform-specific ID, typically 'platformName.platformUserId'</param>
		/// <param name="customisation">The customisation data for this goblin</param>
		public GoblinDataBlob(string id, GoblinCustomisation customisation)
		{
			this.id = id;
			this.customisation = customisation;
		}
	}
}
