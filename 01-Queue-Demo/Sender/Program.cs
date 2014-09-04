using System;
using Messages;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            SendMessages();
            Console.ReadKey();
        }

        private static void SendMessages()
        {
            var connectionString =
                CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

            var client =
                QueueClient.CreateFromConnectionString(connectionString, "TestQueue");

            client.Send(new BrokeredMessage(new NewUserRegistered{Id = 1, FullName = "User 01"}));
        }
    }
}
