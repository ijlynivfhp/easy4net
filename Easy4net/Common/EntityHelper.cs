using System;
using System.Collections.Generic;
using System.Text;
using Easy4net.CustomAttributes;
using Easy4net.DBUtility;
using System.Reflection;
using System.Data;
using Easy4net.Context;

namespace Easy4net.Common
{
	/// <summary>
	/// 实体帮助类
	/// </summary>
    public static class EntityHelper
    {
        //public static string GetTableName(Type classType, DbOperateType type)
        //{
        //    //string strTableName = string.Empty;
        //    //string strEntityName = string.Empty;

        //    //strEntityName = classType.FullName;

        //    //object[] attr = classType.GetCustomAttributes(false);
        //    //if (attr.Length == 0) return strTableName;

        //    //foreach (object classAttr in attr)
        //    //{
        //    //    if (classAttr is TableAttribute)
        //    //    {
        //    //        TableAttribute tableAttr = classAttr as TableAttribute;
        //    //        strTableName = tableAttr.Name;
        //    //    }
        //    //}

        //    TableAttribute tableAttr = GetTableAttribute(classType, type);

        //    //if (string.IsNullOrEmpty(strTableName) && (type == DbOperateType.INSERT || type == DbOperateType.UPDATE || type == DbOperateType.DELETE))
        //    //{
        //    //    throw new Exception("实体类:" + strEntityName + "的属性配置[Table(name=\"tablename\")]错误或未配置");
        //    //}

        //    return tableAttr.Name;
        //}

		/// <summary>
		/// 根据实体类类型和数据操作类型获取数据库表特性
		/// </summary>
		/// <param name="classType">数据库表实例类型</param>
		/// <param name="type">数据操作类型</param>
		/// <returns>数据库表特性</returns>
        public static TableAttribute GetTableAttribute(Type classType, DbOperateType type)
        {
            TableAttribute tableAttr = null;
            //string strTableName = string.Empty;
            string strEntityName = string.Empty;

            strEntityName = classType.FullName;

            object[] attr = classType.GetCustomAttributes(false);
            if (attr.Length == 0) return null;

            foreach (object classAttr in attr)
            {
                if (classAttr is TableAttribute)
                {
                    tableAttr = classAttr as TableAttribute;
                    //atrTableName = tableAttr.Name;
                }
            }

            if (tableAttr == null && (type == DbOperateType.INSERT || type == DbOperateType.UPDATE || type == DbOperateType.DELETE))
            {
                throw new Exception("实体类:" + strEntityName + "的属性配置[Table(name=\"tablename\")]错误或未配置");
            }

            return tableAttr;
        }

		/// <summary>
		/// 根据主键特性跟数据操作类型获取主键名
		/// </summary>
		/// <param name="attribute">IdAttribute主键特性</param>
		/// <param name="type">数据操作类型</param>
		/// <returns>主键名</returns>
        public static string GetPrimaryKey(object attribute, DbOperateType type)
        {
            string strPrimary = string.Empty;
            IdAttribute attr = attribute as IdAttribute;
            if (type == DbOperateType.INSERT)
            {
                switch (attr.Strategy)
                {
                    case GenerationType.INDENTITY:
                        break;
                    case GenerationType.GUID:
                        strPrimary = System.Guid.NewGuid().ToString();
                        break;
                }
            }
            else {
                strPrimary = attr.Name;
            }

            return strPrimary;
        }

		/// <summary>
		/// 根据字段特性或主键特性获取对应字段名
		/// </summary>
		/// <param name="attribute">字段特性或主键特性</param>
		/// <returns>字段名</returns>
        public static string GetColumnName(object attribute)
        {
            string columnName = string.Empty;
            if (attribute is ColumnAttribute)
            {
                ColumnAttribute columnAttr = attribute as ColumnAttribute;
                columnName = columnAttr.Name;
            }
            if (attribute is IdAttribute)
            {
                IdAttribute idAttr = attribute as IdAttribute;
                columnName = idAttr.Name;
            }

            return columnName;
        }

		/// <summary>
		/// 根据数据库表对象及数据操作类型获取表信息
		/// </summary>
		/// <param name="entity">数据库表对象</param>
		/// <param name="dbOpType">操作类型</param>
		/// <param name="properties">数据库表对象属性集合</param>
		/// <returns>数据库表信息</returns>
        public static TableInfo GetTableInfo(object entity, DbOperateType dbOpType, PropertyInfo[] properties)
        {
            bool breakForeach = false;
            string strPrimaryKey = string.Empty;
            TableInfo tableInfo = new TableInfo();
            Type type = entity.GetType();

            TableAttribute tableAttr = GetTableAttribute(type, dbOpType);
            tableInfo.TableName = tableAttr.Name;
            tableInfo.NoAutomaticKey = tableAttr.NoAutomaticKey;

            //tableInfo.TableName = GetTableName(type, dbOpType);

            if (dbOpType == DbOperateType.COUNT)
            {
                return tableInfo;
            }
            
            foreach (PropertyInfo property in properties)
            {
                object propvalue = null;
                string columnName = string.Empty;
                string propName = columnName = property.Name;
          
                propvalue = ReflectionHelper.GetPropertyValue(entity, property);

                object[] propertyAttrs = property.GetCustomAttributes(false);
                for (int i = 0; i < propertyAttrs.Length; i++)
                {
                    object propertyAttr = propertyAttrs[i];
                    if (EntityHelper.IsCaseColumn(propertyAttr, dbOpType))
                    {
                        breakForeach = true;break;
                    }

                    string tempVal = GetColumnName(propertyAttr);
                    columnName = tempVal == string.Empty ? propName : tempVal;

                    if (propertyAttr is IdAttribute)
                    {
                        if (dbOpType == DbOperateType.INSERT || dbOpType == DbOperateType.DELETE)
                        {
                            IdAttribute idAttr = propertyAttr as IdAttribute;
                            tableInfo.Strategy = idAttr.Strategy;

                            if (CommonUtils.IsNullOrEmpty(propvalue))
                            {
                                strPrimaryKey = EntityHelper.GetPrimaryKey(propertyAttr, dbOpType);
                                if (!string.IsNullOrEmpty(strPrimaryKey))
                                    propvalue = strPrimaryKey;
                            }
                        }

                        tableInfo.Id.Key = columnName;
                        tableInfo.Id.Value = propvalue;
                        tableInfo.PropToColumn.Put(propName, columnName);
                        tableInfo.ColumnToProp.Put(columnName, propName);
                        breakForeach = true;
                    }
                }

                if (breakForeach && dbOpType == DbOperateType.DELETE) break;
                if (breakForeach) { breakForeach = false; continue; }
                tableInfo.Columns.Put(columnName, propvalue);
                tableInfo.PropToColumn.Put(propName, columnName);
                tableInfo.ColumnToProp.Put(columnName, propName);
            }

            /*if (dbOpType == DbOperateType.UPDATE)
            {
                tableInfo.Columns.Put(tableInfo.Id.Key, tableInfo.Id.Value);
            }*/

            return tableInfo;
        }

		/// <summary>
		/// 根据数据库表对象获取该对象中主键成员的属性
		/// </summary>
		/// <param name="entity">数据库表对象</param>
		/// <param name="properties">该对象的属性集合</param>
		/// <returns>主键成员属性</returns>
        public static PropertyInfo GetPrimaryKeyPropertyInfo(object entity, PropertyInfo[] properties)
        {
            bool breakForeach = false;
            Type type = entity.GetType();
            PropertyInfo properyInfo = null;

            foreach (PropertyInfo property in properties)
            {
                string columnName = string.Empty;
                string propName = columnName = property.Name;

                object[] propertyAttrs = property.GetCustomAttributes(false);
                for (int i = 0; i < propertyAttrs.Length; i++)
                {
                    object propertyAttr = propertyAttrs[i];

                    if (propertyAttr is IdAttribute)
                    {
                        properyInfo = property;
                        breakForeach = true;
                        break;
                    }
                }
                if (breakForeach) break;
            }

            return properyInfo;
        }

		/// <summary>
		/// 根据数据库表信息从DataReader中读取对应实体类的数据集合
		/// </summary>
		/// <typeparam name="T">数据库表实体类</typeparam>
		/// <param name="sdr">DataReader</param>
		/// <param name="tableInfo">数据库表信息</param>
		/// <param name="properties">实体类属性集合</param>
		/// <returns></returns>
        public static List<T> toList<T>(IDataReader sdr, TableInfo tableInfo, PropertyInfo[] properties) where T : new()
        {
            List<T> list = new List<T>();

            while (sdr.Read())
            {
                T entity = new T();
                foreach (PropertyInfo property in properties)
                {
                    if (EntityHelper.IsCaseColumn(property, DbOperateType.SELECT)) continue;

                    String name = tableInfo.PropToColumn[property.Name].ToString();
                    ReflectionHelper.SetPropertyValue(entity, property, sdr[name]);
                }
                list.Add(entity);
            }

            return list;
        }

		/// <summary>
		/// 从DataReader中读取指定数据库表类型的数据集合
		/// </summary>
		/// <typeparam name="T">数据库表实体类</typeparam>
		/// <param name="sdr">DataReader</param>
		/// <returns></returns>
        public static List<T> toList<T>(IDataReader sdr) where T : new()
        {
            List<T> list = new List<T>();
            PropertyInfo[] properties = ReflectionHelper.GetProperties(new T().GetType());

            while (sdr.Read())
            {
                T entity = new T();
                foreach (PropertyInfo property in properties)
                {
                    String name = property.Name;
                    ReflectionHelper.SetPropertyValue(entity, property, sdr[name]);
                }
                list.Add(entity);
            }

            return list;
        }

		/// <summary>
		/// 根据数据库表信息及条件生成器获取Select语句
		/// </summary>
		/// <param name="tableInfo">数据库表信息</param>
		/// <param name="condition">条件生成器</param>
		/// <returns></returns>
        public static string GetFindSql(TableInfo tableInfo, DbCondition condition)
        {
            DbFactory dbFactory = SessionThreadLocal.Get().DbFactory;
            string dbParmChar = dbFactory.DbParmChar;

            StringBuilder sbColumns = new StringBuilder();

            tableInfo.Columns.Put(tableInfo.Id.Key, tableInfo.Id.Value);
            foreach (String key in tableInfo.Columns.Keys)
            {
                string nKey = DbKeywords.FormatColumnName(key.Trim(), dbFactory.DbType);
                sbColumns.Append(nKey).Append(",");
            }

            if (sbColumns.Length > 0) sbColumns.Remove(sbColumns.ToString().Length - 1, 1);

            string strSql = String.Empty;
            if (String.IsNullOrEmpty(condition.queryString)) {
                strSql = "SELECT {0} FROM {1}";
                strSql = string.Format(strSql, sbColumns.ToString(), tableInfo.TableName);
                strSql += condition.ToString();
            }
            else {
                strSql = condition.ToString();
            }

            strSql = strSql.ToUpper();

            return strSql;
        }

		/// <summary>
		/// 根据数据库表信息获取Selet All语句
		/// </summary>
		/// <param name="tableInfo">数据库表信息</param>
		/// <returns></returns>
        public static string GetFindAllSql(TableInfo tableInfo)
        {
            DbFactory dbFactory = SessionThreadLocal.Get().DbFactory;
            string dbParmChar = dbFactory.DbParmChar;

            StringBuilder sbColumns = new StringBuilder();

            tableInfo.Columns.Put(tableInfo.Id.Key, tableInfo.Id.Value);
            foreach (String key in tableInfo.Columns.Keys)
            {
                string nKey = DbKeywords.FormatColumnName(key.Trim(), dbFactory.DbType);
                sbColumns.Append(nKey).Append(",");
            }

            if (sbColumns.Length > 0) sbColumns.Remove(sbColumns.ToString().Length - 1, 1);

            string strSql = "SELECT {0} FROM {1}";
            strSql = string.Format(strSql, sbColumns.ToString(), tableInfo.TableName);

            return strSql;
        }

		/// <summary>
		/// 根据数据库表信息获取根据主键值进行查询的SQL语句
		/// </summary>
		/// <param name="tableInfo">数据库表信息</param>
		/// <returns></returns>
        public static string GetFindByIdSql(TableInfo tableInfo)
        {
            DbFactory dbFactory = SessionThreadLocal.Get().DbFactory;
            string dbParmChar = dbFactory.DbParmChar;

            StringBuilder sbColumns = new StringBuilder();

            if (tableInfo.Columns.ContainsKey(tableInfo.Id.Key))
                tableInfo.Columns[tableInfo.Id.Key] = tableInfo.Id.Value;
            else
                tableInfo.Columns.Put(tableInfo.Id.Key, tableInfo.Id.Value);

            foreach (String key in tableInfo.Columns.Keys)
            {
                string nKey = DbKeywords.FormatColumnName(key.Trim(), dbFactory.DbType);
                sbColumns.Append(nKey).Append(",");
            }

            if (sbColumns.Length > 0) sbColumns.Remove(sbColumns.ToString().Length - 1, 1);

            string strSql = "SELECT {0} FROM {1} WHERE {2} = " + dbParmChar + "{2}";
            strSql = string.Format(strSql, sbColumns.ToString(), tableInfo.TableName, tableInfo.Id.Key);

            return strSql;
        }

		/// <summary>
		/// 根据数据库表信息获取查询记录条数的SQL语句
		/// </summary>
		/// <param name="tableInfo">数据库表信息</param>
		/// <returns></returns>
        public static string GetFindCountSql(TableInfo tableInfo)
        {
            DbFactory dbFactory = SessionThreadLocal.Get().DbFactory;
            string dbParmChar = dbFactory.DbParmChar;

            StringBuilder sbColumns = new StringBuilder();

            string strSql = "SELECT COUNT(0) FROM {1} ";
            strSql = string.Format(strSql, sbColumns.ToString(), tableInfo.TableName);

            foreach (String key in tableInfo.Columns.Keys)
            {
                string nKey = DbKeywords.FormatColumnName(key.Trim(), dbFactory.DbType);
                sbColumns.Append(nKey).Append("=").Append(dbParmChar).Append(key);
            }

            if (sbColumns.Length > 0)
            {
                strSql += " WHERE " + sbColumns.ToString();
            }

            return strSql;
        }

		/// <summary>
		/// 根据数据库表信息及条件生成器获取查询记录条数的SQL语句
		/// </summary>
		/// <param name="tableInfo">数据库表信息</param>
		/// <param name="condition">条件生成器</param>
		/// <returns></returns>
        public static string GetFindCountSql(TableInfo tableInfo, DbCondition condition)
        {
            string strSql = "SELECT COUNT(0) FROM {0}";
            strSql = string.Format(strSql, tableInfo.TableName);
            strSql += condition.ToString();

            return strSql;
        }

		/// <summary>
		/// 根据数据库表信息获取根据主键值进行查询的SQL语句
		/// 同方法GetFindByIdSql
		/// </summary>
		/// <param name="tableInfo"></param>
		/// <returns></returns>
        public static string GetFindByPropertySql(TableInfo tableInfo)
        {
            DbFactory dbFactory = SessionThreadLocal.Get().DbFactory;
            string dbParmChar = dbFactory.DbParmChar;

            StringBuilder sbColumns = new StringBuilder();

            tableInfo.Columns.Put(tableInfo.Id.Key, tableInfo.Id.Value);
            foreach (String key in tableInfo.Columns.Keys)
            {
                string nKey = DbKeywords.FormatColumnName(key.Trim(), dbFactory.DbType);
                sbColumns.Append(nKey).Append(",");
            }

            if (sbColumns.Length > 0) sbColumns.Remove(sbColumns.ToString().Length - 1, 1);

            string strSql = "SELECT {0} FROM {1} WHERE {2} = " + dbParmChar + "{2}";
            strSql = string.Format(strSql, sbColumns.ToString(), tableInfo.TableName, tableInfo.Id.Key);

            return strSql;
        }

		/// <summary>
		/// 根据数据库类型获取对应的获取在插入操作后自增长型主键字段的SQL语句
		/// </summary>
		/// <param name="dbType">数据库类型</param>
		/// <returns></returns>
        public static string GetAutoSql(DatabaseType dbType)
        {
            string autoSQL = "";
            if (dbType == DatabaseType.SQLSERVER)
            {
                autoSQL = " select scope_identity() as AutoId ";
            }
            else if (dbType == DatabaseType.ACCESS)
            {
                autoSQL = " select @@IDENTITY as AutoId ";
            }

            else if (dbType == DatabaseType.MYSQL)
            {
                autoSQL = " ;select @@identity ";
            }

            else if (dbType == DatabaseType.SQLITE)
            {
                autoSQL = " ;select last_insert_rowid() as AutoId ";
            }

            return autoSQL;
        }

		/// <summary>
		/// 根据数据库表信息获取Insert插入操作语句
		/// </summary>
		/// <param name="tableInfo"></param>
		/// <returns></returns>
        public static string GetInsertSql(TableInfo tableInfo)
        {
            DbFactory dbFactory = SessionThreadLocal.Get().DbFactory;
            string dbParmChar = dbFactory.DbParmChar;

            StringBuilder sbColumns = new StringBuilder();
            StringBuilder sbValues = new StringBuilder();

            if(tableInfo.Strategy != GenerationType.INDENTITY)
            {
                if (tableInfo.Strategy == GenerationType.GUID && tableInfo.Id.Value == null)
                {
                    tableInfo.Id.Value = Guid.NewGuid().ToString();
                }

                if (tableInfo.Id.Value != null)
                {
                    tableInfo.Columns.Put(tableInfo.Id.Key, tableInfo.Id.Value);
                }
            }
           
            foreach (String key in tableInfo.Columns.Keys)
            {
                Object value = tableInfo.Columns[key];
                if (!string.IsNullOrEmpty(key.Trim()) && value != null)
                {
                    string nKey = DbKeywords.FormatColumnName(key.Trim(), dbFactory.DbType);
                    sbColumns.Append(nKey).Append(",");
                    sbValues.Append(dbParmChar).Append(key).Append(",");
                }
            }

            if (sbColumns.Length > 0 && sbValues.Length > 0)
            {
                sbColumns.Remove(sbColumns.ToString().Length - 1, 1);
                sbValues.Remove(sbValues.ToString().Length - 1, 1);
            }

            string strSql = "INSERT INTO {0}({1}) VALUES({2})";
            strSql = string.Format(strSql, tableInfo.TableName, sbColumns.ToString(), sbValues.ToString());


            if (!tableInfo.NoAutomaticKey)
            {
                if (dbFactory.DbType == DatabaseType.SQLSERVER || dbFactory.DbType == DatabaseType.MYSQL || dbFactory.DbType == DatabaseType.SQLITE)
                {
                    string autoSql = EntityHelper.GetAutoSql(dbFactory.DbType);
                    strSql = strSql + autoSql;
                }
            }

            return strSql;
        }

		/// <summary>
		/// 根据数据库表信息获取Update更新操作语句
		/// </summary>
		/// <param name="tableInfo"></param>
		/// <returns></returns>
        public static string GetUpdateSql(TableInfo tableInfo)
        {
            DbFactory dbFactory = SessionThreadLocal.Get().DbFactory;
            string dbParmChar = dbFactory.DbParmChar;

            StringBuilder sbBody = new StringBuilder();
            
            foreach (String key in tableInfo.Columns.Keys)
            {
                Object value = tableInfo.Columns[key];
                if (!string.IsNullOrEmpty(key.Trim()) && value != null)
                {
                    string nKey = DbKeywords.FormatColumnName(key.Trim(), dbFactory.DbType);
                    sbBody.Append(nKey).Append("=").Append(dbParmChar).Append(key).Append(",");
                }
            }

            if (sbBody.Length > 0) sbBody.Remove(sbBody.ToString().Length - 1, 1);

            //tableInfo.Columns.Put(tableInfo.Id.Key, tableInfo.Id.Value);

            string strSql = "update {0} set {1} where {2} =" + dbParmChar + tableInfo.Id.Key;
            strSql = string.Format(strSql, tableInfo.TableName, sbBody.ToString(), tableInfo.Id.Key);

            return strSql;
        }

		/// <summary>
		/// 根据数据库表信息获取根据主键进行Delete删除操作语句
		/// </summary>
		/// <param name="tableInfo"></param>
		/// <returns></returns>
        public static string GetDeleteByIdSql(TableInfo tableInfo)
        {
            DbFactory dbFactory = SessionThreadLocal.Get().DbFactory;
            string dbParmChar = dbFactory.DbParmChar;

            string strSql = "delete from {0} where {1} =" + dbParmChar + tableInfo.Id.Key;
            strSql = string.Format(strSql, tableInfo.TableName, tableInfo.Id.Key);

            return strSql;
        }

		/// <summary>
		/// 将字段信息集合设置到参数数组中
		/// </summary>
		/// <param name="columns">字段信息集合</param>
		/// <param name="parms">[输出] 参数值集合</param>
        public static void SetParameters(ColumnInfo columns, params IDbDataParameter[] parms)
        {
            int i = 0;
            foreach (string key in columns.Keys)
            {
                if (!string.IsNullOrEmpty(key.Trim()))
                {
                    object value = columns[key];
                    if (value != null)
                    {
                        parms[i].ParameterName = key;
                        parms[i].Value = value;
                        i++;
                    }
                }
            }
        }

		/// <summary>
		/// 根据操作类型判断输入的字段特性中指定的字段是否进行忽略
		/// </summary>
		/// <param name="attribute">字段特性ColumnAttribute,如果不为此特性则不进行忽略检测</param>
		/// <param name="dbOperateType">操作类型</param>
		/// <returns>true忽略此字段</returns>
        public static bool IsCaseColumn(object attribute, DbOperateType dbOperateType)
        {
            if (attribute is ColumnAttribute)
            {
                ColumnAttribute columnAttr = attribute as ColumnAttribute;
                if (columnAttr.Ignore)
                {
                    return true;
                }
                if (!columnAttr.IsInsert && dbOperateType == DbOperateType.INSERT)
                {
                    return true;
                }
                if (!columnAttr.IsUpdate && dbOperateType == DbOperateType.UPDATE)
                {
                    return true;
                } 
            }

            return false;
        }

		/// <summary>
		/// 根据操作类型判断输入的成员属性中指定的成员是否进行忽略
		/// </summary>
		/// <param name="property">成员属性</param>
		/// <param name="dbOperateType">数据操作</param>
		/// <returns>true忽略此属性中的成员</returns>
        public static bool IsCaseColumn(PropertyInfo property, DbOperateType dbOperateType)
        {
            bool isBreak = false;
            object[] propertyAttrs = property.GetCustomAttributes(false);
            foreach (object propertyAttr in propertyAttrs)
            {
                if (EntityHelper.IsCaseColumn(propertyAttr, DbOperateType.SELECT))
                {
                    isBreak = true; break;
                }
            }

            return isBreak;
        }
    }
}
