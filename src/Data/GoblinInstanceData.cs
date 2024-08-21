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
		/// <summary> Skin Tone A - main skin tone </summary>
		public Color tintA;

		/// <summary> Skin Tone B - secondary skin tone </summary>
		public Color tintB;

		/// <summary> Skin Tone C - eyebrow tone </summary>
		public Color tintC;

		/// <summary>
		/// Returns the integer size in bytes of a single GoblinInstanceData struct.
		/// Used as the stride argument for the ComputeBuffer provided to instanced rendering.
		/// </summary>
		public static int ByteSize => sizeof(float) * 12;
	}
}
