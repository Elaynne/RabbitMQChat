using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQDemo
{
    public class RabbitMQSender : IDisposable
    {

        private const string Hostname = "localhost";
        private const string Password = "guest";
        private const string Username = "guest";
        private const string QueueName = "Module3.Sample1";
        private const string ExchangeName = "";
        private const bool IsDurable = true;
        private const string VirtualHost = "";
        private int Port = 0;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQSender()
        {
            DisplaySettings();
            SetupRabbitMq();
        }

        private void DisplaySettings()
        {
            Console.WriteLine("Host: {0}",Hostname);
            Console.WriteLine("QueueName: {0}", QueueName);
            Console.WriteLine("ExchangeName: {0}", ExchangeName);
            Console.WriteLine("VirtualHost: {0}", VirtualHost);
            Console.WriteLine("Port: {0}", Port);
            Console.WriteLine("Is Durable: {0}", IsDurable);
        }

        private void SetupRabbitMq()
        {
            ConnectionFactory _connectionFactory = new ConnectionFactory()
            {
                Password = Password,
                UserName = Username,
                HostName = Hostname
            };

            if (!string.IsNullOrEmpty(VirtualHost))
                _connectionFactory.VirtualHost = VirtualHost;
            if (Port > 0)
                _connectionFactory.Port = Port;

            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Send(string msg)
        {
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            byte[] msgBuffer = Encoding.UTF8.GetBytes(msg);
            _channel.BasicPublish(ExchangeName, QueueName, properties, msgBuffer);

            Console.WriteLine("\nMessage sent.");
            Console.ReadLine();
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();

            GC.SuppressFinalize(this);

        }
        
    }
}
