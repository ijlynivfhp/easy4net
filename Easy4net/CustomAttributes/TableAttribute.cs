using System;

namespace Easy4net.CustomAttributes
{
	/// <summary>
	/// 数据库表特性
	/// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TableAttribute : Attribute
    {
		/// <summary>
		/// 数据库名
		/// </summary>
        private string _Name = string.Empty;
        
		/// <summary>
		/// 创建一个空的数据库表特性,默认具备自增长键
		/// </summary>
        public TableAttribute() 
		{
            NoAutomaticKey = false;
        }

		/// <summary>
		/// 创建一个制定表明的数据库表特性,默认具备自增长键
		/// </summary>
		/// <param name="aName">数据库表名</param>
		public TableAttribute(string aName)
			: this()
		{
			this.Name = aName;
		}

		/// <summary>
		/// 数据库名
		/// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// 不具备自增长键的表
        /// </summary>
        public bool NoAutomaticKey { get; set; }
    }
}
