using Easy4net.DBUtility;
using System;
using System.Collections.Generic;

namespace Easy4net.Common
{
	/// <summary>
	/// 数据库关键字检查帮助类
	/// </summary>
    public static class DbKeywords
    {
		/// <summary>
		/// MySQL中关键字集合
		/// </summary>
        private static Dictionary<string, string> m_MySQL = new Dictionary<string, string>();
		/// <summary>
		/// MSSQL中关键字集合
		/// </summary>
        private static Dictionary<string, string> m_MSSQL = new Dictionary<string, string>();

		/// <summary>
		/// 初始化MySQL中关键字集合
		/// </summary>
        private static void InitMySQL()
        {
            if (m_MySQL.Count == 0)
            {
                m_MySQL.Add("order", "`order`");
                m_MySQL.Add("desc", "`desc`");
                m_MySQL.Add("key", "`key`");
            }
        }

		/// <summary>
		/// 初始化MSSQL中关键字集合
		/// </summary>
        private static void InitMSSQL()
        {
            if (m_MSSQL.Count == 0)
            {
                m_MSSQL.Add("order", "[order]");
                m_MSSQL.Add("desc", "[desc]");
                m_MSSQL.Add("key", "[key]");
                m_MSSQL.Add("text", "[text]");
                m_MSSQL.Add("limit", "[limit]");
                m_MSSQL.Add("offset", "[offset]");
                m_MSSQL.Add("password", "[password]");
            }
        }

		/// <summary>
		/// 格式化列名,对列名与数据库关键字相同的进行格式化处理
		/// </summary>
		/// <param name="columnName"></param>
		/// <param name="dbType"></param>
		/// <returns></returns>
        public static string FormatColumnName(string columnName, DatabaseType dbType)
        {
            InitMySQL();
            InitMSSQL();

            string colName = columnName.ToLower();
            if ((dbType == DatabaseType.SQLITE || dbType == DatabaseType.MYSQL) && m_MySQL.ContainsKey(colName))
            {
                return m_MySQL[colName];
            }

            if (dbType == DatabaseType.SQLSERVER && m_MSSQL.ContainsKey(colName))
            {
                return m_MSSQL[colName];
            }

            if (dbType == DatabaseType.ACCESS && m_MSSQL.ContainsKey(colName))
            {
                return m_MSSQL[colName];
            }

            return columnName;
        }

    }
}
