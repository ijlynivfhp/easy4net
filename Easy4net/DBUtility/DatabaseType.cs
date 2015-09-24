using System;
using System.ComponentModel;

namespace Easy4net.DBUtility
{
    /// <summary>
    /// 数据库类型枚举，需要扩展类型可在此添加
    /// </summary>
    public enum DatabaseType
    {
		/// <summary>
		/// MSSQL数据库
		/// </summary>
		[Description("MSSQL数据库")]
		SQLSERVER,
		/// <summary>
		/// ORACLE数据库
		/// </summary>
		[Description("ORACLE数据库")]
		ORACLE,
		/// <summary>
		/// ACCESS数据库
		/// </summary>
		[Description("ACCESS数据库")]
		ACCESS,
		/// <summary>
		/// MYSQL数据库
		/// </summary>
		[Description("MYSQL数据库")]
		MYSQL,
		/// <summary>
		/// SQLITE数据库
		/// </summary>
		[Description("SQLITE数据库")]
		SQLITE
    }
}
