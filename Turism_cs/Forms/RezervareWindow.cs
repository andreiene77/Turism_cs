using System;
using System.Windows.Forms;
using ServerTurism.Model;
using Turism_cs.Networking;

namespace Turism_cs.Forms
{
    public partial class RezervareWindow : Form
    {
        public RezervareWindow(Client client)
        {
            InitializeComponent();
            Client = client;
        }

        public Excursie Ex { get; set; }
        private Client Client { get; }

        private void TextBoxBilete_TextChanged(object sender, EventArgs e)
        {
        }

        private void ButtonRezerva_Click(object sender, EventArgs e)
        {
            var nume = textBoxNume.Text;
            var telefon = textBoxTelefon.Text;
            var nrBilete = int.Parse(textBoxBilete.Text);
            try
            {
                Client.RequestRezervare(Ex, nume, telefon, nrBilete);
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}