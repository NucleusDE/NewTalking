using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Threading;
using System.Collections;

namespace Newtalking_BLL_Server
{
    public class Data_BLL
    {
        public void Analysis(DataPackage data)
        {
            Thread tdAnalysis = new Thread(delegate ()
            {
                short type;
                byte[] bType = new byte[2];

                bType[0] = data.Data[0];
                bType[1] = data.Data[1];
                type = BitConverter.ToInt16(bType, 0);

                switch (type)
                {
                    case 1:            //信息
                        Message message = new Message(data.Data);
                        message.Send();
                        break;
                    case 2:            //登陆
                        AccountLogin accountLogin = new AccountLogin(data);
                        if (accountLogin.Login())
                        {
                            accountLogin.AddToOnlineUserList();
                            Data_BLL data_BLL = new Data_BLL();
                            data_BLL.SendAllMessage(accountLogin.loginData);
                        }
                        accountLogin.Respect();
                        break;
                    case 3:         //账号申请
                        AccountRequest accountRequest = new AccountRequest(data);
                        accountRequest.Respect();
                        break;
                }
            });
            tdAnalysis.Start();
        }

        public void SendAllMessage(LoginData data)
        {
            ArrayList arrTemp = new ArrayList();
            lock(Data.Data.ArrSendingMessages)
            {
                for (int i = 0; i < Data.Data.ArrSendingMessages.Count; i++)
                {
                    MessageData msg = (MessageData)Data.Data.ArrSendingMessages[i];
                    if (msg.Receiver_id == data.User_id)
                        arrTemp.Add(msg);
                }
            }
            for (int i = 0; i < arrTemp.Count; i++)
            {
                MessageData msg = (MessageData)arrTemp[i];
                Newtalking_DAL_Data.MessageDataConvert convert = new Newtalking_DAL_Data.MessageDataConvert();
                Message msgSend = new Message(convert.ConvertSendData(msg));
                msgSend.Send();
            }
        }
    }
}
