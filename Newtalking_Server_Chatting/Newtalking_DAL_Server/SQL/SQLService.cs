using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using MySQLDriverCS;
using System.Data.Common;
using System.Data;
using System.Collections;

namespace Newtalking_DAL_Server
{
    public class SQLService
    {
        string sql = "";
        MySQLConnection con = new MySQLConnection(new MySQLConnectionString("localhost", "NewTalking", "root", "").AsString);

        public bool Login(LoginData data)
        {
            try
            {
                con.Open();
                sql = "SELECT user_password from users where user_id = " + data.User_id + "";
                MySQLCommand com = new MySQLCommand(sql, con);
                DbDataReader reader = com.ExecuteReader();
                if (reader.Read())
                    if (reader["user_password"].ToString() == data.User_password)
                        return true;
                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public int AccountRequest(string pwd)
        {
            try {
                con.Open();
                sql = "INSERT INTO users(user_password) VALUES(" + pwd + ")";
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                sql = "SELECT LAST_INSERT_ID()";
                com = new MySQLCommand(sql, con);
                DbDataReader reader = com.ExecuteReader();
                int user_id = 0;
                if(reader.Read())
                {
                    user_id = Int32.Parse(reader["user_id"].ToString());
                } 
                sql = "INSERT INTO users_information VALUES(null,null,null,null)";
                com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return user_id;
            }
            catch { return 0; }
            finally
            {
                con.Close();
            }
        }

        public AccountInfo AccountInfoReader(int user_id)
        {
            try {
                con.Open();
                sql = "SELECT* FROM users_information WHERE user_id = " + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                DbDataReader reader = com.ExecuteReader();
                if(reader.Read())
                {
                    AccountInfo accountInfo = new AccountInfo();
                    accountInfo.Sex = short.Parse(reader["user_sex"].ToString());
                    accountInfo.Birthday = DateTime.Parse(reader["user_birthday"].ToString());
                    accountInfo.Phone = reader["uesr_phome"].ToString();
                    return accountInfo;
                }
                return null;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public bool AccountInfoEditor(AccountInfo accountInfo)
        {
            try
            {
                con.Open();
                sql = "UPDATE user_sex = " + accountInfo.Sex + ", user_birthday = " + accountInfo.Birthday + ", user_phone = " + accountInfo.Phone + " FROM users_information WHERE user_id = " + accountInfo.User_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }

        }

        public bool CheckOnlineUser(int user_id)
        {
            try
            {
                con.Open();
                sql = "SELECT * FROM user_online WHERE user_id=" + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                MySQLDataReader reader = com.ExecuteReaderEx();
                if (reader.Read())
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool InsertOnlineUser(int user_id)
        {
            try
            {
                con.Open();
                string sql = "INSERT INTO user_online(user_id) VALUES(" + user_id + ")";
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool DelOnlineUser(int user_id)
        {
            try
            {
                con.Open();
                string sql = "DELETE user_online WHERE user_id=" + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool InsertIntoOverMessages(MessageData msg)
        {
            try
            {
                con.Open();
                sql = "INSERT INTO over_messages(sender_id,receiver_id,time,message) VALUES(" + msg.User_id + "," + msg.Receiver_id + "," + msg.Time + "," + msg.Message + ")";
                MySQLCommand com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public ArrayList SelOverMessages(int user_id)
        {
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                sql = "SELECT * FROM over_messages where receiver_id=" + user_id;
                MySQLCommand com = new MySQLCommand(sql, con);
                MySQLDataAdapter adp = new MySQLDataAdapter(com);
                adp.Fill(ds);

                ArrayList messages = new ArrayList();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MessageData msg = new MessageData();
                    msg.User_id = (int)dr["sender_id"];
                    msg.Receiver_id = (int)dr["receiver_id"];
                    msg.Time = (DateTime)dr["time"];
                    msg.Message = dr["message"].ToString();
                    messages.Add(msg);
                }

                sql = "DELETE user_message where receiver_id=" + user_id;
                com = new MySQLCommand(sql, con);
                com.ExecuteNonQuery();

                return messages;
            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        //public bool DelOverMessages(int bool)

//         --插入用户
//INSERT INTO users(user_name, user_password) VALUES(?, ?);

//--搜索最大值
//SELECT LAST_INSERT_ID();

//--插入用户资料
//INSERT INTO users_information VALUES(?, ?, ?, ?);

//--修改全部信息
//UPDATE user_sex = ?, user_birthday = ?, user_phone = ? FROM users_information WHERE user_id = ?;

//--修改密码
//UPDATE user_password FROM users WHERE user_id = ?;

//--查找密码
//SELECT user_password FROM users WHERE user_id = ?;

//--查找全部信息
//SELECT* FROM users_information WHERE user_id = ?;

//--查找用户名
//SELECT user_name FROM users WHERE user_id = ?;
    }
}
