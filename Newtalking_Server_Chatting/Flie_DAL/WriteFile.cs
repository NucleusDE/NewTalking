using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.IO;

namespace File_DAL
{
    public class WriteFile
    {
        public FileStream fileStream;
        bool initFlag = false;
        string filePath;

        public WriteFile(string path)
        {
            filePath = path;
            initFlag = File.Exists(path);
            fileStream = new FileStream(path, FileMode.Create,FileAccess.Write);
        }

        public void Write(byte[] data)
        {
            fileStream.Write(data, 0, data.Length);
        }
        public bool Delete()
        {
            try {
                fileStream.Close();
                File.Delete(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
