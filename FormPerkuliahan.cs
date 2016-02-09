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
            for (int i = 0; i < comboKuliah.Items.Count; ++i) {
                if (comboKuliah.Items[i].Equals(perkuliahan.Kuliah.Nama)) {
                    comboKuliah.SelectedIndex = i;
                    break;
                }
            }
            textTanggung.Text = perkuliahan.PenanggungJawab;
            for (int i = 0; i < comboKuliah.Items.Count; ++i) {
                if (comboRuangan.Items[i].Equals(perkuliahan.Ruangan.Nama)) {
                    comboRuangan.SelectedIndex = i;
                    break;
                }
            }
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
            if (String.IsNullOrWhiteSpace(textTanggung.Text)) {
                MessageBox.Show("Nama penanggung jawab tidak boleh kosong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Kuliah = DaftarKuliah.ElementAt(comboKuliah.SelectedIndex);
            this.Ruangan = DaftarRuangan.ElementAt(comboRuangan.SelectedIndex);
            this.HariKuliah = comboHari.SelectedIndex;
            this.WaktuMulai = (int)numWaktuMulai.Value;
            this.WaktuSelesai = (int)numWaktuSelesai.Value;
            this.PenanggungJawab = textTanggung.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
