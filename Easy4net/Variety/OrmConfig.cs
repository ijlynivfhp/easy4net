using System;
using System.Data;
using System.Linq;
using Easy4net.DBUtility;

namespace Easy4net.Variety
{
	/// <summary>
	/// ORM配置基类
	/// </summary>
	public abstract class OrmConfig
	{
		/// <summary>
		/// 获取当前支持的数据库类型
		/// </summary>
		public abstract DatabaseType DbType { get; }
		/// <summary>
		/// 获取对应的数据库参数字符前缀
		/// </summary>
		public abstract string DbParamChar { get; }
		/// <summary>
		/// 获取数据库连接字符串
		/// </summary>
		public string ConnectionString { get; protected set; }

		/// <summary>
		/// 创建一个数据库链接对象
		/// </summary>
		/// <returns></returns>
		internal abstract IDbConnection CreateDbConnection();
		/// <summary>
		/// 创建一个数据库命令对象
		/// </summary>
		/// <returns></returns>
		internal abstract IDbCommand CreateDbCommand();
		/// <summary>
		/// 创建数据库适配器对象
		/// </summary>
		/// <returns></returns>
		internal abstract IDbDataAdapter CreateDataAdapter();
		/// <summary>
		/// 根据传入的命令对象创建适配器对象
		/// </summary>
		/// <param name="aCmd"></param>
		/// <returns></returns>
		internal abstract IDbDataAdapter CreateDataAdapter(IDbCommand aCmd);
		/// <summary>
		/// 打开数据库连接并创建事物对象
		/// </summary>
		/// <returns></returns>
		internal virtual IDbTransaction CreateDbTransaction()
		{
			IDbConnection conn = CreateDbConnection();
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}

			return conn.BeginTransaction();
		}

		/// <summary>
		/// 打开数据库连接并创建指定连接行为的事物对象
		/// </summary>
		/// <param name="aLevel"></param>
		/// <returns></returns>
		internal virtual IDbTransaction CreateDbTransaction(IsolationLevel aLevel)
		{
			IDbConnection conn = CreateDbConnection();
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}

			return conn.BeginTransaction(aLevel);
		}

		/// <summary>
		/// 创建数据库参数对象
		/// </summary>
		/// <returns></returns>
		public abstract IDbDataParameter CreateDbParameter();

		/// <summary>
		/// 根据参数名与值创建数据库参数对象
		/// </summary>
		/// <param name="aPrmName"></param>
		/// <param name="aValue"></param>
		/// <returns></returns>
		public virtual IDbDataParameter CreateDbParameter(string aPrmName, object aValue)
		{
			IDbDataParameter param = CreateDbParameter();
			param.ParameterName = aPrmName;
			param.Value = aValue;

			return param;
		}

		/// <summary>
		/// 根据参数名与值及参数值类型创建一个数据库参数对象
		/// </summary>
		/// <param name="aPrmName"></param>
		/// <param name="aValue"></param>
		/// <param name="aDbType">参数值的数据类型</param>
		/// <returns></returns>
		public virtual IDbDataParameter CreateDbParameter(string aPrmName, object aValue, DbType aDbType)
		{
			IDbDataParameter param = CreateDbParameter(aPrmName, aValue);
			param.DbType = aDbType;

			return param;
		}

		/// <summary>
		/// 根据参数名与值及参数输入输出类型创建一个数据库参数对象
		/// </summary>
		/// <param name="aPrmName"></param>
		/// <param name="aValue"></param>
		/// <param name="aDirection"></param>
		/// <returns></returns>
		public virtual IDbDataParameter CreateDbParameter(string aPrmName, object aValue, ParameterDirection aDirection)
		{
			IDbDataParameter param = CreateDbParameter(aPrmName, aValue);
			param.Direction = aDirection;

			return param;
		}

		/// <summary>
		/// 根据参数名，值，参数大小，参数方向类型创建一个参数对象
		/// </summary>
		/// <param name="aPrmName"></param>
		/// <param name="aValue"></param>
		/// <param name="aDbType"></param>
		/// <param name="aSize"></param>
		/// <param name="aDirection"></param>
		/// <returns></returns>
		public virtual IDbDataParameter CreateDbParameter(string aPrmName, object aValue, DbType aDbType, int aSize, ParameterDirection aDirection)
		{
			IDbDataParameter param = CreateDbParameter(aPrmName, aValue, aDbType);
			param.Size = aSize;
			param.Direction = aDirection;

			return param;
		}

		/// <summary>
		/// 格式化字段名
		/// </summary>
		/// <param name="aColumnName"></param>
		/// <returns></returns>
		internal abstract string FormatColumnName(string aColumnName);
		/// <summary>
		/// 生成获取自增长列的新添加值的SQL语句
		/// </summary>
		/// <returns></returns>
		internal abstract string GetAutoSql();
	}
}
