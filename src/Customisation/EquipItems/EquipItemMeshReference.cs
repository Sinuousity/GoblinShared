using UnityEngine;

namespace GoblinShared
{
	[System.Serializable]
	public class EquipItemMeshReference
	{
		public SkinnedMeshRenderer sourceRenderer;
		public EquipItemDefinition itemDefinition;
		public Color color = Color.gray;

		public EquipItemMeshReference(SkinnedMeshRenderer sourceRenderer, EquipItemDefinition itemDefinition)
		{
			this.sourceRenderer = sourceRenderer;
			this.itemDefinition = itemDefinition;
			color = itemDefinition.defaultColor;
		}

		public Material material => itemDefinition ? itemDefinition.material : null;
		SkinnedMeshRenderer prefabRenderer => itemDefinition ? itemDefinition.prefabSMR : null;

		public bool createdObjects { get; private set; }
		public GameObject rendererObject { get; private set; }
		public SkinnedMeshRenderer renderer { get; private set; }
		public MaterialPropertyBlock mpb { get; private set; }

		public void CreateObjects(SkinnedMeshRenderer sourceRenderer)
		{
			if (createdObjects) return;
			var sharedMesh = itemDefinition.prefabMesh;
			if (!sharedMesh) return;

			var parent = sourceRenderer.transform.parent;

			rendererObject = new GameObject("tmp-smr-equip-" + sharedMesh.name);
			rendererObject.SetActive(false);
			rendererObject.SetRuntimeFlags(false, false);
			rendererObject.transform.SetParent(parent, false);
			rendererObject.layer = parent.gameObject.layer;

			renderer = rendererObject.AddComponent<SkinnedMeshRenderer>();
			renderer.sharedMaterial = material;
			renderer.sharedMesh = sharedMesh;
			mpb = new MaterialPropertyBlock();
			RefreshColor();

			if (prefabRenderer)
			{
				renderer.bones = prefabRenderer.bones;
				//renderer.bounds = prefabRenderer.bounds;
			}

			SkinnedPoseReplication.CopyBoneAssignments(sourceRenderer, renderer);

			rendererObject.SetActive(true);
			createdObjects = true;
		}

		public void SetColor(Color newColor)
		{
			color = newColor;
			RefreshColor();
		}

		public void RefreshColor()
		{
			if (!createdObjects) return;
			mpb.SetColor("_TintA", color);
			mpb.SetColor("_TintB", color.Saturate(0.85f).ValueMult(0.5f));
			renderer.SetPropertyBlock(mpb);
		}

		public void DestroyObjects()
		{
			if (!createdObjects) return;
			rendererObject.DestroySafe();
			createdObjects = false;
		}
	}
}