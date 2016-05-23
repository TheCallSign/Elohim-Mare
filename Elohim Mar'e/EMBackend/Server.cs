using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AustinHarris.JsonRpc;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace ElohimMare.EMBackend
{
    class Server
    {
        private static object _svc;
        static void Main(string[] args)
        {
            // must new up an instance of the service so it can be registered to handle requests.
            _svc = new exampleService();

            var rpcResultHandler = new AsyncCallback(
                state =>
                {
                    var async = ((JsonRpcStateAsync)state);
                    var result = async.Result;
                    var writer = ((StreamWriter)async.AsyncState);

                    writer.WriteLine(result);
                    writer.FlushAsync();
                });

            SocketsExample.SocketListener.start(3333, (writer, line) =>
            {
                var async = new JsonRpcStateAsync(rpcResultHandler, writer) { JsonRpc = line };
                JsonRpcProcessor.Process(async, writer);
            });
        }
    }
    class exampleService : JsonRpcService
    {
        [JsonRpcMethod] // handles JsonRpc like : {'method':'incr','params':[5],'id':1}
        private int incr(int i) { return i + 1; }

        [JsonRpcMethod] // handles JsonRpc like : {'method':'decr','params':[5],'id':1}
        private int decr(int i) { return i - 1; }
    }
}
namespace SocketsExample
{
    public class SocketListener
    {
        public static void start(int listenPort, Action<StreamWriter, string> handleRequest)
        {
            var server = new TcpListener(IPAddress.Parse("127.0.0.1"), listenPort);
            server.Start();
            Console.WriteLine(" You can connected with Putty on a (RAW session) to {0} to issue JsonRpc requests.", server.LocalEndpoint);
            while (true)
            {
                try
                {
                    using (var client = server.AcceptTcpClient())
                    using (var stream = client.GetStream())
                    {
                        Console.WriteLine("Client Connected..");
                        var reader = new StreamReader(stream, Encoding.UTF8);
                        var writer = new StreamWriter(stream, new UTF8Encoding(false));

                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            handleRequest(writer, line);

                            Console.WriteLine("REQUEST: {0}", line);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("RPCServer exception " + e);
                }
            }
        }
    }
}


