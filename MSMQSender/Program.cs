using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;

namespace MSMQSender
{
    class Program
    {
        static void Main(string[] args)
        {
            //declare the MQ Path  
            string ekQ = ".\\Private$\\EKTestQueue";

            //create the MQ if the MQ is not exist  
            if (!MessageQueue.Exists(ekQ))
                MessageQueue.Create(ekQ);

            //create a new queue  
            using (var queue = new MessageQueue(ekQ))
            { 
                for (int i = 0; i < 2; i++)
                {
                    //create the model that want to send  
                    UserMessage user = new UserMessage();
                    user.Name = "jeremyyang824";
                    user.Sex = "M";
                    //serialize the model  
                    string str = xmlSerial(user);
                    //send the model data to queue  
                    queue.Send("Test" + str);
                    Console.WriteLine("Message sent {0} \n--------------", "Test" + str);
                }
            }
            Console.Read();
            //MessageQueue.Delete(ekQ);  
        }

        public static string xmlSerial<T>(T serializeClass)
        {
            string xmlString = string.Empty;
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringBuilder xmlStringBuilder = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(xmlStringBuilder))
            {
                serializer.Serialize(writer, serializeClass);
                xmlString = xmlStringBuilder.ToString();
            }

            return xmlString;
        }
    }
}
