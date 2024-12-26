using UnityEngine;

namespace GoblinShared
{
	public enum EquipItemType { Skin, Attachment }
	public enum EquipItemSlot { Hair, Face, Torso, Hands, Waist, Legs, Feet, Back, Extra }

	[CreateAssetMenu(menuName = "GoblinView/Definition/EquipItem")]
	public class EquipItemDefinition : GoblinObjectDefinition
	{
		public EquipItemSlot equipSlot = EquipItemSlot.Extra;
		public EquipItemType equipType = EquipItemType.Skin;

		public override string codeNamePrefix => $"equip.{equipType}.{equipSlot}";

		public Color defaultColor = Color.white;
		public Material material;
		public GameObject prefab;

		[HideInInspector] public Mesh prefabMesh;
		[HideInInspector] public MeshFilter prefabMF;
		[HideInInspector] public SkinnedMeshRenderer prefabSMR;
		public Transform[] prefabBones => prefabSMR ? prefabSMR.bones : null;

		public override void AfterValidate()
		{
			if (prefab)
			{
				prefabMF = prefab.GetComponentInChildren<MeshFilter>();
				prefabSMR = prefab.GetComponentInChildren<SkinnedMeshRenderer>();
			}
			prefabMesh = prefabSMR ? prefabSMR.sharedMesh : (prefabMF ? prefabMF.sharedMesh : null);
		}

		public EquipItemMeshReference Equip(SkinnedPoseReplicator replicator, EquipItemReference equipItemRef)
		{
			if (!replicator) return null;
			var characterRenderer = replicator.sourceRenderer;
			if (!characterRenderer) return null;

			var equipMeshRef = new EquipItemMeshReference(characterRenderer, this);
			equipMeshRef.CreateObjects(characterRenderer);
			equipMeshRef.SetColor(equipItemRef.color);
			SkinnedPoseReplication.CopyBoneAssignments(characterRenderer, equipMeshRef.renderer);

			return equipMeshRef;
		}
	}
}