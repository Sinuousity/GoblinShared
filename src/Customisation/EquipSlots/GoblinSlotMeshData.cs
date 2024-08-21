using UnityEngine;

namespace GoblinShared
{
	/// <summary> Runtime data for one MeshRenderer in a Goblin customisation slot. </summary>
	[System.Serializable]
	public class GoblinSlotMeshData
	{
		public string meshName;
		public Transform transform;
		public Renderer renderer;

		public GoblinSlotMeshData(Renderer renderer)
		{
			if (!renderer) throw new System.ArgumentNullException("renderer", "No Renderer was provided");
			this.renderer = renderer;
			transform = renderer.transform;
			meshName = renderer.gameObject.name;
		}
	}
}
