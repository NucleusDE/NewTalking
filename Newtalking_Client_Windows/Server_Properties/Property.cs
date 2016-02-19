using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server_Properties
{
    public class Property
    {
        static IPAddress ip = IPAddress.Parse("127.0.0.1");
        static int port = 2001;
        static TcpClient tcpServer = new TcpClient(new IPEndPoint(ip, port));
        static int uid = 0;

        public static IPAddress Ip
        {
            get
            {
                return ip;
            }

            set
            {
                ip = value;
            }
        }

        public static int Port
        {
            get
            {
                return port;
            }

            set
            {
                port = value;
            }
        }

        public static TcpClient TcpServer
        {
            get
            {
                return tcpServer;
            }

            set
            {
                tcpServer = value;
            }
        }

        public static int Uid
        {
            get
            {
                return uid;
            }

            set
            {
                uid = value;
            }
        }
    }
}
