using UnityEngine;

namespace GoblinShared
{
	/// <summary>
	/// A ScriptableObject that holds runtime data and tracks modifications associated with a single Goblin.
	/// Used as a global state in the GoblinMGMT web app for the logged in Goblin.
	/// Used as a global state in the GoblinView desktop app for the Goblin King.
	/// </summary>
	[CreateAssetMenu(menuName = "Goblin/Customisation Definition")]
	public class GoblinCustomisationDefinition : ScriptableObject
	{
		/// <summary> The container for data associated with this Goblin customisation. </summary>
		public GoblinCustomisation customisation = new GoblinCustomisation();
	}
}
