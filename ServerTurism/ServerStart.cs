using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using ServerTurism.Networking;

namespace ServerTurism
{
    internal static class ServerStart
    {
        private static void Main()
        {
            var serverProv = new BinaryServerFormatterSinkProvider
            {
                TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full
            };
            var clientProv = new BinaryClientFormatterSinkProvider();
            IDictionary props = new Hashtable
            {
                ["port"] = 55555
            };
            var channel = new TcpChannel(props, clientProv, serverProv);
            ChannelServices.RegisterChannel(channel, false);

            var server = new Server();
            RemotingServices.Marshal(server, "TurismServer");
            
            Console.WriteLine("Server started ...");
            Console.WriteLine("Press <enter> to exit...");
            Console.ReadLine();
        }
    }
}