using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy4net.Common
{
    public class PageResult<T>
    {
        public int Total {get; set;}

        public List<T> DataList {get; set;}


    }
}
