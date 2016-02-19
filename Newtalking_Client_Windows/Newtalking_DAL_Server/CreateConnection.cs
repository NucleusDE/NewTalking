using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Newtalking_DAL_Server
{
    public class CreateConnection
    {
        public bool Connect()
        {
            try
            {
                TcpClient server = new TcpClient();
                server.Connect(Server_Properties.Property.Ip, Server_Properties.Property.Port);
                lock(Server_Properties.Property.TcpServer)
                {
                    Server_Properties.Property.TcpServer = server;
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
