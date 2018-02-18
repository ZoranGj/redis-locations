using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;
using ServiceStack.Templates;
using ServiceStack.DataAnnotations;
using SSRQ.ServiceModel;
using ServiceStack.RabbitMq;
using ServiceStack.Messaging;
using SSRQ.ServiceInterface.Routing;

namespace SSRQ.ServiceInterface
{
    public class MyServices : Service
    {
        //Return index.html for unmatched requests so routing is handled on client
        public object Any(FallbackForClientRoutes request) =>
            new PageResult(Request.GetPage("/"));

        public object Any(Hello request)
        {
            var mqServer = new RabbitMqServer("localhost")
            {
                CreateQueueFilter = (queueName, args) =>
                {
                    args["x-dead-letter-exchange"] = "mx.servicestack.dlq";
                    args["x-dead-letter-routing-key"] = "mq:Hello.dlq";
                },
                //PublishMessageFilter = (queueName, properties, msg) =>
                //{
                //    properties.AppId = "app:{0}".Fmt(queueName);
                //},
                //GetMessageFilter = (queueName, basicMsg) =>
                //{
                //    var props = basicMsg.BasicProperties;
                //    //receivedMsgType = props.Type; //automatically added by RabbitMqProducer
                //    //receivedMsgApp = props.AppId;
                //},
            };
            using (var mqClient = mqServer.CreateMessageQueueClient())
            {
                mqClient.Publish(new Hello { Name = "Bugs Bunny" });
            }

            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }
    }
}
