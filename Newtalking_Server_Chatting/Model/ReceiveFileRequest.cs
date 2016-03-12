using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ReceiveFileRequest
    {
        private int user_id;
        private short file_name_length;
        private string file_name;

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

        public short File_name_length
        {
            get
            {
                return file_name_length;
            }

            set
            {
                file_name_length = value;
            }
        }

        public string File_name
        {
            get
            {
                return file_name;
            }

            set
            {
                file_name = value;
            }
        }
    }
}
