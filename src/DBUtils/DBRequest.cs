using System.Threading.Tasks;
using System.Collections;
using System.Text;
using System;

namespace GoblinShared
{
	public static partial class AWSUtils
	{
		private static StringBuilder sb_payload = new StringBuilder(256);

		public interface IDBRequest { }
		public interface IDBReqPayload { }
		public interface IDBReqOptions { }

		[Serializable]
		public abstract class PayloadBase : IDBReqPayload
		{
			public ConsumedPayloadCapacity consumedCapacity = ConsumedPayloadCapacity.NONE;
			public bool consistentReads = false;
		}

		public abstract class DBRequest<T, W> : IDBRequest where T : IDBReqOptions where W : IDBReqPayload
		{
			public abstract W GetPayLoad(T options);
			public bool inProgress { get; private set; }
			public abstract string str_operation { get; }
			public abstract string str_amzTarget { get; }
			public abstract string GetOptionsString(T options);

			public IEnumerator SendRequest(T options, Action<string> withResult, Action<string> withError)
			{
				inProgress = true;
				var host = $"https://{str_service_dynamo}.{Configuration.region}.amazonaws.com";
				var payload_obj = GetPayLoad(options);
				var str_options = GetOptionsString(options);
				requestBody = $"{host}\n{str_operation}( {str_options} )";
				yield return AWSHttpHelpers.Post(
					str_service_dynamo,
					host,
					BuildRequestPayload(payload_obj),
					str_amzTarget,
					withResult,
					withError
				);
			}

			protected string BuildRequestPayloadBase(PayloadBase payload, Action operation)
			{
				sb_payload.Clear();
				sb_payload.Append('{');

				operation?.Invoke();

				if (payload.consistentReads) sb_payload.Append("\"ConsistentRead\":true,");

				if (payload.consumedCapacity != ConsumedPayloadCapacity.NONE)
					sb_payload.Append("\"ReturnConsumedCapacity\":\"").Append(payload.consumedCapacity).Append("\",");

				sb_payload.Append('}');
				var payloadStr = sb_payload.ToString();
				payloadStr = payloadStr.Replace(",}", "}");
				payloadStr = payloadStr.Replace(",,", ",");
				payloadStr = payloadStr.Trim();
				return payloadStr;
			}

			public abstract string BuildRequestPayload(W payload);
		}
	}
}
