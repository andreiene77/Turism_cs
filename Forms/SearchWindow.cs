using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Turism_cs.Service;

namespace Turism_cs.Forms
{
    public partial class SearchWindow : Form
    {
        private ExcursiiManagementService excursiiManagementService;
        BindingList<Excursie> excursiiFiltered;
        //BindingSource excursiiFilteredSource;
        public SearchWindow(ExcursiiManagementService excursiiManagementService)
        {
            InitializeComponent();
            this.excursiiManagementService = excursiiManagementService;
            excursiiFiltered = new BindingList<Excursie>(excursiiManagementService.ExcursiiFiltered);
            //excursiiFilteredSource = new BindingSource(excursiiManagementService, "ExcursiiFiltered");
            //excursiiFilteredSource = new BindingSource();
            //excursiiFilteredSource.DataSource = excursiiFiltered;
            dataGridViewExcursii.DataSource = excursiiFiltered;
            dataGridViewExcursii.Columns["id"].Visible = false;
            dataGridViewExcursii.RowPrePaint += new DataGridViewRowPrePaintEventHandler(DataGridViewExcursii_RowPrePaint);
            dataGridViewExcursii.CellValueChanged += new DataGridViewCellEventHandler(DataGridViewExcursii_CellValueChanged);
        }

        private void UpdateDataGrid()
        {
            string obiectiv = textBoxObiectiv.Text;
            TimeSpan oraStart;
            TimeSpan oraFinish;
            try { oraStart = TimeSpan.Parse(textBoxOraStart.Text); }
            catch (FormatException) { oraStart = new TimeSpan(0, 0, 0); }
            try { oraFinish = TimeSpan.Parse(textBoxOraFinish.Text); }
            catch (FormatException) { oraFinish = new TimeSpan(23, 59, 59); }

            excursiiManagementService.UpdateExcursiiFiltered(obiectiv, oraStart, oraFinish);
            excursiiFiltered = new BindingList<Excursie>(excursiiManagementService.ExcursiiFiltered);
            dataGridViewExcursii.DataSource = excursiiFiltered;
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
            {
                dataGridViewExcursii.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
            }
        }
        private void DataGridViewExcursii_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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
