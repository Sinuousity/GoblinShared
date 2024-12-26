using System;

namespace GoblinShared
{
	public static partial class AWSUtils
	{
		public class DBReqScanOptions : IDBReqOptions
		{
			public string tableName;
		}

		[Serializable]
		public class DBReqScanPayload : PayloadBase
		{
			public string tableName;
			public DBReqScanPayload(string tableName) => this.tableName = tableName;
		}

		public class DBReqScan : DBRequest<DBReqScanOptions, DBReqScanPayload>
		{
			public override string str_operation => "Scan";
			public override string str_amzTarget => str_amztarget_Scan;
			public override string GetOptionsString(DBReqScanOptions options) => options.tableName;
			public override DBReqScanPayload GetPayLoad(DBReqScanOptions options) => new DBReqScanPayload(options.tableName);
			public override string BuildRequestPayload(DBReqScanPayload payload) =>
				BuildRequestPayloadBase(payload, () => sb_payload.Append("\"TableName\":\"").Append(payload.tableName).Append("\","));
		}
	}
}
