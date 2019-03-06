using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServidorHttp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

            int puerto = 8010;
            Servidor servidor = new Servidor();
            servidor.Iniciar(puerto);

            Console.ReadKey();


            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
