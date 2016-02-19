using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using MySQLDriverCS;

namespace Newtalking_DAL_Server
{
    public class SQLService
    {
        string sql = "";
        MysqlHelper mysql = new MysqlHelper();

        public bool Login(LoginData data)
        {
            sql = "SELECT user_password from users where user_id = " + data.User_id + "";
            MySQLDataReader reader = mysql.ExecuteReader(sql);
            if (reader.Read())
                if (reader["user_password"].ToString() == data.User_password)
                    return true;
            return false;
        }

        public int AccountRequest(string pwd)
        {
            
            return 0;
        }
    }
}
