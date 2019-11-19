using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQServerDemo
{
    public class RabbitMQConsumer
    {
        private const string Hostname = "localhost";
        private const string Password = "guest";
        private const string Username = "guest";
        private const string QueueName = "Module3.Sample1";
        private const string ExchangeName = "";
        private const bool IsDurable = true;
        private const string VirtualHost = "";
        private int Port = 0;

        public delegate void OnReceiveMessage(string msg);
        public bool Enabled { get;  set; }

        private IConnection _connection;
        private IModel _channel;


        public RabbitMQConsumer()
        {
            DisplaySettings();
            SetupRabbitMq();
        }

        private void DisplaySettings()
        {
            Console.WriteLine("Host: {0}", Hostname);
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
            _channel.BasicQos(0,1,false);
        }

        public void Start()
        {
            var consumer = new QueueingBasicConsumer(_channel);
            _channel.BasicConsume(QueueName, false, consumer);
            Console.WriteLine();

            while (true)
            {
                var deliveryArgs = consumer.Queue.Dequeue();
                var responseMsg = Encoding.UTF8.GetString(deliveryArgs.Body);
                Console.WriteLine("Message received: {0}", responseMsg);

                _channel.BasicAck(deliveryArgs.DeliveryTag, false);
            }

            /*BasicGetResult result = _channel.BasicGet(QueueName, false);
            if (result == null)
            {
                Console.WriteLine("No messages available.");
            }
            else {
                IBasicProperties props = result.BasicProperties;
                byte[] body = result.Body;
                string responseMsg = Encoding.Unicode.GetString(body, 0, body.Length);

                Console.WriteLine("Message received: {0}", responseMsg);

                _channel.BasicAck(result.DeliveryTag, false);
            }*/
            
        }
    }
}
