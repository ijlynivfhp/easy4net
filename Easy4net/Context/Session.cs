using Easy4net.Common;
using Easy4net.DBUtility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Easy4net.Context
{
	/// <summary>
	/// 持久层实体类
	/// </summary>
    public class Session
    {
		/// <summary>
		/// SQLite数据库帮助对象
		/// </summary>
        public SQLiteHelper sqliteHelper = new SQLiteHelper();
		/// <summary>
		/// 数据库连接字符串
		/// </summary>
        private string connectionString = string.Empty;
		/// <summary>
		/// 支持数据库字符串
		/// </summary>
        private string provider = string.Empty;

		/// <summary>
		/// 数据库事物对象
		/// </summary>
        private IDbTransaction m_Transaction = null;
		/// <summary>
		/// 数据库类型
		/// </summary>
        private DatabaseType dataBaseType = DatabaseType.SQLSERVER;

		/// <summary>
		/// 数据库工厂对象,生成相应的数据库操作对象
		/// </summary>
        private DbFactory dbFactory;

		/// <summary>
		/// /数据库工厂对象,生成相应的数据库操作对象
		/// </summary>
        public DbFactory DbFactory
        {
            get { return dbFactory; }
            set { value = dbFactory; }
        }

        private Session() { }

		/// <summary>
		/// 根据数据库类型名创建一个持久层对象
		/// </summary>
		/// <param name="connName"></param>
		/// <returns></returns>
        public static Session NewInstance(string connName)
        {
            Session session = new Session();

            if (!string.IsNullOrEmpty(connName))
            {
                session.ConnectDB(connName);
            }            

            return session;
        }

		/// <summary>
		/// 获取当前的持久层对象
		/// </summary>
		/// <returns></returns>
        public static Session GetCurrentSession()
        {
            Session session = SessionThreadLocal.Get();
            return session;
        }

		/// <summary>
		/// 根据连接类型名进行连接配置
		/// </summary>
		/// <param name="connName"></param>
        public void ConnectDB(string connName)
        {
            if (connName == null) return;

            ConnectionStringSettings connSettings = ConfigurationManager.ConnectionStrings[connName];

            provider = connSettings.ProviderName;
            connectionString = connSettings.ConnectionString;

            if (provider.Contains("MySqlClient"))
            {
                dataBaseType = DatabaseType.MYSQL;
            }
            else if (provider.Contains("SqlClient"))
            {
                dataBaseType = DatabaseType.SQLSERVER;
            }            
            else if (provider.Contains("OracleClient"))
            {
                dataBaseType = DatabaseType.ORACLE;
            }
            else if (provider.Contains("SQLite"))
            {
                dataBaseType = DatabaseType.SQLITE;
            }

            dbFactory = DbFactory.NewInstance(connectionString, dataBaseType);
        }

		/// <summary>
		/// 开启事物处理功能
		/// </summary>
        public void BeginTransaction()
        {
            m_Transaction = dbFactory.CreateDbTransaction();
        }

		/// <summary>
		/// 根据事物锁定行为开启事物
		/// </summary>
		/// <param name="level"></param>
        public void BeginTransaction(System.Data.IsolationLevel level)
        {
            m_Transaction = dbFactory.CreateDbTransaction(level);
        }

		/// <summary>
		/// 提交事物
		/// </summary>
        public void Commit()
        {
            if (m_Transaction != null && m_Transaction.Connection != null)
            {
                if (m_Transaction.Connection.State != ConnectionState.Closed)
                {
                    m_Transaction.Commit();
                    m_Transaction = null;
                }
            }
        }

		/// <summary>
		/// 事物回滚
		/// </summary>
        public void Rollback()
        {
            if (m_Transaction != null && m_Transaction.Connection != null)
            {
                if (m_Transaction.Connection.State != ConnectionState.Closed)
                {
                    m_Transaction.Rollback();
                    m_Transaction = null;
                }
            }
        }

		/// <summary>
		/// 获取当前的事物
		/// </summary>
		/// <returns></returns>
        private IDbTransaction GetTransaction()
        {
            return m_Transaction;
        }

        #region 将实体数据保存到数据库
		/// <summary>
		/// Insert插入操作
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity">数据库表实体对象</param>
		/// <returns></returns>
        public int Insert<T>(T entity)
        {
            if (entity == null) return 0;

            object val = 0;

            IDbTransaction transaction = null;
            IDbConnection connection = null;
            try
            {
                //获取数据库连接，如果开启了事务，从事务中获取
                connection = GetConnection();
                transaction = GetTransaction();

                Type classType = entity.GetType();
                //从实体对象的属性配置上获取对应的表信息
                PropertyInfo[] properties = ReflectionHelper.GetProperties(classType);
                TableInfo tableInfo = EntityHelper.GetTableInfo(entity, DbOperateType.INSERT, properties);

                //获取SQL语句
                String strSql = EntityHelper.GetInsertSql(tableInfo);

                //获取参数
                IDbDataParameter[] parms = tableInfo.GetParameters();
                //执行Insert命令
                val = AdoHelper.ExecuteScalar(dbFactory, connection, transaction, CommandType.Text, strSql, parms);

                //把自动生成的主键ID赋值给返回的对象
                if (!tableInfo.NoAutomaticKey)
                {
                    if (dataBaseType == DatabaseType.SQLSERVER || dataBaseType == DatabaseType.MYSQL || dataBaseType == DatabaseType.SQLITE)
                    {
                        PropertyInfo propertyInfo = EntityHelper.GetPrimaryKeyPropertyInfo(entity, properties);
                        ReflectionHelper.SetPropertyValue(entity, propertyInfo, val);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (m_Transaction == null)
                {
                    connection.Close();
                }
            }

            return Convert.ToInt32(val);
        }
        #endregion

        #region 批量保存
		/// <summary>
		/// 批量进行Insert插入操作
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entityList">数据库表实体对象集合</param>
		/// <returns></returns>
        public int Insert<T>(List<T> entityList)
        {
            if (entityList == null || entityList.Count == 0) return 0;

            object val = 0;

            IDbTransaction transaction = null;
            IDbConnection connection = null;
            try
            {
                //获取数据库连接，如果开启了事务，从事务中获取
                connection = GetConnection();
                transaction = GetTransaction();

                //从实体对象的属性配置上获取对应的表信息
                T firstEntity = entityList[0];
                Type classType = firstEntity.GetType();

                PropertyInfo[] properties = ReflectionHelper.GetProperties(classType);
                TableInfo tableInfo = EntityHelper.GetTableInfo(firstEntity, DbOperateType.INSERT, properties);

                //获取SQL语句
                String strSQL = EntityHelper.GetInsertSql(tableInfo);
                foreach (T entity in entityList)
                {
                    //从实体对象的属性配置上获取对应的表信息
                    tableInfo = EntityHelper.GetTableInfo(entity, DbOperateType.INSERT, properties);

                    //获取参数
                    IDbDataParameter[] parms = tableInfo.GetParameters();
                    //执行Insert命令
                    val = AdoHelper.ExecuteScalar(dbFactory, connection, transaction, CommandType.Text, strSQL, parms);

                    //Access数据库执行不需要命名参数
                    if (dataBaseType == DatabaseType.ACCESS)
                    {
                        //如果是Access数据库，另外执行获取自动生成的ID
                        String autoSql = EntityHelper.GetAutoSql(dbFactory.DbType);
                        val = AdoHelper.ExecuteScalar(dbFactory, connection, transaction, CommandType.Text, autoSql);
                    }

                    //把自动生成的主键ID赋值给返回的对象
                    if (!tableInfo.NoAutomaticKey)
                    {

                        //把自动生成的主键ID赋值给返回的对象
                        if (dataBaseType == DatabaseType.SQLSERVER || dataBaseType == DatabaseType.MYSQL || dataBaseType == DatabaseType.ACCESS || dataBaseType == DatabaseType.SQLITE)
                        {
                            PropertyInfo propertyInfo = EntityHelper.GetPrimaryKeyPropertyInfo(entity, properties);
                            ReflectionHelper.SetPropertyValue(entity, propertyInfo, val);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (m_Transaction == null)
                {
                    connection.Close();
                }
            }

            return Convert.ToInt32(val);
        }
        #endregion

        #region 将实体数据修改到数据库
		/// <summary>
		/// Update更新操作
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity">数据库表实体对象</param>
		/// <returns></returns>
        public int Update<T>(T entity)
        {
            if (entity == null) return 0;

            object val = 0;

            IDbTransaction transaction = null;
            IDbConnection connection = null;
            try
            {
                //获取数据库连接，如果开启了事务，从事务中获取
                connection = GetConnection();
                transaction = GetTransaction();

                Type classType = entity.GetType();
                PropertyInfo[] properties = ReflectionHelper.GetProperties(classType);
                TableInfo tableInfo = EntityHelper.GetTableInfo(entity, DbOperateType.UPDATE, properties);

                String strSQL = EntityHelper.GetUpdateSql(tableInfo);

                List<IDbDataParameter> paramsList = tableInfo.GetParameterList();
                IDbDataParameter dbParameter = dbFactory.CreateDbParameter(tableInfo.Id.Key, tableInfo.Id.Value);
                paramsList.Add(dbParameter);

                IDbDataParameter[] parms = tableInfo.GetParameters(paramsList);
                val = AdoHelper.ExecuteNonQuery(dbFactory, connection, transaction, CommandType.Text, strSQL, parms);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (m_Transaction == null)
                {
                    connection.Close();
                }
            }

            return Convert.ToInt32(val);
        }
        #endregion

        #region 批量更新
		/// <summary>
		/// 批量进行Update更新操作
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entityList">数据库表实体对象集合</param>
		/// <returns></returns>
        public int Update<T>(List<T> entityList)
        {
            if (entityList == null || entityList.Count == 0) return 0;

            object val = 0;
            IDbTransaction transaction = null;
            IDbConnection connection = null;
            try
            {
                //获取数据库连接，如果开启了事务，从事务中获取
                connection = GetConnection();
                transaction = GetTransaction();

                T firstEntity = entityList[0];
                Type classType = firstEntity.GetType();

                PropertyInfo[] properties = ReflectionHelper.GetProperties(firstEntity.GetType());
                TableInfo tableInfo = EntityHelper.GetTableInfo(firstEntity, DbOperateType.UPDATE, properties);

                String strSQL = EntityHelper.GetUpdateSql(tableInfo);
                
                /*tableInfo.Columns.Add(tableInfo.Id.Key, tableInfo.Id.Value);
                IDbDataParameter[] parms = tableInfo.GetParameters();*/

                foreach (T entity in entityList)
                {
                    tableInfo = EntityHelper.GetTableInfo(entity, DbOperateType.UPDATE, properties);

                    List<IDbDataParameter> paramsList = tableInfo.GetParameterList();
                    IDbDataParameter dbParameter = dbFactory.CreateDbParameter(tableInfo.Id.Key, tableInfo.Id.Value);
                    paramsList.Add(dbParameter);

                    IDbDataParameter[] parms = tableInfo.GetParameters(paramsList);
                    val = AdoHelper.ExecuteNonQuery(dbFactory, connection, transaction, CommandType.Text, strSQL, parms);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (m_Transaction == null)
                {
                    connection.Close();
                }
            }

            return Convert.ToInt32(val);
        }
        #endregion

        #region 执行SQL语句
		/// <summary>
		/// 执行一条带有参数集合的SQL语句
		/// </summary>
		/// <param name="strSQL">SQL语句</param>
		/// <param name="param">参数集合</param>
		/// <returns></returns>
        public int ExcuteSQL(string strSQL, ParamMap param)
        {
            object val = 0;
            IDbTransaction transaction = null;
            IDbConnection connection = null;
            try
            {
                //获取数据库连接，如果开启了事务，从事务中获取
                connection = GetConnection();
                transaction = GetTransaction();

                IDbDataParameter[] parms = param.toDbParameters();

                if (dataBaseType == DatabaseType.ACCESS)
                {
                    strSQL = SQLBuilderHelper.builderAccessSQL(strSQL, parms);
                    val = AdoHelper.ExecuteNonQuery(dbFactory, connection, transaction, CommandType.Text, strSQL);
                }
                else
                {
                    val = AdoHelper.ExecuteNonQuery(dbFactory, connection, transaction, CommandType.Text, strSQL, parms);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (m_Transaction == null)
                {
                    connection.Close();
                }
            }

            return Convert.ToInt32(val);
        }
        #endregion

        #region 执行SQL语句
		/// <summary>
		/// 执行一条SQL语句
		/// </summary>
		/// <param name="strSQL"></param>
		/// <returns></returns>
        public int ExcuteSQL(string strSQL)
        {
            object val = 0;
            IDbTransaction transaction = null;
            IDbConnection connection = null;
            try
            {
                //获取数据库连接，如果开启了事务，从事务中获取
                connection = GetConnection();
                transaction = GetTransaction();

                val = AdoHelper.ExecuteNonQuery(dbFactory, connection, transaction, CommandType.Text, strSQL);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (m_Transaction == null)
                {
                    connection.Close();
                }
            }

            return Convert.ToInt32(val);
        }
        #endregion

        #region 删除实体对应数据库中的数据
		/// <summary>
		/// 进行Delete删除操作
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity">数据库表实体对象</param>
		/// <returns></returns>
        public int Delete<T>(T entity)
        {
            if (entity == null) return 0;

            object val = 0;
            IDbTransaction transaction = null;
            IDbConnection connection = null;
            try
            {
                //获取数据库连接，如果开启了事务，从事务中获取
                connection = GetConnection();
                transaction = GetTransaction();

                Type classType = entity.GetType();
                PropertyInfo[] properties = ReflectionHelper.GetProperties(classType);
                TableInfo tableInfo = EntityHelper.GetTableInfo(entity, DbOperateType.DELETE, properties);

                IDbDataParameter[] parms = dbFactory.CreateDbParameters(1);
                parms[0].ParameterName = tableInfo.Id.Key;
                parms[0].Value = tableInfo.Id.Value;

                String strSQL = EntityHelper.GetDeleteByIdSql(tableInfo);

                if (dataBaseType == DatabaseType.ACCESS)
                {
                    strSQL = SQLBuilderHelper.builderAccessSQL(classType, tableInfo, strSQL, parms);
                    val = AdoHelper.ExecuteNonQuery(dbFactory, connection, transaction, CommandType.Text, strSQL);
                }
                else
                {
                    val = AdoHelper.ExecuteNonQuery(dbFactory, connection, transaction, CommandType.Text, strSQL, parms);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (m_Transaction == null)
                {
                    connection.Close();
                }
            }

            return Convert.ToInt32(val);
        }
        #endregion

        #region 批量删除
		/// <summary>
		/// 批量进行Delete删除操作
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entityList">数据库表实体对象集合</param>
		/// <returns></returns>
        public int Delete<T>(List<T> entityList)
        {
            if (entityList == null || entityList.Count == 0) return 0;

            object val = 0;
            IDbTransaction transaction = null;
            IDbConnection connection = null;
            try
            {
                //获取数据库连接，如果开启了事务，从事务中获取
                connection = GetConnection();
                transaction = GetTransaction();

                T firstEntity = entityList[0];
                Type classType = firstEntity.GetType();

                PropertyInfo[] properties = ReflectionHelper.GetProperties(firstEntity.GetType());
                TableInfo tableInfo = EntityHelper.GetTableInfo(firstEntity, DbOperateType.DELETE, properties);

                String strSQL = EntityHelper.GetDeleteByIdSql(tableInfo);

                foreach (T entity in entityList)
                {
                    tableInfo = EntityHelper.GetTableInfo(entity, DbOperateType.DELETE, properties);

                    IDbDataParameter[] parms = dbFactory.CreateDbParameters(1);
                    parms[0].ParameterName = tableInfo.Id.Key;
                    parms[0].Value = tableInfo.Id.Value;

                    if (dataBaseType == DatabaseType.ACCESS)
                    {
                        strSQL = SQLBuilderHelper.builderAccessSQL(classType, tableInfo, strSQL, parms);
                        val = AdoHelper.ExecuteNonQuery(dbFactory, connection, transaction, CommandType.Text, strSQL);
                    }
                    else
                    {
                        val = AdoHelper.ExecuteNonQuery(dbFactory, connection, transaction, CommandType.Text, strSQL, parms);
                    }

                    //val = AdoHelper.ExecuteNonQuery(connection, transaction, CommandType.Text, strSQL, parms);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (m_Transaction == null)
                {
                    connection.Close();
                }
            }

            return Convert.ToInt32(val);
        }
        #endregion

        #region 根据主键id删除实体对应数据库中的数据
		/// <summary>
		/// 根据主键值删除实体类对应数据库中的数据
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="id">主键值</param>
		/// <returns></returns>
        public int Delete<T>(object id) where T : new()
        {
            object val = 0;
            IDbTransaction transaction = null;
            IDbConnection connection = null;
            try
            {
                //获取数据库连接，如果开启了事务，从事务中获取
                connection = GetConnection();
                transaction = GetTransaction();

                T entity = new T();
                Type classType = entity.GetType();

                PropertyInfo[] properties = ReflectionHelper.GetProperties(entity.GetType());
                TableInfo tableInfo = EntityHelper.GetTableInfo(entity, DbOperateType.DELETE, properties);

                String strSQL = EntityHelper.GetDeleteByIdSql(tableInfo);

                IDbDataParameter[] parms = dbFactory.CreateDbParameters(1);
                parms[0].ParameterName = tableInfo.Id.Key;
                parms[0].Value = id;

                if (dataBaseType == DatabaseType.ACCESS)
                {
                    strSQL = SQLBuilderHelper.builderAccessSQL(classType, tableInfo, strSQL, parms);
                    val = AdoHelper.ExecuteNonQuery(dbFactory, connection, transaction, CommandType.Text, strSQL);
                }
                else
                {
                    val = AdoHelper.ExecuteNonQuery(dbFactory, connection, transaction, CommandType.Text, strSQL, parms);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (m_Transaction == null)
                {
                    connection.Close();
                }
            }

            return Convert.ToInt32(val);
        }
        #endregion

        #region 批量根据主键id删除数据
		/// <summary>
		/// 根据主键ID集合进行删除操作
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="ids"></param>
		/// <returns></returns>
        public int Delete<T>(object[] ids) where T : new()
        {
            if (ids == null || ids.Length == 0) return 0;

            object val = 0;
            IDbTransaction transaction = null;
            IDbConnection connection = null;
            try
            {
                //获取数据库连接，如果开启了事务，从事务中获取
                connection = GetConnection();
                transaction = GetTransaction();

                T entity = new T();
                Type classType = entity.GetType();

                PropertyInfo[] properties = ReflectionHelper.GetProperties(entity.GetType());
                TableInfo tableInfo = EntityHelper.GetTableInfo(entity, DbOperateType.DELETE, properties);

                String strSQL = EntityHelper.GetDeleteByIdSql(tableInfo);

                foreach (object id in ids)
                {
                    tableInfo = EntityHelper.GetTableInfo(entity, DbOperateType.DELETE, properties);

                    IDbDataParameter[] parms = dbFactory.CreateDbParameters(1);
                    parms[0].ParameterName = tableInfo.Id.Key;
                    parms[0].Value = id;

                    val = AdoHelper.ExecuteNonQuery(dbFactory, connection, transaction, CommandType.Text, strSQL, parms);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (m_Transaction == null)
                {
                    connection.Close();
                }
            }

            return Convert.ToInt32(val);
        }
        #endregion

        #region 通过自定义SQL语句查询记录数
		/// <summary>
		/// 根据SQL语句获取记录数
		/// </summary>
		/// <param name="strSQL"></param>
		/// <returns></returns>
        public int Count(string strSQL)
        {
            int count = 0;
            IDbConnection connection = null;
            bool closeConnection = GetWillConnectionState();
            try
            {
                connection = GetConnection();
                count = Convert.ToInt32(AdoHelper.ExecuteScalar(dbFactory, connection, CommandType.Text, strSQL));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (closeConnection)
                {
                    connection.Close();
                }
            }

            return count;
        }
        #endregion

        #region 通过自定义SQL语句查询记录数
		/// <summary>
		/// 通过自定义SQL语句查询记录数
		/// </summary>
		/// <param name="strSql"></param>
		/// <param name="param">参数集合</param>
		/// <returns></returns>
        public int Count(string strSql, ParamMap param)
        {
            int count = 0;
            IDbConnection connection = null;
            bool closeConnection = GetWillConnectionState();
            try
            {
                connection = GetConnection();

                strSql = strSql.ToLower();
                String columns = SQLBuilderHelper.fetchColumns(strSql);

                if (dataBaseType == DatabaseType.ACCESS)
                {
                    strSql = SQLBuilderHelper.builderAccessSQL(strSql, param.toDbParameters());
                    count = Convert.ToInt32(AdoHelper.ExecuteScalar(dbFactory, connection, CommandType.Text, strSql));
                }
                else 
                {
                    count = Convert.ToInt32(AdoHelper.ExecuteScalar(dbFactory, connection, CommandType.Text, strSql, param.toDbParameters()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (closeConnection)
                {
                    connection.Close();
                }
            }

            return count;
        }
        #endregion

        #region 通过自定义SQL语句查询数据
		/// <summary>
		/// 通过自定义SQL语句查询数据
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="strSql"></param>
		/// <returns></returns>
        public List<T> Find<T>(string strSql) where T : new()
        {
            List<T> list = new List<T>();
            IDataReader sdr = null;
            IDbConnection connection = null;
            try
            {
                connection = GetConnection();
                bool closeConnection = GetWillConnectionState();

                strSql = strSql.ToLower();
                String columns = SQLBuilderHelper.fetchColumns(strSql);

                T entity = new T();
                PropertyInfo[] properties = ReflectionHelper.GetProperties(entity.GetType());
                TableInfo tableInfo = EntityHelper.GetTableInfo(entity, DbOperateType.SELECT, properties);

                sdr = AdoHelper.ExecuteReader(dbFactory, closeConnection, connection, CommandType.Text, strSql, null);
                list = EntityHelper.toList<T>(sdr, tableInfo, properties);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sdr != null) sdr.Close();
            }

            return list;
        }
        #endregion

        #region 通过自定义SQL语句查询数据
		/// <summary>
		/// 通过自定义SQL语句查询数据
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="strSQL"></param>
		/// <param name="param"></param>
		/// <returns></returns>
        public List<T> Find<T>(string strSQL, ParamMap param) where T : new()
        {
            List<T> list = new List<T>();
            IDataReader sdr = null;
            IDbConnection connection = null;
            try
            {
                connection = GetConnection();
                bool closeConnection = GetWillConnectionState();

                string lowerSQL = strSQL.ToLower();
                String columns = SQLBuilderHelper.fetchColumns(lowerSQL);

                T entity = new T();
                Type classType = entity.GetType();
                PropertyInfo[] properties = ReflectionHelper.GetProperties(classType);
                TableInfo tableInfo = EntityHelper.GetTableInfo(entity, DbOperateType.SELECT, properties);

                if (param.IsPage && !SQLBuilderHelper.isPage(lowerSQL))
                {
                    strSQL = SQLBuilderHelper.builderPageSQL(strSQL, param.OrderFields, param.IsDesc);
                }

                if (dataBaseType == DatabaseType.ACCESS)
                {
                    strSQL = SQLBuilderHelper.builderAccessPageSQL(strSQL, param);
                }

                sdr = AdoHelper.ExecuteReader(dbFactory, closeConnection, connection, CommandType.Text, strSQL, param.toDbParameters());

                list = EntityHelper.toList<T>(sdr, tableInfo, properties);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sdr != null) sdr.Close();
            }

            return list;
        }
        #endregion

        #region 分页查询返回分页结果
		/// <summary>
		/// 分页查询返回分页结果
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="strSQL"></param>
		/// <param name="param"></param>
		/// <returns></returns>
        public PageResult<T> FindPage<T>(string strSQL, ParamMap param) where T : new()
        {
            PageResult<T> pageResult = new PageResult<T>();
            List<T> list = new List<T>();
            IDataReader sdr = null;
            IDbConnection connection = null;
            try
            {
                connection = GetConnection();
                bool closeConnection = GetWillConnectionState();

                strSQL = strSQL.ToLower();
                String countSQL = SQLBuilderHelper.builderCountSQL(strSQL);
                String columns = SQLBuilderHelper.fetchColumns(strSQL);

                int count = this.Count(countSQL, param);

                T entity = new T();
                Type classType = entity.GetType();

                PropertyInfo[] properties = ReflectionHelper.GetProperties(classType);
                TableInfo tableInfo = EntityHelper.GetTableInfo(entity, DbOperateType.SELECT, properties);

                if (param.IsPage && !SQLBuilderHelper.isPage(strSQL))
                {
                    strSQL = SQLBuilderHelper.builderPageSQL(strSQL, param.OrderFields, param.IsDesc);
                }

                if (dataBaseType == DatabaseType.ACCESS)
                {
                    if (param.getInt("page_offset") > count)
                    {
                        int limit = param.getInt("page_limit") + count - param.getInt("page_offset");
                        if (limit > 0)
                        {
                            strSQL = SQLBuilderHelper.builderAccessPageSQL(strSQL, param, limit);
                            sdr = AdoHelper.ExecuteReader(dbFactory, closeConnection, connection, CommandType.Text, strSQL, param.toDbParameters());
                            list = EntityHelper.toList<T>(sdr, tableInfo, properties);
                        }
                    }
                    else
                    {
                        strSQL = SQLBuilderHelper.builderAccessPageSQL(strSQL, param);
                        sdr = AdoHelper.ExecuteReader(dbFactory, closeConnection, connection, CommandType.Text, strSQL, param.toDbParameters());
                        list = EntityHelper.toList<T>(sdr, tableInfo, properties);
                    }
                }
                else
                {
                    sdr = AdoHelper.ExecuteReader(dbFactory, closeConnection, connection, CommandType.Text, strSQL, param.toDbParameters());
                    list = EntityHelper.toList<T>(sdr, tableInfo, properties);
                }
                pageResult.Total = count;
                pageResult.DataList = list;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sdr != null) sdr.Close();
            }

            return pageResult;
        }
        #endregion

        #region 通过主键ID查询数据
		/// <summary>
		/// 通过主键ID查询数据
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="id"></param>
		/// <returns></returns>
        public T Get<T>(object id) where T : new()
        {
            List<T> list = new List<T>();

            IDataReader sdr = null;
            IDbConnection connection = null;
            try
            {
                connection = GetConnection();
                bool closeConnection = GetWillConnectionState();

                T entity = new T();
                Type classType = entity.GetType();
                PropertyInfo[] properties = ReflectionHelper.GetProperties(classType);

                TableInfo tableInfo = EntityHelper.GetTableInfo(entity, DbOperateType.SELECT, properties);

                IDbDataParameter[] parms = dbFactory.CreateDbParameters(1);
                parms[0].ParameterName = tableInfo.Id.Key;
                parms[0].Value = id;

                String strSQL = EntityHelper.GetFindByIdSql(tableInfo);
                sdr = AdoHelper.ExecuteReader(dbFactory, closeConnection, connection, CommandType.Text, strSQL, parms);

                list = EntityHelper.toList<T>(sdr, tableInfo, properties);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sdr != null) sdr.Close();
            }

            return list.FirstOrDefault();
        }
        #endregion

		/// <summary>
		/// 获取数据库连接
		/// </summary>
		/// <returns></returns>
        private IDbConnection GetConnection()
        {
            //获取数据库连接，如果开启了事务，从事务中获取
            IDbConnection connection = null;
            if (m_Transaction != null)
            {
                connection = m_Transaction.Connection;
            }
            else
            {
                connection = dbFactory.CreateDbConnection();
            }

            return connection;
        }

		/// <summary>
		/// 获取当前事物是否为空
		/// </summary>
		/// <returns></returns>
        private bool GetWillConnectionState()
        {
            return m_Transaction == null;
        }       
    }

	/// <summary>
	/// SQLite数据库帮助类
	/// </summary>
    public class SQLiteHelper
    {    
		/// <summary>
		/// 获取是否存在指定的表
		/// </summary>
		/// <param name="tableName"></param>
		/// <returns></returns>
        public bool IsExistsTable(string tableName)
        {
            Session session = SessionThreadLocal.Get();
            string strSQL = "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='" + tableName + "'";
            int count = session.Count(strSQL);

            return count > 0;
        }

		/// <summary>
		/// 根据SQL语句创建一个表
		/// </summary>
		/// <param name="strSQL"></param>
        public void CreateTable(string strSQL)
        {
            try
            {
                Session session = SessionThreadLocal.Get();
                session.ExcuteSQL(strSQL);
            }
            catch (Exception e)
            {
                throw new Exception("创建表失败", e);
            }            
        }
    }
}
