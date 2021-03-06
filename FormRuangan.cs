﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CariMang {
    public partial class FormRuangan : Form {
        private void InitializeData() {
            foreach (var tipe in Enum.GetValues(typeof(Ruangan.TipeRuangan))) {
                comboTipe.Items.Add(tipe.ToString());
            }
        }

        public FormRuangan() {
            InitializeComponent();
            InitializeData();
            comboTipe.SelectedIndex = comboTipe.Items.Count > 0 ? 0 : -1;
        }

        public FormRuangan(Ruangan ruangan) {
            InitializeComponent();
            InitializeData();
            this.Nama = this.textNama.Text = ruangan.Nama;
            this.textNama.Enabled = false;
            this.Tipe = ruangan.Tipe;
            this.comboTipe.SelectedIndex = (int)ruangan.Tipe;
            this.Kapasitas = ruangan.Kapasitas;
            this.numKapasitas.Value = (Decimal)ruangan.Kapasitas;            
        }        

        public string Nama {
            get; set;
        }

        public Ruangan.TipeRuangan Tipe {
            get; set;
        }

        public int Kapasitas {
            get; set;
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            this.Nama = textNama.Text.Trim();                        
            this.Kapasitas = (int)numKapasitas.Value;
            this.Tipe = (Ruangan.TipeRuangan)comboTipe.SelectedIndex;

            if (this.Kapasitas < Ruangan.MIN_KAPASITAS || this.Kapasitas > Ruangan.MAX_KAPASITAS) {
                MessageBox.Show(String.Format("Kapasitas tidak valid. ({0}-{1})", Ruangan.MIN_KAPASITAS, Ruangan.MAX_KAPASITAS), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (String.IsNullOrWhiteSpace(this.Nama)) {
                MessageBox.Show("Nama ruangan tidak boleh kosong.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
