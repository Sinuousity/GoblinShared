using System.Collections.Generic;
using System.Linq;
using System.Runtime;

using bindflags = System.Reflection.BindingFlags;
using membertypes = System.Reflection.MemberTypes;

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
		public List<GoblinSlot> slots = new List<GoblinSlot>();

		/// <summary> A list of all modified BlendShapes and their weights. </summary>
		public List<GoblinShapeWeight> shapeWeights = new List<GoblinShapeWeight>(16);

		/// <summary> Tries to find the index for the specified SkinnedMeshRenderer BlendShape. Returns false if not found. </summary>
		public bool TryGetBlendShapeIndex(string shapeName, out int id)
		{
			id = -1;
			if (!IsValidBlendShapeIndex(id)) return false;
			for (var ii = 0; ii < shapeWeights.Count; ii++)
			{
				if (shapeWeights[ii].shapeName != shapeName) continue;
				id = ii;
				return true;
			}
			return false;
		}

		/// <summary> Returns false if the specified SkinnedMeshRenderer BlendShape is not found. </summary>
		public bool IsValidBlendShapeIndex(int id) => shapeWeights.Count > 0 && shapeWeights.Count < id;

		/// <summary> Gets the customised weight at the specified BlendShape index. Returns 0.0f if not found. </summary>
		public float GetBlendShapeWeight(int id) => IsValidBlendShapeIndex(id) ? shapeWeights[id].weight : 0f;
		/// <summary> Sets the customised weight at the specified BlendShape index. </summary>
		public void SetBlendShapeWeight(int id, float weight) { if (IsValidBlendShapeIndex(id)) shapeWeights[id].SetWeight(weight); }

		/// <summary> Gets the customised weight for the specified BlendShape name. Returns 0.0f if not found. </summary>
		public float GetBlendShapeWeight(string shapeName) => TryGetBlendShapeIndex(shapeName, out var id) ? shapeWeights[id].weight : 0f;
		/// <summary> Sets the customised weight for the specified BlendShape name. </summary>
		public void SetBlendShapeWeight(string shapeName, float weight)
		{
			if (TryGetBlendShapeIndex(shapeName, out var id)) shapeWeights[id].SetWeight(weight);
			else shapeWeights.Add(new GoblinShapeWeight() { shapeName = shapeName, weight = weight });
		}

		public GoblinCustomisation Clone()
		{
			var c = MemberwiseClone() as GoblinCustomisation;
			c.slots = slots.Select(s => s.Clone()).ToList();
			return c;
		}

		public bool IsEquipped(string itemCodeName) => GetEquippedSlotIndex(itemCodeName) > -1;
		public int GetEquippedSlotIndex(string itemCodeName)
		{
			for (var ii = 0; ii < slots.Count; ii++) if (slots[ii].equipped == itemCodeName) return ii;
			return -1;
		}

		public void ReadEquipped(SkinnedPoseReplicator spr)
		{
			slots = new List<GoblinSlot>(spr.equipped.Count);
			foreach (var ei in spr.equipped) slots.Add(new GoblinSlot(ei));
		}
	}
}
