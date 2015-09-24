using System;
using System.Linq;
using System.Text;

namespace Easy4net.Variety.SQLite
{
	/// <summary>
	/// SQLite数据库连接参数
	/// </summary>
	public class SQLiteConnectionParam
	{
		/// <summary>
		/// 数据库连接字符串
		/// </summary>
		internal static string ConnectionString { get; private set; }

		/// <summary>
		/// SQLite数据库路径
		/// </summary>
		public string FileName { get; private set; }
		/// <summary>
		/// SQLite数据库密码
		/// </summary>
		public string Password { get; private set; }

		/// <summary>
		/// 创建一个无密码的SQLite数据库参数
		/// </summary>
		/// <param name="aFile">数据库文件路径</param>
		public SQLiteConnectionParam(string aFile)
		{
			this.FileName = aFile;
		}

		/// <summary>
		/// 创建一个带密码的SQLite数据库参数
		/// </summary>
		/// <param name="aFile"></param>
		/// <param name="aPwd"></param>
		public SQLiteConnectionParam(string aFile, string aPwd)
			: this(aFile)
		{
			this.Password = aPwd;
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
		internal static string GetConnectionString(SQLiteConnectionParam aParam)
		{
			StringBuilder tmp_sb = new StringBuilder();
			tmp_sb.AppendFormat("Data Source='{0}';", aParam.FileName);

			if (!String.IsNullOrEmpty(aParam.Password))
			{
				tmp_sb.AppendFormat("password={0}", aParam.Password);
			}

			return tmp_sb.ToString();
		}
	}
}
