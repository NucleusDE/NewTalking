using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using Newtalking_DAL_Data;
using Newtalking_DAL_Server;
using System.Net;

namespace Newtalking_BLL_Server
{
    public class Message
    {
        byte[] bData;
        MessageData msgData = new MessageData();
        MessageDataConvert convert = new MessageDataConvert();

        public Message(byte[] data)
        {
            msgData = convert.ConvertReceiveData(data);
            bData = data;
        }

        public void Send()
        {
            lock (Data.Data.ArrOnlineUsers)
            {
                bool isFoundOnline = false;
                for (int i = 0; i < Data.Data.ArrOnlineUsers.Count; i++)
                {
                    Data.OnlineUserProperties user = (Data.OnlineUserProperties)Data.Data.ArrOnlineUsers[i];
                    if (msgData.Receiver_id == user.User_id)
                    {
                        isFoundOnline = true;
                        DataPackage dataPacksge = new DataPackage();
                        dataPacksge.User_IP = user.Ip;
                        Sender sender = new Sender(dataPacksge);
                        if (sender.SendMessage(dataPacksge))
                            return;
                        else
                        {
                            lock (Data.Data.ArrSendingMessages)
                            {
                                Data.Data.ArrSendingMessages.Add(msgData);
                            }
                        }
                    }
                }
                if (!isFoundOnline)
                    lock (Data.Data.ArrSendingMessages)
                    {
                        Data.Data.ArrSendingMessages.Add(msgData);
                    }
            }
        }
    }
}
