using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DB.SQLITE
{
    public abstract class SQLiteObject
    {
        public SqlLiteAdoSession ErrorLogsDB
        {
            get
            {
                return SqliteAdoSessionManager.Current.ErrorLogsDB;
            }
        }

        public SqlLiteAdoSession OperLogsDB
        {
            get
            {
                return SqliteAdoSessionManager.Current.OperLogsDB;
            }
        }
    }
    public sealed class SqliteAdoSessionManager
    {
        [ThreadStatic]
        private static SqliteAdoSessionManager _Instance;
        public static SqliteAdoSessionManager Current
        {
            get
            {
                if (null == _Instance)
                {
                    lock (typeof(SqliteAdoSessionManager))
                    {
                        if (null == _Instance)
                        {
                            _Instance = new SqliteAdoSessionManager();
                        }
                    }
                }
                return _Instance;
            }
        }

        public SqlLiteAdoSession ErrorLogsDB { get; private set; }
        public SqlLiteAdoSession OperLogsDB { get; private set; }

        private SqliteAdoSessionManager()
        {
            ErrorLogsDB = new SqlLiteAdoSession(DatabaseScheme.ErrorLogs);
            OperLogsDB = new SqlLiteAdoSession(DatabaseScheme.OperLogs);
        }
    }

    public class SqlLiteAdoSession : IDisposable
    {
        private string _ConnectionString = "";
        private SQLiteTransaction _Transaction;
        private SQLiteConnection _Connection;

        public bool IsDisposed { get; private set; }

        #region 构造与析构
        public SqlLiteAdoSession(DatabaseScheme scheme)
        {
            _ConnectionString = CreateDBPathString(scheme);
            _Connection = new SQLiteConnection(_ConnectionString);
        }
        public SqlLiteAdoSession(string connectionString)
        {
            _ConnectionString = connectionString;
            _Connection = new SQLiteConnection(_ConnectionString);
        }
        ~SqlLiteAdoSession()
        {
            Dispose(false);
        }
        #endregion


        #region 其他方法
        public string CreateDBPathString(DatabaseScheme scheme)
        {
            string dbPathString = AppDomain.CurrentDomain.BaseDirectory + @"locdb\";
            if (!Directory.Exists(dbPathString))
            {
                Directory.CreateDirectory(dbPathString);
            }
            string pathFullString = string.Format(@"Data Source ={0}locdb\{1}.db", AppDomain.CurrentDomain.BaseDirectory, scheme.ToString().ToLower());
            return pathFullString;
        }
        #endregion

        /// <summary>
        /// 建立数据库连接
        /// </summary>
        /// <returns>如果之前未建立连接，则返回True，反之返回False</returns>
        /// <remarks>
        /// 如果返回True，则调用方负责调用Close关闭该连接。
        /// 如果返回False，调用方不能关闭该连接，因为可能该连接还在被其他对象使用。
        /// 遵循谁打开谁关闭的原则。
        /// </remarks>
        public bool Open()
        {
            if (_Connection.State == ConnectionState.Open)
            {
                return false;
            }
            _Connection.Open();
            return true;
        }
        public void Close()
        {
            if (_Connection.State != ConnectionState.Closed)
            {
                _Connection.Close();
            }
        }
        public bool BeginTransaction()
        {
            if (null == _Transaction)
            {
                _Transaction = _Connection.BeginTransaction();
                return true;
            }
            return false;
        }
        public void Commit()
        {
            if (null != _Transaction)
            {
                _Transaction.Commit();
                _Transaction = null;
            }
        }
        public void Rollback()
        {
            if (null != _Transaction)
            {
                _Transaction.Rollback();
                _Transaction = null;
            }
        }

        public SQLiteCommand CreateSQLiteCommand(string sql)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = sql;
            return cmd;
        }
        public SQLiteCommand CreateSQLiteCommand()
        {
            return CreateSQLiteCommand(String.Empty);
        }
        public SQLiteCommand CreateSQLiteCommand(string sql, SQLiteParameter[] parameters)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, _Connection, _Transaction);
            cmd.Parameters.AddRange(parameters);
            return cmd;
        }

        public int ExecuteNonQuery(SQLiteCommand cmd)
        {
            bool isOpen = this.Open();
            try
            {
                cmd.Connection = _Connection;
                if (null != _Transaction)
                {
                    cmd.Transaction = _Transaction;
                }
                return cmd.ExecuteNonQuery();
            }
            finally
            {
                if (isOpen)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 执行SQL命令，返回SQLiteDataReader，使用该方法必须自己管理数据库连接。
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public SQLiteDataReader ExecuteReader(SQLiteCommand cmd)
        {
            bool isOpen = this.Open();
            cmd.Connection = _Connection;
            if (null != _Transaction)
            {
                cmd.Transaction = _Transaction;
            }
            return cmd.ExecuteReader();
        }

        public object ExecuteScalar(SQLiteCommand cmd)
        {
            bool isOpen = this.Open();
            try
            {
                cmd.Connection = _Connection;
                if (null != _Transaction)
                {
                    cmd.Transaction = _Transaction;
                }
                return cmd.ExecuteScalar();
            }
            finally
            {
                if (isOpen)
                {
                    this.Close();
                }
            }
        }
        public DataTable ExecuteDataTable(SQLiteCommand cmd)
        {
            return ExecuteDataTable(cmd, "table1");
        }
        public DataTable ExecuteDataTable(SQLiteCommand cmd, string tableName)
        {
            if (String.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName");
            }
            bool isOpen = this.Open();
            try
            {
                cmd.Connection = _Connection;
                if (null != _Transaction)
                {
                    cmd.Transaction = _Transaction;
                }
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                {
                    DataSet aDataSet = new DataSet();
                    aDataSet.Tables.Add(tableName);
                    adapter.Fill(aDataSet, tableName);
                    return aDataSet.Tables[tableName];
                }
            }
            finally
            {
                if (isOpen)
                {
                    this.Close();
                }
            }
        }

        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string sqlString)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(sqlString, _Connection, _Transaction))
            {
                bool isOpenByMe = Open();
                try
                {
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                finally
                {
                    if (isOpenByMe)
                    {
                        Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string sqlString, string content)
        {
            bool isOpenByMe = Open();
            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sqlString, _Connection, _Transaction))
                {
                    SQLiteParameter myParameter = new SQLiteParameter("@content", SqlDbType.NText);
                    myParameter.Value = content;
                    cmd.Parameters.Add(myParameter);
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
            }
            finally
            {
                if (isOpenByMe)
                {
                    Close();
                }
            }
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string sqlString)
        {
            bool isOpenByMe = Open();
            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sqlString, _Connection, _Transaction))
                {
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    return obj;
                }
            }
            finally
            {
                if (isOpenByMe)
                {
                    Close();
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回SQLiteDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SQLiteDataReader</returns>
        public SQLiteDataReader ExecuteReader(string sqlText)
        {
            bool isOpenByMe = Open();
            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sqlText, _Connection, _Transaction))
                {
                    SQLiteDataReader myReader = cmd.ExecuteReader();
                    return myReader;
                }
            }
            finally
            {
                if (isOpenByMe)
                {
                    Close();
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string sqlString)
        {
            bool isOpenByMe = Open();
            try
            {
                DataSet ds = new DataSet();
                using (SQLiteCommand cmd = new SQLiteCommand(sqlString, _Connection, _Transaction))
                {
                    using (SQLiteDataAdapter command = new SQLiteDataAdapter(cmd))
                    {
                        command.Fill(ds, "ds");
                    }
                }
                return ds;
            }
            finally
            {
                if (isOpenByMe)
                {
                    Close();
                }
            }
        }
        public IList<T> Query2<T>(string sqlString)
        {
            bool isOpenByMe = Open();
            try
            {
                DataSet ds = new DataSet();
                using (SQLiteCommand cmd = new SQLiteCommand(sqlString, _Connection, _Transaction))
                {
                    using (SQLiteDataAdapter command = new SQLiteDataAdapter(cmd))
                    {
                        command.Fill(ds, "ds");
                        return DataSetToList<T>(ds, 0);
                    }
                }
            }
            finally
            {
                if (isOpenByMe)
                {
                    Close();
                }
            }
        }
        public IList<T> DataSetToList<T>(DataSet dataSet, int tableIndex)
        {
            //确认参数有效
            if (dataSet == null || dataSet.Tables.Count <= 0 || tableIndex < 0)
                return null;

            DataTable dt = dataSet.Tables[tableIndex];

            IList<T> list = new List<T>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //创建泛型对象
                T _t = Activator.CreateInstance<T>();
                //获取对象所有属性
                PropertyInfo[] propertyInfo = _t.GetType().GetProperties();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    foreach (PropertyInfo info in propertyInfo)
                    {
                        //属性名称和列名相同时赋值
                        if (dt.Columns[j].ColumnName.ToUpper().Equals(info.Name.ToUpper()))
                        {
                            if (dt.Rows[i][j] != DBNull.Value)
                            {
                                if (info.PropertyType.Name == "String")
                                    info.SetValue(_t, dt.Rows[i][j].ToString(), null);
                                else if (info.PropertyType.Name == "Int32")
                                    info.SetValue(_t, int.Parse(dt.Rows[i][j].ToString()), null);
                                else
                                    info.SetValue(_t, dt.Rows[i][j], null);
                            }
                            else
                            {
                                info.SetValue(_t, null, null);
                            }
                            break;
                        }
                    }
                }
                list.Add(_t);
            }
            return list;
        }
        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string sqlString, params SQLiteParameter[] cmdParms)
        {
            bool isOpenByMe = Open();
            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    PrepareCommand(cmd, _Connection, _Transaction, sqlString, cmdParms);
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
            }
            finally
            {
                if (isOpenByMe)
                {
                    Close();
                }
            }
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SQLiteParameter[]）</param>
        public void ExecuteSqlTrans(Hashtable sqlStringList)
        {
            bool isOpenByMe = Open();
            bool isBeginTrans = false;
            try
            {
                isBeginTrans = BeginTransaction();
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    //循环
                    foreach (DictionaryEntry myDE in sqlStringList)
                    {
                        string cmdText = myDE.Key.ToString();
                        SQLiteParameter[] cmdParms = (SQLiteParameter[])myDE.Value;
                        PrepareCommand(cmd, _Connection, _Transaction, cmdText, cmdParms);
                        int val = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    if (isBeginTrans)
                    {
                        Commit();
                    }
                }
            }
            catch
            {
                if (isBeginTrans)
                {
                    Rollback();
                }
                throw;
            }
            finally
            {
                if (isOpenByMe)
                {
                    Close();
                }
            }
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object GetSingle(string sqlString, params SQLiteParameter[] cmdParms)
        {
            bool isOpenByMe = Open();
            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    PrepareCommand(cmd, _Connection, _Transaction, sqlString, cmdParms);
                    object obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    return obj;
                }
            }
            finally
            {
                if (isOpenByMe)
                {
                    Close();
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SQLiteDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SQLiteDataReader</returns>
        public SQLiteDataReader ExecuteReader(string sqlString, params SQLiteParameter[] cmdParms)
        {
            bool isOpenByMe = Open();
            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    PrepareCommand(cmd, _Connection, _Transaction, sqlString, cmdParms);
                    SQLiteDataReader myReader = cmd.ExecuteReader();
                    return myReader;
                }
            }
            finally
            {
                if (isOpenByMe)
                {
                    Close();
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string sqlString, params SQLiteParameter[] cmdParms)
        {
            bool isOpenByMe = Open();
            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    PrepareCommand(cmd, _Connection, _Transaction, sqlString, cmdParms);
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds, "ds");
                        return ds;
                    }
                }
            }
            finally
            {
                if (isOpenByMe)
                {
                    Close();
                }
            }
        }
        public IList<T> Query2<T>(string sqlString, params SQLiteParameter[] cmdParms)
        {
            bool isOpenByMe = Open();
            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    PrepareCommand(cmd, _Connection, _Transaction, sqlString, cmdParms);
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds, "ds");
                        return DataSetToList<T>(ds, 0);
                    }
                }
            }
            finally
            {
                if (isOpenByMe)
                {
                    Close();
                }
            }
        }
        /*
        private SQLiteConnection _SQLiteConn = null;
        private SQLiteTransaction _SQLiteTrans = null;
        private bool _IsRunTrans = false;
        private string _SQLiteConnString = null;
        private bool _disposed = false;
        private bool _autocommit = false;
        */

        private void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction trans, string cmdText, SQLiteParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SQLiteParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }

        #endregion

        #region 存储过程操作

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SQLiteDataReader</returns>
        public SQLiteDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            bool isOpenByMe = Open();
            try
            {
                SQLiteDataReader returnReader;
                using (SQLiteCommand command = BuildQueryCommand(_Connection, storedProcName, parameters))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    returnReader = command.ExecuteReader();
                    return returnReader;
                }
            }
            finally
            {
                if (isOpenByMe)
                {
                    Close();
                }
            }
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            bool isOpenByMe = Open();
            try
            {
                DataSet dataSet = new DataSet();
                SQLiteDataAdapter sqlDA = new SQLiteDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(_Connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                return dataSet;
            }
            finally
            {
                if (isOpenByMe)
                {
                    Close();
                }
            }
        }


        /// <summary>
        /// 构建 SQLiteCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SQLiteCommand</returns>
        private SQLiteCommand BuildQueryCommand(SQLiteConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SQLiteCommand command = new SQLiteCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SQLiteParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            return command;
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            bool isOpenByMe = Open();
            try
            {
                int result;
                SQLiteCommand command = BuildIntCommand(_Connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                return result;
            }
            finally
            {
                if (isOpenByMe)
                {
                    Close();
                }
            }
        }

        /// <summary>
        /// 创建 SQLiteCommand 对象实例(用来返回一个整数值)	
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SQLiteCommand 对象实例</returns>
        private SQLiteCommand BuildIntCommand(SQLiteConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SQLiteCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SQLiteParameter("ReturnValue",
                DbType.Int32, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion

        #region DataTypeToSqlDbType 数据类型匹配
        public SqlDbType DataTypeToSqlDbType(string type)
        {
            try
            {
                type = type.ToLower();
                switch (type)
                {
                    case "int":
                        return SqlDbType.Int;
                    case "bigint":
                        return SqlDbType.BigInt;

                    case "binary":
                        return SqlDbType.Binary;

                    case "bit":
                        return SqlDbType.Bit;

                    case "char":
                        return SqlDbType.Char;

                    case "date":
                        return SqlDbType.Date;

                    case "datetime":
                        return SqlDbType.DateTime;

                    case "datetime2":
                        return SqlDbType.DateTime2;

                    case "datetimeoffset":
                        return SqlDbType.DateTimeOffset;

                    case "decimal":
                        return SqlDbType.Decimal;

                    case "float":
                        return SqlDbType.Float;

                    case "image":
                        return SqlDbType.Image;

                    case "money":
                        return SqlDbType.Money;

                    case "nchar":
                        return SqlDbType.NChar;

                    case "ntext":
                        return SqlDbType.NText;

                    case "nvarchar":
                        return SqlDbType.NVarChar;

                    case "smallint":
                        return SqlDbType.SmallInt;

                    case "text":
                        return SqlDbType.Text;

                    case "tinyint":
                        return SqlDbType.TinyInt;

                }
                return SqlDbType.NVarChar;
            }
            catch (Exception ex)
            {
                throw new Exception("调用DataTypeToSqlDbType方法错误:" + ex.Message);
            }
        }
        #endregion

        public static SQLiteParameter CreateParameter(string name, object value)
        {
            SQLiteParameter parameter = new SQLiteParameter();
            parameter.ParameterName = name;
            parameter.Value = (value == null ? DBNull.Value : value);
            if (value != null)
            {
                if (value.GetType() == typeof(DateTime) || value.GetType() == typeof(DateTime?))
                {
                    parameter.DbType = DbType.DateTime;
                }
            }
            return parameter;
        }

        /// <summary>
        /// 创建SQL Server查询参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="dataType">参数数据类型</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public static SQLiteParameter CreateParameter(string name, DbType dataType, object value)
        {
            SQLiteParameter parameter = new SQLiteParameter();
            parameter.ParameterName = name;
            parameter.Value = (value == null ? DBNull.Value : value);
            parameter.DbType = dataType;
            return parameter;
        }


        #region IDisposable 成员

        public void Dispose()
        {
            if (!IsDisposed)
            {
                Dispose(true);
                IsDisposed = true;
            }
            GC.SuppressFinalize(this);
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (null != _Transaction)
                {
                    _Transaction.Dispose();
                }
                if (null != _Connection)
                {
                    _Connection.Dispose();
                }
            }
        }
    }

    public enum DatabaseScheme
    {
        ErrorLogs,
        OperLogs,
    }
}
