using System;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMq
{
    class Program
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        private const string ExchangeName = "Rabbit.Test.Events";
        private const string QueueName = "TestQueue";

        static void Main(string[] args)
        {
            var newMessage1 = "this is test message 1";
            var newMessage2 = "this is test message 2";
            var newMessage3 = "this is test message 3";

            CreateQueue();

            SendMessage(newMessage1);
            SendMessage(newMessage2);
            SendMessage(newMessage3);
        }

        private static void CreateQueue()
        {
            _factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();

            _model.QueueDeclare(QueueName, true, false, false, null);
        }

        private static void SendMessage(string message)
        {
            _model.BasicPublish("", QueueName, null, Encoding.UTF8.GetBytes(message));
        }
    }
}
