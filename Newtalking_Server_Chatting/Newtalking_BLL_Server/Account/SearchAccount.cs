using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Newtalking_DAL_Data;
using Newtalking_DAL_Server;

namespace Newtalking_BLL_Server.Account
{
    public class SearchAccount
    {
        System.Net.Sockets.TcpClient client;
        SelectAccount selAcc;
        SelectAccountConvert converter = new SelectAccountConvert();

        public SearchAccount(DataPackage dpk)
        {
            SelectAccountConvert converter = new SelectAccountConvert();
            selAcc = converter.ConvertToClass(dpk.Data);
            client = dpk.Client;
        }

        public bool Response()
        {
            try
            {
                SQLService sql = new SQLService();
                System.Collections.ArrayList arr = sql.SearchAccount(selAcc);

                foreach (object obj in arr)
                {
                    Sender sender = new Sender(client);
                    DataPackage dpk = new DataPackage();
                    AccountInfoConvet converter = new AccountInfoConvet();
                    dpk.Data = converter.ConvertToBytes((AccountInfo)obj);
                    dpk.Client = client;
                    sender.SendMessage(dpk);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
