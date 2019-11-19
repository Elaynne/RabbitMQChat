using System;
using System.Text;

namespace RabbitMQDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var messageCount = 0;
            var sender = new RabbitMQSender();

            Console.WriteLine("Starting RabbitMQ sender.Press enter to send a message");

            while (true)
            {
                var input = Console.ReadLine();
                var option = Console.ReadKey();

                if (option.Key == ConsoleKey.Q)
                    break;

                if (option.Key == ConsoleKey.Enter)
                {
                    StringBuilder sb = new StringBuilder(string.Format("Message: {0}", messageCount));
                    sb.Append(Environment.NewLine);
                    sb.Append(input.ToString());
                    var msg = sb.ToString();
                    
                    Console.WriteLine(string.Format("Sending - {0}", msg));
                    sender.Send(msg);
                    messageCount++;
                }
            }
            
            Console.ReadLine();
        }
    }
}
