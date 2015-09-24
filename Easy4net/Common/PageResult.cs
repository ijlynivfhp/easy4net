using System;
using System.Collections.Generic;

namespace Easy4net.Common
{
	/// <summary>
	/// 分页查询结果
	/// </summary>
	/// <typeparam name="T">数据库表实体类</typeparam>
    public class PageResult<T>
    {
        /// <summary>
        /// 分页查询中总记录数
        /// </summary>
        public int Total {get; set;}

        /// <summary>
        /// 分页查询中结果集合
        /// </summary>
        public List<T> DataList {get; set;}
    }
}
