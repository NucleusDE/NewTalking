using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Reflection;

namespace Newtalking_DAL_Data
{
    public class GetClientInfo
    {
        Socket GetSocket(TcpClient cln)
        {
            //反射
            PropertyInfo pi = cln.GetType().GetProperty("Client", BindingFlags.NonPublic | BindingFlags.Instance);
            Socket sock = (Socket)pi.GetValue(cln, null);
            return sock;
        }

        string GetRemoteIP(TcpClient cln)
        {
            string ip = GetSocket(cln).RemoteEndPoint.ToString().Split(':')[0];
            return ip;
        }

        public int GetRemotePort(TcpClient cln)
        {
            string temp = GetSocket(cln).RemoteEndPoint.ToString().Split(':')[1];
            int port = Convert.ToInt32(temp);
            return port;
        }
    }
}
