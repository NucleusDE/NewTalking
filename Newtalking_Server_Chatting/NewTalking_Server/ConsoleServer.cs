using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtalking_Service;

namespace NewTalking_Server
{
    class ConsoleServer
    {
        static string command = "";
        static void Main(string[] args)
        {
            Console.WriteLine("NewTaking_Server_Chatting V1.0\n\n\t>>> [Service Activing]");
            Service.ActiveService();
            Console.WriteLine("\t>>> [Service Actived]\n\n");
            do
            {
                Console.Write("NewTalking Server -->");
                command = Console.ReadLine();
                
                switch(command)
                {
                    case "exit":
                        Environment.Exit(0);
                        break;
                }
            } while (true);
        }
    }
}
