using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Newtalking_DAL_Data;
using Newtalking_DAL_Server;
using System.Net;

namespace Newtalking_BLL_Server
{
    public class EditAccountInfo
    {
        AccountInfo accountInfo = new AccountInfo();
        DataPackage dataSend = new DataPackage();

        public EditAccountInfo(DataPackage data)
        {
            AccountInfoConvet convert = new AccountInfoConvet();
            accountInfo = convert.ConvertToClass(data.Data);
            dataSend.Client = data.Client;
        }

        public void Response()
        {
            SQLService sql = new SQLService();
            byte[] bIsSucceed = BitConverter.GetBytes(sql.AccountInfoEditor(accountInfo));
            dataSend.Data = bIsSucceed;
        }
    }
}
