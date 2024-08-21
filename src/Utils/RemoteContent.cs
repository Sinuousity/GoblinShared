namespace GoblinShared
{
	public static class RemoteContent
	{
		const string projectId = "4b955f4e-8e98-4673-98c2-1b48d71d650b";
		const string bucketId = "d236009c-f02c-41a9-a398-b00aeb3a884c";
		const string clientApiURL = "https://" + projectId + ".client-api.unity3dusercontent.com/client_api/v1";
		const string remotePath = clientApiURL + "/buckets/" + bucketId;

		/// <summary>
		/// URL to the uploaded AssetBundle catalog and data.
		/// The AssetBundle local path for target assets should be appended to the end of this URL.
		/// </summary>
		public const string contentPath = remotePath + "/release_by_badge/latest/entry_by_path/content/?path=";
	}
}
