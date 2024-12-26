using System.Collections.Generic;

using Color = UnityEngine.Color;
using ColorUtility = UnityEngine.ColorUtility;

namespace GoblinShared
{
	/// <summary> Customisation for the interior of a goblin home. </summary>
	[System.Serializable]
	public class GoblinHomeState
	{
		public List<FurnitureState> furniture = new List<FurnitureState>();
		public string floorPattern = string.Empty;
		public string wallPattern = string.Empty;
		public string[] colorHexes = new string[4];

		public bool empty => furniture == null || furniture.Count < 1;

		public GoblinHomeState() => Clear();

		public void Clear()
		{
			furniture = new List<FurnitureState>();
			floorPattern = string.Empty;
			wallPattern = string.Empty;
			colorHexes = new string[4];
		}

		public static readonly Color defaultTint = Color.gray;

		public void SetColor(int id, Color color)
		{
			if (id < 0 || id >= colorHexes.Length) return;
			colorHexes[id] = ColorUtility.ToHtmlStringRGB(color);
		}

		public Color GetColor(int id)
		{
			if (id < 0 || id >= colorHexes.Length) return defaultTint;
			return colorHexes[id].TryGetHexColor(out var c) ? c : defaultTint;
		}

		public GoblinHomeState Clone() => MemberwiseClone() as GoblinHomeState;
	}
}
