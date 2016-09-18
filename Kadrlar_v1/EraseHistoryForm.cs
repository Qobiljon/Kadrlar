using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kadrlar
{
    public partial class EraseHistoryForm : Form
    {
        public EraseHistoryForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ҳозир дастур базасидаги ҳама маълумот ўчирилади", "Эҳтиёт бўлинг!") == System.Windows.Forms.DialogResult.OK)
            {
                if (MessageBox.Show("Сиз базадаги ҳама маълумотларни аниқ ўчирмоқчимисиз?", "Ўчирмоқчимисиз?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    SqlConnection con = Tools.dbOrganizations;
                    Tools.execute(con, "if exists (select * from sysobjects where name='organizations' and xType='u') drop table organizations;");

                    con = Tools.dbSettings;
                    Tools.execute(con, "update settings set value='default' where name='organization';");

                    con = Tools.dbWorkers;
                    DataTable tb = con.GetSchema("Tables");
                    for (int n = 0; n < tb.Rows.Count; n++)
                    {
                        Tools.execute(Tools.dbWorkers, "drop table " + tb.Rows[n].ItemArray[2] + ';');
                    }

                    con = Tools.dbReferences;
                    tb = con.GetSchema("Tables");
                    for (int n = 0; n < tb.Rows.Count; n++)
                    {
                        Tools.execute(Tools.dbReferences, "drop table " + tb.Rows[n].ItemArray[2] + ';');
                    }

                    Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "Documents", true);
                    Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + "Images", true);

                    string fileName = AppDomain.CurrentDomain.BaseDirectory + "col_mgr.dat";
                    if (File.Exists(fileName))
                        File.Delete(fileName);

                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Documents");
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Images");

                    MessageBox.Show("Сиз белгилаган пўнктларни барчаси тозаланди!", "Тозаланди");
                }
            }
        }
    }
}
