using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.RabbitMq;
using ServiceStack.Templates;
using SSRQ.ServiceInterface.DTO;
using SSRQ.ServiceInterface.Routing;
using System;
using System.Linq;

namespace SSRQ.ServiceInterface
{
    public class ClientsService : Service
    {

        private IMongoCollection<Client> _clientsDb;

        public ClientsService()
        {
            var client = new MongoClient();
            var _db = client.GetDatabase("SSRQ");
            _clientsDb = _db.GetCollection<Client>("clients");
        }

        //Return index.html for unmatched requests so routing is handled on client
        //public object Any(FallbackForClientRoutes request) =>
        //    new PageResult(Request.GetPage("/"));

        public ClientsResponse Get(Client client)
        {
            var filter = Builders<Client>.Filter.Eq("name", client.Name);
            var clients = _clientsDb.Find(_ => true);
            return new ClientsResponse
            {
                Response = "Yay",
                Clients = clients.ToList().ToHashSet()
            };
        }

        public ClientsResponse Put(Client client)
        {
            _clientsDb.InsertOne(new Client
            {
                Id = new Random().Next(40),
                Name = $"Client_{new Random().Next(40)}"
            });
            return new ClientsResponse { Response = "Success" };
        }

        public ClientsResponse Post(Client client)
        {
            var mqServer = new RabbitMqServer("localhost")
            {
                CreateQueueFilter = (queueName, args) =>
                {
                    args["x-dead-letter-exchange"] = "mx.servicestack.dlq";
                    args["x-dead-letter-routing-key"] = "mq:Client.dlq";
                },
            };
            using (var mqClient = mqServer.CreateMessageQueueClient())
            {
                mqClient.Publish(new Client { Name = "Client!", Id = client.Id });
                //var responseMsg = mqClient.Get<ClientsResponse>(QueueNames<ClientsResponse>.In);
                //mqClient.Ack(responseMsg);
            }

            var message = client != null && client.Id != default(int) ? "Successfull" : "Error";
            return new ClientsResponse { Response = message };
        }
    }
}
