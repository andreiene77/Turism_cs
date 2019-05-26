using System;
using System.Windows.Forms;
using Turism_cs.Networking;

namespace Turism_cs.Forms
{
    public partial class LoginForm : Form
    {
        private ClientRmi Client { get; }
        public LoginForm(ClientRmi client)
        {
            InitializeComponent();
            Client = client;
            Client.LoggedIn += Client_LoggedIn;
        }

        private void Client_LoggedIn(object sender, EventArgs e)
        {
            BeginInvoke(new Action(LoggedInAction));
        }

        private void LoggedInAction()
        {
            Close();
            DialogResult = DialogResult.OK;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            var username = usernameTextBox.Text;
            var password = passwordTextBox.Text;
            Client.RequestLogin(username, password);
        }
    }
}