  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class MessageData
    {
        private int user_id;

        private int receiver_id;

        private DateTime time;

        private string message;

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

        public int Receiver_id
        {
            get
            {
                return receiver_id;
            }

            set
            {
                receiver_id = value;
            }
        }

        public DateTime Time
        {
            get
            {
                return time;
            }

            set
            {
                time = value;
            }
        }

        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }
    }
}
