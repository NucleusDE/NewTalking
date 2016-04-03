using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtalking_DAL_Server;
using Newtalking_DAL_Data;
using Model;
using System.Collections;

namespace Newtalking_BLL_Server
{
    public class MessageFresh
    {
        DataPackage data;
        RefreshRequest rr;

        public MessageFresh(DataPackage dpk)
        {
            data = dpk;
            RefreshRequestConvert converter = new RefreshRequestConvert();
            rr = converter.ConvertToClass(dpk.Data);
        }

        public bool Response()
        {
            SQLService sql = new SQLService();
            ArrayList arrMsg = sql.SelOverMessages(rr.User_id);
            Sender sender = new Sender(data.Client);

            foreach (object obj in arrMsg)
            {
                MessageData msg = (MessageData)obj;
                MessageDataConvert converter = new MessageDataConvert();
                byte[] dataSend = converter.ConvertToBytes(msg);
                DataPackage dpk = new DataPackage();
                dpk.Data = dataSend;
                dpk.Client = data.Client;
                if (!sender.SendMessage(dpk))
                    return false;
            }

            //结束标识
            DataPackage endDpk = new DataPackage();
            endDpk.Data = BitConverter.GetBytes(0);
            endDpk.Client = data.Client;
            sender.SendMessage(endDpk);

            return true;
        }
    }
}
