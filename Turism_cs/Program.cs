using System;
using System.Windows.Forms;
using Turism_cs.Forms;
using Turism_cs.Networking;

namespace Turism_cs
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var client = new Client("localhost", 5252);
            client.Start();
            var loginForm = new LoginForm(client);
            if (loginForm.ShowDialog() == DialogResult.OK)
                Application.Run(new MainWindow(client));
            else
                Application.Exit();
            client.Stop();
        }
    }
}