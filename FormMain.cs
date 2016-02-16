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
        private Button activeTab = null;
        private Dictionary<Button, Button> activePage = new Dictionary<Button, Button>(); 
        private List<Ruangan> daftarRuangan = new List<Ruangan>();
        private Stack<List<Button>> buttonHistory = new Stack<List<Button>>();
        
        public FormMain() {            
            InitializeComponent();
            InitializeLayout();
            InitializeData();
        }

        private void InitializeLayout() {
            tabData.Tag = true;
            pageDataJadwal.Tag = true;            
            pageBookingCek.Tag = true;
            pageStatistikRuangan.Tag = true;
            activeTab = tabData;
            activePage[tabData] = pageDataJadwal;
            activePage[tabBooking] = pageBookingCek;
            activePage[tabStatistik] = pageStatistikRuangan;            
        }

        private void InitializeData() {
            this.GetAllRuangan();
            this.GetAllPerkuliahan();
            this.GetAllPerbaikan();
        }

        private void AddJadwal(Ruangan ruangan, int waktuMulai, int waktuSelesai, string alasan) {
            ListViewItem item = null;
            foreach (ListViewItem itemRuangan in listViewJadwal.Items) {
                if (ruangan.Equals(itemRuangan.Tag)) {
                    item = itemRuangan;
                    break;
                }
            }
            if (item == null) {
                item = new ListViewItem();
                item.UseItemStyleForSubItems = false;
                item.Text = ruangan.Nama;
                for (int i = 0; i < listViewJadwal.Columns.Count-1; ++i)
                    item.SubItems.Add("");
                item.Tag = ruangan;
                listViewJadwal.Items.Add(item);
            }
            for (int i = waktuMulai; i < waktuSelesai; ++i) {
                int subIndex = i - 6;
                if (subIndex <= 0 || subIndex >= item.SubItems.Count)
                    continue;
                item.SubItems[subIndex].Text = alasan;
                item.SubItems[subIndex].BackColor = Color.Red;
            }
        }

        private void GetAllRuangan() {
            daftarRuangan.Clear();
            comboJadwalCari.Items.Clear();
            comboJadwalCari.Items.Add("(Cari semua ruangan)");
            listViewRuangan.Items.Clear();
            comboBookingCek.Items.Clear();
            comboBookingRuangan.Items.Clear();

            foreach (var ruangan in Ruangan.GetAll())
                this.AddRuangan(ruangan);
            comboJadwalCari.SelectedIndex = 0;
            comboBookingCek.SelectedIndex = comboBookingCek.Items.Count > 0 ? 0 : -1;
            comboBookingRuangan.SelectedIndex = comboBookingRuangan.Items.Count > 0 ? 0 : -1;
        }

        private void AddRuangan(Ruangan ruangan) {
            daftarRuangan.Add(ruangan);

            comboJadwalCari.Items.Add(ruangan.Nama);

            var item = new ListViewItem();
            item.Text = ruangan.Nama;
            item.SubItems.Add(ruangan.Tipe.ToString());
            item.SubItems.Add(ruangan.Kapasitas.ToString());            
            item.Tag = ruangan;
            listViewRuangan.Items.Add(item);            

            comboBookingCek.Items.Add(ruangan);
            comboBookingRuangan.Items.Add(ruangan);
        }

        private void EditRuangan(ListViewItem item) {
            Ruangan ruangan = (Ruangan)item.Tag;
            using (FormRuangan form = new FormRuangan(ruangan)) {
                if (form.ShowDialog() != DialogResult.OK)
                    return;
                ruangan.Nama = form.Nama;                
                ruangan.Tipe = form.Tipe;                
                ruangan.Kapasitas = form.Kapasitas;                
                GetAllRuangan();
            }
        }

        private void DeleteRuangan(ListViewItem item) {
            Ruangan ruangan = (Ruangan)item.Tag;
            if (MessageBox.Show("Mau dihapus " + ruangan.Nama + " ?", "Serius", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    != DialogResult.Yes)
                return;
            if (Ruangan.Delete(ruangan)) {
                GetAllRuangan();
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

        private void GetAllPerbaikan()
        {
            listViewRusak.Items.Clear();
            foreach (var perbaikan in Perbaikan.GetAll())
                this.AddPerbaikan(perbaikan);
        }

        private void AddPerbaikan(Perbaikan perbaikan)
        {
            var item = new ListViewItem();
            item.Text = perbaikan.Ruangan.Nama;
            item.SubItems.Add(perbaikan.TanggalMulai.ToString(Perbaikan.FMT_DISPLAY_TANGGAL));
            item.SubItems.Add(perbaikan.TanggalSelesai.ToString(Perbaikan.FMT_DISPLAY_TANGGAL));
            item.SubItems.Add(perbaikan.Deskripsi);
            item.Tag = perbaikan;
            listViewRusak.Items.Add(item);
        }

        private void EditPerbaikan(ListViewItem item)
        {
            Perbaikan perbaikan = (Perbaikan)item.Tag;
            using (FormPerbaikan form = new FormPerbaikan(perbaikan))
            {
                if (form.ShowDialog() != DialogResult.OK)
                    return;
                perbaikan.Ruangan = form.Ruangan;
                item.SubItems[0].Text = perbaikan.Ruangan.Nama;                

                perbaikan.TanggalMulai = form.TanggalMulai;
                item.SubItems[1].Text = perbaikan.TanggalMulai.ToString(Perbaikan.FMT_DISPLAY_TANGGAL);

                perbaikan.TanggalSelesai = form.TanggalSelesai;
                item.SubItems[2].Text = perbaikan.TanggalSelesai.ToString(Perbaikan.FMT_DISPLAY_TANGGAL);

                perbaikan.Deskripsi = form.Deskripsi;
                item.SubItems[3].Text = perbaikan.Deskripsi;
            }
        }

        private void DeletePerbaikan(ListViewItem item)
        {
            Perbaikan perbaikan = (Perbaikan)item.Tag;
            if (MessageBox.Show("Mau dihapus " + perbaikan.Ruangan.Nama + " ?", "Serius", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    != DialogResult.Yes)
                return;
            if (Perbaikan.Delete(perbaikan))
            {
                listViewRusak.Items.Remove(item);
            }
            else {
                MessageBox.Show("Gagal delete ruangan.");
            }
        }

        private void buttonBack_Click(object sender, EventArgs e) {
            if (buttonHistory.Count == 0)
                return;
            foreach (var button in buttonHistory.Pop()) {
                button.PerformClick();
            }
            if (buttonHistory.Count == 0)
                buttonBack.Enabled = false;
        }

        //
        // Tab Data
        //
        private void buttonJadwalCari_Click(object sender, EventArgs e) {
            HashSet<Ruangan> selectedRuangan;
            
            var selectedIndex = comboJadwalCari.SelectedIndex;
            if (selectedIndex == 0) {
                selectedRuangan = new HashSet<Ruangan>(daftarRuangan);
            }
            else {
                selectedRuangan = new HashSet<Ruangan>();
                selectedRuangan.Add(daftarRuangan[selectedIndex - 1]);
            }
            listViewJadwal.Items.Clear();
            foreach (var ruangan in selectedRuangan) {
                AddJadwal(ruangan, 0, 0, null);
            }

            var tanggal = dateJadwalCari.Value;
            foreach (var perkuliahan in Perkuliahan.GetAll(tanggal)) {                
                if (selectedRuangan.Contains(perkuliahan.Ruangan)) {                    
                    AddJadwal(perkuliahan.Ruangan, perkuliahan.WaktuMulai, perkuliahan.WaktuSelesai, perkuliahan.Kuliah.Kode);
                }
            }
            foreach (var kegiatan in Kegiatan.GetAll(tanggal)) {
                if (selectedRuangan.Contains(kegiatan.Ruangan)) {
                    AddJadwal(kegiatan.Ruangan, kegiatan.WaktuMulai, kegiatan.WaktuSelesai, kegiatan.Nama);
                }
            }
            foreach (var perbaikan in Perbaikan.GetAll(tanggal)) {                
                if (selectedRuangan.Contains(perbaikan.Ruangan)) {                    
                    String alasan = String.IsNullOrWhiteSpace(perbaikan.Deskripsi) ? "Perbaikan" : perbaikan.Deskripsi;                                        
                    AddJadwal(perbaikan.Ruangan, 0, 24, alasan);                    
                }
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

        private void buttonRusakTambah_Click(object sender, EventArgs e)
        {
            using (FormPerbaikan form = new FormPerbaikan())
            {
                if (form.ShowDialog() != DialogResult.OK)
                    return;
                var perbaikan = Perbaikan.Add(
                    form.Ruangan, form.TanggalMulai, form.TanggalSelesai,
                    form.Deskripsi);
                if (perbaikan == null)
                {
                    MessageBox.Show("Gagal nambah perbaikan.");
                    return;
                }
                this.AddPerbaikan(perbaikan);
            }
        }       

        private void buttonRusakUbah_Click(object sender, EventArgs e) {
            foreach (ListViewItem item in listViewRusak.Items)
            {
                if (item.Selected)
                {
                    this.EditPerbaikan(item);
                    return;
                }
            }
        }

        private void buttonRusakHapus_Click(object sender, EventArgs e) {
            foreach (ListViewItem item in listViewRusak.Items)
            {
                if (item.Selected)
                {
                    this.DeletePerbaikan(item);
                    return;
                }
            }
        }


        // 
        // Tab Booking
        //
        private void buttonBookingCek_Click(object sender, EventArgs e) {
            Ruangan ruangan = daftarRuangan[comboBookingCek.SelectedIndex];
            int kapasitas = (int)numBookingCek.Value;
            var tanggal = dateBookingCek.Value;
            int mulai = (int)numBookingCekMulai.Value;
            int selesai = (int)numBookingCekSelesai.Value;            
            if (kapasitas > ruangan.Kapasitas) {
                MessageBox.Show("Kapasitas ruangan tidak mencukupi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (mulai >= selesai) {
                MessageBox.Show("Waktu mulai harus lebih kecil dari waktu selesai.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var status = ruangan.Status(tanggal, mulai, selesai);
            if (status.Available) {
                MessageBox.Show("Ruangan tersedia.", "Tersedia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                MessageBox.Show(status.Reason, "Tidak tersedia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }            
        }

        private void buttonBookingRuangan_Click(object sender, EventArgs e) {
            string nama = textBookingRuanganKegiatan.Text.Trim();
            if (nama.Length == 0) {
                MessageBox.Show("Nama kegiatan tidak boleh kosong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string tanggung = textBookingRuanganTanggung.Text.Trim();
            if (tanggung.Length == 0) {
                MessageBox.Show("Penanggung jawab tidak boleh kosong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Ruangan ruangan = daftarRuangan[comboBookingRuangan.SelectedIndex];
            int kapasitas = (int)numBookingRuanganKapasitas.Value;
            var tanggal = dateBookingRuangan.Value;
            int mulai = (int)numBookingRuanganMulai.Value;
            int selesai = (int)numBookingRuanganSelesai.Value;
            if (kapasitas > ruangan.Kapasitas) {
                MessageBox.Show("Kapasitas ruangan tidak mencukupi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (mulai >= selesai) {
                MessageBox.Show("Waktu mulai harus lebih kecil dari waktu selesai.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var status = ruangan.Status(tanggal, mulai, selesai);
            if (!status.Available) {
                MessageBox.Show(status.Reason, "Tidak tersedia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Peminjam peminjam = Peminjam.Add(tanggung);
            var kegiatan = Kegiatan.Add(peminjam, ruangan, nama, tanggal, mulai, selesai);
            if (kegiatan != null) {
                MessageBox.Show("Kegiatan telah ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }               

        // 
        // Tab Statistik
        //
        private void buttonStatistikRusak_Click(object sender, EventArgs e) {
            DateTime tanggalAwal = datePickerTanggalAwalStatistikRusak.Value.Date;
            DateTime tanggalAkhir = datePickerTanggalAkhirStatistikRusak.Value.Date;
            if (tanggalAkhir < tanggalAwal) {
                MessageBox.Show("Maaf, periode tanggal yang diinputkan salah! Tanggal akhir harus berada setelah tanggal awal.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<StatistikRusak> listStatistikRusak = StatistikRusak.GetStatistik(tanggalAwal, tanggalAkhir);
            if (listStatistikRusak.Count == 0) {
                chartStatistikRusak.Visible = false;
                MessageBox.Show("Data pada periode tersebut kosong.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            chartStatistikRusak.DataSource = listStatistikRusak;
            chartStatistikRusak.Series.First().XValueMember = "NamaRuangan";
            chartStatistikRusak.Series.First().YValueMembers = "JumlahPerbaikan";
            chartStatistikRusak.DataBind();
            chartStatistikRusak.Visible = true;   
        }

        private void buttonStatistikRuangan_Click(object sender, EventArgs e) {
            DateTime tanggalAwal = datePickerTanggalAwalStatistikRuangan.Value.Date;
            DateTime tanggalAkhir = datePickerTanggalAkhirStatistikRuangan.Value.Date;
            if (tanggalAkhir < tanggalAwal)
            {
                MessageBox.Show("Maaf, periode tanggal yang diinputkan salah! Tanggal akhir harus berada setelah tanggal awal.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<StatistikRuangan> listStatistikRuangan = StatistikRuangan.GetStatistik(tanggalAwal, tanggalAkhir);
            if (listStatistikRuangan.Count == 0) {
                chartStatistikRuangan.Visible = false;
                MessageBox.Show("Data pada periode tersebut kosong.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            chartStatistikRuangan.DataSource = listStatistikRuangan;
            chartStatistikRuangan.Series.First().XValueMember = "NamaRuangan";
            chartStatistikRuangan.Series.First().YValueMembers = "JumlahPemakaian";
            chartStatistikRuangan.DataBind();
            chartStatistikRuangan.Visible = true;
        }

        private void buttonStatistikPeminjam_Click(object sender, EventArgs e)
        {
            DateTime tanggalAwal = datePickerTanggalAwalStatistikPeminjam.Value.Date;
            DateTime tanggalAkhir = datePickerTanggalAkhirStatistikPeminjam.Value.Date;
            if (tanggalAkhir < tanggalAwal)
            {
                MessageBox.Show("Maaf, periode tanggal yang diinputkan salah! Tanggal akhir harus berada setelah tanggal awal.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<StatistikPeminjam> listStatistikPeminjam = StatistikPeminjam.GetStatistik(tanggalAwal, tanggalAkhir);
            if (listStatistikPeminjam.Count == 0) {
                chartStatistikPeminjam.Visible = false;
                MessageBox.Show("Data pada periode tersebut kosong.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            chartStatistikPeminjam.DataSource = listStatistikPeminjam;
            chartStatistikPeminjam.Series.First().XValueMember = "NamaPeminjam";
            chartStatistikPeminjam.Series.First().YValueMembers = "JumlahPeminjam";
            chartStatistikPeminjam.DataBind();
            chartStatistikPeminjam.Visible = true;
        }
    }
}
