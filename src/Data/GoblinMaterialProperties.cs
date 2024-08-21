using UnityEngine;

namespace GoblinShared
{
	/// <summary>
	/// Contains material ShaderIDs for various material properties relevant to Goblin shaders.
	/// </summary>
	public static class GoblinMaterialProperties
	{
		public static readonly string pname_TintA = "_TintA";
		public static readonly int pid_TintA = Shader.PropertyToID(pname_TintA);

		public static readonly string pname_TintB = "_TintB";
		public static readonly int pid_TintB = Shader.PropertyToID(pname_TintB);

		public static readonly string pname_TintC = "_TintC";
		public static readonly int pid_TintC = Shader.PropertyToID(pname_TintC);
	}
}
