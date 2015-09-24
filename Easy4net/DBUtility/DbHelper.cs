using System;
using System.Collections.Generic;
using System.Linq;
using Easy4net.Common;
using Easy4net.Context;

namespace Easy4net.DBUtility
{
	/// <summary>
	/// 数据库帮助类
	/// </summary>
    public class DBHelper
    {
        Session session;

		/// <summary>
		/// 根据数据库连接类型名创建一个数据库帮助类
		/// </summary>
		/// <param name="connName"></param>
        public DBHelper(string connName)
        {
            session = Session.NewInstance(connName);
        }

		/// <summary>
		/// 根据数据库连接类型名创建一个数据库帮助类
		/// </summary>
		/// <param name="connName"></param>
		/// <returns></returns>
        public static DBHelper getInstance(string connName)
        {
            return new DBHelper(connName);
        }

        /// <summary>
        /// 根据主键ID获取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public T Get<T>(object id) where T : new()
        {
            return session.Get<T>(id);
        }

        /// <summary>
        /// 插入对象数据
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="entity">需要插入的数据对象</param>
        /// <returns></returns>
        public int Save<T>(T entity)
        {
            return session.Insert<T>(entity);
        }

        /// <summary>
        /// 批量插入对象数据
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="entityList">需要插入的数据对象集合</param>
        /// <returns></returns>
        public int Save<T>(List<T> entityList)
        {
            return session.Insert<T>(entityList);
        }

        /// <summary>
        /// 更新对象数据
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="entity">需要更新的数据对象集合</param>
        /// <returns></returns>
        public int Update<T>(T entity)
        {
            return session.Update<T>(entity);
        }

        /// <summary>
        /// 批量更新对象数据
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="entityList">需要更新的数据对象集合</param>
        /// <returns></returns>
        public int Update<T>(List<T> entityList)
        {
            return session.Update<T>(entityList);
        }

        /// <summary>
        /// 删除对象数据
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="entity">需要删除的数据对象</param>
        /// <returns></returns>
        public int Remove<T>(T entity)
        {
            return session.Delete<T>(entity);
        }

        /// <summary>
        /// 批量删除对象数据
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="entityList">需要删除的数据对象集合</param>
        /// <returns></returns>
        public int Remove<T>(List<T> entityList)
        {
            return session.Delete<T>(entityList);
        }

        /// <summary>
        /// 根据主键ID删除数据
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public int Remove<T>(object id) where T : new()
        {
            return session.Delete<T>(id);
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSQL">SQL命令</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public int ExcuteSQL(string strSQL, ParamMap param)
        {
            return session.ExcuteSQL(strSQL, param);
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSQL">SQL命令</param>
        /// <returns></returns>
        public int ExcuteSQL(string strSQL)
        {
            return session.ExcuteSQL(strSQL);
        }

        /// <summary>
        /// 根据SQL查询数量
        /// </summary>
        /// <param name="strSQL">SQL命令</param>
        /// <returns></returns>
        public int Count(string strSQL)
        {
            return session.Count(strSQL);
        }

        /// <summary>
        /// 根据SQL查询记录数
        /// </summary>
        /// <param name="strSQL">SQL命令</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public int Count(string strSQL, ParamMap param)
        {
            return session.Count(strSQL, param);
        }

        /// <summary>
        /// 根据SQL查询数据
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="strSQL">SQL命令</param>
        /// <returns></returns>
        public List<T> FindBySql<T>(string strSQL) where T : new()
        {
            return session.Find<T>(strSQL);
        }

        /// <summary>
        /// 根据SQL查询数据
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="strSQL">SQL命令</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public List<T> FindBySql<T>(string strSQL, ParamMap param) where T : new()
        {
            return session.Find<T>(strSQL, param);
        }

        /// <summary>
        /// 分页查询返回分页对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="strSQL">SQL命令</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public PageResult<T> FindPage<T>(string strSQL, ParamMap param) where T : new()
        {
            return session.FindPage<T>(strSQL, param);
        }

        /// <summary>
        /// 查询一条数据
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="strSQL">SQL命令</param>
        /// <returns></returns>
        public T FindOne<T>(string strSQL) where T : new()
        {
            return session.Find<T>(strSQL).FirstOrDefault();
        }

        /// <summary>
        /// 查询一条数据
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="strSQL">SQL命令</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public T FindOne<T>(string strSQL, ParamMap param) where T : new()
        {
            return session.Find<T>(strSQL, param).FirstOrDefault();
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTransaction()
        {
            session.BeginTransaction();
        }

		/// <summary>
		/// 根据锁定行为开启事物
		/// </summary>
		/// <param name="level"></param>
        public void BeginTransaction(System.Data.IsolationLevel level)
        {
            session.BeginTransaction(level);
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTransaction()
        {
            session.Commit();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTransaction()
        {
            session.Rollback();
        }
    }
}

