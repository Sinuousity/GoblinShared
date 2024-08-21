using UnityEngine;

namespace GoblinShared
{
	/// <summary>
	/// Struct containing skin tone data for rendering an instanced Goblin.
	/// Used when rendering the crowd with Graphics.DrawMeshInstancedIndirect
	/// </summary>
	[System.Serializable]
	public struct GoblinInstanceData
	{
		public Color tintA;
		public Color tintB;
		public Color tintC;

		public static int ByteSize => sizeof(float) * 12;
	}
}
