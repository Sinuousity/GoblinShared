using System.Linq;
using System;

namespace GoblinShared
{
	public static partial class AWSUtils
	{
		[Serializable]
		public class KeyFilter
		{
			[Serializable]
			public class KeyAttribute
			{
				public string name;
				public string value;
				public string valueType;

				public KeyAttribute(string name, string value, string valueType = "S")
				{
					this.name = name;
					this.value = value;
					this.valueType = valueType;
				}
			}

			public KeyAttribute[] attributes = new KeyAttribute[0];

			public KeyFilter(params KeyAttribute[] attributes)
			{
				this.attributes = attributes;
			}

			static string GetAttributeString(KeyAttribute attribute)
			{
				if (attribute == null) return null;
				return $"\"{attribute.name}\":{{\"{attribute.valueType}\":\"{attribute.value}\"}}";
			}

			public override string ToString()
			{
				if (attributes == null || attributes.Length < 1) return null;
				var x = attributes.Select(GetAttributeString).Where(s => !string.IsNullOrEmpty(s));
				if (x.Count() < 1) return null;
				var filter = string.Join(",", x);
				x = null;
				return "{" + filter + "}";
			}
		}


	}

}
