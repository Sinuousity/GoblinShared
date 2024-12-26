namespace GoblinShared
{
	/// <summary> One slot for an equipped item. </summary>
	[System.Serializable]
	public class GoblinSlot
	{
		public string equipped = string.Empty;
		public string design = string.Empty;
		public string colorHex = string.Empty;

		public bool anyEquipped => equipped != string.Empty;

		public GoblinSlot()
		{
			equipped = "";
			design = "";
			colorHex = "";
		}

		public GoblinSlot(string itemName, string design = "", string colorHex = "")
		{
			equipped = itemName;
			this.design = design;
			this.colorHex = colorHex;
		}

		public GoblinSlot(EquipItemReference eir)
		{
			this.equipped = eir.equipItemDefinition.codeName;
			this.design = eir.pattern;
			this.colorHex = UnityEngine.ColorUtility.ToHtmlStringRGB(eir.color);
		}

		public void SetColor(string colorHex) => this.colorHex = colorHex;
		public void SetColor(UnityEngine.Color color) => colorHex = UnityEngine.ColorUtility.ToHtmlStringRGB(color);
		public UnityEngine.Color GetColor() => colorHex.TryGetHexColor(out var c) ? c : UnityEngine.Color.gray;

		public GoblinSlot Clone() => MemberwiseClone() as GoblinSlot;
	}
}
