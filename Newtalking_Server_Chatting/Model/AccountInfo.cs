using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    //CREATE TABLE users_information (user_id INTEGER, user_sex CHAR(2), user_birthday INTEGER, user_phone INTEGER, FOREIGN KEY(user_id) REFERENCES users(user_id));
    public class AccountInfo
    {
        private int user_id;

        public int User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }

        private short sex;

        public short Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        private DateTime birthday;

        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private short messageType;

        public short MessageType
        {
            get
            {
                return messageType;
            }

            set
            {
                messageType = value;
            }
        }

        private int uid;

        public int Uid
        {
            get
            {
                return uid;
            }

            set
            {
                uid = value;
            }
        }

        private string user_name;

        public string User_name
        {
            get
            {
                return user_name;
            }

            set
            {
                user_name = value;
            }
        }
    }
}
