using UnityEngine;

namespace GoblinShared
{
	/// <summary>Serializable data container for one EquipItem Definition and its customization options (color)</summary>
	[System.Serializable]
	public struct EquipItemReference
	{
		public EquipItemDefinition equipItemDefinition;
		public string pattern;
		public Color color;

		public EquipItemReference(EquipItemDefinition itemDefinition)
		{
			equipItemDefinition = itemDefinition;
			color = itemDefinition.defaultColor;
			pattern = string.Empty;
		}
	}
}