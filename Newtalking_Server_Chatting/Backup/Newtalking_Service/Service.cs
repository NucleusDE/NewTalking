using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Data;
using System.Net;
using System.Collections;
using Newtalking_DAL_Server;
using Newtalking_BLL_Server;

namespace Newtalking_Service
{
    public class Service
    {
        static public void ActiveService()
        {
            Thread tdChattingService_NO1 = new Thread(delegate () {
                Data_BLL bll = new Data_BLL();
                Receiver receiver = new Receiver();
                while (true)
                    bll.Analysis(receiver.ReceiveData());
                
            });
            Thread tdChattingService_NO2 = new Thread(delegate () {
                Data_BLL bll = new Data_BLL();
                Receiver receiver = new Receiver();
                while (true)
                    bll.Analysis(receiver.ReceiveData());
            });
            Thread tdChattingService_NO3 = new Thread(delegate () {
                Data_BLL bll = new Data_BLL();
                Receiver receiver = new Receiver();
                while (true)
                    bll.Analysis(receiver.ReceiveData());
            });
            Thread tdChattingService_NO4 = new Thread(delegate () {
                Data_BLL bll = new Data_BLL();
                Receiver receiver = new Receiver();
                while (true)
                    bll.Analysis(receiver.ReceiveData());
            });
            Thread tdChattingService_NO5 = new Thread(delegate () {
                Data_BLL bll = new Data_BLL();
                Receiver receiver = new Receiver();
                while (true)
                    bll.Analysis(receiver.ReceiveData());
            });

            Thread tdHeartBeat = new Thread(delegate ()
            {
                HeartBeat();
                Thread.Sleep(TimeSpan.FromSeconds(5));
            });

            tdChattingService_NO1.Start();
            tdChattingService_NO2.Start();
            tdChattingService_NO3.Start();
            tdChattingService_NO4.Start();
            tdChattingService_NO5.Start();

            tdHeartBeat.Start();
        }

        static public void HeartBeat()
        {
            ArrayList arrOnlineUsers = new ArrayList();
            lock (Data.Data.ArrOnlineUsers)
            {
                for (int i = 0; i < Data.Data.ArrOnlineUsers.Count; i++)
                {
                    arrOnlineUsers.Add(Data.Data.ArrOnlineUsers[i]);
                }
            }
            ArrayList arrTemp = new ArrayList();
            for (int i = 0; i < arrOnlineUsers.Count; i++)
            {
                Model.DataPackage package = new Model.DataPackage();
                OnlineUserProperties user = (OnlineUserProperties)arrOnlineUsers[i];
                package.User_IP = user.Ip;
                short type = 0;
                package.Data = BitConverter.GetBytes(type);
                Newtalking_DAL_Server.Sender sender = new Newtalking_DAL_Server.Sender(package);
                if (sender.SendMessage(package))
                    arrTemp.Add((IPAddress)Data.Data.ArrOnlineUsers[i]);
            }
            lock (Data.Data.ArrOnlineUsers)
            {
                Data.Data.ArrOnlineUsers = arrTemp;
            }

        }
    }
}
