using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.MessageBus
{
    public class AzureServiceBusMessageBus : IMessageBus
    {
        private string connectionString = "Endpoint=sb://mangorestaurantmicroservice.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=NJw9qug97tUleWMbT7ZY9tOpKAsO65y6N+ASbFf6YMY=";

        public async Task PublishMessage(BaseMessage message, string topicName)
        {
            await using var client = new ServiceBusClient(connectionString);

            ServiceBusSender sender = client.CreateSender(topicName); 

            var Jsonmessage = JsonConvert.SerializeObject(message);
            ServiceBusMessage  finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(Jsonmessage))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };

            await sender.SendMessageAsync(finalMessage);
            await client.DisposeAsync();
        }
    }
}
