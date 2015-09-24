using System;
using System.Collections.Generic;
using System.Data;
using Easy4net.DBUtility;
using Easy4net.Context;

namespace Easy4net.Common
{
	/// <summary>
	/// 参数映射集合类
	/// </summary>
    public class ParamMap : Map
    {
		/// <summary>
		/// 是否分页
		/// </summary>
        private bool isPage;
		/// <summary>
		/// 页面偏移量
		/// </summary>
        private int pageOffset;
		/// <summary>
		/// 每页限制记录数
		/// </summary>
        private int pageLimit;
		/// <summary>
		/// 排序字段
		/// </summary>
        private string orderFields;
		/// <summary>
		/// 是否递减排序
		/// </summary>
        private bool isDesc = true;

		/// <summary>
		/// 数据库参数集合
		/// </summary>
        private List<IDbDataParameter> m_ParamList = new List<IDbDataParameter>();

		/// <summary>
		/// 创建一个参数映射集合
		/// </summary>
        private ParamMap() { }

		/// <summary>
		/// 排序的字段
		/// </summary>
        public string OrderFields
        {
            get { return orderFields; }
            set { orderFields = value; }
        }

		/// <summary>
		/// 是否递减排序
		/// </summary>
        public bool IsDesc
        {
            get { return isDesc; }
            set { isDesc = value; }
        }

		/// <summary>
		/// 创建一个参数映射集合
		/// </summary>
		/// <returns></returns>
        public static ParamMap newMap()
        {
            ParamMap paramMap = new ParamMap();
            return paramMap;
        }

		/// <summary>
		/// 是否进行分页
		/// </summary>
        public bool IsPage
        {
            get
            {
                return isPage;
            }
        }

		/// <summary>
		/// 每页的偏移量
		/// </summary>
        public int PageOffset
        {
            get
            {
                if (this.ContainsKey("pageIndex") && this.ContainsKey("pageSize"))
                {
                    int pageIndex = this.getInt("pageIndex");
                    int pageSize = this.getInt("pageSize");
                    if (pageIndex <= 0) pageIndex = 1;
                    if (pageSize <= 0) pageSize = 1;

                    return (pageIndex - 1) * pageSize;
                }

                return 0;
            }
        }

		/// <summary>
		/// 每页的记录条数限制
		/// </summary>
        public int PageLimit
        {
            get
            {
                if (this.ContainsKey("pageSize"))
                {
                    return this.getInt("pageSize");
                }

                return 0;
            }
        }

		/// <summary>
		/// 根据键值获取整型数据
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
        public int getInt(string key)
        {
            var value = this[key];
            return Convert.ToInt32(value);
        }

		/// <summary>
		/// 根据键值获取字符串数据
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
        public String getString(string key)
        {
            var value = this[key];
            return Convert.ToString(value);
        }

		/// <summary>
		/// 根据键值获取Double型数据
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
        public Double toDouble(string key)
        {
            var value = this[key];
            return Convert.ToDouble(value);
        }

		/// <summary>
		/// 根据键值获取Long型数据
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
        public Int64 toLong(string key)
        {
            var value = this[key];
            return Convert.ToInt64(value);
        }

		/// <summary>
		/// 根据键值获取Decimal型数据
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
        public Decimal toDecimal(string key)
        {
            var value = this[key];
            return Convert.ToDecimal(value);
        }

		/// <summary>
		/// 根据键值获取DateTime型数据
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
        public DateTime toDateTime(string key)
        {
            var value = this[key];
            return Convert.ToDateTime(value);
        }

		/// <summary>
		/// 设置排序字段及排序方式
		/// </summary>
		/// <param name="orderFields">排序字段</param>
		/// <param name="isDesc">是否为递减排序</param>
        public void setOrderFields(string orderFields, bool isDesc)
        {
            this.orderFields = orderFields;
            this.isDesc = isDesc;
        }

        /// <summary>
        /// 此方法已过时，请使用 setPageParamters方法分页
        /// </summary>
        /// <param name="pageIndex"></param>
		[Obsolete("此方法已过时，请使用 setPageParamters方法分页")]
		private void setPageIndex(int pageIndex)
        {
            this["pageIndex"] = pageIndex;
            setPages();
        }

        /// <summary>
        /// 此方法已过时，请使用 setPageParamters方法分页
        /// </summary>
		/// <param name="pageSize"></param>
		[Obsolete("此方法已过时，请使用 setPageParamters方法分页")]
        private void setPageSize(int pageSize)
        {
            this["pageSize"] = pageSize;
            setPages();
        }

        /// <summary>
        /// 分页参数设置
        /// </summary>
        /// <param name="page">第几页，从0开始</param>
        /// <param name="limit">每页最多显示几条数据</param>
        public void setPageParamters(int page, int limit)
        {
            this["pageIndex"] = page;
            this["pageSize"] = limit;
            setPages();
        }

		/// <summary>
		/// 设置分页参数
		/// </summary>
        private void setPages()
        {
            Session session = SessionThreadLocal.Get();

            if (this.ContainsKey("pageIndex") && this.ContainsKey("pageSize"))
            {
                this.isPage = true;
                if (session.DbFactory.DbType == DatabaseType.MYSQL || session.DbFactory.DbType == DatabaseType.SQLITE)
                {
                    this["offset"] = this.PageOffset;
                    this["limit"] = this.PageLimit;

                    this.Remove("pageIndex");
                    this.Remove("pageSize");

                    this.Add("offset", this.getInt("offset"));
                    this.Add("limit", this.getInt("limit"));
                }

                if (session.DbFactory.DbType == DatabaseType.SQLSERVER)
                {
                    int pageIndex = this.getInt("pageIndex");
                    int pageSize = this.getInt("pageSize");
                    if (pageIndex <= 0) pageIndex = 1;
                    if (pageSize <= 0) pageSize = 1;

                    this["pageStart"] = (pageIndex - 1) * pageSize + 1;
                    this["pageEnd"] = pageIndex * pageSize;

                    this.Remove("pageIndex");
                    this.Remove("pageSize");

                    this.Add("pageStart", this.getInt("pageStart"));
                    this.Add("pageEnd", this.getInt("pageEnd"));
                }

                if (session.DbFactory.DbType == DatabaseType.ACCESS)
                {
                    int pageIndex = this.getInt("pageIndex");
                    int pageSize = this.getInt("pageSize");

                    this["page_offset"] = pageIndex * pageSize;
                    this["page_limit"] = pageSize;

                    this.Remove("pageIndex");
                    this.Remove("pageSize");
                }
            }
        }

		/// <summary>
		/// 添加键值对映射
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
        public override void Add(object key, object value)
        {
            base.Put(key, value);

            Session session = SessionThreadLocal.Get();

            IDbDataParameter param = session.DbFactory.CreateDbParameter(key.ToString(), value);
            m_ParamList.Add(param);
        }

		/// <summary>
		/// 添加键值对映射
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
        public new void Put(object key, object value)
        {
            this.Add(key, value);
        }

		/// <summary>
		/// 添加键值对映射
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
        public new void setParameter(string key, object value)
        {
            this.Add(key, value);
        }

		/// <summary>
		/// 输出参数集合
		/// </summary>
		/// <returns></returns>
        public IDbDataParameter[] toDbParameters()
        {
            int i = 0;
            IDbDataParameter[] paramArr = new IDbDataParameter[m_ParamList.Count];
            foreach (IDbDataParameter dbParameter in m_ParamList)
            {
                paramArr[i] = dbParameter;
                i++;
            }

            return paramArr;          
        }
    }
}
