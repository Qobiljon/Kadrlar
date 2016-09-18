using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kadrlar
{
    public partial class ViewForm : Form
    {
        public ViewForm(string data, string dataType)
        {
            InitializeComponent();

            switch (dataType)
            {
                case "image":
                    if (!File.Exists(data))
                        panelEmpty.BringToFront();
                    else
                    {
                        picture.BringToFront();
                        picture.ImageLocation = data;
                    }
                    break;
                case "table":
                    dataTable.BringToFront();
                    string[][] tableData = Tools.parseTable(data);

                    if (tableData.Length < 2)
                        panelEmpty.BringToFront();
                    else
                    {
                        for (int n = 0; n < tableData[0].Length; n++)
                            dataTable.Columns.Add(tableData[0][n], tableData[0][n]);

                        for (int n = 1; n < tableData.Length; n++)
                            dataTable.Rows.Add(tableData[n]);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
