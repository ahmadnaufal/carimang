using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CariMang {
    partial class FormMain {
        private Color TAB_COLOR = Color.DimGray;
        private Color TAB_COLOR_HOVER = Color.Gray;
        private Color TAB_COLOR_SELECTED = Color.DarkGray;

        private bool tab_Click(object sender, EventArgs e) {
            Button tab = sender as Button;
            if (tab == null)
                return false;

            bool selected = tab.Tag == null ? false : (bool)tab.Tag;
            if (selected)
                return false;            

            foreach (var control in tab.Parent.Controls) {
                if (control is Button && !control.Equals(tab)) {
                    Button otherTab = control as Button;
                    bool otherSelected = otherTab.Tag == null ? false : (bool)otherTab.Tag;
                    if (!otherSelected)
                        continue;
                    otherTab.Tag = false;
                    otherTab.BackColor = TAB_COLOR;
                }
            }
            tab.Tag = true;
            tab.BackColor = TAB_COLOR_SELECTED;

            if (e is System.Windows.Forms.MouseEventArgs) {
                buttonHistory.Push(new List<Button>() {
                    activeTab, activePage[activeTab]
                });
                buttonBack.Enabled = true;
            }
            activeTab = tab;          

            return true;
        }

        private void tab_BackColorChanged(object sender, EventArgs e) {
            Button tab = sender as Button;
            if (tab == null)
                return;

            bool selected = tab.Tag == null ? false : (bool)tab.Tag;
            if (selected) {
                tab.FlatAppearance.MouseDownBackColor = tab.BackColor;
                tab.FlatAppearance.MouseOverBackColor = tab.BackColor;                
            }
            else {
                tab.FlatAppearance.MouseDownBackColor = TAB_COLOR_SELECTED;
                tab.FlatAppearance.MouseOverBackColor = TAB_COLOR_HOVER;
            }
            tab.Invalidate();
        }

        private void tabData_Click(object sender, EventArgs e) {            
            if (!this.tab_Click(sender, e))
                return;                
            this.panelPageData.BringToFront();            
            this.panelData.BringToFront();            
        }

        private void tabBooking_Click(object sender, EventArgs e) {
            if (!this.tab_Click(sender, e))
                return;
            this.panelPageBooking.BringToFront();            
            this.panelBooking.BringToFront();            
        }

        private void tabStatistik_Click(object sender, EventArgs e) {
            if (!this.tab_Click(sender, e))
                return;
            this.panelPageStatistik.BringToFront();
            this.panelStatistik.BringToFront();            
        }        
    }
}
