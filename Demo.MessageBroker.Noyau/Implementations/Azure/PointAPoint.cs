using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.MessageBroker.Noyau.Implementations.Azure
{
    public class PointAPoint
    {

        public string ChaineConnexion { get; set; }
        public string NomFile { get; set; }

        public async void Produire()
        {
            var client = new ServiceBusClient(ChaineConnexion);

            ServiceBusSender producteur = client.CreateSender(NomFile);

            ServiceBusMessage message = new ServiceBusMessage("Bonjour CGI!");

            await producteur.SendMessageAsync(message);
        }

        public void Consommer()
        {

        }
    }
}
