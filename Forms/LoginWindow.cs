using System;
using System.Windows.Forms;
using Turism_cs.Forms;
using Turism_cs.Service;

namespace Turism_cs
{
    public partial class LoginForm : Form
    {
        private LoginService service;
        public LoginForm(LoginService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            Agentie user = service.Login_user(username, password);
            if (user != null)
            {
                this.Hide();
                new MainWindow(Utils.DI_Container.container.MainWindowService, user).ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("WRONG CREDENTIALS");
            }
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void UsernameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
