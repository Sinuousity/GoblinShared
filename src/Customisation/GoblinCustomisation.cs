namespace GoblinShared
{
	/// <summary> A container for data associated with Goblin customisation. </summary>
	[System.Serializable]
	public class GoblinCustomisation
	{
		/// <summary> Goblin face shape customisation. </summary>
		public GoblinFace face = new GoblinFace();

		/// <summary> Goblin body shape customisation. </summary>
		public GoblinProportions proportions = new GoblinProportions();

		/// <summary> Goblin skin and eye tone customisation. </summary>
		public GoblinSkin skin = new GoblinSkin();

		/// <summary> Goblin walking and idle styles customisation. </summary>
		public GoblinStyle style = new GoblinStyle();

		/// <summary> Goblin equipment and clothing customisation. </summary>
		public GoblinSlots slots = new GoblinSlots();
	}
}
