using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SelectAccount
    {
        private int uid;
        private string sel_info;

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

        public string Sel_info
        {
            get
            {
                return sel_info;
            }

            set
            {
                sel_info = value;
            }
        }
    }
}
