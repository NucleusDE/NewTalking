using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace File_DAL
{
    public class FileCheck
    {
        private string dir = AppDomain.CurrentDomain.BaseDirectory;
        public FileCheck()
        {
            try {
                if (!Directory.Exists(dir + @"Data\"))
                    Directory.CreateDirectory(dir + @"Data\");
                dir += @"Data\";
                if (!Directory.Exists(dir + @"Users\"))
                    Directory.CreateDirectory(dir + @"Users\");
                if (!Directory.Exists(dir + @"ServerProperties\"))
                    Directory.CreateDirectory(dir + @"ServerProperties\");
                if (!Directory.Exists(dir + @"Files\"))
                    Directory.CreateDirectory(dir + @"Files\"); }
            catch
            {
                return;
            }
        }
        public string[] CheckCreateUserDir(int user_id)
        {
            try
            {
                if (!Directory.Exists(dir + "Files\\" + user_id + "\\"))
                    Directory.CreateDirectory(dir + "Files\\" + user_id + "\\");
                if (!Directory.Exists(dir + "Users\\" + user_id + "Information\\"))
                    Directory.CreateDirectory(dir + "Users\\" + user_id + "Information\\");
                if (!Directory.Exists(dir + "Users\\" + user_id + "Chatting\\"))
                    Directory.CreateDirectory(dir + "Users\\" + user_id + "Chatting\\");

                string[] arrAddr = new string[3];
                arrAddr[0] = dir + "Files\\" + user_id + "\\";
                arrAddr[1] = dir + "Users\\" + user_id + "Information\\";
                arrAddr[2] = dir + "Users\\" + user_id + "Chatting\\";

                return arrAddr;
            }
            catch
            {
                return null;
            }
        }


        public string SelUserFileDir(int user_id, string file_name)
        {
            try
            {
                CheckCreateUserDir(user_id);
                if (!File.Exists(dir + "Files\\" + user_id + "\\" + file_name))
                    return "failed";
                else
                    return dir + "Files\\" + user_id + "\\" + file_name;
            }
            catch
            {
                return "failed";
            }
        }

        public string SelUserImage(int user_id)
        {
            try
            {
                CheckCreateUserDir(user_id);
                if (!File.Exists(dir + user_id + "\\Information\\UserImage.txt"))
                    return "failed";
                else
                {
                    StreamReader sr = new StreamReader(dir + user_id + "\\Information\\UserImage.txt");
                    return sr.ReadLine();
                }
            }
            catch
            {
                return "failed";
            }
        }

    }
}