using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server_Properties
{
    public class Data
    {
        static System.Collections.ArrayList arrMsgCallBack = new System.Collections.ArrayList();

        public static ArrayList ArrMsgCallBack
        {
            get
            {
                return arrMsgCallBack;
            }

            set
            {
                arrMsgCallBack = value;
            }
        }
    }
}
