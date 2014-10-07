using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy4net.EntityManager;
using System.Data;
using Easy4net.Common;

namespace Easy4net.DBUtility
{
    public class DBHelper
    {
        public IDbTransaction trans;
        public EntityManager.EntityManager entityManager;

        public DBHelper()
        {
            entityManager = EntityManagerFactory.CreateEntityManager();
        }

        public static DBHelper getInstance()
        {
            return new DBHelper();
        }

        /// <summary>
        /// 根据主键ID获取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public T Get<T>(object id) where T : new()
        {
            return entityManager.Get<T>(id);
        }

        /// <summary>
        /// 插入对象数据
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="entity">需要插入的数据对象</param>
        /// <returns></returns>
        public int Insert<T>(T entity) 
        {
            return entityManager.Insert<T>(entity);
        }

        /// <summary>
        /// 批量插入对象数据
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="entityList">需要插入的数据对象集合</param>
        /// <returns></returns>
        public int Insert<T>(List<T> entityList) 
        {
            return entityManager.Insert<T>(entityList);
        }

        /// <summary>
        /// 更新对象数据
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="entity">需要更新的数据对象集合</param>
        /// <returns></returns>
        public int Update<T>(T entity) 
        {
            return entityManager.Update<T>(entity);
        }

        /// <summary>
        /// 批量更新对象数据
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="entityList">需要更新的数据对象集合</param>
        /// <returns></returns>
        public int Update<T>(List<T> entityList) 
        {
            return entityManager.Update<T>(entityList);
        }

        /// <summary>
        /// 删除对象数据
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="entity">需要删除的数据对象</param>
        /// <returns></returns>
        public int Delete<T>(T entity)
        {
            return entityManager.Delete<T>(entity);
        }

        /// <summary>
        /// 批量删除对象数据
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="entityList">需要删除的数据对象集合</param>
        /// <returns></returns>
        public int Delete<T>(List<T> entityList)
        {
            return entityManager.Delete<T>(entityList);
        }

        /// <summary>
        /// 根据主键ID删除数据
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public int Delete<T>(object id) where T : new()
        {
            return entityManager.Delete<T>(id);
        }

        /// <summary>
        /// 根据SQL查询数量
        /// </summary>
        /// <param name="strSQL">SQL命令</param>
        /// <returns></returns>
        public int Count(string strSQL)
        {
            return entityManager.Count(strSQL);
        }

        /// <summary>
        /// 根据SQL查询记录数
        /// </summary>
        /// <param name="strSQL">SQL命令</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public int Count(string strSQL, ParamMap param)
        {
            return entityManager.Count(strSQL, param);
        }

        /// <summary>
        /// 根据SQL查询数据
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="strSQL">SQL命令</param>
        /// <returns></returns>
        public List<T> Find<T>(string strSQL) where T : new()
        {
            return entityManager.Find<T>(strSQL);
        }

        /// <summary>
        /// 根据SQL查询数据
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="strSQL">SQL命令</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public List<T> Find<T>(string strSQL, ParamMap param) where T : new()
        {
            return entityManager.Find<T>(strSQL, param);
        }

        /// <summary>
        /// 分页查询返回分页对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="strSQL">SQL命令</param>
        /// <returns></returns>
        PageResult<T> FindPage<T>(string strSQL) where T : new()
        {
            return entityManager.FindPage<T>(strSQL);
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
            return entityManager.FindPage<T>(strSQL, param);
        }

        /// <summary>
        /// 查询一条数据
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="strSQL">SQL命令</param>
        /// <returns></returns>
        public T FindOne<T>(string strSQL) where T : new()
        {
            return entityManager.Find<T>(strSQL).FirstOrDefault();
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
            return entityManager.Find<T>(strSQL, param).FirstOrDefault();
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTransaction()
        {
            trans = DbFactory.CreateDbTransaction();
            entityManager.Transaction = trans;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTransaction()
        {
            if (trans != null)
            {
                trans.Commit();
                trans.Dispose();
                trans = null;
                entityManager.Transaction = null;
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTransaction()
        {
            if (trans != null)
            {
                trans.Rollback();
                trans.Dispose();
                trans = null;
                entityManager.Transaction = null;
            }
        }
    }
}
