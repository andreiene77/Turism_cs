using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ServerTurism.Model;
using Turism_cs.Networking;

namespace Turism_cs.Forms
{
    public partial class MainWindow : Form
    {
        private BindingList<Excursie> excursiiList;

        public MainWindow(Client client)
        {
            InitializeComponent();
            Client = client;
            Client.LoggedOut += Client_LoggedOut;
            Client.ExcursiiListChanged += Client_ExcursiiListChanged;

            excursiiList = new BindingList<Excursie>();
            dataGridViewExcursii.DataSource = excursiiList;
            dataGridViewExcursii.Columns["id"].Visible = false;
            dataGridViewExcursii.RowPrePaint += DataGridViewExcursii_RowPrePaint;
            dataGridViewExcursii.CellValueChanged += DataGridViewExcursii_CellValueChanged;

            Client.RequestGetExcursii();
        }

        private Client Client { get; }

        private void Client_ExcursiiListChanged(object sender, EventArgs e)
        {
            Invoke(new Action(UpdateExcursiiList));
        }

        private void UpdateExcursiiList()
        {
            excursiiList = new BindingList<Excursie>(Client.Excursii);
            dataGridViewExcursii.DataSource = excursiiList;
        }

        private void Client_LoggedOut(object sender, EventArgs e)
        {
            BeginInvoke(new Action(Close));
        }

        private void ButtonSearch_Click(object sender, EventArgs e) =>
            new SearchWindow(Client).Show();

        private void ButtonBook_Click(object sender, EventArgs e)
        {
            var selectedExcursie = (Excursie) dataGridViewExcursii.CurrentRow?.DataBoundItem;
            var rezervareWindow = new RezervareWindow(Client) {Ex = selectedExcursie};
            rezervareWindow.Show();
        }

        private void DataGridViewExcursii_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (Convert.ToInt32(dataGridViewExcursii.Rows[e.RowIndex].Cells[5].Value) == 0)
                dataGridViewExcursii.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
        }

        private void DataGridViewExcursii_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewExcursii.Rows)
                if (Convert.ToInt32(row.Cells[5].Value) == 0)
                    row.DefaultCellStyle.ForeColor = Color.Red;
        }

        private void ButtonLogout_Click(object sender, EventArgs e)
        {
            Client.RequestLogout();
        }
    }
}