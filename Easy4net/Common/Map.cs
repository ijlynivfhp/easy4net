using System;
using System.Collections;

namespace Easy4net.Common
{
	/// <summary>
	/// 键值对映射类
	/// </summary>
    public class Map : Hashtable
    {
		/// <summary>
		/// 设置/添加键值对
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
        public void Put(object key,object value)
        {
            if (this.ContainsKey(key)) this.Remove(key);
            base.Add(key, value);
        }

		/// <summary>
		/// 设置/添加参数
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
        public void setParameter(string key, object value)
        {
            if (this.ContainsKey(key)) this.Remove(key);
            base.Add(key, value);
        }
    }
}
