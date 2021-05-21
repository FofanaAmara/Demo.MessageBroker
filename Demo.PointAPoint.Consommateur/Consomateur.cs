using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.PointAPoint.Consommateur
{
    public class Consomateur
    {
        private string _chaineConnexion = "chaineConnexionAzure";
        private string _nomFile = "queue1";

        public async void RecevoirAsync()
        {
            var client = new ServiceBusClient(_chaineConnexion);

            ServiceBusReceiver consommateur = client.CreateReceiver(_nomFile);

            while (true)
            {
                Console.WriteLine("En attente de message sur la file : " + _nomFile + " ...");

                try
                {
                    ServiceBusReceivedMessage messageRecu = await consommateur.ReceiveMessageAsync(TimeSpan.FromMilliseconds(2000));

                    if (messageRecu is null)
                        continue;

                    string contenuMessage = messageRecu.Body.ToString();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Message recu : " + contenuMessage);
                    Console.ForegroundColor = ConsoleColor.White;

                    await consommateur.CompleteMessageAsync(messageRecu);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Confirmation de traitement Envoyée  : " + contenuMessage);
                    Console.ForegroundColor = ConsoleColor.White;




                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Une erreur est survenue lors de la reception du message : " + e.Message);
                    Console.ForegroundColor = ConsoleColor.White;

                }

            }


        }
    }
}
