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
                    accountRequest.Response();
                    break;
                case 4:        //获取账户信息
                    ReadUserInfo readUserInfo = new ReadUserInfo(data);
                    readUserInfo.Response();
                    break;
                case 5:        //修改账户信息
                    EditAccountInfo editUserInfo = new EditAccountInfo(data);
                    editUserInfo.Response();
                    break;
            }
        }

        public void SendAllMessage(LoginData data)
        {
            ArrayList arrTemp = new ArrayList();
            lock(Data.Data.ArrSendingMessages)
            {
                ArrayList arrNew = Data.Data.ArrSendingMessages;
                Data.Data.ArrSendingMessages.Clear();
                for (int i = 0; i < arrNew.Count; i++)
                {
                    MessageData msg = (MessageData)arrNew[i];
                    if (msg.Receiver_id == data.User_id)
                        arrTemp.Add(msg);
                    else
                        Data.Data.ArrSendingMessages.Add(msg);
                }
            }
            for (int i = 0; i < arrTemp.Count; i++)
            {
                MessageData msg = (MessageData)arrTemp[i];
                Newtalking_DAL_Data.MessageDataConvert convert = new Newtalking_DAL_Data.MessageDataConvert();
                Message msgSend = new Message(convert.ConvertToBytes(msg));
                msgSend.Send();
            }
        }
    }
}
