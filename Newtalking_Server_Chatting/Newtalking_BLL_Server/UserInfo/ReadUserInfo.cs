using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Newtalking_DAL_Server;
using Newtalking_DAL_Data;
using System.Net;

namespace Newtalking_BLL_Server
{
    public class ReadUserInfo
    {
        AccountInfo accountInfo = new AccountInfo();
        DataPackage dataSend = new DataPackage();

        public ReadUserInfo(DataPackage data)
        {
            AccountInfoConvet convert = new AccountInfoConvet();
            accountInfo = convert.ConvertToClass(data.Data);
            SQLService sql = new SQLService();
            accountInfo = sql.AccountInfoReader(accountInfo.User_id);
            dataSend.Client = data.Client;
            dataSend.Data = convert.ConvertToBytes(accountInfo);
        }

        public AccountInfo Response()
        {
            Sender sender = new Sender(dataSend.Client);
            sender.SendMessage(dataSend);
            return accountInfo;
        }
    }
}