using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.PubSub.Souscripteur
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var nomSouscription = args[0];

            var souscripteur = new Souscripteur(nomSouscription);

            await souscripteur.RecevoirMessagesAsync();

            await souscripteur.ArreterReceptionMessages();

            Console.ReadLine();
        }

    }
}
