using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Model;

namespace Newtalking_DAL_Server
{
    public class SendFile
    {
        TcpClient tcpTarget;
        const int BufferSize = 1024;
        FileStream fsSend;
        byte[] bPackSendBegin;

        public SendFile(TcpClient tcp, FileStream fsTemp, byte[] bPackBeginTemp)
        {
            tcpTarget = tcp;
            fsSend = fsTemp;
            bPackSendBegin = bPackBeginTemp;
        }

        public bool Send()
        {
            try {
                byte[] data = new byte[BufferSize - 4];
                Sender sender = new Sender(tcpTarget);
                int op = 0;
                while (fsSend.Read(data, op, BufferSize - 4) != 0)
                {
                    byte[] bResult = new byte[BufferSize];
                    bPackSendBegin.CopyTo(bResult, 0);
                    data.CopyTo(bResult, 4);     
                    DataPackage dpk = new DataPackage();
                    dpk.Data = data;
                    sender.SendMessage(dpk);
                }
                return true; }
            catch
            {
                return false;
            }
        }
    }
}
