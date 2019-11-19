using System;
using System.Threading.Tasks;

namespace RabbitMQServerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting RabbitMQ processor \n");

            var queueProcessor = new RabbitMQConsumer() { Enabled = true };
            queueProcessor.Start();
            Console.ReadLine();
        }   
    }
}
