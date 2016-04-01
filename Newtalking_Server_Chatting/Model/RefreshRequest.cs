using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class RefreshRequest
    {
        short messageType;
        int uid;
        int user_id;

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

        public int User_id
        {
            get
            {
                return user_id;
            }

            set
            {
                user_id = value;
            }
        }
    }
}
