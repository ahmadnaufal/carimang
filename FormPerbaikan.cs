using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CariMang {
    public partial class FormPerbaikan : Form {
        private List<Ruangan> DaftarRuangan = null;

        private void InitializeData() {
            this.DaftarRuangan = Ruangan.GetAll();
            foreach (var ruangan in this.DaftarRuangan)
                comboRuangan.Items.Add(ruangan.Nama);
        }

        public FormPerbaikan() {
            InitializeComponent();
            InitializeData();
            comboRuangan.SelectedIndex = comboRuangan.Items.Count > 0 ? 0 : -1;
        }

        public FormPerbaikan(Perbaikan perbaikan) {
            InitializeComponent();
            InitializeData();
            for (int i = 0; i < DaftarRuangan.Count; ++i)
            {
                if (DaftarRuangan[i].Equals(perbaikan.Ruangan))
                {
                    comboRuangan.SelectedIndex = i;
                    break;
                }
            }

            dateTimeMulai.Value = perbaikan.TanggalMulai.Date;
            dateTimeSelesai.Value = perbaikan.TanggalSelesai.Date;

            textBoxDeskripsi.Text = perbaikan.Deskripsi;
        }        

        public Ruangan Ruangan {
            get; set;
        }

        public DateTime TanggalMulai {
            get; set;
        }

        public DateTime TanggalSelesai {
            get; set;
        }

        public string Deskripsi {
            get; set;
        }

        private void buttonOK_Click(object sender, EventArgs e) {
            this.Ruangan = DaftarRuangan.ElementAt(comboRuangan.SelectedIndex);                        
            this.TanggalMulai = dateTimeMulai.Value;
            this.TanggalSelesai = dateTimeSelesai.Value;            
            this.Deskripsi = textBoxDeskripsi.Text;

            if (this.TanggalMulai > this.TanggalSelesai) {
                MessageBox.Show("Tanggal mulai harus lebih kecil/sama dengan tanggal selesai.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
