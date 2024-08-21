namespace GoblinShared
{
    [System.Serializable]
    public class GoblinSlotController
    {
        static readonly string slotNamePrefix = "slot_";

        /// <summary> All Goblin equipment / clothing slots. </summary>
        public GoblinSlotData[] slots = new GoblinSlotData[0];

        bool _hideSlotMeshes  = false;
        /// <summary> Temporarily disable all slot MeshRenderers. </summary>
        public bool hideSlotMeshes
		{
            get => _hideSlotMeshes;
			set => SetVisibility (value);
		}

        /// <summary> Create a new Goblin Slot Controller for the specified IGoblinController. </summary>
        public GoblinSlotController(IGoblinController goblin)
        {
            if (!goblin.IsCreated) throw new System.ArgumentException("Goblin not yet created!", "goblin");

            var allSlots = goblin.customisationDefinition.customisation.slots.allSlots;
            slots = new GoblinSlotData[allSlots.Length];
            for (var ii = 0; ii < slots.Length; ii++)
            {
                var slotName = slotNamePrefix + allSlots[ii].slotName.ToLower().Trim();
                var slotRoot = goblin.bodyAnimator.transform.FindRecursive(slotName);
                slots[ii] = new GoblinSlotData(allSlots[ii], slotRoot);
            }
        }

        /// <summary> Get the slot data for the specified slot name. Returns null if not found. </summary>
        public GoblinSlotData GetSlotData(string slotName)
        {
            foreach (var slot in slots)
            {
                if (slot.slotName != slotName) continue;
                return slot;
            }
            return null;
        }

        /// <summary> Set the visibility for all slot MeshRenderers. </summary>
        public void SetVisibility(bool visible)
        {
            _hideSlotMeshes = visible;
            RefreshSlotVisibility();
        }

        /// <summary> Updates the visibility for all slot MeshRenderers. </summary>
        public void RefreshSlotVisibility()
        {
            if (hideSlotMeshes) DisableAll();
            else foreach (var slot in slots) slot.EnableIfSelected();
        }

        /// <summary> Disables all slot MeshRenderers. </summary>
        public void DisableAll()
        {
            foreach (var slot in slots) slot.DisableAllMeshes();
        }
    }
}
