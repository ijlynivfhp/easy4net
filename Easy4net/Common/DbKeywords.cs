using Easy4net.DBUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            }
        }

        public static string FormatColumnName(string colounName)
        {
            InitMySQL();
            InitMSSQL();

            if(AdoHelper.DbType == DatabaseType.MYSQL && m_MySQL.ContainsKey(colounName))
            {
                return m_MySQL[colounName];
            }

            if (AdoHelper.DbType == DatabaseType.SQLSERVER && m_MSSQL.ContainsKey(colounName))
            {
                return m_MSSQL[colounName];
            }

            return colounName;
        }

    }
}
