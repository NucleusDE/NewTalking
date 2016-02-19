using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Newtalking_BLL_Data;
using System.Threading;

namespace Test
{
    class ConsoleDemo
    {
        static void Main(string[] args)
        {
            Newtalking_BLL_Data.Data_Send sender = new Newtalking_BLL_Data.Data_Send();
            Console.WriteLine("Connecting...");
            if (sender.Connect())
                Console.WriteLine("Connection Created...");
            else {
                Console.WriteLine("falied!");
                return;
            }

            Console.WriteLine("Logining...");

            FuncMessageCallBack func = delegate (byte[] data)
              {
                  byte[] bIsSucceed = new byte[2];
                  bIsSucceed[0] = data[0];
                  bIsSucceed[1] = data[1];

                  if (BitConverter.ToBoolean(bIsSucceed, 0))
                  {
                      Console.Write("Logined");
                  }
                  else
                      Console.Write("Fail!");
              };

            Thread tdReceive = new Thread(delegate ()
            {
                Data_Receive receive = new Data_Receive();
                receive.Receive();
            });
            tdReceive.Start();

            sender.Login(1, "123456", func);

            Console.WriteLine("Sending Message...");
            func = delegate (byte[] data) {
                Console.WriteLine("Message Sent");
            };

            sender.SendMessage("hhhhhhh", 1, func);
        }
    }
}
