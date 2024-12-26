using System;

namespace GoblinShared
{
	public static partial class AWSUtils
	{
		public class DBReqGetItemOptions : IDBReqOptions
		{
			public string tableName;
			public string itemName;
		}

		[Serializable]
		public class DBReqGetItemPayload : PayloadBase
		{
			public string tableName;
			public string keyFilter;
			public string attributeFilter;

			public DBReqGetItemPayload(string tableName, string keyFilter, string attributeFilter = "")
			{
				this.tableName = tableName;
				this.keyFilter = keyFilter;
				this.attributeFilter = attributeFilter;
			}
		}

		public class DBReqGetItem : DBRequest<DBReqGetItemOptions, DBReqGetItemPayload>
		{
			public override string str_amzTarget => str_amztarget_GetItem;
			public override string str_operation => "GetItem";
			public override string GetOptionsString(DBReqGetItemOptions options) => options.tableName + ", " + options.itemName;
			public override DBReqGetItemPayload GetPayLoad(DBReqGetItemOptions options)
			{
				var itemFilter = new KeyFilter(new KeyFilter.KeyAttribute("platform_user_id", options.itemName));
				return new DBReqGetItemPayload(options.tableName, itemFilter.ToString());
			}
			public override string BuildRequestPayload(DBReqGetItemPayload payload)
			{
				return BuildRequestPayloadBase(
					payload,
					() =>
					{
						sb_payload.Append("\"TableName\":\"").Append(payload.tableName).Append("\",");
						sb_payload.Append("\"Key\":").Append(payload.keyFilter).Append(",");

						if (!string.IsNullOrEmpty(payload.attributeFilter))
							sb_payload.Append("\"ProjectionExpression\":\"").Append(payload.attributeFilter).Append("\",");
					}
				);
			}
		}
	}
}
