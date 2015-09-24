using System;

namespace Easy4net.DBUtility
{
	/// <summary>
	/// 数据类型转换帮助类
	/// </summary>
    public class DbTypeConvert
    {
		/// <summary>
		/// 转为Decimal类型
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
        static decimal ToDecimal(int value)
        {
            return Convert.ToDecimal(value);
        }
    }
}
