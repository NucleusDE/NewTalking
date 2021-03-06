﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Model;

namespace Newtalking_DAL_Server
{
    public class Sender
    {
        public TcpClient client;
        public Sender(DataPackage dp) {
            TcpClient c = new TcpClient();
            c.Connect(dp.User_IP, Server_Properties.Property.Port);
            client = c;
        }
        public bool SendMessage(DataPackage dp)
        {
            if (client==null) return false;
            try{
                NetworkStream streamToServer = client.GetStream();
                streamToServer.Write(dp.Data, 0, dp.Data.Length);
                return true;
            } catch {
                return false;
            }
        }
    }
}
