using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace Data
{
    public class Data
    {
        static private ArrayList arrOnlineUsers = new ArrayList();

        static public ArrayList ArrOnlineUsers
        {
            get { return arrOnlineUsers; }
            set { arrOnlineUsers = value; }
        }

        static private ArrayList arrSendingMessages = new ArrayList();

        static public ArrayList ArrSendingMessages
        {
            get { return arrSendingMessages; }
            set { arrSendingMessages = value; }
        }
    }
}
