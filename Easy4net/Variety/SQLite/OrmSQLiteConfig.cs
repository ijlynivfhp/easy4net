using System;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using Easy4net.DBUtility;

namespace Easy4net.Variety.SQLite
{
	/// <summary>
	/// SQLite数据库配置类
	/// </summary>
	public class OrmSQLiteConfig : OrmConfig
	{
		/// <summary>
		/// SQLite关键字集合
		/// </summary>
		private readonly static string[] _keySQLite = { "order", "desc", "key" };

		/// <summary>
		/// 获取当前支持的数据库类型
		/// </summary>
		public override DatabaseType DbType
		{
			get
			{
				return DatabaseType.SQLITE;
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
		public OrmSQLiteConfig(string aConn)
		{
			this.ConnectionString = aConn;
		}
		
		/// <summary>
		/// 根据SQLite数据库参数获取一个MSSQL的数据库参数对象
		/// </summary>
		/// <param name="aPrm"></param>
		public OrmSQLiteConfig(SQLiteConnectionParam aPrm)
		{
			this.ConnectionString = SQLiteConnectionParam.GetConnectionString(aPrm);
		}

		/// <summary>
		/// 根据当前的全局配置获取一个SQLite的数据库参数对象
		/// </summary>
		public OrmSQLiteConfig()
		{
			this.ConnectionString = SQLiteConnectionParam.ConnectionString;
		}


		/// <summary>
		/// 创建一个数据库链接对象
		/// </summary>
		/// <returns></returns>
		internal override IDbConnection CreateDbConnection()
		{
			return new SQLiteConnection(ConnectionString);
		}

		/// <summary>
		/// 创建一个数据库命令对象
		/// </summary>
		/// <returns></returns>
		internal override IDbCommand CreateDbCommand()
		{
			return new SQLiteCommand();
		}

		/// <summary>
		/// 创建数据库适配器对象
		/// </summary>
		/// <returns></returns>
		internal override IDbDataAdapter CreateDataAdapter()
		{
			return new SQLiteDataAdapter();
		}

		/// <summary>
		/// 根据传入的命令对象创建适配器对象
		/// </summary>
		/// <param name="aCmd"></param>
		/// <returns></returns>
		internal override IDbDataAdapter CreateDataAdapter(IDbCommand aCmd)
		{
			return new SQLiteDataAdapter((SQLiteCommand)aCmd);
		}

		/// <summary>
		/// 创建数据库参数对象
		/// </summary>
		/// <returns></returns>
		public override IDbDataParameter CreateDbParameter()
		{
			return new SQLiteParameter();
		}

		/// <summary>
		/// 根据参数名与值创建数据库参数对象
		/// </summary>
		/// <param name="aPrmName"></param>
		/// <param name="aValue"></param>
		/// <returns></returns>
		public override IDbDataParameter CreateDbParameter(string aPrmName, object aValue)
		{
			IDbDataParameter param = CreateDbParameter();
			param.ParameterName = "@" + aPrmName;
			param.Value = aValue;

			return param;
		}

		/// <summary>
		/// 检查SQLite列名是否为数据库的关键字，是则进行格式化，否则返回原列名
		/// </summary>
		/// <param name="aColounName"></param>
		/// <returns></returns>
		internal override string FormatColumnName(string aColounName)
		{
			string tmp_str = aColounName.ToLower();
			if (_keySQLite.Contains(tmp_str))
			{
				return string.Format("`{0}`", tmp_str);
			}

			return aColounName;
		}

		/// <summary>
		/// 生成获取自增长列的新添加值的SQL语句
		/// </summary>
		/// <returns></returns>
		internal override string GetAutoSql()
		{
			return " ;select last_insert_rowid() as AutoId ";
		}
	}
}
