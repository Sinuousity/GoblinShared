using UnityEngine;

namespace GoblinShared
{
	/// <summary> Data associated with one SkinnedMeshRenderer BlendShape. </summary>
	[System.Serializable]
	public class GoblinShapeWeight
	{
		public string shapeName;
		[Range(0f, 1f)] public float weight;
		public void SetWeight(float weight) { this.weight = weight; }
	}

}
