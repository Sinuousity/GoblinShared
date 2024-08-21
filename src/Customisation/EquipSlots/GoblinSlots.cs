namespace GoblinShared
{
	/// <summary> Goblin equipment and clothing customisation. </summary>
	[System.Serializable]
	public class GoblinSlots
	{
		public GoblinSlot hat = new GoblinSlot("hats");
		public GoblinSlot hair = new GoblinSlot("hair");
		public GoblinSlot face = new GoblinSlot("face");
		public GoblinSlot neck = new GoblinSlot("neck");
		public GoblinSlot torso = new GoblinSlot("torso");
		public GoblinSlot waist = new GoblinSlot("waist");
		public GoblinSlot legs = new GoblinSlot("legs");
		public GoblinSlot feet = new GoblinSlot("feet");
		public GoblinSlot extra = new GoblinSlot("extra");

		public GoblinSlot[] allSlots => new GoblinSlot[]
			{
			hat,
			hair,
			face,
			neck,
			torso,
			waist,
			legs,
			feet,
			extra
			};
	}
}
