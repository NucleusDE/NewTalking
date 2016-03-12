using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Newtalking_DAL_Server;
using System.IO;
using File_DAL;
using Newtalking_DAL_Data;

namespace Newtalking_BLL_Server
{
    public class SendFile
    {
        DataPackage dataPack;
        FileStream fsSend;
        byte[] bPackBegin = new byte[4];
        string path;

        public SendFile(DataPackage dataPackTemp)
        {
            dataPack = dataPackTemp;
            Buffer.BlockCopy(dataPackTemp.Data, 2, bPackBegin, 0, 4);

            FileCheck fileCheck = new FileCheck();
            FileRequestConvert fileRc = new FileRequestConvert();
            FileRequest fr = fileRc.ConvertToClass_Send(dataPackTemp.Data);
            path = fileCheck.SelUserFileDir(fr.User_id, fr.FileName);
        }

        public bool Send()
        {
            if (path == "failed")
                return false;
            else
                fsSend = new FileStream(path, FileMode.Open, FileAccess.Read);
            Newtalking_DAL_Server.SendFile sender = new Newtalking_DAL_Server.SendFile(dataPack.Client, fsSend, bPackBegin);
            return sender.Send();
        }
    }
}
