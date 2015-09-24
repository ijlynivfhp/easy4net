using System;
using System.Linq;
using System.Text;

namespace Easy4net.Variety.MSSQL
{
	/// <summary>
	/// SQL数据库连接参数配置类
	/// </summary>
	public class SqlConnectionParam
	{
		/// <summary>
		/// 数据库连接字符串
		/// </summary>
		internal static string ConnectionString { get; private set; }

		/// <summary>
		/// 数据库主机地址
		/// </summary>
		public string DbHost { get; private set; }
		/// <summary>
		/// 数据库端口
		/// </summary>
		public string DbPort { get; private set; }
		/// <summary>
		/// 数据库名
		/// </summary>
		public string DbName { get; private set; }
		/// <summary>
		/// 数据库用户名
		/// </summary>
		public string DbUser { get; private set; }
		/// <summary>
		/// 数据库密码
		/// </summary>
		public string DbPassword { get; private set; }

		/// <summary>
		/// 最小缓存池大小，空则未配置
		/// </summary>
		public string DbMinPoolSize { get; private set; }
		/// <summary>
		/// 最大缓存池大小，空则未配置
		/// </summary>
		public string DbMaxPoolSize { get; private set; }

		/// <summary>
		/// 字符集设置，空则未配置
		/// </summary>
		public string DbCharset { get; private set; }

		/// <summary>
		/// 新建一个MSSQL链接参数对象
		/// </summary>
		/// <param name="aHost">数据库主机地址</param>
		/// <param name="aPort">端口号</param>
		/// <param name="aName">数据库名</param>
		/// <param name="aUser">用户名</param>
		/// <param name="aPassword">密码</param>
		public SqlConnectionParam(string aHost, string aPort, string aName,
									string aUser, string aPassword)
		{
			this.DbHost = aHost;
			this.DbPort = aPort;
			this.DbName = aName;
			this.DbUser = aUser;
			this.DbPassword = aPassword;
		}

		/// <summary>
		/// 新建一个MSSQL链接参数对象
		/// </summary>
		/// <param name="aHost">数据库主机地址</param>
		/// <param name="aPort">端口号</param>
		/// <param name="aName">数据库名</param>
		/// <param name="aUser">用户名</param>
		/// <param name="aPassword">密码</param>
		/// <param name="aCharset">字符集</param>
		public SqlConnectionParam(string aHost, string aPort, string aName,
								string aUser, string aPassword, string aCharset)
			: this(aHost, aPort, aName, aUser, aPassword)
		{
			this.DbCharset = aCharset;
		}

		/// <summary>
		/// 新建一个MSSQL链接参数对象
		/// </summary>
		/// <param name="aHost">数据库主机地址</param>
		/// <param name="aPort">端口号</param>
		/// <param name="aName">数据库名</param>
		/// <param name="aUser">用户名</param>
		/// <param name="aPassword">密码</param>
		/// <param name="aMinPool">最小缓存池大小</param>
		/// <param name="aMaxPool">最大缓存池大小</param>
		public SqlConnectionParam(string aHost, string aPort, string aName,
								string aUser, string aPassword,
								string aMinPool, string aMaxPool)
			: this(aHost, aPort, aName, aUser, aPassword)
		{
			this.DbMinPoolSize = aMinPool;
			this.DbMaxPoolSize = aMaxPool;
		}

		/// <summary>
		/// 新建一个MSSQL链接参数对象
		/// </summary>
		/// <param name="aHost">数据库主机地址</param>
		/// <param name="aPort">端口号</param>
		/// <param name="aName">数据库名</param>
		/// <param name="aUser">用户名</param>
		/// <param name="aPassword">密码</param>
		/// <param name="aMinPool">最小缓存池大小</param>
		/// <param name="aMaxPool">最大缓存池大小</param>
		/// <param name="aCharset">字符集</param>
		public SqlConnectionParam(string aHost, string aPort, string aName,
								string aUser, string aPassword,
								string aMinPool, string aMaxPool, string aCharset)
			: this(aHost, aPort, aName, aUser, aPassword, aMinPool, aMaxPool)
		{
			this.DbCharset = aCharset;
		}

		/// <summary>
		/// 将当前参数设置为全局连接参数
		/// </summary>
		public void SetGlobal()
		{
			// 更新连接字符串
			ConnectionString = GetConnectionString(this);
		}

		/// <summary>
		/// 输出当前连接参数字符串
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return GetConnectionString(this);
		}

		/// <summary>
		/// 根据当前的变量生成链接字符串
		/// </summary>
		/// <param name="aParam">数据库连接参数</param>
		/// <returns>连接字符串</returns>
		internal static string GetConnectionString(SqlConnectionParam aParam)
		{
			StringBuilder tmp_sb = new StringBuilder();
			tmp_sb.Append("Data Source=").Append(aParam.DbHost);

			if (!String.IsNullOrEmpty(aParam.DbPort))
			{
				tmp_sb.Append(",").Append(aParam.DbPort);
			}

			tmp_sb.Append(";");
			tmp_sb.AppendFormat("User ID={0};", aParam.DbUser);
			// 解密密码
			tmp_sb.AppendFormat("Password={0};", aParam.DbPassword);
			tmp_sb.AppendFormat("DataBase={0};", aParam.DbName);

			if (!String.IsNullOrEmpty(aParam.DbMinPoolSize))
			{
				tmp_sb.AppendFormat("Min Pool Size={0};", aParam.DbMinPoolSize);
			}

			if (!String.IsNullOrEmpty(aParam.DbMaxPoolSize))
			{
				tmp_sb.AppendFormat("Max Pool Size={0};", aParam.DbMaxPoolSize);
			}

			if (!String.IsNullOrEmpty(aParam.DbCharset))
			{
				tmp_sb.AppendFormat("charset={0};", aParam.DbCharset);
			}

			return tmp_sb.ToString();
		}
	}
}
