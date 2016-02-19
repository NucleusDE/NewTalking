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
        TcpClient remoteClient;

        public Receiver()
        {
            remoteClient = Server_Properties.Property.TcpServer;
        }

        public byte[] ReceiveData()
        {
            try
            {
                NetworkStream streamToClient = remoteClient.GetStream();
                byte[] buffer = new byte[BufferSize];
                int bytesRead = streamToClient.Read(buffer, 0, BufferSize);

                byte[] data = new byte[BufferSize];
                data = buffer;

                return data;
            }
            catch
            {
                return null;
            }
            
        }
    }
}
