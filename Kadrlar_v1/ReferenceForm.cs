using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kadrlar
{
    public partial class ReferenceForm : Form
    {
        public ReferenceForm(string name)
        {
            InitializeComponent();

            this.name = name;
            nameTruncated = Tools.truncateNonLetters(name);
            Text = "Базага киритилган " + name.ToLower() + "лар рўйхати";
            
            SqlDataReader rd;
            try
            {
                rd = Tools.read(Tools.dbReferences, "select * from " + nameTruncated + ";");
                rd.Close();
            }
            catch
            {
                Tools.execute(Tools.dbReferences, "create table " + nameTruncated + "([" + name + "] nvarchar(500) primary key);");
            }

            Tools.loadDataToView(dataGridView1, Tools.dbReferences, "select * from " + nameTruncated + ';');
        }

        public ReferenceForm(object[] data)
        {
            InitializeComponent();

            name = "Data";
            nameTruncated = Tools.truncateNonLetters(name);
            Text = "Элементлардан бирини танланг";

            dataGridView1.Columns.Add("elementName", "Элемент номи");
            dataGridView1.Columns[0].Width = dataGridView1.Width;

            foreach (object obj in data)
                dataGridView1.Rows.Add(obj);
        }

        #region VARIABLES
        private string name, nameTruncated;
        private string value;
        public string Value { get { return value; } }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            string data = Microsoft.VisualBasic.Interaction.InputBox("Илтимос янги дала маълумотини шу ерда киритинг", "Киритинг", "");

            if (data == "")
            {
                MessageBox.Show("Илтимос, олдин маълумот киритинг!", "Тўлдиринг");
                return;
            }
            try
            {
                Tools.execute(Tools.dbReferences, "insert into " + nameTruncated + "([" + name + "]) values(" + Tools.escapeStringSql(data) + ");");
            }
            catch
            {
                MessageBox.Show("Сиз киритган маълумот базада киритилган", "Ҳатолик рўй берди");
            }

            Tools.loadDataToView(dataGridView1, Tools.dbReferences, "select * from " + nameTruncated);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string data = (string)dataGridView1.CurrentCell.Value;
                
                Tools.execute(Tools.dbReferences, "delete from " + nameTruncated + " where [" + name + "]=" + Tools.escapeStringSql(data) + ";");
            }
            catch
            {
                MessageBox.Show("Илтимос олдин далалардан бирини танланг!", "Ҳатолик рўй берди");
            }

            Tools.loadDataToView(dataGridView1, Tools.dbReferences, "select * from " + nameTruncated);
        }

        private void ReferenceForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            value = (string)dataGridView1.CurrentCell.Value;
            Close();
        }
    }
}
