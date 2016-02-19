using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Data
{
    public class OnlineUserProperties
    {
        private IPAddress ip;

        public IPAddress Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        private int user_id;

        public int User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }
    }
}
