using ElohimMare.EMBackend;
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
            EMConsole server = new EMConsole();
            server.StartConsole();
            server.Shutdown();
        }
    }
}
