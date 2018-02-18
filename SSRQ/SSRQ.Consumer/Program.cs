using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSRQ.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            using (var connection = factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    var arguments = new Dictionary<string, object>();
                    //arguments.Add("x-message-ttl", 60000);
                    arguments.Add("x-dead-letter-exchange", "mx.servicestack.dlq");
                    arguments.Add("x-dead-letter-routing-key", "mq:Hello.dlq");

                    channel.QueueDeclare(queue: "mq:Hello.inq",
                                        durable: true,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: arguments);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($"Message received at {DateTime.Now.ToShortDateString()}: {message}");
                        Console.WriteLine("");
                    };

                    channel.BasicConsume(queue: "mq:Hello.inq",
                                 autoAck: true,
                                 consumer: consumer);


                    var clientConsumer = new EventingBasicConsumer(channel);
                    clientConsumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($"CLIENT {DateTime.Now.ToShortDateString()}: {message}");
                        Console.WriteLine("");

                        channel.BasicPublish(exchange: "", routingKey: "client", body : Encoding.UTF8.GetBytes("Client success!"));
                    };

                    channel.BasicConsume(queue: "mq:Client.inq",
                                 autoAck: true,
                                 consumer: clientConsumer);


                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
