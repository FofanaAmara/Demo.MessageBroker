using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.PointAPoint.Producteur
{
    class Program
    {


        static void Main(string[] args)
        {

            while (true)
            {

                Console.WriteLine("Combien de messages voulez vous envoyer ? ");

                try
                {
                    var nombreMessages = int.Parse(Console.ReadLine());

                    var tacheEnvoi = Envoyer(nombreMessages);

                    Task.WaitAny(tacheEnvoi);
                }
                catch
                {

                }


            }

        }

        public async static Task Envoyer(int nombreMessages)
        {
            Console.WriteLine("Envoi de " + nombreMessages + " au broker ...");

            var producteur = new Producteur();

            var messages = producteur.GenererMessages(nombreMessages);

            await producteur.ProduireAsync(messages);

        }


    }
}
