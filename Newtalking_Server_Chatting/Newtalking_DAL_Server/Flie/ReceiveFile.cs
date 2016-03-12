using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Model;
using Newtalking_DAL_Server;

namespace Newtalking_DAL_Server
{
    public class ReceiveFile
    {
        const int BufferSize = 1024;
        TcpClient remoteClient;
        Receiver rece;

        public ReceiveFile(TcpClient tcpClient)
        {
            //端口2002
            remoteClient = tcpClient;
        }

        public byte[] Receive()
        {
            try
            {
                NetworkStream streamToClient = remoteClient.GetStream();
                byte[] buffer = new byte[BufferSize];
                int bytesRead = streamToClient.Read(buffer, 0, BufferSize);

                DataPackage rdp = new DataPackage();

                rdp.Client = remoteClient;
                byte[] data = buffer;
                return data;
            }
            catch
            {
                return null;
            }
        }

    }
}
