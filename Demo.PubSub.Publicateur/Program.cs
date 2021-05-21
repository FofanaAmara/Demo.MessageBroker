using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.PubSub.Publicateur
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Combien de messages voulez vous envoyer ? ");

                try
                {
                    var nombreMessages = int.Parse(Console.ReadLine());

                    var tacheEnvoi = Publier(nombreMessages);

                    Task.WaitAny(tacheEnvoi);
                }
                catch
                {

                }


            }

        }

        public async static Task Publier(int nombreMessages)
        {
            Console.WriteLine("Envoi de " + nombreMessages + " au broker ...");

            var producteur = new Publicateur();

            var messages = producteur.GenererMessages(nombreMessages);

            await producteur.PublierAsync(messages);

        }
    }
}
