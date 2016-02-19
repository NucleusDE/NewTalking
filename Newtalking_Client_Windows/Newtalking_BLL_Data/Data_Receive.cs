using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Newtalking_DAL_Data;
using Newtalking_DAL_Server;

namespace Newtalking_BLL_Data
{
    public class Data_Receive
    {
        public void Receive()
        {
            while(true)
            {
                Receiver receiver = new Receiver();
                Analysis(receiver.ReceiveData());
            }
        }

        public void Analysis(byte[] data)
        {
            byte[] bMessageType = new byte[2];
            bMessageType[0] = data[0];
            bMessageType[1] = data[1];

            short type = BitConverter.ToInt16(bMessageType, 0);
            switch(type)
            {
                case 1:
                    break;
                default:
                    int uid;
                    byte[] bType = new byte[4];

                    for (int i = 0; i < 4; i++)
                        bType[i] = data[i + 2];
                    uid = BitConverter.ToInt32(bType, 0);

                    FuncMessageCallBack func;
                    lock (Server_Properties.Data.ArrMsgCallBack)
                    {
                        func = (FuncMessageCallBack)Server_Properties.Data.ArrMsgCallBack[uid];
                    }
                    func(data);
                    break;
            };
        }
    }
}
