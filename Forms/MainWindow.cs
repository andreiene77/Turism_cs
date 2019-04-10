using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Turism_cs.Service;
using Turism_cs.Utils;

namespace Turism_cs.Forms
{
    public partial class MainWindow : Form
    {
        private ExcursiiManagementService ExcursiiManagementService;
        private BindingList<Excursie> excursiiList;
        public MainWindow(ExcursiiManagementService excursiiManagementService, Agentie user)
        {
            InitializeComponent();
            ExcursiiManagementService = excursiiManagementService;
            ExcursiiManagementService.User = user;
            excursiiList = new BindingList<Excursie>(excursiiManagementService.Excursii);

            dataGridViewExcursii.DataSource = excursiiList;
            dataGridViewExcursii.Columns["id"].Visible = false;
            dataGridViewExcursii.RowPrePaint += new DataGridViewRowPrePaintEventHandler(dataGridViewExcursii_RowPrePaint);
            dataGridViewExcursii.CellValueChanged += new DataGridViewCellEventHandler(dataGridViewExcursii_CellValueChanged);
        }

        private void ButtonLogout_Click(object sender, System.EventArgs e)
        {
            Hide();
            new LoginForm(DI_Container.container.LoginService).ShowDialog();
            Close();
        }

        private void ButtonSearch_Click(object sender, System.EventArgs e)
        {
            new SearchWindow(ExcursiiManagementService).ShowDialog();
        }

        private void ButtonBook_Click(object sender, System.EventArgs e)
        {
            var selectedExcursie = (Excursie)dataGridViewExcursii.CurrentRow.DataBoundItem;
            new RezervareWindow(DI_Container.container.RezervareService, ExcursiiManagementService.User, selectedExcursie).Show();
        }
        private void dataGridViewExcursii_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (Convert.ToInt32(dataGridViewExcursii.Rows[e.RowIndex].Cells[5].Value) == 0)
            {
                dataGridViewExcursii.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
            }
        }
        private void dataGridViewExcursii_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewExcursii.Rows)
            {
                if (Convert.ToInt32(row.Cells[5].Value) == 0)
                {
                    row.DefaultCellStyle.ForeColor = Color.Red;
                }
            }
        }
    }
}
