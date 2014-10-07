using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy4net.Common
{
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
