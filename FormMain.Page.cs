using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CariMang {
    partial class FormMain {
        private Color PAGE_COLOR = Color.Transparent;
        private Color PAGE_COLOR_HOVER = Color.Gray;
        private Color PAGE_COLOR_SELECTED = Color.Gainsboro;

        private Color PAGE_FORE = Color.White;
        private Color PAGE_FORE_SELECTED = Color.Black;

        private bool page_Click(object sender, EventArgs e) {
            Button page = sender as Button;
            if (page == null)
                return false;

            bool selected = page.Tag == null ? false : (bool)page.Tag;
            if (selected)
                return false;

            foreach (var control in page.Parent.Controls) {
                if (control is Button && !control.Equals(page)) {
                    Button otherPage = control as Button;
                    bool otherSelected = otherPage.Tag == null ? false : (bool)otherPage.Tag;
                    if (!otherSelected)
                        continue;
                    otherPage.Tag = false;
                    otherPage.BackColor = PAGE_COLOR;
                }
            }
            page.Tag = true;
            page.BackColor = PAGE_COLOR_SELECTED;

            if (e is System.Windows.Forms.MouseEventArgs) {
                buttonHistory.Push(new List<Button>() {
                    activeTab, activePage[activeTab]
                });
                buttonBack.Enabled = true;
            }
            activePage[activeTab] = page;

            return true;
        }

        private void page_BackColorChanged(object sender, EventArgs e) {
            Button page = sender as Button;
            if (page == null)
                return;

            bool selected = page.Tag == null ? false : (bool)page.Tag;
            if (selected) {
                page.FlatAppearance.MouseDownBackColor = page.BackColor;
                page.FlatAppearance.MouseOverBackColor = page.BackColor;                
                page.ForeColor = PAGE_FORE_SELECTED;
            }
            else {
                page.FlatAppearance.MouseDownBackColor = PAGE_COLOR_HOVER;
                page.FlatAppearance.MouseOverBackColor = PAGE_COLOR_HOVER;                
                page.ForeColor = PAGE_FORE;
            }
            page.Invalidate();
        }

        private void pageDataJadwal_Click(object sender, EventArgs e) {            
            if (!this.page_Click(sender, e)) return;
            activePage[tabData] = (Button)sender;
            this.panelDataJadwal.BringToFront();
        }        

        private void pageDataRuangan_Click(object sender, EventArgs e) {
            if (!this.page_Click(sender, e)) return;
            activePage[tabData] = (Button)sender;
            this.panelDataRuangan.BringToFront();
        }

        private void pageDataKuliah_Click(object sender, EventArgs e) {            
            if (!this.page_Click(sender, e)) return;
            activePage[tabData] = (Button)sender;
            this.panelDataKuliah.BringToFront();
        }

        private void pageDataRusak_Click(object sender, EventArgs e) {
            if (!this.page_Click(sender, e)) return;
            activePage[tabData] = (Button)sender;
            this.panelDataRusak.BringToFront();
        }

        private void pageBookingCek_Click(object sender, EventArgs e) {            
            if (!this.page_Click(sender, e)) return;
            activePage[tabBooking] = (Button)sender;
            this.panelBookingCek.BringToFront();
        }

        private void pageBookingRuangan_Click(object sender, EventArgs e) {
            if (!this.page_Click(sender, e)) return;
            activePage[tabBooking] = (Button)sender;
            this.panelBookingRuangan.BringToFront();
        }

        private void pageStatistikRuangan_Click(object sender, EventArgs e) {
            if (!this.page_Click(sender, e)) return;
            activePage[tabStatistik] = (Button)sender;
            this.panelStatistikRuangan.BringToFront();
        }

        private void pageStatistikPeminjam_Click(object sender, EventArgs e) {
            if (!this.page_Click(sender, e)) return;
            activePage[tabStatistik] = (Button)sender;
            this.panelStatistikPeminjam.BringToFront();
        }

        private void pageStatistikRusak_Click(object sender, EventArgs e) {
            if (!this.page_Click(sender, e)) return;
            activePage[tabStatistik] = (Button)sender;
            this.panelStatistikRusak.BringToFront();
        }        
    }
}
