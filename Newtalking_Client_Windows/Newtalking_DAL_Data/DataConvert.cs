using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Newtalking_DAL_Data
{
    public class MessageDataConvert
    {
        public byte[] ConvertToBytes(MessageData data)
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
            for (i = 0; i < 1438 && i < bMessage.Length; i++)
                bResult[i + 14] = bMessage[i];

            return bResult;
        }

        public MessageData ConvertToClass(byte[] bReceived)
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
        public byte[] ConvertToBytes(LoginData data)
        {
            byte[] bResult = new byte[26];

            short type = 2;
            BitConverter.GetBytes(type).CopyTo(bResult, 0);
            BitConverter.GetBytes(data.Uid).CopyTo(bResult, 2);
            BitConverter.GetBytes(data.User_id).CopyTo(bResult, 6);
            Encoding.Default.GetBytes(data.User_password).CopyTo(bResult, 10);

            return bResult;
        }

        public LoginData ConvertToClass(byte[] data)
        {
            byte[] bUid = new byte[4];
            byte[] bUser_id = new byte[4];
            byte[] bUser_pwd = new byte[16];

            for (int i = 0; i < 4; i++)
                bUid[i] = data[i + 2];
            for (int i = 0; i < 4; i++)
                bUser_id[i] = data[i + 6];
            for (int i = 0; i < 16; i++)
                bUser_pwd[i] = data[i + 10];

            LoginData dataResult = new LoginData();
            dataResult.User_id = BitConverter.ToInt32(bUser_id, 0);
            dataResult.User_password = Encoding.Default.GetString(bUser_pwd);

            return dataResult;
        }
    }

    public class AccountRequestConvert
    {
        public LoginData ConvertToClass(byte[] data)
        {
            byte[] bUid = new byte[4];
            byte[] bPassword = new byte[16];

            for (int i = 0; i < 4; i++)
                bUid[i] = data[i + 2];
            for (int i = 0; i < 16; i++)
                bPassword[i] = data[i + 10];

            LoginData dataResult = new LoginData();
            dataResult.Uid = BitConverter.ToInt32(bUid, 0);
            dataResult.User_password = BitConverter.ToString(bPassword);

            return dataResult;
        }

        public byte[] ConvertToBytes(LoginData data)
        {
            short type = 3;
            byte[] bMessageType = BitConverter.GetBytes(type);
            byte[] bUid = BitConverter.GetBytes(data.Uid);
            byte[] bUser_id = BitConverter.GetBytes(data.User_id);
            byte[] bUser_password = Encoding.Default.GetBytes(data.User_password);

            byte[] bResult = new byte[26];
            for (int i = 0; i < 2; i++)
                bResult[i + 0] = bMessageType[i];
            for (int i = 0; i < 4; i++)
                bResult[i + 2] = bUid[i];
            for (int i = 0; i < 4; i++)
                bResult[i + 6] = bUser_id[i];
            for (int i = 0; i < 16; i++)
                bResult[i + 10] = bUser_password[i];

            return bResult;
        }
    }
    //    CREATE DATABASE NewTalking;
    //CREATE TABLE users(user_id INTEGER PRIMARY KEY AUTO_INCREMENT, user_name VARCHAR(30) NOT NULL, user_password VARCHAR(20) NOT NULL);
    //CREATE TABLE users_information(user_id INTEGER, user_sex CHAR(2), user_birthday INTEGER, user_phone INTEGER, FOREIGN KEY(user_id) REFERENCES users(user_id));

    //INSERT INTO users(user_name, user_password) VALUES('大明', 123456);
    //    INSERT INTO users(user_name, user_password) VALUES('小明', 123456);

    //    INSERT INTO users_information VALUES(1, '男', 946659661, 10086);
    //    INSERT INTO users_information VALUES(2, '男', 946659662, 10010);

    //    SELECT* FROM users INNER JOIN users_information ON users.user_id = users_information.user_id;
    public class AccountInfoConvet
    {
        public byte[] ConvertToBytes(AccountInfo data)
        {
            //4+2+4+24+4=42
            byte[] bResult = new byte[42];

            byte[] bMessageType = BitConverter.GetBytes(data.MessageType);
            byte[] bUid = BitConverter.GetBytes(data.Uid);
            byte[] bUser_id = BitConverter.GetBytes(data.User_id);
            byte[] bUser_sex = BitConverter.GetBytes(data.Sex);
            byte[] bUser_birthdat = BitConverter.GetBytes(data.Birthday.Ticks);
            byte[] bUser_phone = Encoding.Default.GetBytes(data.Phone);

            for (int i = 0; i < bMessageType.Length; i++)
                bResult[i + 0] = bMessageType[i];
            for (int i = 0; i < bUid.Length; i++)
                bResult[i + 2] = bUid[i];
            for (int i = 0; i < bUser_id.Length; i++)
                bResult[i + 6] = bUser_id[i];
            for (int i = 0; i < bUser_sex.Length; i++)
                bResult[i + 10] = bUser_sex[i];
            for (int i = 0; i < bUser_birthdat.Length; i++)
                bResult[i + 12] = bUser_birthdat[i];
            for (int i = 0; i < bUser_phone.Length; i++)
                bResult[i + 16] = bUser_phone[i];

            return bResult;
        }

        public AccountInfo ConvertToClass(byte[] data)
        {
            AccountInfo dataResult = new AccountInfo();

            byte[] bUid = new byte[4];
            byte[] bUser_id = new byte[4];
            byte[] bUser_sex = new byte[2];
            byte[] bUser_birthday = new byte[4];
            byte[] bUser_phone = new byte[24];

            for (int i = 0; i < 4; i++)
                bUid[i] = data[i + 2];
            for (int i = 0; i < 4; i++)
                bUser_id[i] = data[i + 6];
            for (int i = 0; i < 2; i++)
                bUser_sex[i] = data[i + 10];
            for (int i = 0; i < 4; i++)
                bUser_birthday[i] = data[i + 12];
            for (int i = 0; i < 24; i++)
                bUser_phone[i] = data[i + 16];

            return null;
        }
    }
}
