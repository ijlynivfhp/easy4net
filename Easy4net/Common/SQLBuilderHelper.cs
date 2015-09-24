using System;
using Easy4net.DBUtility;
using System.Data;
using Easy4net.Context;

namespace Easy4net.Common
{
	/// <summary>
	/// SQL语句创建帮助类
	/// </summary>
    public static class SQLBuilderHelper
    {
		/// <summary>
		/// MSSQL分页语句模版
		/// </summary>
        private static string mssqlPageTemplate = "select * from (select ROW_NUMBER() OVER(order by {0}) AS RowNumber, {1}) as tmp_tbl where RowNumber BETWEEN @pageStart and @pageEnd ";
		/// <summary>
		/// MySQL排序分页语句模版
		/// </summary>
        private static string mysqlOrderPageTemplate = "{0} order by {1} limit ?offset,?limit";
		/// <summary>
		/// MySQL分页语句模板
		/// </summary>
        private static string mysqlPageTemplate = "{0} limit ?offset,?limit";
		/// <summary>
		/// SQLite排序分页语句模板
		/// </summary>
        private static string sqliteOrderPageTemplate = "{0} order by {1} limit @offset,@limit";
		/// <summary>
		/// SQLite分页语句模板
		/// </summary>
        private static string sqlitePageTemplate = "{0} limit @offset,@limit";
		/// <summary>
		/// ACCESS分页语句模板
		/// </summary>
        private static string accessPageTemplate = "select * from (select top @page_limit * from (select top @page_offset {0} order by id desc) order by id) order by {1}";

		/// <summary>
		/// 从SQL语句中获取查询的列名集合
		/// Select 与 From中间的部分
		/// </summary>
		/// <param name="strSQL"></param>
		/// <returns></returns>
        public static string fetchColumns(string strSQL)
        {
            string lowerSQL = strSQL.ToLower();
            String columns = lowerSQL.Substring(6, lowerSQL.IndexOf("from") - 6);
            return columns;
        }

		/// <summary>
		/// 从SQL语句中获取分页语句
		/// </summary>
		/// <param name="strSQL"></param>
		/// <returns></returns>
        public static string fetchPageBody(string strSQL)
        {
            string body = strSQL.Substring(6, strSQL.Length - 6);
            return body;
        }

		/// <summary>
		/// 从SQL语句中获取查询语句
		/// </summary>
		/// <param name="strSQL"></param>
		/// <returns></returns>
        public static string fetchWhere(string strSQL)
        {
            int index = strSQL.LastIndexOf("where");
            if (index == -1) return "";

            String where = strSQL.Substring(index, strSQL.Length - index);
            return where;
        }

		/// <summary>
		/// 根据SQL语句判断是否为分页语句
		/// </summary>
		/// <param name="strSQL"></param>
		/// <returns></returns>
        public static bool isPage(string strSQL)
        { 
            string strSql = strSQL.ToLower();
            Session session = SessionThreadLocal.Get();

            if (session.DbFactory.DbType == DatabaseType.ACCESS && strSql.IndexOf("top") == -1)
            {
                return false;
            }

            if (session.DbFactory.DbType == DatabaseType.SQLSERVER && strSql.IndexOf("row_number()") == -1)
            {
                return false;
            }

            if (session.DbFactory.DbType == DatabaseType.MYSQL && strSql.IndexOf("limit") == -1)
            {
                return false;
            }

            if (session.DbFactory.DbType == DatabaseType.SQLITE && strSql.IndexOf("limit") == -1)
            {
                return false;
            }

            if (session.DbFactory.DbType == DatabaseType.ORACLE && strSql.IndexOf("rowid") == -1)
            {
                return false;
            }

            return true;
        }

		/// <summary>
		/// 根据SQL语句及排序条件创建分页语句
		/// </summary>
		/// <param name="strSql">SQL语句</param>
		/// <param name="order">排序字段</param>
		/// <param name="desc">是否为递减排序</param>
		/// <returns></returns>
        public static string builderPageSQL(string strSql, string order, bool desc)
        {
            Session session = SessionThreadLocal.Get();

            string columns = fetchColumns(strSql);
            string orderBy = order + (desc ? " desc " : " asc ");


            if (session.DbFactory.DbType == DatabaseType.SQLSERVER && strSql.IndexOf("row_number()") == -1)
            {
                if (string.IsNullOrEmpty(order))
                {
                    throw new Exception(" SqlException: order field is null, you must support the order field for sqlserver page. ");
                }

                string pageBody = fetchPageBody(strSql);
                strSql = string.Format(mssqlPageTemplate, orderBy, pageBody);
            }

            if (session.DbFactory.DbType == DatabaseType.ACCESS && strSql.IndexOf("top") == -1)
            {
                if (string.IsNullOrEmpty(order))
                {
                    throw new Exception(" SqlException: order field is null, you must support the order field for sqlserver page. ");
                }

                //select {0} from (select top @pageSize {1} from (select top @pageSize*@pageIndex {2} from {3} order by {4}) order by id) order by {5}
                string pageBody = fetchPageBody(strSql);
                strSql = string.Format(accessPageTemplate, pageBody, orderBy);
            }

            if (session.DbFactory.DbType == DatabaseType.MYSQL)
            {
                if (!string.IsNullOrEmpty(order))
                {
                    strSql = string.Format(mysqlOrderPageTemplate, strSql, orderBy);
                }
                else
                {
                    strSql = string.Format(mysqlPageTemplate, strSql);
                }
            }

            if (session.DbFactory.DbType == DatabaseType.SQLITE)
            {
                if (!string.IsNullOrEmpty(order))
                {
                    strSql = string.Format(sqliteOrderPageTemplate, strSql, orderBy);
                }
                else
                {
                    strSql = string.Format(sqlitePageTemplate, strSql);
                }
            }
            
            return strSql;
        }

		/// <summary>
		/// 根据SQL语句创建一个获取记录数的SQL语句
		/// </summary>
		/// <param name="strSQL"></param>
		/// <returns></returns>
        public static string builderCountSQL(string strSQL)
        {
            int index = strSQL.IndexOf("from");
            string strFooter = strSQL.Substring(index, strSQL.Length - index);
            string strText = "select count(*) " + strFooter;

            return strText;
        }

		/// <summary>
		/// 创建ACCESS数据库的分页SQL语句
		/// </summary>
		/// <param name="strSql"></param>
		/// <param name="param">参数集合</param>
		/// <returns></returns>
        public static string builderAccessPageSQL(string strSql, ParamMap param)
        {
            return builderAccessPageSQL(strSql, param, -1);
        }

		/// <summary>
		/// 创建ACCESS的分页SQL语句
		/// </summary>
		/// <param name="strSql"></param>
		/// <param name="param">参数集合</param>
		/// <param name="limit">每页记录数</param>
		/// <returns></returns>
        public static string builderAccessPageSQL(string strSql, ParamMap param, int limit)
        {
            Session session = SessionThreadLocal.Get();

            if (session.DbFactory.DbType != DatabaseType.ACCESS)
            {
                return strSql;
            }

            if (param.ContainsKey("page_limit"))
            {
                if (limit != -1)
                {
                    strSql = strSql.Replace("@" + "page_limit", limit.ToString());
                }
                else
                {
                    strSql = strSql.Replace("@" + "page_limit", param.getString("page_limit"));
                }
                param.Remove("page_limit");
            }

            if (param.ContainsKey("page_offset"))
            {
                strSql = strSql.Replace("@" + "page_offset", param.getString("page_offset"));
                param.Remove("page_offset");
            }

            return strSql;
        }

		/// <summary>
		/// 创建ACCESS的SQL语句
		/// </summary>
		/// <param name="classType">数据库表实体对象类型</param>
		/// <param name="tableInfo">表信息</param>
		/// <param name="strSql">SQL语句</param>
		/// <param name="parameters">参数集合</param>
		/// <returns></returns>
        public static string builderAccessSQL(Type classType, TableInfo tableInfo, string strSql, IDbDataParameter[] parameters)
        {
            Session session = SessionThreadLocal.Get();

            if (session.DbFactory.DbType != DatabaseType.ACCESS)
            {
                return strSql;
            }

            foreach (IDbDataParameter param in parameters)
            {
                if (param.Value == null) continue;

                string paramName = param.ParameterName;
                string paramValue = param.Value.ToString();

                float i = 0;
                if (tableInfo.ColumnToProp.ContainsKey(paramName))
                {
                    string propertyName = tableInfo.ColumnToProp[paramName].ToString();
                    Type type = ReflectionHelper.GetPropertyType(classType, propertyName);

                    string typeName = TypeUtils.GetTypeName(type);
                    if (typeName == "System.String" || typeName == "System.DateTime")
                    {
                        paramValue = "'" + paramValue + "'";
                    }
                }
                else if (!float.TryParse(paramValue, out i)) {
                    paramValue = "'" + paramValue + "'";
                }

                //paramName = paramName.ToLower();
                strSql = strSql.Replace("@"+paramName, paramValue);
            }

            return strSql;
        }

		/// <summary>
		/// 创建ACCESS SQL语句
		/// </summary>
		/// <param name="strSql"></param>
		/// <param name="parameters">参数集合</param>
		/// <returns></returns>
        public static string builderAccessSQL(string strSql, IDbDataParameter[] parameters)
        {
            Session session = SessionThreadLocal.Get();

            if (session.DbFactory.DbType != DatabaseType.ACCESS)
            {
                return strSql;
            }

            foreach (IDbDataParameter param in parameters)
            {
                if (param.Value == null) continue;

                string paramName = param.ParameterName;
                string paramValue = param.Value.ToString();

                paramValue = "'" + paramValue + "'";
                strSql = strSql.Replace("@" + paramName, paramValue);
            }

            return strSql;
        }
    }
}
