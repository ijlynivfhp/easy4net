using System;
using System.Collections;

namespace Easy4net.Common
{
	/// <summary>
	/// 键值对映射类
	/// </summary>
    public class Map : Hashtable
    {
        public void Put(object key,object value)
        {
            if (this.ContainsKey(key)) this.Remove(key);
            this.Add(key, value);
        }

        public void setParameter(string key, object value)
        {
            if (this.ContainsKey(key)) this.Remove(key);
            this.Add(key, value);
        }
    }
}
