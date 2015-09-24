using System;

namespace Easy4net.Common
{
	/// <summary>
	/// 索引字段信息
	/// </summary>
    public class IdInfo
    {
		/// <summary>
		/// 索引字段名
		/// </summary>
        private String key;
		/// <summary>
		/// 索引字段值
		/// </summary>
        private Object value;

		/// <summary>
		/// 索引字段名
		/// </summary>
        public String Key
        {
            get { return key; }
            set { key = value; }
        }        

		/// <summary>
		/// 索引字段值
		/// </summary>
        public Object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
}
