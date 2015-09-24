using System;
using System.Threading;

namespace Easy4net.Context
{
	/// <summary>
	/// 持久层线程池
	/// </summary>
    public class SessionThreadLocal
    {
        private static ThreadLocal<Session> m_SessionLocal = new ThreadLocal<Session>();

		/// <summary>
		/// 设置持久层到线程池
		/// </summary>
		/// <param name="session"></param>
        public static void Set(Session session)
        {
            m_SessionLocal.Value = session;
        }

		/// <summary>
		/// 从线程池获取持久层
		/// </summary>
		/// <returns></returns>
        public static Session Get()
        {
            return m_SessionLocal.Value;
        }

		/// <summary>
		/// 清空线程池
		/// </summary>
        public static void Clear()
        {
            m_SessionLocal.Value = null;
        }
    }
}
