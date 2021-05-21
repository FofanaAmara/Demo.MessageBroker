using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.PubSub.Publicateur
{
    public class Publicateur
    {
        private string _chaineConnexion = "chaineConnexionAzure";
        private string _nomTopic = "nomTopic";


        public List<ServiceBusMessage> GenererMessages(int nombreMessages)
        {
            var messages = new List<ServiceBusMessage>();

            for (int i = 1; i <= nombreMessages; i++)
            {
                messages.Add(new ServiceBusMessage("Message " + i));
            }

            return messages;
        }

        public async Task PublierAsync(List<ServiceBusMessage> messages)
        {
            var client = new ServiceBusClient(_chaineConnexion);

            // create a sender for the topic
            ServiceBusSender publicateur = client.CreateSender(_nomTopic);

            foreach (var message in messages)
            {
                Console.WriteLine("Envoi de \"" + message.Body.ToString() + "\" vers le topic : " + _nomTopic);

                try
                {
                    await publicateur.SendMessageAsync(message);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Message envoyé avec suucès !");
                    Console.ForegroundColor = ConsoleColor.White;

                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Une erreur est survenue lors de l'envoi : " + e.Message);
                    Console.ForegroundColor = ConsoleColor.White;

                }
            }

        }
    }
}
