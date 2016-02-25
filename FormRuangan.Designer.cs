namespace CariMang {
    partial class FormRuangan {
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
            this.labelNama = new System.Windows.Forms.Label();
            this.textNama = new System.Windows.Forms.TextBox();
            this.labelKapasitas = new System.Windows.Forms.Label();
            this.numKapasitas = new System.Windows.Forms.NumericUpDown();
            this.comboTipe = new System.Windows.Forms.ComboBox();
            this.labelTipe = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numKapasitas)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(116, 106);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(197, 106);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Batal";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelNama
            // 
            this.labelNama.AutoSize = true;
            this.labelNama.Location = new System.Drawing.Point(13, 13);
            this.labelNama.Name = "labelNama";
            this.labelNama.Size = new System.Drawing.Size(35, 13);
            this.labelNama.TabIndex = 0;
            this.labelNama.Text = "Nama";
            // 
            // textNama
            // 
            this.textNama.Location = new System.Drawing.Point(87, 12);
            this.textNama.Name = "textNama";
            this.textNama.Size = new System.Drawing.Size(185, 20);
            this.textNama.TabIndex = 1;
            // 
            // labelKapasitas
            // 
            this.labelKapasitas.AutoSize = true;
            this.labelKapasitas.Location = new System.Drawing.Point(13, 75);
            this.labelKapasitas.Name = "labelKapasitas";
            this.labelKapasitas.Size = new System.Drawing.Size(53, 13);
            this.labelKapasitas.TabIndex = 4;
            this.labelKapasitas.Text = "Kapasitas";
            // 
            // numKapasitas
            // 
            this.numKapasitas.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numKapasitas.Location = new System.Drawing.Point(87, 75);
            this.numKapasitas.Maximum = new decimal(new int[] {
            501,
            0,
            0,
            0});
            this.numKapasitas.Name = "numKapasitas";
            this.numKapasitas.Size = new System.Drawing.Size(47, 20);
            this.numKapasitas.TabIndex = 5;
            this.numKapasitas.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // comboTipe
            // 
            this.comboTipe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTipe.FormattingEnabled = true;
            this.comboTipe.Location = new System.Drawing.Point(87, 44);
            this.comboTipe.Name = "comboTipe";
            this.comboTipe.Size = new System.Drawing.Size(185, 21);
            this.comboTipe.TabIndex = 3;
            // 
            // labelTipe
            // 
            this.labelTipe.AutoSize = true;
            this.labelTipe.Location = new System.Drawing.Point(13, 44);
            this.labelTipe.Name = "labelTipe";
            this.labelTipe.Size = new System.Drawing.Size(28, 13);
            this.labelTipe.TabIndex = 2;
            this.labelTipe.Text = "Tipe";
            // 
            // FormRuangan
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(284, 145);
            this.Controls.Add(this.labelTipe);
            this.Controls.Add(this.comboTipe);
            this.Controls.Add(this.numKapasitas);
            this.Controls.Add(this.labelKapasitas);
            this.Controls.Add(this.textNama);
            this.Controls.Add(this.labelNama);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRuangan";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ruangan";
            ((System.ComponentModel.ISupportInitialize)(this.numKapasitas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelNama;
        private System.Windows.Forms.TextBox textNama;
        private System.Windows.Forms.Label labelKapasitas;
        private System.Windows.Forms.NumericUpDown numKapasitas;
        private System.Windows.Forms.ComboBox comboTipe;
        private System.Windows.Forms.Label labelTipe;
    }
}