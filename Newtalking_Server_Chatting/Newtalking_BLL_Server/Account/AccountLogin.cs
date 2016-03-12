using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Newtalking_DAL_Data;
using Newtalking_DAL_Server;
using Data;
using File_DAL;

namespace Newtalking_BLL_Server
{
    public class AccountLogin
    {
        public LoginData loginData = new LoginData();
        LoginDataConvert convert = new LoginDataConvert();
        bool isLogined = false;
        OnlineUserProperties onlineUser = new OnlineUserProperties();

        public AccountLogin(DataPackage data)
        {
            LoginDataConvert convert = new LoginDataConvert();
            loginData = convert.ConvertToClass(data.Data);
            onlineUser.Client = data.Client;
            onlineUser.User_id = loginData.User_id;
        }

        public bool Login()
        {
            SQLService sql = new SQLService();
            return sql.Login(loginData);
        }

        public bool Respect()
        {
            DataPackage data = new DataPackage();
            data.Client = onlineUser.Client;
            data.Data = convert.ConvertToBytes(isLogined, loginData.Uid);
            Sender sender = new Sender(onlineUser.Client);
            return sender.SendMessage(data);
        }

        public void AddToOnlineUserList()
        {
            lock (Data.Data.ArrOnlineUsers)
            {
                Data.Data.ArrOnlineUsers.Add(onlineUser);
            }
        }
    }

}
