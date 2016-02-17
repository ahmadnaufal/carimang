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
    public partial class FormPerkuliahan : Form {
        private List<Ruangan> DaftarRuangan = null;
        private List<Kuliah> DaftarKuliah = null;        

        private void InitializeData() {
            this.DaftarRuangan = Ruangan.GetAll();
            this.DaftarKuliah = Kuliah.GetAll();            
            foreach (var ruangan in this.DaftarRuangan)
                comboRuangan.Items.Add(ruangan.Nama);            
            foreach (var kuliah in this.DaftarKuliah)
                comboKuliah.Items.Add(kuliah.Kode + " - " + kuliah.Nama);
            foreach (var hari in Enum.GetNames(typeof(Perkuliahan.DaftarHari)))            
                comboHari.Items.Add(hari.ToString());                                
        }

        public FormPerkuliahan() {
            InitializeComponent();
            InitializeData();
            comboRuangan.SelectedIndex = comboRuangan.Items.Count > 0 ? 0 : -1;
            comboKuliah.SelectedIndex = comboKuliah.Items.Count > 0 ? 0 : -1;
            comboHari.SelectedIndex = comboHari.Items.Count > 0 ? 0 : -1;
        }

        public FormPerkuliahan(Perkuliahan perkuliahan) {
            InitializeComponent();
            InitializeData();
            for (int i = 0; i < DaftarKuliah.Count; ++i) {
                if (DaftarKuliah[i].Equals(perkuliahan.Kuliah)) {
                    comboKuliah.SelectedIndex = i;
                    break;
                }
            }
            textTanggung.Text = perkuliahan.PenanggungJawab;
            for (int i = 0; i < DaftarRuangan.Count; ++i) {
                if (DaftarRuangan[i].Equals(perkuliahan.Ruangan)) {
                    comboRuangan.SelectedIndex = i;
                    break;
                }
            }
            comboHari.SelectedIndex = perkuliahan.HariPerkuliahan;
            numWaktuMulai.Value = (Decimal)perkuliahan.WaktuMulai;
            numWaktuSelesai.Value = (Decimal)perkuliahan.WaktuSelesai;
        }

        public Kuliah Kuliah {
            get; set;
        }

        public Ruangan Ruangan {
            get; set;
        }

        public int HariKuliah {
            get; set;
        }

        public int WaktuMulai {
            get; set;
        }

        public int WaktuSelesai {
            get; set;
        }

        public string PenanggungJawab {
            get; set;
        }

        private void buttonOK_Click(object sender, EventArgs e) {                        
            this.Kuliah = DaftarKuliah.ElementAt(comboKuliah.SelectedIndex);
            this.Ruangan = DaftarRuangan.ElementAt(comboRuangan.SelectedIndex);
            this.HariKuliah = comboHari.SelectedIndex;
            this.WaktuMulai = (int)numWaktuMulai.Value;
            this.WaktuSelesai = (int)numWaktuSelesai.Value;
            this.PenanggungJawab = textTanggung.Text.Trim();

            if (String.IsNullOrWhiteSpace(this.PenanggungJawab)) {
                MessageBox.Show("Nama penanggung jawab tidak boleh kosong.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (this.WaktuMulai >= this.WaktuSelesai) {
                MessageBox.Show("Waktu mulai harus lebih kecil dari waktu selesai.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
