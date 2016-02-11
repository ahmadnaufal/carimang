namespace CariMang
{
    partial class FormPerbaikan
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelRuangan = new System.Windows.Forms.Label();
            this.labelTanggalSelesai = new System.Windows.Forms.Label();
            this.comboRuangan = new System.Windows.Forms.ComboBox();
            this.labelTanggalMulai = new System.Windows.Forms.Label();
            this.dateTimeMulai = new System.Windows.Forms.DateTimePicker();
            this.dateTimeSelesai = new System.Windows.Forms.DateTimePicker();
            this.textBoxDeskripsi = new System.Windows.Forms.TextBox();
            this.labelDeskripsi = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(152, 202);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(233, 202);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Batal";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelRuangan
            // 
            this.labelRuangan.AutoSize = true;
            this.labelRuangan.Location = new System.Drawing.Point(13, 13);
            this.labelRuangan.Name = "labelRuangan";
            this.labelRuangan.Size = new System.Drawing.Size(51, 13);
            this.labelRuangan.TabIndex = 2;
            this.labelRuangan.Text = "Ruangan";
            // 
            // labelTanggalSelesai
            // 
            this.labelTanggalSelesai.AutoSize = true;
            this.labelTanggalSelesai.Location = new System.Drawing.Point(13, 78);
            this.labelTanggalSelesai.Name = "labelTanggalSelesai";
            this.labelTanggalSelesai.Size = new System.Drawing.Size(83, 13);
            this.labelTanggalSelesai.TabIndex = 4;
            this.labelTanggalSelesai.Text = "Tanggal Selesai";
            // 
            // comboRuangan
            // 
            this.comboRuangan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRuangan.FormattingEnabled = true;
            this.comboRuangan.Location = new System.Drawing.Point(108, 10);
            this.comboRuangan.Name = "comboRuangan";
            this.comboRuangan.Size = new System.Drawing.Size(200, 21);
            this.comboRuangan.TabIndex = 6;
            // 
            // labelTanggalMulai
            // 
            this.labelTanggalMulai.AutoSize = true;
            this.labelTanggalMulai.Location = new System.Drawing.Point(13, 46);
            this.labelTanggalMulai.Name = "labelTanggalMulai";
            this.labelTanggalMulai.Size = new System.Drawing.Size(74, 13);
            this.labelTanggalMulai.TabIndex = 7;
            this.labelTanggalMulai.Text = "Tanggal Mulai";
            // 
            // dateTimeMulai
            // 
            this.dateTimeMulai.Location = new System.Drawing.Point(108, 42);
            this.dateTimeMulai.Name = "dateTimeMulai";
            this.dateTimeMulai.Size = new System.Drawing.Size(200, 20);
            this.dateTimeMulai.TabIndex = 8;
            // 
            // dateTimeSelesai
            // 
            this.dateTimeSelesai.Location = new System.Drawing.Point(108, 75);
            this.dateTimeSelesai.Name = "dateTimeSelesai";
            this.dateTimeSelesai.Size = new System.Drawing.Size(200, 20);
            this.dateTimeSelesai.TabIndex = 9;
            // 
            // textBoxDeskripsi
            // 
            this.textBoxDeskripsi.Location = new System.Drawing.Point(108, 108);
            this.textBoxDeskripsi.Multiline = true;
            this.textBoxDeskripsi.Name = "textBoxDeskripsi";
            this.textBoxDeskripsi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDeskripsi.Size = new System.Drawing.Size(200, 77);
            this.textBoxDeskripsi.TabIndex = 10;
            // 
            // labelDeskripsi
            // 
            this.labelDeskripsi.AutoSize = true;
            this.labelDeskripsi.Location = new System.Drawing.Point(14, 110);
            this.labelDeskripsi.Name = "labelDeskripsi";
            this.labelDeskripsi.Size = new System.Drawing.Size(50, 13);
            this.labelDeskripsi.TabIndex = 11;
            this.labelDeskripsi.Text = "Deskripsi";
            // 
            // FormPerbaikan
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(329, 237);
            this.Controls.Add(this.labelDeskripsi);
            this.Controls.Add(this.textBoxDeskripsi);
            this.Controls.Add(this.dateTimeSelesai);
            this.Controls.Add(this.dateTimeMulai);
            this.Controls.Add(this.labelTanggalMulai);
            this.Controls.Add(this.comboRuangan);
            this.Controls.Add(this.labelTanggalSelesai);
            this.Controls.Add(this.labelRuangan);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPerbaikan";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ruangan";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelRuangan;
        private System.Windows.Forms.Label labelTanggalSelesai;
        private System.Windows.Forms.ComboBox comboRuangan;
        private System.Windows.Forms.Label labelTanggalMulai;
        private System.Windows.Forms.DateTimePicker dateTimeMulai;
        private System.Windows.Forms.DateTimePicker dateTimeSelesai;
        private System.Windows.Forms.TextBox textBoxDeskripsi;
        private System.Windows.Forms.Label labelDeskripsi;
    }
}