using UnityEngine;

namespace GoblinShared
{
	/// <summary> One slot for an equipped item. </summary>
	[System.Serializable]
	public class GoblinSlot
	{
		public string slotName = "";
		public string equipped = string.Empty;
		public Color tint = Color.white;

		public bool anyEquipped => equipped != string.Empty;

		public GoblinSlot(string slotName)
		{
			this.slotName = slotName;
			equipped = string.Empty;
			tint = Color.white;
		}

		public void SetColor(Color color)
		{
			tint = color;
		}
	}

}
