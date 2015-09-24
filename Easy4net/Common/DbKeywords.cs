using Easy4net.DBUtility;
using System;
using System.Collections.Generic;

namespace Easy4net.Common
{
    public class DbKeywords
    {
        private static Dictionary<string, string> m_MySQL = new Dictionary<string, string>();
        private static Dictionary<string, string> m_MSSQL = new Dictionary<string, string>();

        private static void InitMySQL()
        {
            if (m_MySQL.Count == 0)
            {
                m_MySQL.Add("order", "`order`");
                m_MySQL.Add("desc", "`desc`");
                m_MySQL.Add("key", "`key`");
            }
        }

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
