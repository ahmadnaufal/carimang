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

        private void InitializeLayout() {
            tabData.Tag = true;
            pageDataJadwal.Tag = true;
            pageStatistikRuangan.Tag = true;
        }

        private void InitializeData() {
            this.GetAllRuangan();
            this.GetAllPerkuliahan();
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
            if (Ruangan.Delete(ruangan)) {
                listViewRuangan.Items.Remove(item);
            }
            else {
                MessageBox.Show("Gagal delete ruangan.");
            }
        }

        private void GetAllPerkuliahan() {
            listViewKuliah.Items.Clear();
            foreach (var perkuliahan in Perkuliahan.GetAll())
                this.AddPerkuliahan(perkuliahan);
        }

        private void AddPerkuliahan(Perkuliahan perkuliahan) {
            var item = new ListViewItem();
            item.Text = perkuliahan.Kuliah.Kode;
            item.SubItems.Add(perkuliahan.Kuliah.Nama);
            item.SubItems.Add(perkuliahan.Ruangan.Nama);
            item.SubItems.Add(((Perkuliahan.DaftarHari)perkuliahan.HariPerkuliahan).ToString());
            item.SubItems.Add(String.Format("{0:00}:00 - {1:00}:00", perkuliahan.WaktuMulai, perkuliahan.WaktuSelesai));
            item.SubItems.Add(perkuliahan.PenanggungJawab);
            item.Tag = perkuliahan;
            listViewKuliah.Items.Add(item);
        }

        private void EditPerkuliahan(ListViewItem item) {
            Perkuliahan perkuliahan = (Perkuliahan)item.Tag;
            using (FormPerkuliahan form = new FormPerkuliahan(perkuliahan)) {
                if (form.ShowDialog() != DialogResult.OK)
                    return;
                perkuliahan.Kuliah = form.Kuliah;
                item.SubItems[0].Text = form.Kuliah.Kode;
                item.SubItems[1].Text = form.Kuliah.Nama;

                perkuliahan.Ruangan = form.Ruangan;
                item.SubItems[2].Text = form.Ruangan.Nama;

                perkuliahan.HariPerkuliahan = form.HariKuliah;
                item.SubItems[3].Text = ((Perkuliahan.DaftarHari)form.HariKuliah).ToString();

                perkuliahan.WaktuMulai = form.WaktuMulai;                
                perkuliahan.WaktuSelesai = form.WaktuSelesai;
                item.SubItems[4].Text = String.Format("{0:00}:00 - {1:00}:00", form.WaktuMulai, form.WaktuSelesai);

                perkuliahan.PenanggungJawab = form.PenanggungJawab;
                item.SubItems[5].Text = form.PenanggungJawab;
            }
        }

        private void DeletePerkuliahan(ListViewItem item) {
            Perkuliahan perkuliahan = (Perkuliahan)item.Tag;

            if (MessageBox.Show("Mau dihapus?", "Serius", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    != DialogResult.Yes)
                return;
            if (Perkuliahan.Delete(perkuliahan)) {
                listViewKuliah.Items.Remove(item);
            }
            else {
                MessageBox.Show("Gagal delete perkuliahan.");
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
                if (item.Selected) {
                    this.EditRuangan(item);
                    return;
                }                    
            }
        }

        private void buttonRuanganHapus_Click(object sender, EventArgs e) {
            foreach (ListViewItem item in listViewRuangan.Items) {
                if (item.Selected) {
                    this.DeleteRuangan(item);
                    return;
                }                    
            }
        }

        private void buttonKuliahTambah_Click(object sender, EventArgs e) {
            using (FormPerkuliahan form = new FormPerkuliahan()) {
                if (form.ShowDialog() != DialogResult.OK)
                    return;
                var perkuliahan = Perkuliahan.Add(
                    form.Kuliah, form.Ruangan, form.HariKuliah,
                    form.WaktuMulai, form.WaktuSelesai, form.PenanggungJawab);
                if (perkuliahan == null) {
                    MessageBox.Show("Gagal nambah perkuliahan.");
                    return;
                }
                this.AddPerkuliahan(perkuliahan);
            }
        }

        private void buttonKuliahUbah_Click(object sender, EventArgs e) {
            foreach (ListViewItem item in listViewKuliah.Items) {
                if (item.Selected) {
                    this.EditPerkuliahan(item);
                    return;
                }                    
            }
        }

        private void buttonKuliahHapus_Click(object sender, EventArgs e) {
            foreach (ListViewItem item in listViewKuliah.Items) {
                if (item.Selected) {
                    this.DeletePerkuliahan(item);
                    return;
                }
            }
        }
    }
}
