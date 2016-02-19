using System;
using System.Data;
using MySQLDriverCS;
using System.Text.RegularExpressions;

public class MysqlHelper
{
    /// <summary>
    /// string server, string database, string login, string pass, int port
    /// </summary>
    public string connectionString = new MySQLConnectionString(".", "NewTalking", "root", "!@#$%^&*())(*&^%$#@!").AsString;
    public MysqlHelper()
    {

    }

    #region ExecuteNonQuery
    //执行SQL语句，返回影响的记录数
    /// <summary>
    /// 执行SQL语句，返回影响的记录数
    /// </summary>
    /// <param name="SQLString">SQL语句</param>
    /// <returns>影响的记录数</returns>
    public int ExecuteNonQuery(string SQLString)
    {
        using (MySQLConnection connection = new MySQLConnection(connectionString))
        {
            using (MySQLCommand cmd = new MySQLCommand(SQLString, connection))
            {
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (MySQLException e)
                {
                    connection.Close();
                    throw e;
                }
            }
        }
    }
    /// <summary>
    /// 执行SQL语句，返回影响的记录数
    /// </summary>
    /// <param name="SQLString">SQL语句</param>
    /// <returns>影响的记录数</returns>
    public int ExecuteNonQuery(string SQLString, params MySQLParameter[] cmdParms)
    {
        using (MySQLConnection connection = new MySQLConnection(connectionString))
        {
            using (MySQLCommand cmd = new MySQLCommand())
            {
                try
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return rows;
                }
                catch (MySQLException e)
                {
                    throw e;
                }
            }
        }
    }
    #endregion

    #region ExecuteScalar
    /// <summary>
    /// 执行一条计算查询结果语句，返回查询结果（object）。
    /// </summary>
    /// <param name="SQLString">计算查询结果语句</param>
    /// <returns>查询结果（object）</returns>
    public object ExecuteScalar(string SQLString)
    {
        using (MySQLConnection connection = new MySQLConnection(connectionString))
        {
            using (MySQLCommand cmd = new MySQLCommand(SQLString, connection))
            {
                try
                {
                    connection.Open();
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (MySQLException e)
                {
                    connection.Close();
                    throw e;
                }
            }
        }
    }
    /// <summary>
    /// 执行一条计算查询结果语句，返回查询结果（object）。
    /// </summary>
    /// <param name="SQLString">计算查询结果语句</param>
    /// <returns>查询结果（object）</returns>
    public object ExecuteScalar(string SQLString, params MySQLParameter[] cmdParms)
    {
        using (MySQLConnection connection = new MySQLConnection(connectionString))
        {
            using (MySQLCommand cmd = new MySQLCommand())
            {
                try
                {
                    PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                    object obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (MySQLException e)
                {
                    throw e;
                }
            }
        }
    }
    #endregion

    #region ExecuteReader
    /// <summary>
    /// 执行查询语句，返回MySqlDataReader (注意：调用该方法后，一定要对MySqlDataReader进行Close )
    /// </summary>
    /// <param name="strSQL">查询语句</param>
    /// <returns>MySqlDataReader</returns>
    public MySQLDataReader ExecuteReader(string strSQL)
    {
        MySQLConnection connection = new MySQLConnection(connectionString);
        MySQLCommand cmd = new MySQLCommand(strSQL, connection);
        MySQLDataReader myReader = null;
        try
        {
            connection.Open();
            myReader = cmd.ExecuteReaderEx();

            return myReader;
        }
        catch (MySQLException e)
        {
            throw e;
        }
        finally
        {
            myReader.Close();
        }
    }
    /// <summary>
    /// 执行查询语句，返回MySqlDataReader ( 注意：调用该方法后，一定要对MySqlDataReader进行Close )
    /// </summary>
    /// <param name="strSQL">查询语句</param>
    /// <returns>MySqlDataReader</returns>
    public MySQLDataReader ExecuteReader(string SQLString, params MySQLParameter[] cmdParms)
    {
        MySQLConnection connection = new MySQLConnection(connectionString);
        MySQLCommand cmd = new MySQLCommand();
        MySQLDataReader myReader = null;
        try
        {
            PrepareCommand(cmd, connection, null, SQLString, cmdParms);
            myReader = cmd.ExecuteReaderEx();
            cmd.Parameters.Clear();
            return myReader;
        }
        catch (MySQLException e)
        {
            throw e;
        }
        finally
        {
            myReader.Close();
            cmd.Dispose();
            connection.Close();

        }
    }
    #endregion

    #region ExecuteDataTable
    /// <summary>
    /// 执行查询语句，返回DataTable
    /// </summary>
    /// <param name="SQLString">查询语句</param>
    /// <returns>DataTable</returns>
    public DataTable ExecuteDataTable(string SQLString)
    {
        using (MySQLConnection connection = new MySQLConnection(connectionString))
        {
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                MySQLDataAdapter command = new MySQLDataAdapter(SQLString, connection);
                command.Fill(ds, "ds");
            }
            catch (MySQLException ex)
            {
                throw new Exception(ex.Message);
            }
            return ds.Tables[0];
        }
    }
    /// <summary>
    /// 执行查询语句，返回DataSet
    /// </summary>
    /// <param name="SQLString">查询语句</param>
    /// <returns>DataTable</returns>
    public DataTable ExecuteDataTable(string SQLString, params MySQLParameter[] cmdParms)
    {
        using (MySQLConnection connection = new MySQLConnection(connectionString))
        {
            MySQLCommand cmd = new MySQLCommand();
            PrepareCommand(cmd, connection, null, SQLString, cmdParms);
            using (MySQLDataAdapter da = new MySQLDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds, "ds");
                    cmd.Parameters.Clear();
                }
                catch (MySQLException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds.Tables[0];
            }
        }
    }
    //获取起始页码和结束页码
    public DataTable ExecuteDataTable(string cmdText, int startResord, int maxRecord)
    {
        using (MySQLConnection connection = new MySQLConnection(connectionString))
        {
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                MySQLDataAdapter command = new MySQLDataAdapter(cmdText, connection);
                command.Fill(ds, startResord, maxRecord, "ds");
            }
            catch (MySQLException ex)
            {
                throw new Exception(ex.Message);
            }
            return ds.Tables[0];
        }
    }
    #endregion

    #region PageList Without Proc
    /// <summary>
    /// 获取分页数据 在不用存储过程情况下
    /// </summary>
    /// <param name="recordCount">总记录条数</param>
    /// <param name="selectList">选择的列逗号隔开,支持top num</param>
    /// <param name="tableName">表名字</param>
    /// <param name="whereStr">条件字符 必须前加 and</param>
    /// <param name="orderExpression">排序 例如 ID</param>
    /// <param name="pageIdex">当前索引页</param>
    /// <param name="pageSize">每页记录数</param>
    /// <returns></returns>
    public DataTable getPager(out int recordCount, string selectList, string tableName, string whereStr, string orderExpression, int pageIdex, int pageSize)
    {
        int rows = 0;
        DataTable dt = new DataTable();
        MatchCollection matchs = Regex.Matches(selectList, @"top\s+\d{1,}", RegexOptions.IgnoreCase);//含有top
        string sqlStr = sqlStr = string.Format("select {0} from {1} where 1=1 {2}", selectList, tableName, whereStr);
        if (!string.IsNullOrEmpty(orderExpression)) { sqlStr += string.Format(" Order by {0}", orderExpression); }
        if (matchs.Count > 0) //含有top的时候
        {
            DataTable dtTemp = ExecuteDataTable(sqlStr);
            rows = dtTemp.Rows.Count;
        }
        else //不含有top的时候
        {
            string sqlCount = string.Format("select count(*) from {0} where 1=1 {1} ", tableName, whereStr);
            //获取行数
            object obj = ExecuteScalar(sqlCount);
            if (obj != null)
            {
                rows = Convert.ToInt32(obj);
            }
        }
        dt = ExecuteDataTable(sqlStr, (pageIdex - 1) * pageSize, pageSize);
        recordCount = rows;
        return dt;
    }
    #endregion

    #region 创建command
    private void PrepareCommand(MySQLCommand cmd, MySQLConnection conn, MySQLTransaction trans, string cmdText, MySQLParameter[] cmdParms)
    {
        if (conn.State != ConnectionState.Open)
            conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = cmdText;
        if (trans != null)
            cmd.Transaction = trans;
        cmd.CommandType = CommandType.Text;//cmdType;
        if (cmdParms != null)
        {
            foreach (MySQLParameter parameter in cmdParms)
            {
                if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                (parameter.Value == null))
                {
                    parameter.Value = DBNull.Value;
                }
                cmd.Parameters.Add(parameter);
            }
        }
    }
    #endregion
}