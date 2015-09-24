using System;

namespace Easy4net.CustomAttributes
{
	/// <summary>
	/// 数据库表字段属性
	/// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, 
        AllowMultiple = false, Inherited = false)]
    public class ColumnAttribute : Attribute
    {
		/// <summary>
		/// 字段名
		/// </summary>
        private string _Name = string.Empty;
     	/// <summary>
     	/// 是否唯一
     	/// </summary>
        private bool _IsUnique = false;
        /// <summary>
        /// 是否允许为空
        /// </summary>
        private bool _IsNull = true;
		/// <summary>
		/// 是否插入到表中
		/// </summary>
        private bool _IsInsert = true;
		/// <summary>
		/// 是否修改到表中
		/// </summary>
        private bool _IsUpdate = true;
		/// <summary>
		/// 在所有操作中是否忽略此字段
		/// </summary>
        private bool _Ignore = false;

		/// <summary>
		/// 表字段名
		/// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

		/// <summary>
		/// 是否是唯一的,默认为否
		/// </summary>
        public bool IsUnique
        {
            get { return _IsUnique; }
            set { _IsUnique = value; }
        }

		/// <summary>
		/// 此字段是否允许为空,默认允许为空
		/// </summary>
        public bool IsNull
        {
            get { return _IsNull; }
            set { _IsNull = value; }
        }        

		/// <summary>
		/// 在执行插入操作时此是否插入此字段值,默认为插入
		/// </summary>
        public bool IsInsert
        {
            get { return _IsInsert; }
            set { _IsInsert = value; }
        }        

		/// <summary>
		/// 在执行更新操作时是否更新此字段值,默认为更新
		/// </summary>
        public bool IsUpdate
        {
            get { return _IsUpdate; }
            set { _IsUpdate = value; }
        }

		/// <summary>
		/// 在执行所有操作时是否忽略此字段,默认不忽略
		/// </summary>
        public bool Ignore
        {
            get { return _Ignore; }
            set { _Ignore = value; }
        }

		/// <summary>
		/// 创建一个空的字段属性
		/// </summary>
		public ColumnAttribute()
		{

		}

		/// <summary>
		/// 创建一个指定字段名的字段属性
		/// </summary>
		/// <param name="aName">字段名</param>
		public ColumnAttribute(string aName)
			: this()
		{
			this.Name = aName;
		}

		/// <summary>
		/// 创建一个制定字段名的字段属性
		/// </summary>
		/// <param name="aName">字段名</param>
		/// <param name="aIgnore">在执行数据操作时是否忽略此字段</param>
		public ColumnAttribute(string aName, bool aIgnore)
			: this(aName)
		{
			this.Ignore = aIgnore;
		}

		/// <summary>
		/// 创建一个制定字段名的字段属性
		/// </summary>
		/// <param name="aName">字段名</param>
		/// <param name="aInsert">此字段是否参与插入操作</param>
		/// <param name="aUpdate">此字段是否参与更新操作</param>
		public ColumnAttribute(string aName, bool aInsert, bool aUpdate)
			: this(aName)
		{
			this.IsInsert = aInsert;
			this.IsUpdate = aUpdate;
		}
    }
}
