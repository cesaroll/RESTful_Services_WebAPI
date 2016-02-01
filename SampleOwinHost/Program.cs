using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace SampleOwinHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<StartUp>("http://+:8080"))
            {
                Console.WriteLine("OWIN Running ...");
                Console.ReadKey();

            }
        }
    }
}
