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
        public DataPackage ReceiveData(TcpClient tcpClient)
        {
            TcpClient remoteClient = tcpClient;
            try
            {
                NetworkStream streamToClient = remoteClient.GetStream();
                byte[] buffer = new byte[BufferSize];
                int bytesRead = streamToClient.Read(buffer, 0, BufferSize);

                DataPackage rdp = new DataPackage();

                rdp.Client = remoteClient;
                int correctSize = 0;
                while (buffer[correctSize] != 0)
                {
                    correctSize++;
                }
                byte[] correctBuffer = new byte[correctSize];
                Buffer.BlockCopy(buffer, 0, correctBuffer, 0, correctSize);

                rdp.Data = buffer;
                return rdp;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
