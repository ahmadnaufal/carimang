using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CariMang {
    partial class FormMain {
        private Color PAGE_COLOR = Color.DarkGray;
        private Color PAGE_COLOR_HOVER = Color.Silver;
        private Color PAGE_COLOR_SELECTED = Color.Gainsboro;

        private Color PAGE_FORE = Color.White;
        private Color PAGE_FORE_SELECTED = Color.DimGray;

        private void page_Click(object sender) {
            Button page = sender as Button;
            if (page == null)
                return;

            bool selected = page.Tag == null ? false : (bool)page.Tag;
            if (selected)
                return;

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
                page.FlatAppearance.MouseDownBackColor = PAGE_COLOR_SELECTED;
                page.FlatAppearance.MouseOverBackColor = PAGE_COLOR_HOVER;
                page.ForeColor = PAGE_FORE;
            }
            page.Invalidate();
        }

        private void pageDataJadwal_Click(object sender, EventArgs e) {            
            this.page_Click(sender);            
            this.panelDataJadwal.BringToFront();
        }        

        private void pageDataRuangan_Click(object sender, EventArgs e) {
            this.page_Click(sender);            
            this.panelDataRuangan.BringToFront();
        }

        private void pageDataKuliah_Click(object sender, EventArgs e) {            
            this.page_Click(sender);            
            this.panelDataKuliah.BringToFront();
        }

        private void pageDataRusak_Click(object sender, EventArgs e) {
            this.page_Click(sender);            
            this.panelDataRusak.BringToFront();
        }

        private void pageBookingCek_Click(object sender, EventArgs e) {            
            this.page_Click(sender);
            this.panelBookingCek.BringToFront();
        }

        private void pageBookingRuangan_Click(object sender, EventArgs e) {
            this.page_Click(sender);
            this.panelBookingRuangan.BringToFront();
        }

        private void pageStatistikRuangan_Click(object sender, EventArgs e) {
            this.page_Click(sender);
            this.panelStatistikRuangan.BringToFront();
        }

        private void pageStatistikPeminjam_Click(object sender, EventArgs e) {
            this.page_Click(sender);
            this.panelStatistikPeminjam.BringToFront();
        }

        private void pageStatistikRusak_Click(object sender, EventArgs e) {
            this.page_Click(sender);
            this.panelStatistikRusak.BringToFront();
        }        
    }
}
