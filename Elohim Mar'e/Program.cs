using ElohimMare.Server;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ElohimMare
{
    class Program
    {
        static void Main(string[] args)
        {
            (new LDAPManager()).SearchForEntry("g16g2513");
            Console.ReadKey();
        }
    }

    class EmRPCServer
    {
        public EmRPCServer()
        {

        }

        public int InitServer()
        {
            
            return 0;
        }
    }
}
