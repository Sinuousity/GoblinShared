using UnityEngine;

namespace GoblinShared
{
	/// <summary> Goblin body shape customisation. </summary>
	[System.Serializable]
	public class GoblinProportions
	{
		[Range(-1f, 1f)] public float height = 0f;
		[Range(-1f, 1f)] public float width = 0f;
		[Range(-1f, 1f)] public float headSize = 0f;
		[Range(-1f, 1f)] public float handSize = 0f;
		[Range(-1f, 1f)] public float footSize = 0f;
	}
}
