using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMq.Send
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("My.TestExchange", ExchangeType.Topic, true, false, null);

                channel.QueueDeclare(queue: "My.TestQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonConvert.SerializeObject(new Student());
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "My.TestExchange",
                                     routingKey: "TestRouting",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }

    class Student
    {
        public string Name { get; set; } = "Nika";
        public string LastName { get; set; } = "Tabatadze";
        public int Age { get; set; } = 21;
    }
}
