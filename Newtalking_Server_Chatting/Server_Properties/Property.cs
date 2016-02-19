using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Server_Properties
{
    public class Property
    {
        static IPAddress ip = IPAddress.Parse("0.0.0.0");
        static int port = 2001;

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
    }
}
