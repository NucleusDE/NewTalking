using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Newtalking_Service
{
    static public class ThreadPool
    {
        static ArrayList arrServiceThreads = new ArrayList();

        static public ArrayList ArrServiceThreads
        {
            get
            {
                return arrServiceThreads;
            }

            set
            {
                arrServiceThreads = value;
            }
        }
    }
}
