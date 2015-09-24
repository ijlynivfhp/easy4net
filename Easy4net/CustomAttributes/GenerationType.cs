using System;

namespace Easy4net.CustomAttributes
{
	/// <summary>
	/// 数据库表主键生成类型定义
	/// </summary>
    public class GenerationType 
    {
		/// <summary>
		/// 自动增长型
		/// </summary>
        public const int INDENTITY = 1;
		/// <summary>
		/// GUID型
		/// </summary>
        public const int GUID = 2;
		/// <summary>
		/// 提前生成并填充
		/// </summary>
        public const int FILL = 3;

        private GenerationType() { }//私有构造函数，不可被实例化对象
    }
}
