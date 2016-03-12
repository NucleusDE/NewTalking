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
    public class AccountRequest
    {
        AccountRequestConvert convert = new AccountRequestConvert();
        DataPackage dataResponse = new DataPackage();

        public AccountRequest(DataPackage data)
        {
            SQLService sql = new SQLService();
            dataResponse.Client = data.Client;

            LoginData loginData = convert.ConvertToClass(data.Data);
            loginData.User_id = sql.AccountRequest(loginData.User_password);

            dataResponse.Data = convert.ConvertToBytes(loginData);
            FileCheck fileCheck = new FileCheck();
            fileCheck.CheckCreateUserDir(loginData.User_id);
        }

        public void Response()
        {
            Sender sender = new Sender(dataResponse.Client);
            sender.SendMessage(dataResponse);
        }
    }
}
