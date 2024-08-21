using UnityEngine;

namespace GoblinShared
{
    /// <summary> Determines and stores the MeshRenderers available for this customisation slot. </summary>
    [System.Serializable]
    public class GoblinSlotData
    {
        /// <summary> The name for this customisation slot. Used to match against the rig hierarchy. </summary>
        public string slotName;
        /// <summary> The data for this customisation slot. </summary>
        public GoblinSlot slot;
        /// <summary> The root Transform for this customisation slot, part of the Goblin's rig. </summary>
        public Transform root;
        /// <summary> Data for the MeshRenderers in this customisation slot. </summary>
        public GoblinSlotMeshData[] meshes;

        public GoblinSlotData(GoblinSlot slot, Transform root)
        {
            this.slotName = slot.slotName;
            this.slot = slot;
            this.root = root;
            meshes = new GoblinSlotMeshData[0];

            if (!root) return;

            var childRenderers = root.GetComponentsInChildren<Renderer>();
            if (childRenderers.Length < 1) return;

            meshes = new GoblinSlotMeshData[childRenderers.Length];
            for (var ii = 0; ii < childRenderers.Length; ii++) meshes[ii] = new GoblinSlotMeshData(childRenderers[ii]);

            EnableIfSelected();
        }

        public void DisableAllMeshes()
        {
            if (meshes == null) return;
            if (meshes.Length < 1) return;
            for (var ii = 0; ii < meshes.Length; ii++)
            {
                if (!meshes[ii].renderer) continue;
                meshes[ii].renderer.gameObject.SetActive(false);
            }
        }

        public void EnableIfSelected()
        {
            DisableAllMeshes();
            if (slot.anyEquipped) EnableMeshByName(slot.equipped);
        }

        public void EnableMeshByName(string meshName)
        {
            if (meshName == string.Empty) return;
            if (meshes.Length < 1) return;

            for (var ii = 0; ii < meshes.Length; ii++)
            {
                if (meshes[ii].meshName != meshName) continue;
                meshes[ii].renderer.gameObject.SetActive(true);
            }
        }

        public void UpdateColor()
        {
            foreach (var mesh in meshes)
            {
                var mpb = new MaterialPropertyBlock();
                mesh.renderer.GetPropertyBlock(mpb);
                mpb.SetColor(GoblinMaterialProperties.pid_TintA, slot.tint);
                mesh.renderer.SetPropertyBlock(mpb);
            }
        }
    }
}
