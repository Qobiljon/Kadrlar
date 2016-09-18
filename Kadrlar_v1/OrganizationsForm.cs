using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Kadrlar
{
    public partial class OrganizationsForm : Form
    {
        public OrganizationsForm()
        {
            InitializeComponent();
        }



        #region VARIABLES
        public const string selectPartOrganization = "select [Организация тўлиқ номи], [Организация номи], [Организация қайдлар]";
        #endregion



        private void OrganizationsForm_Load(object sender, EventArgs e)
        {
            reloadOrganizations();
        }

        private void reloadOrganizations()
        {
            SqlDataReader rd = Tools.read(Tools.dbSettings, "select value from settings where name='organization';");
            if (rd.Read())
            {
                textBox1.Text = rd[0].ToString();
                rd.Close();
                Tools.Organization = textBox1.Text;
            }
            rd.Close();
            Tools.loadDataToView(dataGridView1, Tools.dbOrganizations, "if exists (select * from sysobjects where name='organizations' and xType='u') " + selectPartOrganization + " from organizations;");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NewOrganizationForm form = new NewOrganizationForm(true, null);
            form.ShowDialog(this);
            reloadOrganizations();
        }



        private DataGridViewRow row1;
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                row1 = dataGridView1.SelectedRows[0];

                if (e.Button == MouseButtons.Right)
                {
                    contextMenuStrip1.Items[0].Text = row1.Cells[0].Value + " базасини танлаш";
                    contextMenuStrip1.Show(Cursor.Position);
                }
                else
                {
                    string tableName = "workers_" + Tools.truncateNonLetters((string)row1.Cells[0].Value);
                    Tools.loadDataToView(dataGridView2, Tools.dbWorkers, "if exists (select * from sysobjects where name=N'" + tableName + "' and xType='u') " + (radioButton1.Checked ? WorkersForm.selectPartVacancy : WorkersForm.selectPartWorker) + " from " + tableName + " where " + (radioButton1.Checked ? WorkersForm.vacancyCondition : WorkersForm.workerCondition) + ";");
                }

                radioButton1.Enabled = radioButton2.Enabled = button1.Enabled = true;
            }
        }

        private DataGridViewRow row2;
        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 1)
            {
                row2 = dataGridView2.SelectedRows[0];

                if (e.Button == MouseButtons.Right)
                {
                    contextMenuStrip2.Items[0].Text = row2.Cells[0].Value + " " + row2.Cells[1].Value + " " + row2.Cells[2].Value + " кадрини бошқа базага кўчириш";
                    contextMenuStrip2.Show(Cursor.Position);
                }
            }
        }



        private void malumotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<NameValuePair<string>> data = new List<NameValuePair<string>>();

            foreach (DataGridViewCell cell in row1.Cells)
                data.Add(new NameValuePair<string>(cell.OwningColumn.HeaderText, cell.Value.ToString()));

            NewOrganizationForm form = new NewOrganizationForm(false, data);
            Tools.displayForm(form, this);
            reloadOrganizations();
        }

        private void organizatsiyaniOzgartirishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<NameValuePair<string>> data = new List<NameValuePair<string>>();

            foreach (DataGridViewCell cell in row1.Cells)
                data.Add(new NameValuePair<string>(cell.OwningColumn.HeaderText, cell.Value.ToString()));

            NewOrganizationForm form = new NewOrganizationForm(true, data);
            Tools.displayForm(form, this);
            reloadOrganizations();
        }

        private void ochirishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string command = "where ";
            foreach (DataGridViewCell cell in row1.Cells)
                command += '[' + cell.OwningColumn.HeaderText + "]=" + Tools.escapeStringSql(cell.Value.ToString()) + " and ";
            command = command.Substring(0, command.Length - 5);

            SqlDataReader rd = Tools.read(Tools.dbOrganizations, "select * from organizations " + command + ';');
            string temp;
            if (rd.Read())
                for (int n = 0; n < rd.FieldCount; n++)
                {
                    if ((temp = rd[n].ToString()).EndsWith(".pdf"))
                    {
                        if (File.Exists(temp = AppDomain.CurrentDomain.BaseDirectory + "Documents\\" + temp))
                            File.Delete(temp);
                    }
                    else if (temp.EndsWith(".png"))
                    {
                        if (File.Exists(temp = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + temp))
                            File.Delete(temp);
                    }
                    if (rd[n].Equals(textBox1.Text))
                    {
                        Tools.execute(Tools.dbSettings, "update settings set value='default' where name='organization';");
                    }
                }

            rd.Close();

            command = "delete from organizations " + command + ';';
            Tools.execute(Tools.dbOrganizations, command);
            reloadOrganizations();
        }

        private void bazaniTanlashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tools.Organization = row1.Cells[0].Value.ToString();
            Tools.execute(Tools.dbSettings, "update settings set value=N'" + Tools.OrganizationForSql + "' where name='organization';");
            reloadOrganizations();
        }



        private string tableFrom, tableTo;
        private void copyProfile(object sender, EventArgs e)
        {
            string organizationFrom = (string)row1.Cells[0].Value;

            tableFrom = "workers_" + Tools.truncateNonLetters(organizationFrom);
            string whereCondition = "where ";
            foreach (DataGridViewCell cell in row2.Cells)
                whereCondition += '[' + cell.OwningColumn.HeaderText + "]=" + Tools.escapeStringSql((string)cell.Value) + " and ";
            whereCondition = whereCondition.Substring(0, whereCondition.Length - 5);

            List<object> data = new List<object>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string temp = (string)row.Cells[0].Value;
                if (!temp.Equals(organizationFrom))
                    data.Add(temp);
            }
            ReferenceForm form = new ReferenceForm(data.ToArray());
            form.ShowDialog(this);
            if (form.Value != null)
            {
                MessageBox.Show("Илтимос энди ўтқазиладиган базани танланг!", "Кадрни базадан базага кўчириш");
                tableTo = "workers_" + Tools.truncateNonLetters(form.Value);

                Tools.execute(Tools.dbWorkers, "insert into " + tableTo + " select * from " + tableFrom + ' ' + whereCondition);
                MessageBox.Show("Кадр базадан базага муваффаққиятли кўчирилди!", "Бажарилди");
            }

            string tableName = "workers_" + Tools.truncateNonLetters((string)row1.Cells[0].Value);
            Tools.loadDataToView(dataGridView2, Tools.dbWorkers, "if exists (select * from sysobjects where name='" + tableName + "' and xType='u') " + (radioButton1.Checked ? WorkersForm.selectPartVacancy : WorkersForm.selectPartWorker) + " from " + tableName + " where " + (radioButton1.Checked ? WorkersForm.vacancyCondition : WorkersForm.workerCondition) + ";");
        }

        private void moveProfile(object sender, EventArgs e)
        {
            string organizationFrom = (string)row1.Cells[0].Value;

            tableFrom = "workers_" + Tools.truncateNonLetters(organizationFrom);
            string whereCondition = "where ";
            foreach (DataGridViewCell cell in row2.Cells)
                whereCondition += '[' + cell.OwningColumn.HeaderText + "]=" + Tools.escapeStringSql((string)cell.Value) + " and ";
            whereCondition = whereCondition.Substring(0, whereCondition.Length - 5);

            List<object> data = new List<object>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string temp = (string)row.Cells[0].Value;
                if (!temp.Equals(organizationFrom))
                    data.Add(temp);
            }
            ReferenceForm form = new ReferenceForm(data.ToArray());
            MessageBox.Show("Илтимос энди ўтқазиладиган базани танланг!", "Кадрни базадан базага кўчириш");
            form.ShowDialog(this);
            if (form.Value != null)
            {
                tableTo = "workers_" + Tools.truncateNonLetters(form.Value);

                Tools.execute(Tools.dbWorkers, "insert into " + tableTo + " select * from " + tableFrom + ' ' + whereCondition + ';');
                Tools.execute(Tools.dbWorkers, "delete from " + tableFrom + ' ' + whereCondition + ';');
                MessageBox.Show("Кадр базадан базага муваффаққиятли кўчирилди!", "Бажарилди");
            }

            string tableName = "workers_" + Tools.truncateNonLetters((string)row1.Cells[0].Value);
            Tools.loadDataToView(dataGridView2, Tools.dbWorkers, "if exists (select * from sysobjects where name='" + tableName + "' and xType='u') " + (radioButton1.Checked ? WorkersForm.selectPartVacancy : WorkersForm.selectPartWorker) + " from " + tableName + " where " + (radioButton1.Checked ? WorkersForm.vacancyCondition : WorkersForm.workerCondition) + ";");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            string tableName = "workers_" + Tools.truncateNonLetters((string)row1.Cells[0].Value);
            Tools.loadDataToView(dataGridView2, Tools.dbWorkers, "if exists (select * from sysobjects where name=N'" + tableName + "' and xType='u') " + (radioButton1.Checked ? WorkersForm.selectPartVacancy : WorkersForm.selectPartWorker) + " from " + tableName + " where " + (radioButton1.Checked ? WorkersForm.vacancyCondition : WorkersForm.workerCondition) + ";");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewWorkerForm form = new NewWorkerForm();
            form.ShowDialog(this);

            string tableName = "workers_" + Tools.truncateNonLetters((string)row1.Cells[0].Value);
            Tools.loadDataToView(dataGridView2, Tools.dbWorkers, "if exists (select * from sysobjects where name=N'" + tableName + "' and xType='u') " + (radioButton1.Checked ? WorkersForm.selectPartVacancy : WorkersForm.selectPartWorker) + " from " + tableName + " where " + (radioButton1.Checked ? WorkersForm.vacancyCondition : WorkersForm.workerCondition) + ";");
        }
    }
}
