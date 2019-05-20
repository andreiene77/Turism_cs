using System;
using ServerTurism.Networking;

namespace ServerTurism
{
    internal static class ProgramServer
    {
        private static void Main()
        {
            Console.WriteLine("Starting server");
            //Console.WriteLine(Utils.DiContainer.Container);
            var server = new Server(5252);
            server.Start();
            while (server.Running)
            {
                var cmd = Console.ReadLine();
                if (cmd == "stop")
                    server.Stop();
            }
        }
    }
}