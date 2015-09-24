using System;

namespace Easy4net.Common
{
	/// <summary>
	/// 主键字段信息
	/// </summary>
    public class IdInfo
    {
		/// <summary>
		/// 主键字段名
		/// </summary>
        private String key;
		/// <summary>
		/// 主键字段值
		/// </summary>
        private Object value;

		/// <summary>
		/// 主键字段名
		/// </summary>
        public String Key
        {
            get { return key; }
            set { key = value; }
        }        

		/// <summary>
		/// 主键字段值
		/// </summary>
        public Object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
}
