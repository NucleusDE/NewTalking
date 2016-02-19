using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newtalking_User_Info
{
    public class User_Info
    {
        private static int user_id;

        public static int User_id
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
