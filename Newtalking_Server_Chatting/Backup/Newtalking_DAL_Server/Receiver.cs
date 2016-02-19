using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Model;
using System.Reflection;

namespace Newtalking_DAL_Server
{
    public class Receiver
    {
        const int BufferSize = 1452;
        public DataPackage ReceiveData()
        {
            TcpListener listener = new TcpListener(Server_Properties.Property.Ip, Server_Properties.Property.Port);
            listener.Start();
            TcpClient remoteClient = listener.AcceptTcpClient();
            try
            {
                NetworkStream streamToClient = remoteClient.GetStream();
                byte[] buffer = new byte[BufferSize];
                int bytesRead = streamToClient.Read(buffer, 0, BufferSize);

                DataPackage rdp = new DataPackage();

                rdp.User_IP = IPAddress.Parse(GetRemoteIP(remoteClient));
                rdp.Data = buffer;
                remoteClient.Close();
                streamToClient.Close();
                return rdp;
            }
            catch
            {
                return null;
            }
            
        }
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
