﻿using System;
using System.Threading;

namespace Easy4net.Context
{
    public class SessionThreadLocal
    {
        private static ThreadLocal<Session> m_SessionLocal = new ThreadLocal<Session>();

        public static void Set(Session session)
        {
            m_SessionLocal.Value = session;
        }

        public static Session Get()
        {
            return m_SessionLocal.Value;
        }

        public static void Clear()
        {
            m_SessionLocal.Value = null;
        }
    }
}
