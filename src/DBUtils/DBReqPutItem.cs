using System;
using System.Linq;

namespace GoblinShared
{
	public static partial class AWSUtils
	{
		public class DBReqPutItemOptions : IDBReqOptions
		{
			public string tableName;
			public string attributeValues;

			public DBReqPutItemOptions(string tableName, string attributeValues)
			{
				this.tableName = tableName;
				this.attributeValues = attributeValues;
			}

			public DBReqPutItemOptions(string tableName, params string[] attributeValues)
			{
				this.tableName = tableName;
				this.attributeValues = string.Join(",", attributeValues);
			}

			public DBReqPutItemOptions(string tableName, params (string, string)[] attributeValues)
			{
				this.tableName = tableName;
				this.attributeValues = string.Join(",", attributeValues.Select(x => $"\"{x.Item1}\":{{\"S\":\"{x.Item2}\"}}"));
			}
		}

		[Serializable]
		public class DBReqPutItemPayload : PayloadBase
		{
			public string tableName;
			public string attributeValues;

			public DBReqPutItemPayload(string tableName, string attributeValues)
			{
				this.tableName = tableName;
				this.attributeValues = attributeValues;
			}
		}

		public class DBReqPutItem : DBRequest<DBReqPutItemOptions, DBReqPutItemPayload>
		{
			public override string str_operation => "PutItem";
			public override string str_amzTarget => str_amztarget_PutItem;
			public override string GetOptionsString(DBReqPutItemOptions options) => options.tableName + ", " + options.attributeValues;
			public override DBReqPutItemPayload GetPayLoad(DBReqPutItemOptions options) => new DBReqPutItemPayload(options.tableName, options.attributeValues);
			public override string BuildRequestPayload(DBReqPutItemPayload payload)
			{
				return BuildRequestPayloadBase(
					payload,
					() =>
					{
						sb_payload.Append("\"TableName\":\"").Append(payload.tableName).Append("\",");
						sb_payload.Append("\"Item\":{").Append(payload.attributeValues).Append("},");
					}
				);
			}
		}
	}
}
