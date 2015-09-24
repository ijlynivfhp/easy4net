using System;
using System.Linq;

namespace Easy4net.Context
{
    public class SessionFactory
    {
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
