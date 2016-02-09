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
    public partial class FormMain : Form {       
        public FormMain() {
            InitializeComponent();
            InitializeLayout();
        }

        private const int EM_SETCUEBANNER = 0x1501;

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]string lParam);

        private void InitializeLayout() {
            //SendMessage(textBoxNama.Handle, EM_SETCUEBANNER, 0, "Nama Ruangan");
            tabData.Tag = true;
            pageDataJadwal.Tag = true;            
        }

        private void button4_Click(object sender, EventArgs e) {
            MessageBox.Show(dateTimePicker1.Value.ToString());
        }

        private void buttonRuanganTambah_Click(object sender, EventArgs e) {
            using (FormTambahRuangan form = new FormTambahRuangan()) {
                if (form.ShowDialog() == DialogResult.OK) {
                    MessageBox.Show("ads");
                }                                
            }
        }
    }
}
