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
                if (DaftarRuangan[i].Nama.Equals(perbaikan.NamaRuangan))
                {
                    comboRuangan.SelectedIndex = i;
                    break;
                }
            }

            dateTimeMulai.Value = perbaikan.TanggalMulai;
            dateTimeSelesai.Value = perbaikan.TanggalSelesai;

            textBoxDeskripsi.Text = perbaikan.Deskripsi;
        }        

        public string NamaRuangan {
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

        private void button1_Click(object sender, EventArgs e) {
            this.NamaRuangan = DaftarRuangan.ElementAt(comboRuangan.SelectedIndex).Nama;
            if (String.IsNullOrWhiteSpace(this.NamaRuangan)) {
                MessageBox.Show("Nama ruangan tidak boleh kosong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.NamaRuangan = this.NamaRuangan.Trim();
            this.TanggalMulai = dateTimeMulai.Value;
            this.TanggalSelesai = dateTimeSelesai.Value;
            this.Deskripsi = textBoxDeskripsi.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
        }

        private void labelNama_Click(object sender, EventArgs e)
        {

        }
    }
}
