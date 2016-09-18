using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Kadrlar
{
    public partial class NewWorkerForm : Form
    {
        public NewWorkerForm(bool editable, List<NameValuePair<string>> data)
        {
            InitializeComponent();

            if (data != null)
            {
                button6.Enabled = button7.Enabled = true;

                whereCondition = "where ";
                foreach(NameValuePair<string> pr in data)
                    whereCondition += '[' + pr.Name + "]=" + Tools.escapeStringSql(pr.Value) + " and ";
                whereCondition = whereCondition.Substring(0, whereCondition.Length - 5);

                string command = "select * from " + Tools.WorkersTableName + " " + whereCondition  + ";";
                SqlDataReader rd = Tools.read(Tools.dbWorkers, command);

                List<string> readData = new List<string>();
                if (rd.Read())
                {
                    for (int n = 0; n < rd.FieldCount; n++)
                        readData.Add(rd[n].ToString());
                    rd.Close();
                }

                foreach (TabPage tp in container.TabPages)
                    setAllData(tp, readData, editable);

                updateData = true;
            }

            imagesCount = docsCount = 0;

            string[] temp = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "Images\\");
            string name;
            if (temp.Length != 0)
            {
                name = temp[temp.Length - 1].Substring(temp[temp.Length-1].LastIndexOf('\\') + 1);
                name = name.Substring(0, name.IndexOf('.'));
                imagesCount = int.Parse(name) + 1;
            }
            temp = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "Documents\\");
            if (temp.Length != 0)
            {
                name = temp[temp.Length - 1].Substring(temp[temp.Length - 1].LastIndexOf('\\') + 1);
                name = name.Substring(0, name.IndexOf('.'));
                docsCount = int.Parse(name) + 1;
            }

            vacancyMode = false;
            vacancyTextBox46.Text = "Йўқ";
        }

        public NewWorkerForm()
        {
            InitializeComponent();

            Text = "Янги бўш иш ўрни маълумотлари";
            vacancyMode = true;
            vacancyTextBox46.Text = "Ҳа";

            Control[] data = new Control[]{
                textBox3,
                textBox4,
                textBox5,
                panel9,
                textBox6,
                tabPage2,
                tabPage3
            };

            foreach (Control cn in data)
                cn.Enabled = false;

            button9.Text = "Бўш иш ўрнини сақлаш";
        }



        #region VARIABLES
        private bool updateData = false;
        private bool vacancyMode = false;
        private static string tableString;
        public static List<string> tags;
        private string whereCondition;
        private int imagesCount = 0;
        private int docsCount = 0;
        #endregion



        private void pictureBox_Click(object sender, EventArgs e)
        {
            string fileName = Path.GetTempFileName();

            Bitmap image = ImageChooser.Choose();
            if (image != null)
            {
                PictureBox temp = sender as PictureBox;
                temp.Image = ImageChooser.CropBitmap(image, temp.Size);
                image.Save(fileName);
                temp.Name = fileName + '*';
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            List<string> values = new List<string>();
            foreach (TabPage tp in container.TabPages)
            {
                getAllData(tp, values);
            }

            if (tableString == null) // run only once on first run
            {
                tags = new List<string>();
                foreach (TabPage tp in container.TabPages)
                    getAllTags(tp, tags);


                tableString = Tools.WorkersTableName + "(";
                foreach (string col in tags)
                    tableString += '[' + col + "], ";
                tableString = tableString.Substring(0, tableString.Length - 2) + ")";

                //Clipboard.SetText(tableString);
            }

            SqlDataReader rd = Tools.read(Tools.dbWorkers, "if exists (select * from sysobjects where name=N'" + Tools.WorkersTableName + "' and xType='u') select * from " + Tools.WorkersTableName + ";");
            string command;

            if (rd.FieldCount == 0) // run only at the very beginning of using the app
            {
                rd.Close();

                // create the default table
                command = "create table " + Tools.WorkersTableName + '(';
                foreach (string col in tags)
                    command += '[' + col + "] nvarchar(max), ";
                command = command.Substring(0, command.Length - 2) + ");";
                Tools.execute(Tools.dbWorkers, command);

                // insert the column names
                command = "insert into " + Tools.WorkersTableName + '(';
                foreach (string col in tags)
                    command += '[' + col + "], ";
                command = command.Substring(0, command.Length - 2) + ") values(";
                foreach (string val in values)
                    command += Tools.escapeStringSql(val) + ", ";
                command = command.Substring(0, command.Length - 2) + ");";
                Tools.execute(Tools.dbWorkers, command);

                string data = "";
                foreach (string str in tags)
                    data += str + "■";
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "col_mgr.dat", data.Substring(0, data.Length - 1));
            }
            // run always (but not on first run)
            else if(updateData) // updaing existing worker information
            {
                rd.Close();
                command = "update " + Tools.WorkersTableName + " set ";
                for (int n = 0; n < values.Count; n++)
                    command += '[' + tags[n] + "]=" + Tools.escapeStringSql(values[n]) + ", ";
                command = command.Substring(0, command.Length - 2) + ' ' + whereCondition + ';';
                Tools.execute(Tools.dbWorkers, command);
            }
            else // adding new worker
            {
                rd.Close();
                command = "insert into " + tableString + " values(";
                foreach (string val in values)
                    command += Tools.escapeStringSql(val) + ", ";
                command = command.Substring(0, command.Length - 2) + ");";
                Tools.execute(Tools.dbWorkers, command);
            }

            MessageBox.Show("Operatsiya muvaffaqqiyatli tugallandi!", "Bajarildi");
        }



        private void getAllData(Control tp, List<string> data)
        {
            foreach (Control cn in tp.Controls)
            {
                if (cn.Tag != null && cn.Tag.Equals("Service Control"))
                    continue;
                if (cn is RichTextBox)
                {
                    RichTextBox temp = cn as RichTextBox;
                    data.Add(temp.Text);
                }
                else if (cn is DateTimePicker)
                {
                    DateTimePicker temp = cn as DateTimePicker;
                    data.Add(temp.Value.ToShortDateString().ToString());
                }
                else if (cn is ComboBox)
                {
                    ComboBox temp = cn as ComboBox;
                    if (temp.SelectedItem == null)
                        temp.SelectedItem = temp.Items[0];
                    data.Add((string)temp.SelectedItem);
                }
                else if (cn is PictureBox)
                {
                    PictureBox temp = cn as PictureBox;
                    string newFile = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + imagesCount + ".png";
                    if (temp.Name[temp.Name.Length - 1] == '*')
                    {
                        File.Copy(temp.Name.Substring(0, temp.Name.Length - 1), newFile, true);
                        data.Add(imagesCount++ + ".png");
                    }
                    else if (File.Exists(temp.Name))
                    {
                        data.Add(temp.Name.Substring(temp.Name.LastIndexOf('\\') + 1));
                        continue;
                    }
                    else data.Add(temp.Name);
                }
                else if (cn is DataGridView)
                {
                    DataGridView temp = cn as DataGridView;
                    data.Add(Tools.tableToString(temp));
                }
                else if (cn is Panel || cn is GroupBox)
                {
                    getAllData(cn, data);
                }
            }
        }

        private void setAllData(Control tp, List<string> data, bool editable)
        {
            foreach (Control cn in tp.Controls)
            {
                if (cn.Tag != null && cn.Tag.Equals("Service Control"))
                    continue;
                if (cn is RichTextBox)
                {
                    RichTextBox temp = cn as RichTextBox;
                    temp.Text = data[0];
                    data.RemoveAt(0);
                    temp.Enabled = editable;
                }
                else if (cn is DateTimePicker)
                {
                    DateTimePicker temp = cn as DateTimePicker;
                    temp.Value = DateTime.Parse(data[0]);
                    data.RemoveAt(0);
                    temp.Enabled = editable;
                }
                else if (cn is ComboBox)
                {
                    ComboBox temp = cn as ComboBox;
                    if (temp.SelectedItem == null)
                        temp.SelectedItem = temp.Items[0];
                    temp.SelectedItem = data[0];
                    data.RemoveAt(0);
                    temp.Enabled = editable;
                }
                else if (cn is PictureBox)
                {
                    PictureBox temp = cn as PictureBox;
                    string fileName = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + data[0];
                    if (File.Exists(fileName))
                    {
                        temp.Image = ImageChooser.CropBitmap((Bitmap)Bitmap.FromFile(fileName), temp.Size);
                    }
                    temp.Name = data[0];
                    data.RemoveAt(0);
                    temp.Enabled = editable;
                }
                else if (cn is DataGridView)
                {
                    DataGridView temp = cn as DataGridView;
                    if (!data[0].Equals(""))
                    {
                        string[][] values = Tools.parseTable(data[0]);
                        for (int n = 1; n < values.Length; n++)
                            temp.Rows.Add(values[n]);
                    }
                    data.RemoveAt(0);

                    if (!editable)
                        temp.ReadOnly = temp.AllowUserToAddRows = temp.AllowUserToOrderColumns = temp.AllowUserToDeleteRows = temp.RowHeadersVisible = editable;
                }
                else if (cn is Panel || cn is GroupBox)
                {
                    cn.Enabled = editable;
                    setAllData(cn, data, editable);
                }
                else cn.Enabled = editable;
            }
        }

        private void getAllTags(Control tp, List<string> data)
        {
            foreach (Control cn in tp.Controls)
            {
                if (cn.Tag != null && cn.Tag.Equals("Service Control"))
                    continue;

                if (cn is RichTextBox)
                {
                    RichTextBox temp = cn as RichTextBox;
                    data.Add(temp.Tag.ToString());
                }
                else if (cn is DateTimePicker)
                {
                    DateTimePicker temp = cn as DateTimePicker;
                    data.Add(temp.Tag.ToString());
                }
                else if (cn is ComboBox)
                {
                    ComboBox temp = cn as ComboBox;
                    if (temp.SelectedItem == null)
                        temp.SelectedItem = temp.Items[0];
                    data.Add(temp.Tag.ToString());
                }
                else if (cn is PictureBox)
                {
                    PictureBox temp = cn as PictureBox;
                    data.Add(temp.Tag.ToString());
                }
                else if (cn is DataGridView)
                {
                    DataGridView temp = cn as DataGridView;
                    data.Add(temp.Tag.ToString());
                }
                else if (cn is Panel || cn is GroupBox)
                {
                    getAllTags(cn, data);
                }
            }
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            qizFamiliyaLabel.Visible = comboBox1.SelectedItem == comboBox1.Items[1];
            qizFamiliyaTextBox.Visible = comboBox1.SelectedItem == comboBox1.Items[1];
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            panel9.Visible = checkBox2.Checked;
            textBox39.Text = checkBox2.Checked ? "Ҳа" : "Йўқ";
        }

        private void selectFromReference(object sender, EventArgs e)
        {
            RichTextBox temp = sender as RichTextBox;

            string name = temp.Name;
            name = string.Concat(Regex.Replace(name.Substring(0, name.LastIndexOf('_')), "_", " "));

            ReferenceForm form = new ReferenceForm(name);
            form.ShowDialog(this);
            if (form.Value != null)
                temp.Text = form.Value;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WordFile file = new WordFile(AppDomain.CurrentDomain.BaseDirectory + @"Templates\t2.docx", true);
            string whereCondition = "where ";
            foreach (DataGridViewCell cell in WorkersForm.row.Cells)
                whereCondition += '[' + cell.OwningColumn.HeaderText + "]=" + Tools.escapeStringSql(cell.Value.ToString()) + " and ";
            whereCondition = whereCondition.Substring(0, whereCondition.Length - 5) + ';';

            if (Tools.fillTemplateFile(file, whereCondition) && MessageBox.Show(this, "Printerdan chiqarilsinmi?", "Print?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                file.print();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;
            container.SelectedIndex++;
            if (container.SelectedIndex == container.TabCount - 1)
                button5.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button5.Enabled = true;
            container.SelectedIndex--;
            if (container.SelectedIndex == 0)
                button4.Enabled = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(new string[]{
                Қариндошчилик_textBox8.Text,
                ismi_textBox21.Text,
                familiyasi_textBox23.Text,
                otasiniismi_textBox25.Text,
                tugilgankuni_dateTimePicker12.Value.ToShortDateString(),
                tugilganjoyi_textBox30.Text,
                ishjoyi_textBox26.Text,
                vazifasi_textBox32.Text
            });
        }

        private void container_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (container.SelectedIndex == 0)
                button4.Enabled = false;
            else if (container.SelectedIndex == container.TabCount - 1)
                button5.Enabled = false;
            else
                button4.Enabled = button5.Enabled = true;
        }

        private void handleNumberInput(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;
        }

        private void handleSalaryInput(object sender, EventArgs e)
        {
            RichTextBox temp = sender as RichTextBox;
            if (temp.Text.Length == 0)
                temp.Text = "0";

            int sum = int.Parse(textBox22.Text) + int.Parse(textBox23.Text) + int.Parse(textBox25.Text) + int.Parse(textBox26.Text);

            textBox30.Text = sum.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox40.Text = checkBox1.Checked ? "Ҳа" : "Йўқ";
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            textBox43.Text = checkBox4.Checked ? "Ҳа" : "Йўқ";
        }

        private void buttonT8_Click(object sender, EventArgs e)
        {
            WordFile file = new WordFile(AppDomain.CurrentDomain.BaseDirectory + @"Templates\t8.docx", true);
            string whereCondition = "where ";
            foreach (DataGridViewCell cell in WorkersForm.row.Cells)
                whereCondition += '[' + cell.OwningColumn.HeaderText + "]=" + Tools.escapeStringSql(cell.Value.ToString()) + " and ";
            whereCondition = whereCondition.Substring(0, whereCondition.Length - 5) + ';';

            if (Tools.fillTemplateFile(file, whereCondition) && MessageBox.Show(this, "Printerdan chiqarilsinmi?", "Print?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                file.print();
            }
        }

        private void buttonObyektivka_Click(object sender, EventArgs e)
        {
            WordFile file = new WordFile(AppDomain.CurrentDomain.BaseDirectory + @"Templates\t1.docx", true);
            string whereCondition = "where ";
            foreach (DataGridViewCell cell in WorkersForm.row.Cells)
                whereCondition += '[' + cell.OwningColumn.HeaderText + "]=" + Tools.escapeStringSql(cell.Value.ToString()) + " and ";
            whereCondition = whereCondition.Substring(0, whereCondition.Length - 5) + ';';

            if (Tools.fillTemplateFile(file, whereCondition) && MessageBox.Show(this, "Printerdan chiqarilsinmi?", "Print?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                file.print();
            }
        }
    }
}
