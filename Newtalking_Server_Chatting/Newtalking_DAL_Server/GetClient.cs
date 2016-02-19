using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Net;
using System.Net.Sockets;
using System.Reflection;

namespace Newtalking_DAL_Server
{
    public class GetClient
    {
        const int BufferSize = 1452;
        TcpListener listener = new TcpListener(Server_Properties.Property.Ip, Server_Properties.Property.Port);
        public TcpClient Get()
        {
            try
            {
                listener.Start();
                return listener.AcceptTcpClient();
            }
            catch
            {
                return null;
            }

        }
    }
}
