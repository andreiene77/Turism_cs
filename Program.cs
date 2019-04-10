using System;
using System.Windows.Forms;

namespace Turism_cs
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm(Utils.DI_Container.container.LoginService));
        }
    }
}
