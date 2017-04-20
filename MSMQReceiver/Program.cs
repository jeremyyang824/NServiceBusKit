using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Threading;

namespace MSMQReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            string ekQ = ".\\Private$\\EKTestQueue";

            using (var queue = new MessageQueue(ekQ))
            {
                queue.Formatter = new XmlMessageFormatter(new[] { typeof(String) });
                while (!MessageQueue.Exists(ekQ))
                {
                    Console.WriteLine("No existing queue");
                    Thread.Sleep(1000);
                }

                while (true)
                {
                    var m = queue.Receive();
                    Console.WriteLine("Message Received {0} \n--------------", (string)m.Body);
                    Thread.Sleep(1000);  
                }
            }
        }
    }
}
