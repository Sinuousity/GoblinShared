using System.Collections.Generic;
using UnityEngine;

namespace GoblinShared
{
	public class SkinnedPoseReplicator : MonoBehaviour
	{
		public SkinnedMeshRenderer sourceRenderer;
		public List<EquipItemReference> equipped = new List<EquipItemReference>();
		public int equipItemCount => equipped == null ? 0 : equipped.Count;

		public bool created { get; private set; } = false;
		public RunningDirty dirtyState { get; private set; } = null;
		public EquipItemMeshReference[] equipItemMeshRefs { get; private set; } = null;

		public System.Action OnMeshRefsChanged;
		public System.Action<int> AfterSlotEquipped;
		public System.Action<int> BeforeSlotUnequipped;
		public List<int> addedSlotIndices { get; private set; } = new List<int>();

		void OnValidate() => dirtyState?.MarkDirty();
		void OnEnable() => TryCreate();
		void Update() => dirtyState?.CheckDirty();
		void OnDisable() => TryDestroy();

		void Recreate() { TryDestroy(); TryCreate(); }

		public int GetEquipmentIndex(EquipItemDefinition itemDefinition)
		{
			if (!itemDefinition) return -1;
			for (var ii = 0; ii < equipItemCount; ii++)
			{
				if (!equipped[ii].equipItemDefinition) continue;
				if (equipped[ii].equipItemDefinition != itemDefinition) continue;
				return ii;
			}
			return -1;
		}

		public int GetEquipmentIndex(EquipItemSlot slot)
		{
			for (var ii = 0; ii < equipItemCount; ii++)
			{
				if (!equipped[ii].equipItemDefinition) continue;
				if (equipped[ii].equipItemDefinition.equipSlot != slot) continue;
				return ii;
			}
			return -1;
		}

		public bool IsSlotEquipped(EquipItemSlot slot) => GetEquipmentIndex(slot) > -1;
		public void TryEquip(EquipItemDefinition itemDef, bool replaceSlot = true) => TryEquip(new EquipItemReference(itemDef), replaceSlot);
		public void TryEquip(EquipItemReference itemRef, bool replaceSlot = true)
		{
			var item = itemRef.equipItemDefinition;
			var stackable = item.equipSlot == EquipItemSlot.Extra;
			if (!stackable)
			{
				var existing = GetEquipmentIndex(item.equipSlot);
				if (!replaceSlot && existing > -1) return;
				while (existing > -1)
				{
					equipped.RemoveAt(existing);
					existing = GetEquipmentIndex(item.equipSlot);
				}
			}
			addedSlotIndices.Add(equipped.Count);
			equipped.Add(itemRef);
			AfterSlotEquipped?.Invoke(equipped.Count - 1);
			dirtyState?.MarkDirty();
		}

		public void TryUnequipItem(EquipItemReference itemRef)
		{
			var item = itemRef.equipItemDefinition;
			var existing = GetEquipmentIndex(item);
			while (existing > -1)
			{
				BeforeSlotUnequipped?.Invoke(equipped.Count - 1);
				equipped.RemoveAt(existing);
				existing = GetEquipmentIndex(item);
			}
			dirtyState?.MarkDirty();
		}

		public void TryUnequipItemAt(int itemSlotIndex)
		{
			if (itemSlotIndex < 0) return;
			if (itemSlotIndex >= equipped.Count) return;
			equipped.RemoveAt(itemSlotIndex);
			dirtyState?.MarkDirty();
		}

		public void TryUnequipSlot(EquipItemSlot slot)
		{
			var existing = GetEquipmentIndex(slot);
			if (existing < 0) return;
			equipped.RemoveAt(existing);
			dirtyState?.MarkDirty();
		}

		void TryCreate()
		{
			if (created) return;
			created = true;

			if (dirtyState == null) dirtyState = new RunningDirty(Recreate, 0.1f, false);
			CreateEquipItemMeshReferences();
			addedSlotIndices.Clear();
			dirtyState.MarkDirty(false);
		}

		void TryDestroy()
		{
			if (!created) return;
			created = false;
			ReleaseEquipItemMeshReferences();
		}

		void CreateEquipItemMeshReferences()
		{
			equipItemMeshRefs = new EquipItemMeshReference[equipItemCount];
			if (equipItemMeshRefs.Length < 1) return;
			for (var ii = 0; ii < equipItemMeshRefs.Length; ii++)
			{
				var e = equipped[ii];
				if (!e.equipItemDefinition) continue;
				equipItemMeshRefs[ii] = e.equipItemDefinition.Equip(this, e);
			}

			OnMeshRefsChanged?.Invoke();
		}

		void ReleaseEquipItemMeshReferences()
		{
			if (equipItemMeshRefs == null) return;
			foreach (var e in equipItemMeshRefs)
			{
				if (e == null) continue;
				e.DestroyObjects();
			}
			equipItemMeshRefs = null;
		}
	}
}