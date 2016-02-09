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
            InitializeData();            
        }

        private const int EM_SETCUEBANNER = 0x1501;

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]string lParam);

        private void InitializeLayout() {
            //SendMessage(textBoxNama.Handle, EM_SETCUEBANNER, 0, "Nama Ruangan");
            tabData.Tag = true;
            pageDataJadwal.Tag = true;            
        }

        private void InitializeData() {
            this.GetAllRuangan();
        }

        private void GetAllRuangan() {
            listViewRuangan.Items.Clear();
            foreach (var ruangan in Ruangan.GetAll())
                this.AddRuangan(ruangan);
        }

        private void AddRuangan(Ruangan ruangan) {
            var item = new ListViewItem();
            item.Text = ruangan.Nama;                     
            item.SubItems.Add(ruangan.Tipe.ToString());
            item.SubItems.Add(ruangan.Kapasitas.ToString());
            item.Tag = ruangan;
            listViewRuangan.Items.Add(item);
        }

        private void EditRuangan(ListViewItem item) {
            Ruangan ruangan = (Ruangan)item.Tag;
            using (FormRuangan form = new FormRuangan(ruangan)) {
                if (form.ShowDialog() != DialogResult.OK)
                    return;
                ruangan.Nama = form.Nama;
                item.SubItems[0].Text = ruangan.Nama;

                ruangan.Tipe = form.Tipe;
                item.SubItems[1].Text = ruangan.Tipe.ToString();

                ruangan.Kapasitas = form.Kapasitas;
                item.SubItems[2].Text = ruangan.Kapasitas.ToString();                
            }
        }

        private void DeleteRuangan(ListViewItem item) {
            Ruangan ruangan = (Ruangan)item.Tag;
            if (MessageBox.Show("Mau dihapus " + ruangan.Nama + " ?", "Serius", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    != DialogResult.Yes)
                return;
            if (Ruangan.Delete(ruangan.Nama)) {                
                listViewRuangan.Items.Remove(item);                
            }
            else {
                MessageBox.Show("Gagal delete ruangan.");
            }            
        }                                            

        private void buttonRuanganTambah_Click(object sender, EventArgs e) {
            using (FormRuangan form = new FormRuangan()) {                
                if (form.ShowDialog() != DialogResult.OK)
                    return;
                var ruangan = Ruangan.Add(form.Tipe, form.Nama, form.Kapasitas);
                if (ruangan == null) {
                    MessageBox.Show("Gagal nambah ruangan.");
                    return;
                }
                this.AddRuangan(ruangan);
            }
        }

        private void buttonRuanganUbah_Click(object sender, EventArgs e) {
            foreach (ListViewItem item in listViewRuangan.Items) {
                if (item.Selected)
                    this.EditRuangan(item);
            }
        }

        private void buttonRuanganHapus_Click(object sender, EventArgs e) {
            foreach (ListViewItem item in listViewRuangan.Items) {
                if (item.Selected)
                    this.DeleteRuangan(item);                
            }
        }        
    }
}
