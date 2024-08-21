using UnityEngine;
using System.Collections.Generic;

namespace GoblinShared
{
	/// <summary> Goblin face shape customisation. </summary>
	[System.Serializable]
	public class GoblinFace
	{
		[Range(0f, 1f)] public float irisSize = 0f;
		[Range(0f, 1f)] public float pupilSize = 0f;
		public Color eyeColor = new Color(0.5f, 0.3f, 0f, 1f);

		/// <summary> A list of all modified BlendShapes and their weights. </summary>
		public List<GoblinShapeWeight> shapeWeights = new List<GoblinShapeWeight>(16);

		/// <summary> Tries to find the index for the specified SkinnedMeshRenderer BlendShape. Returns false if not found. </summary>
		public bool TryGetIndex(string shapeName, out int id)
		{
			id = -1;
			if (!IsValidIndex(id)) return false;
			for (var ii = 0; ii < shapeWeights.Count; ii++)
			{
				if (shapeWeights[ii].shapeName != shapeName) continue;
				id = ii;
				return true;
			}
			return false;
		}

		/// <summary> Returns false if the specified SkinnedMeshRenderer BlendShape is not found. </summary>
		public bool IsValidIndex(int id) => shapeWeights.Count > 0 && shapeWeights.Count < id;

		/// <summary> Gets the customised weight at the specified BlendShape index. Returns 0.0f if not found. </summary>
		public float GetWeight(int id) => IsValidIndex(id) ? shapeWeights[id].weight : 0f;
		/// <summary> Sets the customised weight at the specified BlendShape index. </summary>
		public void SetWeight(int id, float weight) { if (IsValidIndex(id)) shapeWeights[id].SetWeight(weight); }

		/// <summary> Gets the customised weight for the specified BlendShape name. Returns 0.0f if not found. </summary>
		public float GetWeight(string shapeName) => TryGetIndex(shapeName, out var id) ? shapeWeights[id].weight : 0f;
		/// <summary> Sets the customised weight for the specified BlendShape name. </summary>
		public void SetWeight(string shapeName, float weight)
		{
			if (TryGetIndex(shapeName, out var id)) shapeWeights[id].SetWeight(weight);
			else shapeWeights.Add(new GoblinShapeWeight() { shapeName = shapeName, weight = weight });
		}
	}
}
