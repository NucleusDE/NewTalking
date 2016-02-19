using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Newtalking_DAL_Server;
using Newtalking_DAL_Data;
using Newtalking_User_Info;

namespace Newtalking_BLL_Data
{
    public delegate void FuncMessageCallBack(byte[] data);

    public class Data_Send
    {

        public int uid;

        public bool Connect()
        {
            try
            {
                CreateConnection conn = new CreateConnection();
                if (conn.Connect())
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public bool SendMessage(string message, int receiver_id, FuncMessageCallBack func)
        {
            try { 
                MessageData msgDataSend = new MessageData();
                msgDataSend.User_id = User_Info.User_id;
                msgDataSend.Time = DateTime.Now;
                msgDataSend.Message = message;
                msgDataSend.Receiver_id = receiver_id;

                MessageDataConvert convert = new MessageDataConvert();
                Sender sender = new Sender();

                sender.SendMessage(convert.ConvertToBytes(msgDataSend));
                return true;
            }
            catch
            {
                func(new byte[0]);
                return false;
            }
        }

        public bool Login(int id, string pwd, FuncMessageCallBack func)
        {
            try
            {
                uid = uid + 1;
                lock (Server_Properties.Data.ArrMsgCallBack)
                {
                    Server_Properties.Data.ArrMsgCallBack.Add(func);
                }

                LoginData loginDataSend = new LoginData();
                loginDataSend.Uid = uid;
                loginDataSend.User_id = id;
                loginDataSend.User_password = pwd;

                Sender sender = new Sender();
                LoginDataConvert convert = new LoginDataConvert();
                sender.SendMessage(convert.ConvertToBytes(loginDataSend));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AccountRequest(string pwd, FuncMessageCallBack func)
        {
            try
            {
                uid = uid + 1;
                lock (Server_Properties.Data.ArrMsgCallBack)
                {
                    Server_Properties.Data.ArrMsgCallBack.Add(func);
                }

                LoginData loginDataSend = new LoginData();
                loginDataSend.Uid = uid;
                loginDataSend.User_id = User_Info.User_id;
                loginDataSend.User_password = pwd;

                Sender sender = new Sender();
                AccountRequestConvert convert = new AccountRequestConvert();
                sender.SendMessage(convert.ConvertToBytes(loginDataSend));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SelAccountInfo(int user_id, FuncMessageCallBack func)
        {
            try {
                uid = uid + 1;
                lock (Server_Properties.Data.ArrMsgCallBack)
                {
                    Server_Properties.Data.ArrMsgCallBack.Add(func);
                }

                AccountInfo accountDataSend = new AccountInfo();
                accountDataSend.User_id = user_id;

                Sender sender = new Sender();
                AccountInfoConvet convert = new AccountInfoConvet();
                sender.SendMessage(convert.ConvertToBytes(accountDataSend));

                return true; }
            catch
            {
                return false;
            }
        }

        public bool EditAccountInfo(AccountInfo data, FuncMessageCallBack func)
        {
            try
            {
                uid = uid + 1;
                lock (Server_Properties.Data.ArrMsgCallBack)
                {
                    Server_Properties.Data.ArrMsgCallBack.Add(func);
                }

                AccountInfoConvet convert = new AccountInfoConvet();
                Sender sender = new Sender();
                sender.SendMessage(convert.ConvertToBytes(data));

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
