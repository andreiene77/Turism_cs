using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ServerTurism.Model;
using Turism_cs.Networking;

namespace Turism_cs.Forms
{
    public partial class SearchWindow : Form
    {
        private BindingList<Excursie> excursiiFiltered;

        public SearchWindow(ClientRmi client)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Client = client;
            Client.ExcursiiFilteredListChanged += Client_ExcursiiFilteredListChanged;

            excursiiFiltered = new BindingList<Excursie>();
            dataGridViewExcursii.DataSource = excursiiFiltered;
            dataGridViewExcursii.Columns["id"].Visible = false;
            dataGridViewExcursii.RowPrePaint += DataGridViewExcursii_RowPrePaint;
            dataGridViewExcursii.CellValueChanged += DataGridViewExcursii_CellValueChanged;

            UpdateDataGrid();
        }

        private ClientRmi Client { get; }

        private void Client_ExcursiiFilteredListChanged(object sender, EventArgs e) => UpdateExcursiiFiltered();

        private void UpdateExcursiiFiltered()
        {
            excursiiFiltered = new BindingList<Excursie>(Client.ExcursiiFiltered);
            dataGridViewExcursii.DataSource = excursiiFiltered;
        }

        private void UpdateDataGrid()
        {
            var obiectiv = textBoxObiectiv.Text;
            TimeSpan oraStart;
            TimeSpan oraFinish;
            try
            {
                oraStart = TimeSpan.Parse(textBoxOraStart.Text);
            }
            catch (FormatException)
            {
                oraStart = new TimeSpan(0, 0, 0);
            }

            try
            {
                oraFinish = TimeSpan.Parse(textBoxOraFinish.Text);
            }
            catch (FormatException)
            {
                oraFinish = new TimeSpan(23, 59, 59);
            }

            Client.RequestFilteredExcursii(obiectiv, oraStart, oraFinish);
        }

        private void TextBoxObiectiv_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        private void TextBoxOraStart_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        private void TextBoxOraFinish_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();
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
    }
}