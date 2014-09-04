using System;
using Messages;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ListenToMessages();
        }

        private static void ListenToMessages()
        {
            var connectionString =
                CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

            var client =
                QueueClient.CreateFromConnectionString(connectionString, "TestQueue");

            while (true)
            {
                var message = client.Receive();

                if (message != null)
                {
                    try
                    {
                        var payload = message.GetBody<NewUserRegistered>();
                        Console.WriteLine("MessageID: " + message.MessageId);
                        Console.WriteLine("User id #{0} name: {1}", payload.Id, payload.FullName);

                        // Remove message from queue
                        message.Complete();
                    }
                    catch
                    {
                        // Indicate a problem, unlock message in queue
                        message.Abandon();
                    }
                }
            }
        }
    }
}
