using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
