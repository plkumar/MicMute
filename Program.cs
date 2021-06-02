using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicMute
{
    static class Program
    {
        public static MainForm mf = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool result;
            var mutex = new System.Threading.Mutex(true, "MicMute", out result);

            if (!result)
            {
                MessageBox.Show("Another instance of MicMute is already running.");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mf = new MainForm();
            Application.Run(mf);

            GC.KeepAlive(mutex);
        }
    }
}
