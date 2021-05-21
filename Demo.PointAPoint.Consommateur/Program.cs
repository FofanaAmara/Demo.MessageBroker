using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.PointAPoint.Consommateur
{
    class Program
    {
        static void Main(string[] args)
        {
            var consomateur = new Consomateur();

            consomateur.RecevoirAsync();

            Console.ReadLine();
        }


    }
}
