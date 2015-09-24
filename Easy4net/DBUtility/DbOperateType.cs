using System;
using System.ComponentModel;

namespace Easy4net.DBUtility
{
	/// <summary>
	/// 数据库表操作类型定义
	/// </summary>
    public enum DbOperateType
    {
		/// <summary>
		/// 插入操作
		/// </summary>
		[Description("插入操作")]
		INSERT,
		/// <summary>
		/// 更新操作
		/// </summary>
		[Description("更新操作")]
		UPDATE,
		/// <summary>
		/// 删除操作
		/// </summary>
		[Description("删除操作")]
		DELETE,
		/// <summary>
		/// 查询操作
		/// </summary>
		[Description("查询操作")]
		SELECT,
		/// <summary>
		/// 获取记录数操作
		/// </summary>
		[Description("获取记录数操作")]
		COUNT
    }
}
