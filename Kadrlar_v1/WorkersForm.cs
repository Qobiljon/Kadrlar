using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Kadrlar
{
    public partial class WorkersForm : Form
    {
        public WorkersForm()
        {
            if (Tools.Organization.Equals("default"))
            {
                MessageBox.Show("Илтимос, кадрлар устида ишлашдан олдин кадрларни жойлаш учун организация яратинг!", "Олдин база танланг (яратинг)");
                Close();
            }

            InitializeComponent();
        }

        private void WorkersForm_Load(object sender, EventArgs e)
        {
            reloadWorkers();
        }



        #region VARIABLES
        private string[] whereConditions;
        public const string selectPartWorker = "select [Кадр фамилияси], [Кадр исми], [Кадр отасини исми], [Кадр лавозими], [Кадр ишга олинган сана]";
        public const string selectPartVacancy = "select [Кадр лавозими], [Кадр бўлими], [Кадр бўлим бошлиғи]";
        public const string vacancyCondition = "[Кадр бўш иш ўрни]=N'Ҳа'";
        public const string workerCondition = "[Кадр бўш иш ўрни]=N'Йўқ'";
        #endregion



        private void button3_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (dataGridView1.ReadOnly)
            {
                dataGridView1.DefaultCellStyle.BackColor = Color.Lime;
                button.Text = "Сақлаш";
                button.ForeColor = Color.Green;
                backupCells();
            }
            else
            {
                saveWorkers();
                button.Text = "Ўзгартириш";
                button.ForeColor = Color.Red;
                dataGridView1.DefaultCellStyle.BackColor = Color.White;
            }

            dataGridView1.ReadOnly = !dataGridView1.ReadOnly;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                if (!dataGridView1.ReadOnly)
                {
                    dataGridView1.ReadOnly = true;
                    dataGridView1.DefaultCellStyle.BackColor = dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
                    button3.Text = "O'zgartirish";
                    reloadWorkers();
                }
            }
        }

        private void createWorker(object sender, EventArgs e)
        {
            NewWorkerForm form = null;
            if (checkBox1.Checked)
                form = new NewWorkerForm();
            else
                form = new NewWorkerForm(true, null);
            form.ShowDialog(this);
            reloadWorkers();
        }



        private void reloadWorkers()
        {
            Tools.loadDataToView(dataGridView1, Tools.dbWorkers, "if exists (select * from sysobjects where name=N'" + Tools.WorkersTableName + "' and xType='u') " + selectPartWorker + " from " + Tools.WorkersTableName + " where " + (radioButton1.Checked ? vacancyCondition : workerCondition) + ";");
        }

        private void backupCells()
        {
            whereConditions = new string[dataGridView1.Rows.Count];
            for (int row = 0; row < whereConditions.Length; row++)
            {
                whereConditions[row] = "where ";
                foreach (DataGridViewCell cell in dataGridView1.Rows[row].Cells)
                    whereConditions[row] += '[' + cell.OwningColumn.HeaderText + "]=" + Tools.escapeStringSql(cell.Value.ToString()) + " and ";
                whereConditions[row] = whereConditions[row].Substring(0, whereConditions[row].Length - 5);
            }
        }

        private void saveWorkers()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string command = "update " + Tools.WorkersTableName + " set ";
                foreach (DataGridViewCell cell in row.Cells)
                    command += '[' + cell.OwningColumn.HeaderText + "]=" + Tools.escapeStringSql(cell.Value.ToString()) + ", ";
                command = command.Substring(0, command.Length - 2) + ' ' + whereConditions[row.Index] + ';';
                Tools.execute(Tools.dbWorkers, command);
            }
        }

        

        private void doubleClickWorker(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.ReadOnly)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.DefaultCellStyle.BackColor = dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Lime;
                button3.Text = "Saqlash";
                backupCells();
            }
        }

        public static DataGridViewRow row;
        private void selectWorker(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                row = dataGridView1.SelectedRows[0];

                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    contextMenuStrip1.Items[0].Text = "💼 " + row.Cells[0].Value + " " + row.Cells[1].Value + ' ' + row.Cells[2].Value + " кадр маълумоти";
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void deleteWorker(object sender, EventArgs e)
        {
            if(row == null)
            {
                MessageBox.Show("Илтимос аввал кадрни танланг", "Ҳатолик рўй берди");
                return;
            }
            
            if (MessageBox.Show(this, "Сиз ростданхам шу кадрни базадан ўчирмоқчимисиз?", "Тасдиыланг", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            string command = "where ";
            foreach (DataGridViewCell cell in row.Cells)
                command += '[' + cell.OwningColumn.HeaderText + "]=" + Tools.escapeStringSql(cell.Value.ToString()) + " and ";
            command = command.Substring(0, command.Length - 5);

            SqlDataReader rd = Tools.read(Tools.dbWorkers, "select * from " + Tools.WorkersTableName + ' ' + command + ';');
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
                }
                
            rd.Close();

            command = "delete from " + Tools.WorkersTableName + ' ' + command + ';';
            Tools.execute(Tools.dbWorkers, command);
            reloadWorkers();
            row = null;
        }

        private void showWorkerInfo(object sender, EventArgs e)
        {
            if (row == null)
            {
                MessageBox.Show("Илтимос аввал кадрни танланг", "Ҳатолик рўй берди");
                return;
            }

            List<NameValuePair<string>> data = new List<NameValuePair<string>>();

            foreach (DataGridViewCell cell in row.Cells)
                data.Add(new NameValuePair<string>(cell.OwningColumn.HeaderText, cell.Value.ToString()));

            NewWorkerForm form = new NewWorkerForm(false, data);
            form.ShowDialog(this);
            reloadWorkers();
        }

        private void editWorkerInfo(object sender, EventArgs e)
        {
            if (row == null)
            {
                MessageBox.Show("Илтимос аввал кадрни танланг", "Ҳатолик рўй берди");
                return;
            }

            List<NameValuePair<string>> data = new List<NameValuePair<string>>();

            foreach (DataGridViewCell cell in row.Cells)
                data.Add(new NameValuePair<string>(cell.OwningColumn.HeaderText, cell.Value.ToString()));

            NewWorkerForm form = new NewWorkerForm(true, data);
            form.ShowDialog(this);
            reloadWorkers();
        }

        private void fillTemplateFile(object sender, EventArgs e)
        {
            OpenFileDialog form = new OpenFileDialog();
            form.Filter = WordFile.typename + "|*.docx";
            form.Title = "Илтимос, шаблон файлини танланг";

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                WordFile file = new WordFile(form.FileName, true);

                string whereCondition = "where ";
                foreach (DataGridViewCell cell in row.Cells)
                    whereCondition += '[' + cell.OwningColumn.HeaderText + "]=" + Tools.escapeStringSql(cell.Value.ToString()) + " and ";
                whereCondition = whereCondition.Substring(0, whereCondition.Length - 5) + ';';

                if (!Tools.fillTemplateFile(file, whereCondition))
                {
                    MessageBox.Show("Шаблон тўлдришда хатолик кетди, хатолик сиз танлаган организация билан боғлиқ.\nИлдимос организацияни текширинг!", "Муъаммо");
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string[] data = Regex.Split(keywordsTextBox.Text, " ");

            string colFile = AppDomain.CurrentDomain.BaseDirectory + "col_mgr.dat";
            if (File.Exists(colFile))
            {
                string read = File.ReadAllText(colFile);
                string colString = '[' + Regex.Replace(read, "■", "], [") + ']';

                string command = selectPartWorker + " from " + Tools.WorkersTableName + " where ";
                for (int n = 0; n < data.Length; n++)
                {
                    if (n == 0)
                    {
                        command += "[Ишдан бўшатилганлиги]='" + radioButton1.Checked + "'";
                        if (erkakRadioButton.Checked)
                            command += " and [Кадрнинг жинси]='Эркак' and " + Tools.escapeStringSql(data[n]) + " in (" + colString + ")";
                        else if (ayolRadioButton.Checked)
                            command += " and [Кадрнинг жинси]='Аёл' and " + Tools.escapeStringSql(data[n]) + " in (" + colString + ")";
                        else
                            command += " and " + Tools.escapeStringSql(data[n]) + " in (" + colString + ")";
                    }
                    else command += " and " + Tools.escapeStringSql(data[n]) + " in (" + colString + ")";
                }
                Tools.loadDataToView(dataGridView1, Tools.dbWorkers, command);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Tools.loadDataToView(dataGridView1, Tools.dbWorkers, "if exists (select * from sysobjects where name=N'" + Tools.WorkersTableName + "' and xType='u') " + (radioButton1.Checked ? selectPartVacancy : selectPartWorker) + " from " + Tools.WorkersTableName + " where " + (radioButton1.Checked ? WorkersForm.vacancyCondition : WorkersForm.workerCondition) + ";");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            deleteWorker(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text.Equals("Бўшатилганлар"))
            {
                button2.Text = "Ишлаётганлар";
                Tools.loadDataToView(dataGridView1, Tools.dbWorkers, "if exists (select * from sysobjects where name=N'" + Tools.WorkersTableName + "' and xType='u') " + selectPartWorker + " from " + Tools.WorkersTableName + " where [Ишдан бўшатилганлиги]=" + false.ToString() + ";");
                MessageBox.Show("Базадаги ишлаётган (бўшатилмаган) кадрлар ойнада кўрсатилмоқда", "Танланди");
            }
            else
            {
                button2.Text = "Бўшатилганлар";
                Tools.loadDataToView(dataGridView1, Tools.dbWorkers, "if exists (select * from sysobjects where name=N'" + Tools.WorkersTableName + "' and xType='u') " + selectPartWorker + " from " + Tools.WorkersTableName + " where [Ишдан бўшатилганлиги]=" + true.ToString() + ";");
                MessageBox.Show("Базадаги бўшатилган кадрлар ойнада кўрсатилмоқда", "Танланди");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            editWorkerInfo(null, null);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Tools.displayForm(new ReportForm(), this);
        }
    }
}
