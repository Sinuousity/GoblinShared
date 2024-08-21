namespace GoblinShared
{
	/// <summary> A container for data associated with Goblin customisation. </summary>
	[System.Serializable]
	public class GoblinCustomisation
	{
		/// <summary> Goblin face shape customisation. </summary>
		public GoblinFace face = new GoblinFace(); // face blendshapes
		/// <summary> Goblin body shape customisation. </summary>
		public GoblinProportions proportions = new GoblinProportions(); // general body shape
		/// <summary> Goblin skin and eye tone customisation. </summary>
		public GoblinSkin skin = new GoblinSkin(); // colors / texture / material of skin
		/// <summary> Goblin walking and idle styles customisation. </summary>
		public GoblinStyle style = new GoblinStyle(); // idle / walking styles
		/// <summary> Goblin equipment and clothing customisation. </summary>
		public GoblinSlots slots = new GoblinSlots(); // equipment
	}
}
