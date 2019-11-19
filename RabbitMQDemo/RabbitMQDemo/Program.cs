using System;

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
                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.Q)
                    break;

                if (input.Key == ConsoleKey.Enter)
                {
                    var msg = string.Format("Message: {0}", messageCount);
                    Console.WriteLine(string.Format("Sending - {0}", msg));
                    sender.Send(msg);
                    messageCount++;
                }
            }
            
            Console.ReadLine();
        }
    }
}
