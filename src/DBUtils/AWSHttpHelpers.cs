using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using System.Net.Http;
using System;
using UnityEngine.Networking;

using UWR = UnityEngine.Networking.UnityWebRequest;

namespace GoblinShared
{
	public static partial class AWSUtils
	{
		/// <summary>
		/// Adapted from the AWS API Request Signing examples: 
		/// https://github.com/aws-samples/sigv4-signing-examples/blob/main/no-sdk/dotnet/HttpHelpers.cs
		/// </summary>
		public static class AWSHttpHelpers
		{
			/*
			public static async Task<string> Get(string service, string url)
			{
				var uri = new Uri(url);
				var now = DateTime.UtcNow;
				var amzDate = AWSSigner.ToAmzDate(now);
				var authorizationHeader = AWSSigner.GetAuthorizationHeader(service, AWSUtils.Configuration.region, "GET", uri, now);

				// Make the request
				using (var client = new HttpClient())
				{
					client.DefaultRequestHeaders.Add("Host", uri.Host);
					client.DefaultRequestHeaders.Add("x-amz-date", amzDate);
					if (!string.IsNullOrWhiteSpace(AWSUtils.Configuration.sessionToken))
						client.DefaultRequestHeaders.Add("x-amz-security-token", AWSUtils.Configuration.sessionToken);
					client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationHeader);

					var response = await client.GetAsync(uri);

					var responseBody = response.IsSuccessStatusCode
						? response.Content.ReadAsStringAsync().Result
						: $"Error: {(int)response.StatusCode} {response.ReasonPhrase} {response.Content.ReadAsStringAsync().Result}";

					return responseBody;
				}
			}
			*/

			public static IEnumerator Get(string service, string url, System.Action<string> withResult, System.Action<string> withError)
			{
				var uri = new Uri(url);
				var now = DateTime.UtcNow;
				var amzDate = AWSUtils.AWSSigner.ToAmzDate(now);
				var authorizationHeader = AWSUtils.AWSSigner.GetAuthorizationHeader(service, AWSUtils.Configuration.region, "GET", uri, now);

				using (var webRequest = UWR.Get(uri))
				{
					//webRequest.SetRequestHeader("Host", uri.Host);
					webRequest.SetRequestHeader("x-amz-date", amzDate);

					if (!string.IsNullOrWhiteSpace(AWSUtils.Configuration.sessionToken))
						webRequest.SetRequestHeader("x-amz-security-token", AWSUtils.Configuration.sessionToken);

					webRequest.SetRequestHeader("Authorization", authorizationHeader);

					var operation = webRequest.SendWebRequest();
					while (!operation.isDone) yield return null;

					if (webRequest.responseCode >= 200 && webRequest.responseCode < 300) withResult?.Invoke(webRequest.downloadHandler.text);
					else withError?.Invoke($"Error: {webRequest.responseCode} {webRequest.error} {webRequest.downloadHandler.text}");
				}
			}

			public static IEnumerator Post(
				string service,
				string url,
				string payload,
				string amzTarget,
				System.Action<string> withResult,
				System.Action<string> withError
			)
			{
				var uri = new Uri(url);
				var now = DateTime.UtcNow;
				var amzDate = AWSSigner.ToAmzDate(now);
				var payloadHash = AWSSigner.CalculateHash(payload);
				var headers = new Dictionary<string, string>
				{
					{"x-amz-content-sha256", payloadHash},
					{"content-length", payload.Length.ToString()},
					{"content-type", "application/json"}
				};
				var authorizationHeader = AWSSigner.GetAuthorizationHeader(service, AWSUtils.Configuration.region, "POST", uri, now, headers, payloadHash);

				var webRequest = new UWR(uri, "POST");

				webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(payload));
				webRequest.downloadHandler = new DownloadHandlerBuffer(); // manual assignment required for UWR() constructor

				//webRequest.SetRequestHeader("Host", uri.Host);
				webRequest.SetRequestHeader("x-amz-date", amzDate);
				if (!string.IsNullOrWhiteSpace(amzTarget)) webRequest.SetRequestHeader("x-amz-target", amzTarget);
				if (!string.IsNullOrWhiteSpace(AWSUtils.Configuration.sessionToken)) webRequest.SetRequestHeader("x-amz-security-token", AWSUtils.Configuration.sessionToken);
				webRequest.SetRequestHeader("content-type", "application/json");
				//requestContent.Headers.Add("content-length", payload.Length.ToString());
				webRequest.SetRequestHeader("x-amz-content-sha256", payloadHash);

				webRequest.SetRequestHeader("Authorization", authorizationHeader);

				var operation = webRequest.SendWebRequest();
				while (!operation.isDone) yield return null;

				var resultJson = webRequest.downloadHandler.text;

				if (webRequest.responseCode >= 200 && webRequest.responseCode < 300) withResult?.Invoke(resultJson);
				else withError?.Invoke($"Error: {webRequest.responseCode} {webRequest.error} {resultJson}");

				webRequest.Dispose();
			}
		}
	}
}