using System;

namespace Easy4net.CustomAttributes
{
	/// <summary>
	/// 数据库主键字段特性
	/// </summary>
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property, 
        AllowMultiple = false, Inherited = false)]
    public class IdAttribute : Attribute
    {
		/// <summary>
		/// 主键字段名
		/// </summary>
        private string _Name = string.Empty;
		/// <summary>
		/// 主键字段名
		/// </summary>
		public string Name
		{
			get { return this._Name; }
			set { this._Name = value; }
		}
        
		/// <summary>
		/// 主键字段生成方式,参考GenerationType定义
		/// </summary>
		public int Strategy { get; set; }

		/// <summary>
		/// 创建一个默认的主键特性,字段生成方式为自动增长型
		/// </summary>
		public IdAttribute()
		{
			this.Strategy = GenerationType.INDENTITY;
		}

		/// <summary>
		/// 创建一个制定字段名的主键特性,字段生成方式为自动增长型
		/// </summary>
		/// <param name="aName">主键字段名</param>
		public IdAttribute(string aName)
			: this()
		{
			this.Name = aName;
		}
    }
}
