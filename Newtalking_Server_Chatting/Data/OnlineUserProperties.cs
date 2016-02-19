using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Data
{
    public class OnlineUserProperties
    {
        private System.Net.Sockets.TcpClient client;

        private int user_id;

        public TcpClient Client
        {
            get
            {
                return client;
            }

            set
            {
                client = value;
            }
        }

        public int User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }
    }
}
