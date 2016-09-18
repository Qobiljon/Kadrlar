using BackendlessAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kadrlar
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (object sender, DoWorkEventArgs ev) =>
            {
                System.Threading.Thread.Sleep(50400000);
                quit(true);
            };
            worker.RunWorkerAsync();
        }



        #region VARIABLES
        private bool forceExit = false;
        #endregion



        private void chqishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quit(false);
        }

        private void quit(bool systematicQuitOperation)
        {
            if (systematicQuitOperation || MessageBox.Show("Сиз ростдан дастурни тарк этмоқчимисиз?", "Чиқиш", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (Tools.dbWorkers != null)
                    Tools.dbWorkers.Close();

                if (Tools.dbSettings != null)
                    Tools.dbSettings.Close();

                if (Tools.dbOrganizations != null)
                    Tools.dbOrganizations.Close();

                forceExit = true;
                Close();
            }
        }

        private void kadrlarRoyhatiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tools.displayForm(new WorkersForm(), this, true);
        }

        private void organizatsiyaniTanlashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tools.displayForm(new OrganizationsForm(), this, true);
        }

        private void hotiraniBoshatishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tools.displayForm(new EraseHistoryForm(), this, true);
        }

        private void litsenziyaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tools.displayForm(new LicenseForm(), this, true);
        }

        private void dasturHaqidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tools.displayForm(new AboutForm(), this, true);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!forceExit)
            {
                e.Cancel = true;
                Hide();
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(0);
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quit(false);
        }

        private void openAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            notifyIcon.Visible = false;
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyOptions.Close();
        }

        private int time = 0;
        private void notifyIcon_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (Environment.TickCount - time < 500)
                {
                    Show();
                    BringToFront();
                    notifyIcon.Visible = false;
                }
                time = Environment.TickCount;
            }
        }
    }
}
