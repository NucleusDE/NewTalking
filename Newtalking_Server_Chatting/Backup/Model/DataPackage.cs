using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class DataPackage
    {
        private System.Net.IPAddress user_IP;

        private byte[] data;

        public System.Net.IPAddress User_IP
        {
            get
            {
                return user_IP;
            }

            set
            {
                user_IP = value;
            }
        }

        public byte[] Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }
    }
}
