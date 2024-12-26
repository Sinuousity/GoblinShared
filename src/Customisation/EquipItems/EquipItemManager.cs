using UnityEngine;

public static class EquipItemManager
{
	static bool everLoaded = false;
	static EquipItemDefinition[] allLoaded;
	static bool ValidLoadId(int id) => allLoaded != null && id > -1 && id < allLoaded.Length;
	public static EquipItemDefinition GetItem(int loadId) => ValidLoadId(loadId) ? null : allLoaded[loadId];

	public static void TryLoadAllFirstTime() { if (!everLoaded) ForceLoadAll(); }
	public static void ForceLoadAll()
	{
		allLoaded = Resources.LoadAll<EquipItemDefinition>("definitions/equip_items");
		Debug.Log("Loaded " + allLoaded.Length + " EquipItems");
		everLoaded = true;
	}
}
