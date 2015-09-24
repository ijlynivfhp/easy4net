using System;
using System.Linq;

namespace Easy4net.Context
{
	/// <summary>
	/// 持久层对象获取帮助
	/// </summary>
    public class SessionFactory
    {
		/// <summary>
		/// 根据数据库类型名获取对应的持久层对象
		/// </summary>
		/// <param name="connName"></param>
		/// <returns></returns>
        public static Session GetSession(String connName)
        {
            Session session = SessionThreadLocal.Get();
            if (session == null)
            {
                session = Session.NewInstance(connName);
                SessionThreadLocal.Set(session);
            }
            else
            {
                session.ConnectDB(connName);
            }

            return session;
        }

		/// <summary>
		/// 获取MSSQL持久层对象
		/// </summary>
		/// <returns></returns>
        public static Session GetSession()
        {
            Session session = SessionThreadLocal.Get();
            if (session == null)
            {
                session = Session.NewInstance(null);
                SessionThreadLocal.Set(session);
            }

            return session;
        }
    }
}
