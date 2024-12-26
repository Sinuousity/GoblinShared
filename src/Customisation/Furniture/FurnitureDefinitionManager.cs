namespace GoblinShared
{
	public static class FurnitureDefinitionManager
	{
		static bool everLoaded = false;
		static FurnitureDefinition[] allLoaded;
		static bool ValidLoadId(int id) => allLoaded != null && id > -1 && id < allLoaded.Length;

		public static int GetItemId(string name)
		{
			TryLoadAllFirstTime();
			if (allLoaded == null) return -1;
			if (allLoaded.Length < 1) return -1;
			for (var ii = 0; ii < allLoaded.Length; ii++)
			{
				if (allLoaded[ii].codeName == name) return ii;
				if (allLoaded[ii].displayName == name) return ii;
			}
			return -1;
		}

		public static bool FindItem(string name, out FurnitureDefinition equipItem)
		{
			TryLoadAllFirstTime();
			var id = GetItemId(name);
			var valid = ValidLoadId(id);
			equipItem = valid ? allLoaded[id] : null;
			return valid;
		}

		public static FurnitureDefinition GetItem(int loadId) => ValidLoadId(loadId) ? null : allLoaded[loadId];

		public static void TryLoadAllFirstTime() { if (!everLoaded) ForceLoadAll(); }
		public static void ForceLoadAll()
		{
			allLoaded = UnityEngine.Resources.LoadAll<FurnitureDefinition>("BundleContent");
			UnityEngine.Debug.Log("Loaded " + allLoaded.Length + " Furniture Definitions From Resources");
			everLoaded = true;
		}
	}
}
