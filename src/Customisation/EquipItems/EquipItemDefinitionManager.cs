namespace GoblinShared
{
	public static class EquipItemDefinitionManager
	{
		static bool everLoaded = false;
		static EquipItemDefinition[] allLoaded;
		static bool ValidLoadId(int id) => allLoaded != null && id > -1 && id < allLoaded.Length;

		public static int GetItemId(string name)
		{
			TryLoadAllFirstTime();
			if (allLoaded == null || allLoaded.Length < 1) return -1;
			for (var ii = 0; ii < allLoaded.Length; ii++)
			{
				if (allLoaded[ii].codeName == name || allLoaded[ii].displayName == name) return ii;
			}
			return -1;
		}

		public static bool FindItem(string name, out EquipItemDefinition equipItem)
		{
			TryLoadAllFirstTime();
			var id = GetItemId(name);
			var valid = ValidLoadId(id);
			equipItem = valid ? allLoaded[id] : null;
			return valid;
		}

		public static EquipItemDefinition GetItem(int loadId) => ValidLoadId(loadId) ? null : allLoaded[loadId];

		public static void TryLoadAllFirstTime() { if (!everLoaded) ForceLoadAll(); }
		public static void ForceLoadAll()
		{
			allLoaded = UnityEngine.Resources.LoadAll<EquipItemDefinition>("BundleContent");
			UnityEngine.Debug.Log("Loaded " + allLoaded.Length + " EquipItem Definitions From Resources");
			everLoaded = true;
		}
	}

}
