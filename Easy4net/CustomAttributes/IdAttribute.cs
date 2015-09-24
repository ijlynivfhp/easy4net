using System;

namespace Easy4net.CustomAttributes
{
	/// <summary>
	/// 数据库索引字段属性
	/// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property, 
        AllowMultiple = false, Inherited = false)]
    public class IdAttribute : Attribute
    {
		/// <summary>
		/// 索引字段名
		/// </summary>
        private string _Name = string.Empty;
		/// <summary>
		/// 索引字段名
		/// </summary>
		public string Name
		{
			get { return this._Name; }
			set { this._Name = value; }
		}
        
		/// <summary>
		/// 索引字段生成方式
		/// </summary>
		public int Strategy { get; set; }

		/// <summary>
		/// 创建一个默认的索引属性,字段生成方式为自动增长型
		/// </summary>
		public IdAttribute()
		{
			this.Strategy = GenerationType.INDENTITY;
		}

		/// <summary>
		/// 创建一个制定字段名的索引属性,字段生成方式为自动增长型
		/// </summary>
		/// <param name="aName">索引字段名</param>
		public IdAttribute(string aName)
			: this()
		{
			this.Name = aName;
		}
    }
}
