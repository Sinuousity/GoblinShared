using UnityEngine;
using System.Text.RegularExpressions;

namespace GoblinShared
{
	[System.Serializable]
	public abstract class GoblinObjectDefinition : ScriptableObject
	{
		public string displayName = "Null Object";
		public string codeName = "null.object";
		public virtual string codeNamePrefix => string.Empty;

		const string codeNameSeparator = ".";
		const string str_rgx_invalidCodeNameChars = @"[^\w\d]+";
		static readonly Regex rgx_invalidCodeNameChars = new Regex(str_rgx_invalidCodeNameChars);

		public void RegenerateCodeName()
		{
			codeName = rgx_invalidCodeNameChars.Replace((codeNamePrefix + codeNameSeparator + displayName.Trim()).ToLower(), codeNameSeparator);
			if (codeName.EndsWith(codeNameSeparator)) codeName = codeName.Substring(0, codeName.Length - 1);
		}

		void OnValidate()
		{
			RegenerateCodeName();
			AfterValidate();
		}
		public virtual void AfterValidate() { }
	}

	public abstract class ColoredGoblinObjectDefinition : GoblinObjectDefinition
	{
		public Color[] colors;
		public Color GetColor(int id) => colors[id];
		public void SetColor(int id, Color c) => colors[id] = c;
	}

	public abstract class PatternedGoblinObjectDefinition : ColoredGoblinObjectDefinition
	{
		public string pattern = string.Empty;
	}
}
