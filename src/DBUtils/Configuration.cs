namespace GoblinShared
{
	public static partial class AWSUtils
	{
		public static bool configurationEverUpdated => Configuration.everUpdated;
		public static void UpdateConfiguration(string region, string accessKey, string secretKey, string sessionToken = null)
		{
			Configuration.Update(region, accessKey, secretKey, sessionToken);
		}

		protected static class Configuration
		{
			public static bool everUpdated { get; set; } = false;

			public static string region { get; set; }
			public static string accessKey { get; set; }
			public static string secretKey { get; set; }
			public static string sessionToken { get; set; }

			public static void Update(string region, string accessKey, string secretKey, string sessionToken)
			{
				Configuration.region = region;
				Configuration.accessKey = accessKey;
				Configuration.secretKey = secretKey;
				Configuration.sessionToken = sessionToken;

				everUpdated = true;
			}
		}
	}
}
