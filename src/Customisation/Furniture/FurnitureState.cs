using Color = UnityEngine.Color;
using ColorUtility = UnityEngine.ColorUtility;

namespace GoblinShared
{
	/// <summary> One slot for a placed furniture item. </summary>
	[System.Serializable]
	public class FurnitureState
	{
		public string furniture = string.Empty;
		public string pattern = string.Empty;
		public string colorAHex = string.Empty;
		public string colorBHex = string.Empty;
		public string colorCHex = string.Empty;

		public bool empty => string.IsNullOrWhiteSpace(furniture);

		public FurnitureState() => Clear();

		public void Clear()
		{
			furniture = string.Empty;
			pattern = string.Empty;
			colorAHex = string.Empty;
			colorBHex = string.Empty;
			colorCHex = string.Empty;
		}

		public FurnitureState(string furniture, string pattern = "")
		{
			this.furniture = furniture;
			this.pattern = pattern;
			colorAHex = string.Empty;
			colorBHex = string.Empty;
			colorCHex = string.Empty;
		}

		public FurnitureState(FurnitureRuntime runtime)
		{
			furniture = runtime.state.furniture;
			pattern = runtime.state.pattern;
			colorAHex = runtime.state.colorAHex;
			colorBHex = runtime.state.colorBHex;
			colorCHex = runtime.state.colorCHex;
		}

		public FurnitureState(FurnitureState other)
		{
			furniture = other.furniture;
			pattern = other.pattern;
			colorAHex = other.colorAHex;
			colorBHex = other.colorBHex;
			colorCHex = other.colorCHex;
		}

		public void SetColorA(Color color) => colorAHex = ColorUtility.ToHtmlStringRGB(color);
		public void SetColorB(Color color) => colorBHex = ColorUtility.ToHtmlStringRGB(color);
		public void SetColorC(Color color) => colorCHex = ColorUtility.ToHtmlStringRGB(color);
		public Color GetColorA() => colorAHex.TryGetHexColor(out var c) ? c : Color.gray;
		public Color GetColorB() => colorBHex.TryGetHexColor(out var c) ? c : Color.gray;
		public Color GetColorC() => colorCHex.TryGetHexColor(out var c) ? c : Color.gray;

		public FurnitureState Clone() => MemberwiseClone() as FurnitureState;
	}
}
