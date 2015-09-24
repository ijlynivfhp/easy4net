using System;
using System.Text;
using Easy4net.DBUtility;
using Easy4net.Context;

namespace Easy4net.Common
{
	/// <summary>
	/// 数据库条件语句生成器
	/// </summary>
    public class DbCondition : Map
    {
        private static string WHERE = " WHERE ";
        private static string EQUAL = " {0} = {1} ";

        private static string AND_EQ = " AND {0} = {1} ";
        private static string OR_EQ = " OR {0} = {1} ";

        private static string GT = " {0} > {1} ";
        private static string GT_EQ = " {0} >= {1} ";

        private static string AND_GT = " AND {0} > {1} ";
        private static string AND_GT_EQ = " AND {0} >= {1} ";

        private static string OR_GT = " OR {0} > {1} ";
        private static string OR_GT_EQ = " OR {0} >= {1} ";

        private static string LT = " {0} < {1} ";
        private static string LT_EQ = " {0} <= {1} ";

        private static string AND_LT = " AND {0} < {1} ";
        private static string AND_LT_EQ = " AND {0} <= {1} ";
       
        private static string OR_LT = " OR {0} < {1} ";
        private static string OR_LT_EQ = " OR {0} <= {1} ";
        
        private static string ORDER_BY_ASC = " ORDER BY {0} ASC ";
        private static string ORDER_BY_DESC = " ORDER BY {0} DESC ";

        private static string paramChar = string.Empty;
        private StringBuilder sbSQL = new StringBuilder();
        public string queryString = String.Empty;
        public ColumnInfo Columns = new ColumnInfo();

		/// <summary>
		/// 根据配置文件创建一个默认的条件语句生成器
		/// </summary>
        public DbCondition()
        {
            DbFactory dbFactory = SessionThreadLocal.Get().DbFactory;
            paramChar = dbFactory.DbParmChar;
        }

		/// <summary>
		/// 根据配置文件创建一个默认的条件语句生成器
		/// </summary>
		/// <param name="query">查询语句</param>
        public DbCondition(string query)
        {
            DbFactory dbFactory = SessionThreadLocal.Get().DbFactory;
            paramChar = dbFactory.DbParmChar;

            this.queryString = query;
            sbSQL.Append(query);
        }

		/// <summary>
		/// 传入查询语句
		/// </summary>
		/// <param name="query">查询语句</param>
		/// <returns></returns>
        public DbCondition Query(string query)
        {
            this.queryString = query;
            sbSQL.Append(query);
            return this;
        }

		/// <summary>
		/// 增加Where关键字
		/// </summary>
		/// <returns></returns>
        public DbCondition Where()
        {
            sbSQL.Append(WHERE);
            return this;
        }

		/// <summary>
		/// 增加Where等于语句
		/// </summary>
		/// <param name="fieldName">字段名</param>
		/// <param name="fieldValue">字段值</param>
		/// <returns></returns>
        public DbCondition Where(string fieldName, object fieldValue)
        {
            string formatName = formatKey(fieldName);
            sbSQL.AppendFormat(WHERE + EQUAL, fieldName, paramChar + formatName);
            Columns[formatName] = fieldValue;

            return this;
        }

		/// <summary>
		/// 增加等于语句
		/// </summary>
		/// <param name="fieldName">字段名</param>
		/// <param name="fieldValue">字段值</param>
		/// <returns></returns>
        public DbCondition Equal(string fieldName, object fieldValue)
        {
            string formatName = formatKey(fieldName);
            sbSQL.AppendFormat(EQUAL, fieldName, paramChar + formatName);
            Columns[formatName] = fieldValue;

            return this;
        }

		/// <summary>
		/// 增加And等于语句
		/// </summary>
		/// <param name="fieldName">字段名</param>
		/// <param name="fieldValue">字段值</param>
		/// <returns></returns>
        public DbCondition AndEqual(string fieldName, object fieldValue)
        {
            string formatName = formatKey(fieldName);
            sbSQL.AppendFormat(AND_EQ, fieldName, paramChar + formatName);
            Columns[formatName] = fieldValue;

            return this;
        }

		/// <summary>
		/// 增加Or等于语句
		/// </summary>
		/// <param name="fieldName">字段名</param>
		/// <param name="fieldValue">字段值</param>
		/// <returns></returns>
        public DbCondition OrEqual(string fieldName, object fieldValue)
        {
            string formatName = formatKey(fieldName);
            sbSQL.AppendFormat(OR_EQ, fieldName, paramChar + formatName);
            Columns[formatName] = fieldValue;

            return this;
        }

		/// <summary>
		/// 增加大于语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition GreaterThan(string fieldName, object fieldValue)
        {
            string formatName = formatKey(fieldName);
            sbSQL.AppendFormat(GT, fieldName, paramChar + formatName);
            Columns[formatName] = fieldValue;

            return this;
        }

		/// <summary>
		/// 增加大于等于语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition GreaterThanEqual(string fieldName, object fieldValue)
        {
            string formatName = formatKey(fieldName);
            sbSQL.AppendFormat(GT_EQ, fieldName, paramChar + formatName);
            Columns[formatName] = fieldValue;

            return this;
        }

		/// <summary>
		/// 增加And大于语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition AndGreaterThan(string fieldName, object fieldValue)
        {
            string formatName = formatKey(fieldName);
            sbSQL.AppendFormat(AND_GT, fieldName, paramChar + formatName);
            Columns[formatName] = fieldValue;

            return this;
        }

		/// <summary>
		/// 增加And大于等于语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition AndGreaterThanEqual(string fieldName, object fieldValue)
        {
            string formatName = formatKey(fieldName);
            sbSQL.AppendFormat(AND_GT_EQ, fieldName, paramChar + formatName);
            Columns[formatName] = fieldValue;

            return this;
        }

		/// <summary>
		/// 增加Or大于语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition OrGreaterThan(string fieldName, object fieldValue)
        {
            string formatName = formatKey(fieldName);
            sbSQL.AppendFormat(OR_GT, fieldName, paramChar + formatName);
            Columns[formatName] = fieldValue;

            return this;
        }

		/// <summary>
		/// 增加Or大于等于语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition OrGreaterThanEqual(string fieldName, object fieldValue)
        {
            string formatName = formatKey(fieldName);
            sbSQL.AppendFormat(OR_GT_EQ, fieldName, paramChar + formatName);
            Columns[formatName] = fieldValue;

            return this;
        }

		/// <summary>
		/// 增加小于语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition LessThan(string fieldName, object fieldValue)
        {
            string formatName = formatKey(fieldName);
            sbSQL.AppendFormat(LT, fieldName, paramChar + formatName);
            Columns[formatName] = fieldValue;

            return this;
        }

		/// <summary>
		/// 增加小于等于语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition LessThanEqual(string fieldName, object fieldValue)
        {
            string formatName = formatKey(fieldName);
            sbSQL.AppendFormat(LT_EQ, fieldName, paramChar + formatName);
            Columns[formatName] = fieldValue;

            return this;
        }

		/// <summary>
		/// 增加And小于语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition AndLessThan(string fieldName, object fieldValue)
        {
            string formatName = formatKey(fieldName);
            sbSQL.AppendFormat(AND_LT, fieldName, paramChar + formatName);
            Columns[formatName] = fieldValue;

            return this;
        }

		/// <summary>
		/// 增加And小于等于语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition AndLessThanEqual(string fieldName, object fieldValue)
        {
            string formatName = formatKey(fieldName);
            sbSQL.AppendFormat(AND_LT_EQ, fieldName, paramChar + formatName);
            Columns[formatName] = fieldValue;

            return this;
        }

		/// <summary>
		/// 增加Or小于语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition OrLessThan(string fieldName, object fieldValue)
        {
            string formatName = formatKey(fieldName);
            sbSQL.AppendFormat(OR_LT, fieldName, paramChar + formatName);
            Columns[formatName] = fieldValue;

            return this;
        }

		/// <summary>
		/// 增加Or小于等于语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition OrLessThanEqual(string fieldName, object fieldValue)
        {
            string formatName = formatKey(fieldName);
            sbSQL.AppendFormat(OR_LT_EQ, fieldName, paramChar + formatName);
            Columns[formatName] = fieldValue;

            return this;
        }

		/// <summary>
		/// 增加And等于语句,相当于AndEqual方法
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition And(string fieldName, object fieldValue)
        {
            return this.AndEqual(fieldName, fieldValue);
        }

		/// <summary>
		/// 增加Or等于语句,相当于OrEqual方法
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition Or(string fieldName, object fieldValue)
        {
            return this.OrEqual(fieldName, fieldValue);
        }

		/// <summary>
		/// 增加Order By Asc递增排序语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <returns></returns>
        public DbCondition OrderByASC(string fieldName)
        {
            sbSQL.AppendFormat(ORDER_BY_ASC, fieldName);

            return this;
        }

		/// <summary>
		/// 增加Order By Desc递减排序语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <returns></returns>
        public DbCondition OrderByDESC(string fieldName)
        {
            sbSQL.AppendFormat(ORDER_BY_DESC, fieldName);

            return this;
        }

		/// <summary>
		/// 增加Like %XX%语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition Like(string fieldName, object fieldValue)
        {
            sbSQL.AppendFormat(" {0} LIKE '%{1}%' ", fieldName, fieldValue);
            return this;
        }

		/// <summary>
		/// 增加And Like %XX%语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition AndLike(string fieldName, object fieldValue)
        {
            sbSQL.AppendFormat(" AND {0} LIKE '%{1}%' ", fieldName, fieldValue);
            return this;
        }

		/// <summary>
		/// 增加Or Like %XX%语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition OrLike(string fieldName, object fieldValue)
        {
            sbSQL.AppendFormat(" OR {0} LIKE '%{1}%' ", fieldName, fieldValue);
            return this;
        }

		/// <summary>
		/// 增加Like %XX语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition LeftLike(string fieldName, object fieldValue)
        {
            sbSQL.AppendFormat(" {0} LIKE '%{1}' ", fieldName, fieldValue);
            return this;
        }

		/// <summary>
		/// 增加And Like %XX语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition AndLeftLike(string fieldName, object fieldValue)
        {
            sbSQL.AppendFormat(" AND {0} LIKE '%{1}' ", fieldName, fieldValue);
            return this;
        }

		/// <summary>
		/// 增加Or Like %XX语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition OrLeftLike(string fieldName, object fieldValue)
        {
            sbSQL.AppendFormat(" OR {0} LIKE '%{1}' ", fieldName, fieldValue);
            return this;
        }

		/// <summary>
		/// 增加Like XX%语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition RightLike(string fieldName, object fieldValue)
        {
            sbSQL.AppendFormat(" {0} LIKE '{1}%' ", fieldName, fieldValue);
            return this;
        }

		/// <summary>
		/// 增加And Like XX%语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition AndRightLike(string fieldName, object fieldValue)
        {
            sbSQL.AppendFormat(" AND {0} LIKE '{1}%' ", fieldName, fieldValue);
            return this;
        }

		/// <summary>
		/// 增加Or Like XX%语句
		/// </summary>
		/// <param name="fieldName"></param>
		/// <param name="fieldValue"></param>
		/// <returns></returns>
        public DbCondition OrRightLike(string fieldName, object fieldValue)
        {
            sbSQL.AppendFormat(" OR {0} LIKE '{1}%' ", fieldName, fieldValue);
            return this;
        }

		/// <summary>
		/// 输出本条件对象的字符串描述
		/// </summary>
		/// <returns></returns>
        public override string ToString()
        {
            return sbSQL.ToString();
        }

		/// <summary>
		/// 对关键字进行格式化输出
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
        private string formatKey(string key)
        {
            int index = key.IndexOf('.');
            if (index >= 0)
            {
                key = key.Substring(index + 1, key.Length-(index+1));
            }

            return key;
        }
    }
}
