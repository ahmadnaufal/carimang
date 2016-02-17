using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CariMang {

    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!MySqlConnector.CheckConnection()) {
                MessageBox.Show("Gagal terhubung dengan server basis data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Application.Run(new FormMain());
        }
    }
}
