using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace Kadrlar
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }



        private void buttonDataClick_Click(object sender, EventArgs e)
        {
            panelButtons.Enabled = false;
            Cursor = Cursors.WaitCursor;

            BackgroundWorker exec = new BackgroundWorker();
            exec.DoWork += (object sen, DoWorkEventArgs ev) =>
            {
                Button button = sender as Button;

                string data = (string)button.Tag;
                List<string> tags = new List<string>(Regex.Split(button.Tag + "", ", "));
                tags.Insert(0, "Кадр фамилияси");
                tags.Insert(1, "Кадр исми");
                tags.Insert(2, "Кадр отасини исми");
                string command = "if exists (select * from sysobjects where name=N'" + Tools.WorkersTableName + "' and xType='u') select ";
                {
                    string temp;
                    Invoke(new Action(() =>
                    {
                        foreach (string tag in tags)
                        {
                            if (tag.Equals("age"))
                            {
                                dataTable.Columns.Add("Кадр йоши", "Кадр йоши");
                                command += "[Кадр туғилган куни], ";
                            }
                            else
                            {
                                command += '[' + tag + "], ";

                                if (tag.StartsWith("image") || tag.StartsWith("table"))
                                {
                                    temp = tag.Substring(6);
                                    DataGridViewButtonColumn column = new DataGridViewButtonColumn();
                                    column.Tag = tag.Substring(0, 5);
                                    column.HeaderText = temp;
                                    dataTable.Columns.Add(column);
                                } 
                                else dataTable.Columns.Add(tag, tag);
                            }
                        }
                    }));
                }
                command = command.Substring(0, command.Length - 2);
                command += " from " + Tools.WorkersTableName + " where " + WorkersForm.workerCondition + ';';
                
                SqlDataReader rd = Tools.read(Tools.dbWorkers, command);
                string value, name;

                while (rd.Read())
                {
                    DataGridViewRow row = new DataGridViewRow();
                    for (int n = 0; n < rd.FieldCount; n++)
                    {
                        name = tags[n];
                        value = (string)rd[n];

                        if (name.Equals("age"))
                        {
                            DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();

                            DateTime birthDay = DateTime.Parse(value);
                            cell.Value = DateTime.Now.Year - birthDay.Year;

                            row.Cells.Add(cell);
                        }
                        else if (name.StartsWith("table"))
                        {
                            DataGridViewButtonCell cell = new DataGridViewButtonCell();
                            
                            string[][] temp = Tools.parseTable(value);
                            cell.Value = "Жами сони: " + (temp.Length - 1) + "та";
                            cell.Tag = value;
                            
                            row.Cells.Add(cell);
                        }
                        else if (name.StartsWith("image"))
                        {
                            DataGridViewButtonCell cell = new DataGridViewButtonCell();
                            
                            cell.Value = value;
                            cell.Tag = AppDomain.CurrentDomain.BaseDirectory + @"Images\" + value;

                            row.Cells.Add(cell);
                        }
                        else
                        {
                            DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                            cell.Value = value;

                            row.Cells.Add(cell);
                        }
                    }
                    Invoke(new Action(() =>
                    {
                        dataTable.Rows.Add(row);
                    }));
                }

                Invoke(new Action(()=>
                {
                    int width = (dataTable.Width - 10) / tags.Count;
                    foreach (DataGridViewColumn col in dataTable.Columns)
                        col.Width = width;
                    rd.Close();

                    panelData.BringToFront();
                    panelButtons.Enabled = true;
                    Cursor = Cursors.Default;
                }));
            };

            exec.RunWorkerAsync();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            panelButtons.BringToFront();
            panelButtons.Focus();
            dataTable.Rows.Clear();
            dataTable.Columns.Clear();
        }



        private void dataTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataTable.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                string type = dataTable.Columns[e.ColumnIndex].Tag.ToString();

                ViewForm form = new ViewForm(dataTable.CurrentCell.Tag.ToString(), type);
                form.ShowDialog(this);
            }
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            panelButtons.Focus();
        }



        private void buttonSaveFile_Click(object sender, EventArgs e)
        {
            Enabled = false;
            Cursor = Cursors.WaitCursor;

            BackgroundWorker exec = new BackgroundWorker();
            exec.DoWork += (object sen, DoWorkEventArgs ev) =>
            {
                WordFile file = createReportFile();

                Invoke(new Action(() =>
                {
                    Cursor = Cursors.Default;
                    Enabled = true;

                    MessageBox.Show(this, "Сиз танлаган критерия бўйича тахлилий файл тайор бўлди!", "Тахлилий файл");
                }));
            };

            exec.RunWorkerAsync();
        }

        private WordFile createReportFile()
        {
            WordFile file = new WordFile(true);
            {
                Word.Paragraph par = file.Document.Paragraphs.Add();
                Word.Range rng = par.Range;

                rng.Font.Size = 24;
                rng.Font.Bold = -1;
                rng.Text = "Ҳисобот. (Жами " + dataTable.Rows.Count + " та кадр маълумоти)";
                rng.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                rng.InsertParagraphAfter();
                rng.InsertParagraphAfter();
                rng.InsertParagraphAfter();
            }

            for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
            {
                {
                    var par = file.Document.Paragraphs.Add();
                    var rng = par.Range;
                    rng.InlineShapes.AddHorizontalLineStandard();

                    par = file.Document.Paragraphs.Add();
                    rng = par.Range;
                    rng.Font.Bold = -1;
                    rng.Font.Size = 16;
                    rng.Text = (rowIndex + 1) + ". " + dataTable.Rows[rowIndex].Cells[0].Value + ' ' + dataTable.Rows[rowIndex].Cells[1].Value + ' ' + dataTable.Rows[rowIndex].Cells[2].Value;
                    rng.InsertParagraphAfter();
                }

                for (int n = 3; n < dataTable.Columns.Count; n++)
                {
                    // for a key
                    var par = file.Document.Paragraphs.Add();
                    var rng = par.Range;
                    rng.Font.Bold = -1;
                    rng.Text = dataTable.Columns[n].HeaderText + ": ";
                    rng.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    rng.InsertParagraphAfter();

                    // for a value
                    par = file.Document.Paragraphs.Add();
                    rng = par.Range;

                    // setting a value
                    if (dataTable.Columns[n] is DataGridViewButtonColumn)
                    {
                        string type = dataTable.Columns[n].Tag.ToString();
                        switch (type)
                        {
                            case "image":
                                string picPath = dataTable.Rows[rowIndex].Cells[n].Tag.ToString();
                                Word.InlineShape picture = rng.InlineShapes.AddPicture(picPath);
                                picture.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
                                picture.ScaleHeight = 100 * 150f / picture.Height;

                                //Word.Shape shape = picture.ConvertToShape();
                                //shape.Left = (float)Word.WdShapePosition.wdShapeRight;
                                break;
                            case "table":
                                string[][] tableData = Tools.parseTable(dataTable.Rows[rowIndex].Cells[n].Tag.ToString());

                                int rowsCount = tableData.Length, colsCount = tableData[0].Length;
                                Word.Table newTable = rng.Tables.Add(rng, rowsCount, colsCount);
                                newTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                                newTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;

                                for (int r = 0; r < rowsCount; r++)
                                {
                                    for (int c = 0; c < colsCount; c++)
                                    {
                                        var cell = newTable.Cell(r + 1, c + 1);
                                        cell.Range.Text = tableData[r][c];
                                    }
                                }
                                for (int c = 1; c <= colsCount; c++)
                                    newTable.Cell(1, c).Range.Font.Bold = -1;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        rng.Text = dataTable.Rows[rowIndex].Cells[n].Value.ToString();
                        rng.InsertParagraphAfter();
                    }
                }
            }

            {
                var par = file.Document.Paragraphs.Add();
                var rng = par.Range;
                rng.InlineShapes.AddHorizontalLineStandard();
            }

            {
                var par = file.Document.Paragraphs.Add();
                var rng = par.Range;
                rng.InsertParagraphBefore();
                rng.InsertParagraphBefore();
                rng.InsertParagraphBefore();
                rng.InsertParagraphBefore();
                rng.Font.Italic = -1;
                rng.Text = "Бугун: " + DateTime.Now.ToShortDateString();
            }

            return file;
        }
    }
}
