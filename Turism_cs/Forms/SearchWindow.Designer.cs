namespace Turism_cs.Forms
{
    partial class SearchWindow
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
            this.dataGridViewExcursii = new System.Windows.Forms.DataGridView();
            this.textBoxObiectiv = new System.Windows.Forms.TextBox();
            this.textBoxOraStart = new System.Windows.Forms.TextBox();
            this.textBoxOraFinish = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExcursii)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewExcursii
            // 
            this.dataGridViewExcursii.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewExcursii.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewExcursii.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExcursii.Location = new System.Drawing.Point(218, 12);
            this.dataGridViewExcursii.Name = "dataGridViewExcursii";
            this.dataGridViewExcursii.Size = new System.Drawing.Size(570, 350);
            this.dataGridViewExcursii.TabIndex = 0;
            // 
            // textBoxObiectiv
            // 
            this.textBoxObiectiv.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxObiectiv.Location = new System.Drawing.Point(12, 64);
            this.textBoxObiectiv.Name = "textBoxObiectiv";
            this.textBoxObiectiv.Size = new System.Drawing.Size(200, 29);
            this.textBoxObiectiv.TabIndex = 1;
            this.textBoxObiectiv.TextChanged += new System.EventHandler(this.TextBoxObiectiv_TextChanged);
            // 
            // textBoxOraStart
            // 
            this.textBoxOraStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOraStart.Location = new System.Drawing.Point(12, 142);
            this.textBoxOraStart.Name = "textBoxOraStart";
            this.textBoxOraStart.Size = new System.Drawing.Size(95, 29);
            this.textBoxOraStart.TabIndex = 2;
            this.textBoxOraStart.TextChanged += new System.EventHandler(this.TextBoxOraStart_TextChanged);
            // 
            // textBoxOraFinish
            // 
            this.textBoxOraFinish.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOraFinish.Location = new System.Drawing.Point(12, 216);
            this.textBoxOraFinish.Name = "textBoxOraFinish";
            this.textBoxOraFinish.Size = new System.Drawing.Size(95, 29);
            this.textBoxOraFinish.TabIndex = 3;
            this.textBoxOraFinish.TextChanged += new System.EventHandler(this.TextBoxOraFinish_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 29);
            this.label1.TabIndex = 4;
            this.label1.Text = "Obiectiv: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "Ora min: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 29);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ora max:";
            // 
            // SearchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 381);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxOraFinish);
            this.Controls.Add(this.textBoxOraStart);
            this.Controls.Add(this.textBoxObiectiv);
            this.Controls.Add(this.dataGridViewExcursii);
            this.Name = "SearchWindow";
            this.Text = "SearchWindow";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExcursii)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewExcursii;
        private System.Windows.Forms.TextBox textBoxObiectiv;
        private System.Windows.Forms.TextBox textBoxOraStart;
        private System.Windows.Forms.TextBox textBoxOraFinish;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}