using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Easy4net.Common;

namespace Easy4net.EntityManager
{
    public interface EntityManager
    {
        IDbTransaction Transaction{get;set;}

        int Insert<T>(T entity);

        int Insert<T>(List<T> entityList);

        int Update<T>(T entity);

        int Update<T>(List<T> entityList);

        int ExcuteSQL(string strSQL, ParamMap param);

        int Delete<T>(T entity);

        int Delete<T>(List<T> entityList);

        int Delete<T>(object id) where T : new();

        int Delete<T>(object[] ids) where T : new();

        int Count(string strSQL);
        int Count(string strSql, ParamMap param);

        List<T> Find<T>(string strSql) where T : new();

        List<T> Find<T>(string strSql, ParamMap param) where T : new();

        T Get<T>(object id) where T : new();        
    }
}
