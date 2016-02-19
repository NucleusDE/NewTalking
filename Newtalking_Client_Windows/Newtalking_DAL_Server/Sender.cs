using System;
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
        public Sender() {
            client = Server_Properties.Property.TcpServer;
        }
        public bool SendMessage(byte[] data)
        {
            if (client==null) return false;
            try{
                NetworkStream streamToServer = client.GetStream();
                streamToServer.Write(data, 0, data.Length);
                return true;
            } catch {
                return false;
            }
        }
    }
}
