using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(Service.Service1));
            host.Open();
            Console.WriteLine("Server");
            Console.ReadLine();
            host.Close();
        }
    }
}
