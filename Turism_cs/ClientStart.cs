using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Windows.Forms;
using ServerTurism.Networking;
using Turism_cs.Forms;
using Turism_cs.Networking;

namespace Turism_cs
{
    internal static class ClientStart
    {
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var serverProv = new BinaryServerFormatterSinkProvider
            {
                TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full
            };
            var clientProv = new BinaryClientFormatterSinkProvider();
            IDictionary props = new Hashtable
            {
                ["port"] = 0
            };
            var channel = new TcpChannel(props, clientProv, serverProv);
            ChannelServices.RegisterChannel(channel, false);
            var server = (Server)Activator.GetObject(typeof(Server), "tcp://localhost:55555/TurismServer");
          
            var client = new ClientRmi(server);
            var loginForm = new LoginForm(client);
            if (loginForm.ShowDialog() == DialogResult.OK)
                Application.Run(new MainWindow(client));
            else
                Application.Exit();
        }
    }
}