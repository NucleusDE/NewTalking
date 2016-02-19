using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Newtalking_DAL_Data
{
    public class MessageDataConvert
    {
        public byte[] ConvertSendData(MessageData data)
        {
            byte[] bResult = new byte[1452];

            short type = 1;
            byte[] bMessageType = BitConverter.GetBytes(type);
            byte[] bSender_id = BitConverter.GetBytes(data.User_id);
            byte[] bReceiver_id = BitConverter.GetBytes(data.Receiver_id);
            byte[] bTime = BitConverter.GetBytes(data.Time.Ticks);
            byte[] bMessage = Encoding.Default.GetBytes(data.Message);

            int i = 0;

            for (i = 0; i < bMessageType.Length; i++)
                bResult[i + 0] = bSender_id[i];
            for (i = 0; i < bSender_id.Length; i++)
                bResult[i + 2] = bSender_id[i];
            for (i = 0; i < bReceiver_id.Length; i++)
                bResult[i + 6] = bReceiver_id[i];
            for (i = 0; i < bTime.Length; i++)
                bResult[i + 10] = bTime[i];
            for (i = 0; i < 1438; i++)
                bResult[i + 14] = bMessage[i];

            return bResult;
        }

        public MessageData ConvertReceiveData(byte[] bReceived)
        {
            MessageData dataResult = new MessageData();

            byte[] bSender_id = new byte[4];
            byte[] bReceiver_id = new byte[4];
            byte[] bTime = new byte[4];
            byte[] bMessage = new byte[1440];

            int i = 0;
            for (i = 0; i < 4; i++)
                bSender_id[i] = bReceived[i + 2];
            for (i = 0; i < 4; i++)
                bReceiver_id[i] = bReceived[i + 6];
            for (i = 0; i < 4; i++)
                bTime[i] = bReceived[i + 10];
            for (i = 0; i < 1438; i++)
                bMessage[i] = bReceived[i + 14];

            dataResult.User_id = BitConverter.ToInt32(bSender_id, 0);
            dataResult.Receiver_id = BitConverter.ToInt32(bReceiver_id, 0);
            long timeTick = BitConverter.ToInt64(bTime, 0);
            dataResult.Time = new DateTime(timeTick);
            dataResult.Message = Encoding.Default.GetString(bMessage);

            return dataResult;
        }
    }

    public class LoginDataConvert
    {
        public byte[] ConvertToSend(bool boolean)
        {
            byte[] bResult = BitConverter.GetBytes(boolean);
            return bResult;
        }

        public LoginData ConvertToReceive(byte[] data)
        {
            byte[] bUser_id = new byte[4];
            byte[] bUser_pwd = new byte[16];
            
            for (int i = 0; i < 4; i++)
                bUser_id[i] = data[i + 2];
            for (int i = 0; i < 16; i++)
                bUser_pwd[i] = data[i + 6];

            LoginData dataResult = new LoginData();
            dataResult.User_id = BitConverter.ToInt32(bUser_id, 0);
            dataResult.User_password = Encoding.Default.GetString(bUser_pwd);

            return dataResult;
        }
    }

    public class AccountRequestConvert
    {
        public string ConvertToPassword(byte[] data)
        {
            byte[] bPassword = new byte[16];
            for (int i = 0; i < data.Length; i++)
                bPassword[i] = data[i + 2];
            return Encoding.Default.GetString(bPassword);  
        }

        public byte[] ConvertToSend(LoginData data)
        {
            short type = 3;
            byte[] bMessageType = BitConverter.GetBytes(type);
            byte[] bUser_id = BitConverter.GetBytes(data.User_id);
            byte[] bUser_password = Encoding.Default.GetBytes(data.User_password);

            byte[] bResult = new byte[22];
            for (int i = 0; i < 2; i++)
                bResult[i + 0] = bMessageType[i];
            for (int i = 0; i < 4; i++)
                bResult[i + 2] = bUser_id[i];
            for (int i = 0; i < 16; i++)
                bResult[i + 6] = bUser_password[i];

            return bResult;
        }
    }
}
