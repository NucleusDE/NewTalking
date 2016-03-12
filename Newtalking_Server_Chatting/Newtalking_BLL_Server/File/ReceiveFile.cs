using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using File_DAL;
using Newtalking_DAL_Server;
using Newtalking_DAL_Data;
using Model;

namespace Newtalking_BLL_Server.File
{
    public class ReceiveFile
    {
        ReceiveFileRequest rfr = new ReceiveFileRequest();
        System.Net.Sockets.TcpClient remoteClient;

        public ReceiveFile(DataPackage data)
        {
            FileRequestConvert frc = new FileRequestConvert();
            rfr = frc.ConvertToData_Receive(data.Data);
            remoteClient = data.Client;
        }

        public bool Receive()
        {
            try {
                Newtalking_DAL_Server.ReceiveFile rece = new Newtalking_DAL_Server.ReceiveFile(remoteClient);
                FileCheck fileCheck = new FileCheck();
                string[] strs = fileCheck.CheckCreateUserDir(rfr.User_id);
                WriteFile writer = new WriteFile(strs[0]);
                byte[] data;
                do
                {
                    data = rece.Receive();
                    writer.Write(data);
                } while (data.Length == 1024);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
