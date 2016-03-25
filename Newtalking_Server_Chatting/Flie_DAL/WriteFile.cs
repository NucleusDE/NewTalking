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
        FileStream fileStream;
        bool initFlag = false;

        public WriteFile(string path)
        {
            initFlag = File.Exists(path);
            fileStream = new FileStream(path, FileMode.Create,FileAccess.Write);
        }

        public void Write(byte[] data)
        {
            fileStream.Write(data, 0, data.Length);
        }
    }
}
