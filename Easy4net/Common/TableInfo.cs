using System;
using System.Collections.Generic;
using System.Data;
using Easy4net.Context;

namespace Easy4net.Common
{
	/// <summary>
	/// 数据库表信息实体类
	/// </summary>
    public class TableInfo
    {
		/// <summary>
		/// 数据库表名
		/// </summary>
        private string tableName;
		/// <summary>
		/// 索引生成方式
		/// </summary>
        private int strategy;

		/// <summary>
		/// 索引信息
		/// </summary>
        private IdInfo id = new IdInfo();
		/// <summary>
		/// 普通字段信息集合
		/// </summary>
        private ColumnInfo columns = new ColumnInfo();
		/// <summary>
		/// 属性到字段信息的映射集合
		/// </summary>
        private Map propToColumn = new Map();
		/// <summary>
		/// 字段信息到属性的映射集合
		/// </summary>
        private Map columnToProp = new Map();

		/// <summary>
		/// 是否不具备自增长键
		/// </summary>
        public bool NoAutomaticKey
        {
            get;
            set;
        }

		/// <summary>
		/// 数据库表名
		/// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

		/// <summary>
		/// 索引生成方式,参考GenerationType定义
		/// </summary>
        public int Strategy
        {
            get { return strategy; }
            set { strategy = value; }
        }

		/// <summary>
		/// 索引字段信息
		/// </summary>
        public IdInfo Id
        {
            get { return id; }
            set { id = value; }
        }        

		/// <summary>
		/// 普通字段信息集合
		/// </summary>
        public ColumnInfo Columns
        {
            get { return columns; }
            set { columns = value; }
        }

		/// <summary>
		/// 实体类属性对应字段信息映射集合
		/// </summary>
        public Map PropToColumn
        {
            get { return propToColumn; }
            set { propToColumn = value; }
        }

		/// <summary>
		/// 字段信息对应实体类属性映射集合
		/// </summary>
        public Map ColumnToProp
        {
            get { return columnToProp; }
            set { columnToProp = value; }
        }

		/// <summary>
		/// 获取本数据库表信息中的参数键值集合
		/// </summary>
		/// <returns></returns>
        public List<IDbDataParameter> GetParameterList()
        {
            if (this.Columns == null || this.Columns.Count == 0) return new List<IDbDataParameter>();

            Session session = SessionThreadLocal.Get();

            List<IDbDataParameter> paramList = new List<IDbDataParameter>();
            foreach (string key in this.Columns.Keys)
            {
                if (!string.IsNullOrEmpty(key.Trim()))
                {
                    object value = this.Columns[key];
                    if (value != null)
                    {
                        IDbDataParameter param = session.DbFactory.CreateDbParameter();
                        param.ParameterName = key;
                        param.Value = value;

                        paramList.Add(param);
                    }
                }
            }

            return paramList;
        }

		/// <summary>
		/// 将参数集合转为参数数组
		/// </summary>
		/// <param name="paramList">参数集合</param>
		/// <returns></returns>
        public IDbDataParameter[] GetParameters(List<IDbDataParameter> paramList)
        {
            Session session = SessionThreadLocal.Get();

            int i = 0;
            IDbDataParameter[] parameters = session.DbFactory.CreateDbParameters(paramList.Count);
            foreach (IDbDataParameter dbParameter in paramList)
            {
                parameters[i] = dbParameter;
                i++;
            }

            return parameters;
        }  

		/// <summary>
		/// 获取本数据库表信息中的参数键值数组
		/// </summary>
		/// <returns></returns>
        public IDbDataParameter[] GetParameters()
        {
            Session session = SessionThreadLocal.Get();

            if (this.Columns == null || this.Columns.Count == 0) return session.DbFactory.CreateDbParameters(1);
            
            List<IDbDataParameter> paramList = new List<IDbDataParameter>();
            foreach (string key in this.Columns.Keys)
            {
                if (!string.IsNullOrEmpty(key.Trim()))
                {
                    object value = this.Columns[key];
                    if (value != null)
                    {
                        IDbDataParameter param = session.DbFactory.CreateDbParameter();
                        param.ParameterName = key;
                        param.Value = value;

                        paramList.Add(param);
                    }
                }
            }

            int i = 0;
            IDbDataParameter[] parameters = session.DbFactory.CreateDbParameters(paramList.Count);
            foreach (IDbDataParameter dbParameter in paramList)
            {
                parameters[i] = dbParameter;
                i++;
            }
         
            return parameters;
        }       
    }
}
