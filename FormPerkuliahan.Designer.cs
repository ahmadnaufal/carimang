namespace CariMang {
    partial class FormPerkuliahan {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelKuliah = new System.Windows.Forms.Label();
            this.comboRuangan = new System.Windows.Forms.ComboBox();
            this.labelRuangan = new System.Windows.Forms.Label();
            this.textTanggung = new System.Windows.Forms.TextBox();
            this.labelTanggung = new System.Windows.Forms.Label();
            this.labelHari = new System.Windows.Forms.Label();
            this.comboHari = new System.Windows.Forms.ComboBox();
            this.numWaktuMulai = new System.Windows.Forms.NumericUpDown();
            this.labelPukul = new System.Windows.Forms.Label();
            this.labelSd = new System.Windows.Forms.Label();
            this.numWaktuSelesai = new System.Windows.Forms.NumericUpDown();
            this.comboKuliah = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numWaktuMulai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaktuSelesai)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(149, 161);
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
            this.buttonCancel.Location = new System.Drawing.Point(230, 161);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Batal";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelKuliah
            // 
            this.labelKuliah.AutoSize = true;
            this.labelKuliah.Location = new System.Drawing.Point(12, 16);
            this.labelKuliah.Name = "labelKuliah";
            this.labelKuliah.Size = new System.Drawing.Size(67, 13);
            this.labelKuliah.TabIndex = 2;
            this.labelKuliah.Text = "Nama Kuliah";
            // 
            // comboRuangan
            // 
            this.comboRuangan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRuangan.FormattingEnabled = true;
            this.comboRuangan.Location = new System.Drawing.Point(120, 65);
            this.comboRuangan.Name = "comboRuangan";
            this.comboRuangan.Size = new System.Drawing.Size(185, 21);
            this.comboRuangan.TabIndex = 6;
            // 
            // labelRuangan
            // 
            this.labelRuangan.AutoSize = true;
            this.labelRuangan.Location = new System.Drawing.Point(12, 68);
            this.labelRuangan.Name = "labelRuangan";
            this.labelRuangan.Size = new System.Drawing.Size(71, 13);
            this.labelRuangan.TabIndex = 7;
            this.labelRuangan.Text = "Ruang Kuliah";
            // 
            // textTanggung
            // 
            this.textTanggung.Location = new System.Drawing.Point(120, 39);
            this.textTanggung.Name = "textTanggung";
            this.textTanggung.Size = new System.Drawing.Size(185, 20);
            this.textTanggung.TabIndex = 11;
            // 
            // labelTanggung
            // 
            this.labelTanggung.AutoSize = true;
            this.labelTanggung.Location = new System.Drawing.Point(12, 42);
            this.labelTanggung.Name = "labelTanggung";
            this.labelTanggung.Size = new System.Drawing.Size(102, 13);
            this.labelTanggung.TabIndex = 10;
            this.labelTanggung.Text = "Penanggung Jawab";
            // 
            // labelHari
            // 
            this.labelHari.AutoSize = true;
            this.labelHari.Location = new System.Drawing.Point(12, 95);
            this.labelHari.Name = "labelHari";
            this.labelHari.Size = new System.Drawing.Size(58, 13);
            this.labelHari.TabIndex = 13;
            this.labelHari.Text = "Hari Kuliah";
            // 
            // comboHari
            // 
            this.comboHari.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboHari.FormattingEnabled = true;
            this.comboHari.Location = new System.Drawing.Point(120, 92);
            this.comboHari.Name = "comboHari";
            this.comboHari.Size = new System.Drawing.Size(185, 21);
            this.comboHari.TabIndex = 12;
            // 
            // numWaktuMulai
            // 
            this.numWaktuMulai.Location = new System.Drawing.Point(120, 119);
            this.numWaktuMulai.Maximum = new decimal(new int[] {
            17,
            0,
            0,
            0});
            this.numWaktuMulai.Minimum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numWaktuMulai.Name = "numWaktuMulai";
            this.numWaktuMulai.Size = new System.Drawing.Size(54, 20);
            this.numWaktuMulai.TabIndex = 14;
            this.numWaktuMulai.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numWaktuMulai.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // labelPukul
            // 
            this.labelPukul.AutoSize = true;
            this.labelPukul.Location = new System.Drawing.Point(12, 121);
            this.labelPukul.Name = "labelPukul";
            this.labelPukul.Size = new System.Drawing.Size(34, 13);
            this.labelPukul.TabIndex = 15;
            this.labelPukul.Text = "Pukul";
            // 
            // labelSd
            // 
            this.labelSd.AutoSize = true;
            this.labelSd.Location = new System.Drawing.Point(180, 121);
            this.labelSd.Name = "labelSd";
            this.labelSd.Size = new System.Drawing.Size(24, 13);
            this.labelSd.TabIndex = 16;
            this.labelSd.Text = "s.d.";
            // 
            // numWaktuSelesai
            // 
            this.numWaktuSelesai.Location = new System.Drawing.Point(210, 119);
            this.numWaktuSelesai.Maximum = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.numWaktuSelesai.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numWaktuSelesai.Name = "numWaktuSelesai";
            this.numWaktuSelesai.Size = new System.Drawing.Size(54, 20);
            this.numWaktuSelesai.TabIndex = 17;
            this.numWaktuSelesai.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numWaktuSelesai.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // comboKuliah
            // 
            this.comboKuliah.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboKuliah.FormattingEnabled = true;
            this.comboKuliah.Location = new System.Drawing.Point(120, 12);
            this.comboKuliah.Name = "comboKuliah";
            this.comboKuliah.Size = new System.Drawing.Size(185, 21);
            this.comboKuliah.TabIndex = 18;
            // 
            // FormPerkuliahan
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(323, 195);
            this.Controls.Add(this.comboKuliah);
            this.Controls.Add(this.numWaktuSelesai);
            this.Controls.Add(this.labelSd);
            this.Controls.Add(this.labelPukul);
            this.Controls.Add(this.numWaktuMulai);
            this.Controls.Add(this.labelHari);
            this.Controls.Add(this.comboHari);
            this.Controls.Add(this.textTanggung);
            this.Controls.Add(this.labelTanggung);
            this.Controls.Add(this.labelRuangan);
            this.Controls.Add(this.comboRuangan);
            this.Controls.Add(this.labelKuliah);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPerkuliahan";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Kuliah";
            ((System.ComponentModel.ISupportInitialize)(this.numWaktuMulai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaktuSelesai)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelKuliah;
        private System.Windows.Forms.ComboBox comboRuangan;
        private System.Windows.Forms.Label labelRuangan;
        private System.Windows.Forms.TextBox textTanggung;
        private System.Windows.Forms.Label labelTanggung;
        private System.Windows.Forms.Label labelHari;
        private System.Windows.Forms.ComboBox comboHari;
        private System.Windows.Forms.NumericUpDown numWaktuMulai;
        private System.Windows.Forms.Label labelPukul;
        private System.Windows.Forms.Label labelSd;
        private System.Windows.Forms.NumericUpDown numWaktuSelesai;
        private System.Windows.Forms.ComboBox comboKuliah;
    }
}