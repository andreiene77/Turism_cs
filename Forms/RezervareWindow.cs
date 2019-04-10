using System;
using System.Windows.Forms;
using Turism_cs.Service;

namespace Turism_cs.Forms
{
    public partial class RezervareWindow : Form
    {
        private RezervareService rezervareService;
        private Excursie ex;

        public RezervareWindow(RezervareService rezervareService, Agentie user, Excursie ex)
        {
            InitializeComponent();
            this.rezervareService = rezervareService;
            this.rezervareService.User = user;
            this.ex = ex;
        }

        private void TextBoxBilete_TextChanged(object sender, EventArgs e) { }

        private void ButtonRezerva_Click(object sender, EventArgs e)
        {
            string nume = textBoxNume.Text;
            string telefon = textBoxTelefon.Text;
            int nrBilete = int.Parse(textBoxBilete.Text);
            try
            {
                rezervareService.Rezerva(ex, nume, telefon, nrBilete);
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
        }
    }
}
