using Color = UnityEngine.Color;

namespace GoblinShared
{
	[UnityEngine.CreateAssetMenu(menuName = "GoblinView/Definition/Furniture")]
	public class FurnitureDefinition : GoblinObjectDefinition
	{
		public override string codeNamePrefix => "furniture";

		public UnityEngine.GameObject prefab;

		public string defaultPattern = string.Empty;
		public Color defaultColorA = Color.gray;
		public Color defaultColorB = Color.gray;
		public Color defaultColorC = Color.gray;
	}
}
