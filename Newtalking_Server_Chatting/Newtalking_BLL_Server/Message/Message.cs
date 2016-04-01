using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using Newtalking_DAL_Data;
using Newtalking_DAL_Server;
using System.Net;
using System.Collections;

namespace Newtalking_BLL_Server
{
    public class Message
    {
        byte[] bData;
        MessageData msgData = new MessageData();
        MessageDataConvert convert = new MessageDataConvert();

        public Message(byte[] data)
        {
            msgData = convert.ConvertToClass(data);
            bData = data;
        }

        public void Send()
        {
            ArrayList arrOnlineUsers = new ArrayList();
            lock (Data.Data.ArrOnlineUsers)
            {
                for (int i = 0; i < Data.Data.ArrOnlineUsers.Count; i++)
                    arrOnlineUsers.Add(Data.Data.ArrOnlineUsers[i]);
            }
            bool isFoundOnline = false;

            //[未升级] 数据库处理
            for (int i = 0; i < arrOnlineUsers.Count; i++)
            {
                Data.OnlineUserProperties user = (Data.OnlineUserProperties)arrOnlineUsers[i];
                if (msgData.Receiver_id == user.User_id)
                {
                    isFoundOnline = true;
                    DataPackage dataPackage = new DataPackage();
                    dataPackage.Client = user.Client;
                    dataPackage.Data = bData;
                    Sender sender = new Sender(dataPackage.Client);
                    if (sender.SendMessage(dataPackage))
                        return;
                    else
                    {
                        //lock (Data.Data.ArrSendingMessages)
                        //{
                        //    Data.Data.ArrSendingMessages.Add(msgData);
                        //}
                        SQLService sql = new SQLService();
                        sql.InsertIntoOverMessages(msgData);
                    }
                    break;
                }
            }
            if (!isFoundOnline)
            {
                SQLService sql = new SQLService();
                sql.InsertIntoOverMessages(msgData);
            }
                //lock (Data.Data.ArrSendingMessages)
                //{
                //    Data.Data.ArrSendingMessages.Add(msgData);
                //}
        }
    }
}