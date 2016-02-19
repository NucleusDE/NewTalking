using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Newtalking_DAL_Data;
using Newtalking_DAL_Server;
using Data;

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
            loginData = convert.ConvertToReceive(data.Data);
            onlineUser.Ip = data.User_IP;
            onlineUser.User_id = loginData.User_id;
        }

        public bool Login()
        {
            SQLService sql = new SQLService();
            return sql.Login(loginData);
        }

        public void Respect()
        {
            DataPackage data = new DataPackage();
            data.User_IP = onlineUser.Ip;
            data.Data = convert.ConvertToSend(isLogined);
            Sender sender = new Sender(data);
            sender.SendMessage(data);
        }

        public void AddToOnlineUserList()
        {
            lock (Data.Data.ArrOnlineUsers)
            {
                Data.Data.ArrOnlineUsers.Add(onlineUser);
            }
        }
    }

    public class AccountRequest
    {
        AccountRequestConvert convert = new AccountRequestConvert();
        DataPackage dataRespect = new DataPackage();

        public AccountRequest(DataPackage data)
        {
            SQLService sql = new SQLService();
            dataRespect.User_IP = data.User_IP;

            LoginData loginData = new LoginData();
            loginData.User_password = convert.ConvertToPassword(data.Data);
            loginData.User_id = sql.AccountRequest(loginData.User_password);
            dataRespect.Data = convert.ConvertToSend(loginData);
        }

        public void Respect()
        {
            Sender sender = new Sender(dataRespect);
            sender.SendMessage(dataRespect);
        }
    }
}
