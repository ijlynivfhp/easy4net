using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Easy4net.DBUtility;

namespace Easy4net.Variety.MSSQL
{
	/// <summary>
	/// MSSQL数据库配置类
	/// </summary>
	public class OrmMSSQLConfig : OrmConfig
	{
		/// <summary>
		/// MSSQL关键字集合
		/// </summary>
		private readonly static string[] _keyMSSQL = { "order", "desc", "key", "text", "limit", "offset", "password" };

		/// <summary>
		/// 获取当前支持的数据库类型
		/// </summary>
		public override DatabaseType DbType
		{
			get
			{
				return DatabaseType.SQLSERVER;
			}
		}
		/// <summary>
		/// 获取对应的数据库参数字符前缀
		/// </summary>
		public override string DbParamChar
		{
			get
			{
				return "@";
			}
		}

		/// <summary>
		/// 创建一个数据库参数对象
		/// </summary>
		/// <param name="aConn"></param>
		public OrmMSSQLConfig(string aConn)
		{
			this.ConnectionString = aConn;
		}

		/// <summary>
		/// 根据MSSQL数据库参数获取一个MSSQL的数据库参数对象
		/// </summary>
		/// <param name="aPrm"></param>
		public OrmMSSQLConfig(SqlConnectionParam aPrm)
		{
			this.ConnectionString = SqlConnectionParam.GetConnectionString(aPrm);
		}

		/// <summary>
		/// 根据当前的全局配置获取一个MSSQL的数据库参数对象
		/// </summary>
		public OrmMSSQLConfig()
		{
			this.ConnectionString = SqlConnectionParam.ConnectionString;
		}

		/// <summary>
		/// 创建一个数据库链接对象
		/// </summary>
		/// <returns></returns>
		internal override IDbConnection CreateDbConnection()
		{
			return new SqlConnection(ConnectionString);
		}

		/// <summary>
		/// 创建一个数据库命令对象
		/// </summary>
		/// <returns></returns>
		internal override IDbCommand CreateDbCommand()
		{
			return new SqlCommand();
		}

		/// <summary>
		/// 创建数据库适配器对象
		/// </summary>
		/// <returns></returns>
		internal override IDbDataAdapter CreateDataAdapter()
		{
			return new SqlDataAdapter();
		}

		/// <summary>
		/// 根据传入的命令对象创建适配器对象
		/// </summary>
		/// <param name="aCmd"></param>
		/// <returns></returns>
		internal override IDbDataAdapter CreateDataAdapter(IDbCommand aCmd)
		{
			return new SqlDataAdapter((SqlCommand)aCmd);
		}

		/// <summary>
		/// 创建数据库参数对象
		/// </summary>
		/// <returns></returns>
		public override IDbDataParameter CreateDbParameter()
		{
			return new SqlParameter();
		}

		/// <summary>
		/// 检查MSSQL列名是否为数据库的关键字，是则进行格式化，否则返回原列名
		/// </summary>
		/// <param name="aColounName"></param>
		/// <returns></returns>
		internal override string FormatColumnName(string aColounName)
		{
			string tmp_str = aColounName.ToLower();
			if (_keyMSSQL.Contains(tmp_str))
			{
				return string.Format("[{0}]", tmp_str);
			}

			return aColounName;
		}

		/// <summary>
		/// 生成获取自增长列的新添加值的SQL语句
		/// </summary>
		/// <returns></returns>
		internal override string GetAutoSql()
		{
			return " SELECT scope_identity() as AutoId ";
		}
	}
}
