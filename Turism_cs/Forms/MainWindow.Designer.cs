namespace Turism_cs.Forms
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridViewExcursii = new System.Windows.Forms.DataGridView();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonBook = new System.Windows.Forms.Button();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.mainWindowServiceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainWindowServiceBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.excursieRepositoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExcursii)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainWindowServiceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainWindowServiceBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.excursieRepositoryBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewExcursii
            // 
            this.dataGridViewExcursii.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewExcursii.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewExcursii.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExcursii.Location = new System.Drawing.Point(13, 13);
            this.dataGridViewExcursii.Name = "dataGridViewExcursii";
            this.dataGridViewExcursii.Size = new System.Drawing.Size(775, 325);
            this.dataGridViewExcursii.TabIndex = 0;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(12, 344);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(255, 35);
            this.buttonSearch.TabIndex = 1;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // buttonBook
            // 
            this.buttonBook.Location = new System.Drawing.Point(273, 344);
            this.buttonBook.Name = "buttonBook";
            this.buttonBook.Size = new System.Drawing.Size(255, 35);
            this.buttonBook.TabIndex = 2;
            this.buttonBook.Text = "Book";
            this.buttonBook.UseVisualStyleBackColor = true;
            this.buttonBook.Click += new System.EventHandler(this.ButtonBook_Click);
            // 
            // buttonLogout
            // 
            this.buttonLogout.Location = new System.Drawing.Point(533, 344);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(255, 35);
            this.buttonLogout.TabIndex = 3;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.ButtonLogout_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonLogout);
            this.Controls.Add(this.buttonBook);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.dataGridViewExcursii);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExcursii)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainWindowServiceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainWindowServiceBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.excursieRepositoryBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewExcursii;
        private System.Windows.Forms.BindingSource mainWindowServiceBindingSource;
        private System.Windows.Forms.BindingSource mainWindowServiceBindingSource1;
        private System.Windows.Forms.BindingSource excursieRepositoryBindingSource;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonBook;
        private System.Windows.Forms.Button buttonLogout;
    }
}