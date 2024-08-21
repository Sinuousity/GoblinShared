using UnityEngine;

namespace GoblinShared
{
	/// <summary> Goblin walking and idle styles customisation. </summary>
	[System.Serializable]
	public class GoblinStyle
	{
		[Range(0f, 1f)] public float idleStyle = 0.5f;
		[Range(0f, 1f)] public float walkStyle = 0.5f;
		[Range(0f, 1f)] public float danceStyle = 0.5f;
	}
}
