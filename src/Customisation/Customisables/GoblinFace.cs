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
	}
}
