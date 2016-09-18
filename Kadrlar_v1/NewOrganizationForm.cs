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
    public partial class NewOrganizationForm : Form
    {
        public NewOrganizationForm(bool editable, List<NameValuePair<string>> data)
        {
            InitializeComponent();

            SqlDataReader rd;
            if (data != null)
            {
                whereCondition = "where ";
                foreach (NameValuePair<string> pr in data)
                    whereCondition += '[' + pr.Name + "]=" + Tools.escapeStringSql(pr.Value) + " and ";
                whereCondition = whereCondition.Substring(0, whereCondition.Length - 5);

                string command = "select * from organizations " + whereCondition + ";";
                rd = Tools.read(Tools.dbOrganizations, command);

                List<string> readData = new List<string>();
                if (rd.Read())
                {
                    for (int n = 0; n < rd.FieldCount; n++)
                        readData.Add(rd[n].ToString());
                }
                rd.Close();
                setAllData(this, readData, editable);
                updateData = true;
            }

            rd = Tools.read(Tools.dbSettings, "select value from settings where name='organization';");
            if (rd.Read())
                isSelected = rd[0].Equals(textBox1.Text);
            rd.Close();
        }



        #region VARIABLES
        private string whereCondition;
        private static string tableString;
        public static List<string> tags;
        private bool updateData;
        private bool isSelected;
        #endregion



        private void getAllData(Control tp, List<string> data)
        {
            foreach (Control cn in tp.Controls)
            {
                if (cn is TextBox)
                {
                    TextBox temp = cn as TextBox;
                    data.Add(temp.Text);
                }
                else if (cn is Panel)
                {
                    getAllData(cn, data);
                }
            }
        }

        private void setAllData(Control tp, List<string> data, bool editable)
        {
            foreach (Control cn in tp.Controls)
            {
                if (cn is TextBox)
                {
                    TextBox temp = cn as TextBox;
                    temp.Text = data[0];
                    data.RemoveAt(0);
                    temp.Enabled = editable;
                }
                else if (cn is Panel)
                {
                    setAllData(cn, data, editable);
                }
                else
                    cn.Enabled = editable;
            }
        }

        private void getAllTags(Control tp, List<string> data)
        {
            foreach (Control cn in tp.Controls)
            {
                if (cn is TextBox)
                {
                    TextBox temp = cn as TextBox;
                    data.Add(temp.Tag.ToString());
                }
                else if (cn is Panel)
                {
                    getAllTags(cn, data);
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            List<string> values = new List<string>();
            getAllData(this, values);

            if (tableString == null) // run only once on first run
            {
                tags = new List<string>();
                getAllTags(this, tags);

                tableString = "organizations(";
                foreach (string col in tags)
                    tableString += '[' + col + "], ";
                tableString = tableString.Substring(0, tableString.Length - 2) + ")";
            }

            SqlDataReader rd = Tools.read(Tools.dbOrganizations, "if exists (select * from sysobjects where name='organizations' and xType='u') select * from organizations;");
            string command;

            if (rd.FieldCount == 0) // run only at the very beginning of using the app
            {
                rd.Close();

                // create the default table
                command = "create table organizations(";
                foreach (string col in tags)
                    command += '[' + col + "] nvarchar(300), ";
                command = command.Substring(0, command.Length - 2) + ");";
                Tools.execute(Tools.dbOrganizations, command);

                // insert the column names
                command = "insert into organizations(";
                foreach (string col in tags)
                    command += '[' + col + "], ";
                command = command.Substring(0, command.Length - 2) + ") values(";
                foreach (string val in values)
                    command += Tools.escapeStringSql(val) + ", ";
                command = command.Substring(0, command.Length - 2) + ");";
                Tools.execute(Tools.dbOrganizations, command);
            }
            // run always (but not on first run)
            else if (updateData) // updaing existing worker information
            {
                rd.Close();
                command = "update organizations set ";
                for (int n = 0; n < values.Count; n++)
                    command += '[' + tags[n] + "]=" + Tools.escapeStringSql(values[n]) + ", ";
                command = command.Substring(0, command.Length - 2) + ' ' + whereCondition + ';';
                Tools.execute(Tools.dbOrganizations, command);
            }
            else // adding new worker
            {
                rd.Close();
                command = "insert into " + tableString + " values(";
                foreach (string val in values)
                    command += Tools.escapeStringSql(val) + ", ";
                command = command.Substring(0, command.Length - 2) + ");";
                Tools.execute(Tools.dbOrganizations, command);
            }

            if (isSelected)
                Tools.Organization = textBox1.Text;

            MessageBox.Show("Operatsiya muvaffaqqiyatli tugallandi!", "Bajarildi");
        }
    }
}
