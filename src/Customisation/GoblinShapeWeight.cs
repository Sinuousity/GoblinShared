namespace GoblinShared
{
	/// <summary>
	/// Customisation data associated with one SkinnedMeshRenderer BlendShape.
	/// Typical BlendShape weight range is -1.0f -> 1.0f
	/// </summary>
	[System.Serializable]
	public class GoblinShapeWeight
	{
		/// <summary> The string name of the target BlendShape, matching exactly one BlendShape from the mesh file. Default = string.Empty </summary>
		public string shapeName = string.Empty;

		/// <summary> The current weight value of the target BlendShape. Default = 0.0f </summary>
		[UnityEngine.Range(-1f, 1f)] public float weight = 0f;

		/// <summary> Sets the current weight value. </summary>
		public void SetWeight(float weight) { this.weight = weight; }
	}

}
