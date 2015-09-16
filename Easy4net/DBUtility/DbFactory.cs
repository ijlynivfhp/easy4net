using System;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Data.Odbc;
using MySql.Data.MySqlClient;
using Easy4net.Common;

namespace Easy4net.DBUtility
{
    public class DbFactory
    {

        private DbFactory()
        {

        }

        public static DbFactory NewInstance(string connectionString, DatabaseType dbType)
        {
            DbFactory factory = new DbFactory();
            factory.connectionString = connectionString;
            factory.dbType = dbType;
            factory.DbParmChar = factory.CreateDbParmCharacter();

            return factory;
        }

        private string connectionString;
        private DatabaseType dbType;
        private string dbParmChar;

        public DatabaseType DbType
        {
            get { return dbType; }
            set { value = dbType; }
        }

        public string ConnectionString
        {
            get { return connectionString; }
            set { value = connectionString; }
        }

        public string DbParmChar
        {
            get { return dbParmChar; }
            set { dbParmChar = value; }
        }


        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来获取命令参数中的参数符号oracle为":",sqlserver为"@"
        /// </summary>
        /// <returns></returns>
        public string CreateDbParmCharacter()
        {
            string character = string.Empty;

            switch (dbType)
            {
                case DatabaseType.SQLSERVER:
                    character = "@";
                    break;
                case DatabaseType.ORACLE:
                    character = ":";
                    break;
                case DatabaseType.MYSQL:
                    character = "?";
                    break;
                case DatabaseType.ACCESS:
                    character = "@";
                    break;
                case DatabaseType.SQLITE:
                    character = "@";
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }

            return character;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型和传入的
        /// 数据库链接字符串来创建相应数据库连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public IDbConnection CreateDbConnection()
        {
            IDbConnection conn = null;
            switch (dbType)
            {
                case DatabaseType.SQLSERVER:
                    conn = new SqlConnection(connectionString);
                    break;
                case DatabaseType.ORACLE:
                    conn = new OracleConnection(connectionString);
                    break;
                case DatabaseType.MYSQL:
                    conn = new MySqlConnection(connectionString);
                    break;
                case DatabaseType.ACCESS:
                    conn = new OleDbConnection(connectionString);
                    break;
                /*case DatabaseType.SQLITE:
                    conn = new SQLiteConnection(connectionString);
                    break;*/
                default:
                    throw new Exception("数据库类型目前不支持！");
            }

            return conn;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库命令对象
        /// </summary>
        /// <returns></returns>
        public IDbCommand CreateDbCommand()
        {
            IDbCommand cmd = null;
            switch (dbType)
            {
                case DatabaseType.SQLSERVER:
                    cmd = new SqlCommand();
                    break;
                case DatabaseType.ORACLE:
                    cmd = new OracleCommand();
                    break;
                case DatabaseType.MYSQL:
                    cmd = new MySqlCommand();
                    break;
                case DatabaseType.ACCESS:
                    cmd = new OleDbCommand();
                    break;
               /* case DatabaseType.SQLITE:
                    cmd = new SQLiteCommand();
                    break; */
                default:
                    throw new Exception("数据库类型目前不支持！");
            }

            return cmd;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库适配器对象
        /// </summary>
        /// <returns></returns>
        public IDbDataAdapter CreateDataAdapter()
        {
            IDbDataAdapter adapter = null;
            switch (dbType)
            {
                case DatabaseType.SQLSERVER:
                    adapter = new SqlDataAdapter();
                    break;
                case DatabaseType.ORACLE:
                    adapter = new OracleDataAdapter();
                    break;
                case DatabaseType.MYSQL:
                    adapter = new MySqlDataAdapter();
                    break;
                case DatabaseType.ACCESS:
                    adapter = new OleDbDataAdapter();
                    break;
                /*case DatabaseType.SQLITE:
                    adapter = new SQLiteDataAdapter();
                    break;*/
                default:
                    throw new Exception("数据库类型目前不支持！");
            }

            return adapter;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 和传入的命令对象来创建相应数据库适配器对象
        /// </summary>
        /// <returns></returns>
        public IDbDataAdapter CreateDataAdapter(IDbCommand cmd)
        {
            IDbDataAdapter adapter = null;
            switch (dbType)
            {
                case DatabaseType.SQLSERVER:
                    adapter = new SqlDataAdapter((SqlCommand)cmd);
                    break;
                case DatabaseType.ORACLE:
                    adapter = new OracleDataAdapter((OracleCommand)cmd);
                    break;
                case DatabaseType.MYSQL:
                    adapter = new MySqlDataAdapter((MySqlCommand)cmd);
                    break;
                case DatabaseType.ACCESS:
                    adapter = new OleDbDataAdapter((OleDbCommand)cmd);
                    break;
                /*case DatabaseType.SQLITE:
                    adapter = new SQLiteDataAdapter((SQLiteCommand)cmd);
                    break;*/
                default: throw new Exception("数据库类型目前不支持！");
            }

            return adapter;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public IDbDataParameter CreateDbParameter()
        {
            IDbDataParameter param = null;
            switch (dbType)
            {
                case DatabaseType.SQLSERVER:
                    param = new SqlParameter();
                    break;
                case DatabaseType.ORACLE:
                    param = new OracleParameter();
                    break;
                case DatabaseType.MYSQL:
                    param = new MySqlParameter();
                    break;
                case DatabaseType.ACCESS:
                    param = new OleDbParameter();
                    break;
                /*case DatabaseType.SQLITE:
                    param = new SQLiteParameter();
                    break;*/
                default:
                    throw new Exception("数据库类型目前不支持！");
            }

            return param;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public IDbDataParameter CreateDbParameter(string paramName, object value)
        {
            if (dbType == DatabaseType.ACCESS || dbType == DatabaseType.SQLITE)
            {
                paramName = "@" + paramName;
            }

            IDbDataParameter param = CreateDbParameter();
            param.ParameterName = paramName;
            param.Value = value;

            return param;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public IDbDataParameter CreateDbParameter(string paramName, object value, DbType _dataType)
        {
            if (dbType == DatabaseType.ACCESS || dbType == DatabaseType.SQLITE)
            {
                paramName = "@" + paramName;
            }

            IDbDataParameter param = CreateDbParameter();
            param.DbType = _dataType;
            param.ParameterName = paramName;
            param.Value = value;

            return param;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public IDbDataParameter CreateDbParameter(string paramName, object value, ParameterDirection direction)
        {
            if (dbType == DatabaseType.ACCESS || dbType == DatabaseType.SQLITE)
            {
                paramName = "@" + paramName;
            }

            IDbDataParameter param = CreateDbParameter();
            param.Direction = direction;
            param.ParameterName = paramName;
            param.Value = value;

            return param;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public IDbDataParameter CreateDbParameter(string paramName, object value, int size, ParameterDirection direction)
        {
            if (dbType == DatabaseType.ACCESS || dbType == DatabaseType.SQLITE)
            {
                paramName = "@" + paramName;
            }

            IDbDataParameter param = CreateDbParameter();
            param.Direction = direction;
            param.ParameterName = paramName;
            param.Value = value;
            param.Size = size;

            return param;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public IDbDataParameter CreateDbOutParameter(string paramName, int size)
        {
            if (dbType == DatabaseType.ACCESS || dbType == DatabaseType.SQLITE)
            {
                paramName = "@" + paramName;
            }

            IDbDataParameter param = CreateDbParameter();
            param.Direction = ParameterDirection.Output;
            param.ParameterName = paramName;
            param.Size = size;

            return param;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public IDbDataParameter CreateDbParameter(string paramName, object value, DbType _dataType, ParameterDirection direction)
        {
            if (dbType == DatabaseType.ACCESS || dbType == DatabaseType.SQLITE)
            {
                paramName = "@" + paramName;
            }

            IDbDataParameter param = CreateDbParameter();
            param.Direction = direction;
            param.DbType = _dataType;
            param.ParameterName = paramName;
            param.Value = value;

            return param;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 和传入的参数来创建相应数据库的参数数组对象
        /// </summary>
        /// <returns></returns>
        public IDbDataParameter[] CreateDbParameters(int size)
        {
            int i = 0;
            IDbDataParameter[] param = null;
            switch (dbType)
            {
                case DatabaseType.SQLSERVER:
                    param = new SqlParameter[size];
                    while (i < size) { param[i] = new SqlParameter(); i++; }
                    break;
                case DatabaseType.ORACLE:
                    param = new OracleParameter[size];
                    while (i < size) { param[i] = new OracleParameter(); i++; }
                    break;
                case DatabaseType.MYSQL:
                    param = new MySqlParameter[size];
                    while (i < size) { param[i] = new MySqlParameter(); i++; }
                    break;
                case DatabaseType.ACCESS:
                    param = new OleDbParameter[size];
                    while (i < size) { param[i] = new OleDbParameter(); i++; }
                    break;
                /*case DatabaseType.SQLITE:
                    param = new SQLiteParameter[size];
                    while (i < size) { param[i] = new SQLiteParameter(); i++; }
                    break;*/
                default:
                    throw new Exception("数据库类型目前不支持！");

            }

            return param;
        }

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的事物对象
        /// </summary>
        /// <returns></returns>
        public IDbTransaction CreateDbTransaction()
        {
            IDbConnection conn = CreateDbConnection();

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            return conn.BeginTransaction();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public IDbTransaction CreateDbTransaction(System.Data.IsolationLevel level)
        {
            IDbConnection conn = CreateDbConnection();

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            return conn.BeginTransaction(level);
        } 
    }
}