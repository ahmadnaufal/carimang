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
    public partial class FormTambahRuangan : Form {
        public FormTambahRuangan() {
            InitializeComponent();
            foreach (var tipe in Enum.GetValues(typeof(Ruangan.TipeRuangan))) {
                comboBox1.Items.Add(tipe.ToString());
            }
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

        private void button1_Click(object sender, EventArgs e) {
            this.Nama = textBox1.Text;            
            this.Kapasitas = (int)numericUpDown1.Value;
            this.Tipe = (Ruangan.TipeRuangan)comboBox1.SelectedIndex;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
