using AForge.Video;
using AForge.Video.DirectShow;
using Microsoft.Vbe.Interop;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace Kadrlar
{
    public class ExcelFile
    {
        public ExcelFile(bool appVisible)
        {
            app = new Excel.Application();
            app.DisplayAlerts = false;
            app.Visible = appVisible;
            app.EnableAnimations = false;

            if (app == null)
                throw new ApplicationException("Microsoft Excel is not properly installed in your system!");

            book = app.Workbooks.Add(misValue);

            filePath = null;
        }

        public ExcelFile(string filePath, bool appVisible)
        {
            app = new Excel.Application();
            app.DisplayAlerts = false;
            app.Visible = appVisible;
            app.EnableAnimations = false;

            if (app == null)
                throw new ApplicationException("Microsoft Excel is not properly installed in your system!");

            if (filePath.EndsWith("xls"))
                filePath += "x";
            else if (!filePath.EndsWith("xlsx"))
                filePath += ".xlsx";

            this.filePath = filePath;

            try
            {
                book = app.Workbooks.Open(filePath);
            }
            catch
            {
                throw new ApplicationException("Cannot open a file!, path: " + filePath);
            }
        }



        #region Variables
        private Excel.Workbook book;
        public Excel.Workbook Book { get { return book; } }

        public Excel.Application Application { get { return app; } }

        private Excel.Application app;

        private string filePath;

        private object misValue = System.Reflection.Missing.Value;

        public static string typename = "Excel File(Microsoft Office)";
        #endregion



        public static Excel.Range getLastCell(Excel.Worksheet sheet)
        {
            return sheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);
        }

        public void print()
        {
            string filePath = Path.GetTempPath() + @"\temp_" + Environment.TickCount + ".xlsx";
            saveAs(filePath);

            using (PrintDialog pd = new PrintDialog())
            {
                pd.ShowDialog();
                ProcessStartInfo info = new ProcessStartInfo(filePath);
                info.Verb = "PrintTo";
                info.Arguments = pd.PrinterSettings.PrinterName;
                info.CreateNoWindow = true;
                info.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(info);
            }
        }

        public void save()
        {
            if (filePath == null)
                throw new ApplicationException("Cannot save a new file without giving a file path. Call saveAs(string) method instead!");

            book.Save();
        }

        public void saveAs(string filePath)
        {
            if (filePath.EndsWith("xls"))
                filePath += "x";
            else if (!filePath.EndsWith("xlsx"))
                filePath += ".xlsx";

            book.SaveAs(filePath);
        }

        public void close()
        {
            book.Close();
            app.Quit();

            releaseObject(book);
            releaseObject(app);
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                Console.WriteLine("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }

    public class WordFile
    {
        public WordFile(bool visible)
        {
            app = new Word.Application();
            app.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
            app.Visible = visible;
            app.ShowAnimation = false;

            if (app == null)
                throw new ApplicationException("Microsoft Word is not properly installed in your system!");

            document = app.Documents.Add();

            filePath = null;
        }

        public WordFile(string filePath, bool visible)
        {
            app = new Word.Application();
            app.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
            app.Visible = visible;
            app.ShowAnimation = false;

            if (app == null)
                throw new ApplicationException("Microsoft Word is not properly installed in your system!");

            if (filePath.EndsWith("doc"))
                filePath += "x";
            else if (!filePath.EndsWith("docx"))
                filePath += ".docx";

            this.filePath = filePath;

            try
            {
                document = app.Documents.Open(filePath);
            }
            catch
            {
                throw new ApplicationException("Cannot open a file!, path: " + filePath);
            }
        }



        // ------- variables start
        private Word.Document document;
        public Word.Document Document { get { return document; } }

        public Word.Application Application { get { return app; } }

        private Word.Application app;

        private string filePath;

        private object misValue = System.Reflection.Missing.Value;

        public static string typename = "Word File(Microsoft Office)";
        // ------- variables end




        public void print()
        {
            document.PrintOut();

            /*string filePath = Path.GetTempPath() + @"\temp_" + Environment.TickCount + ".docx";
            saveAs(filePath);
            using (PrintDialog pd = new PrintDialog())
            {
                pd.ShowDialog();
                ProcessStartInfo info = new ProcessStartInfo(filePath);
                info.Verb = "PrintTo";
                info.Arguments = pd.PrinterSettings.PrinterName;
                info.CreateNoWindow = true;
                info.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(info);
            }*/
        }

        public bool setValue(string bookmarkName, object value, bool clearBackground)
        {
            if (document.Bookmarks.Exists(bookmarkName))
            {
                string data = value.ToString();
                Word.Bookmark bm = document.Bookmarks[bookmarkName];

                int len = bm.Range.StoryLength - data.Length;
                int n = 0;
                for (; n < len / 2; n++)
                    data = ' ' + data;
                for (; n < len; n++)
                    data = data + ' ';

                /*bm.Range.Text = data;
                object rng = bm.Range;
                string bmName = bm.Name;

                bm.Range.Text = data;

                document.Bookmarks.Add(bmName, ref rng); 
                //bm.Range.Fields[1].Result.Text = data;

                if (clearBackground)
                    bm.Range.HighlightColorIndex = Word.WdColorIndex.wdWhite;
                    //document.Bookmarks[bookmarkName].Range.Fields[1].Result.HighlightColorIndex = Word.WdColorIndex.wdWhite;*/
            }
            else return false;
            return true;
        }

        public void save()
        {
            if (filePath == null)
                throw new ApplicationException("Cannot save a new file without giving a file path. Call saveAs(string) method instead!");

            document.Save();
        }

        public void saveAs(string filePath)
        {
            if (filePath.EndsWith("doc"))
                filePath += "x";
            else if (!filePath.EndsWith("docx"))
                filePath += ".docx";

            document.SaveAs2(filePath);
        }

        public void close()
        {
            document.Close();
            app.Quit();

            releaseObject(document);
            releaseObject(app);
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                Console.WriteLine("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }

    public class ImageChooser
    {
        public static Bitmap Choose(string title = null, string file = null, string camera = null, string retake = null, string save = null, string confirm = null)
        {
            Bitmap res = null;

            ImageChooserForm form = new ImageChooserForm(title, file, camera, retake, save, confirm);
            form.ShowDialog();
            if (ImageChooserForm.chosenImage != null)
            {
                if (ImageChooserForm.chosenImage is Image)
                    res = (Bitmap)ImageChooserForm.chosenImage;
                else
                {
                    string filePath = (string)ImageChooserForm.chosenImage;
                    res = Image.FromFile(filePath) as Bitmap;
                }
            }

            return res;
        }

        public static Bitmap CropBitmap(Bitmap bitmap, Size size)
        {
            float width = size.Width;
            float height = size.Height;
            var brush = new SolidBrush(Color.Black);
            float scale = Math.Max(width / bitmap.Width, height / bitmap.Height);


            var bmp = new Bitmap((int)width, (int)height);
            var graph = Graphics.FromImage(bmp);

            graph.InterpolationMode = InterpolationMode.High;
            graph.CompositingQuality = CompositingQuality.HighQuality;
            graph.SmoothingMode = SmoothingMode.AntiAlias;

            var scaleWidth = (int)(bitmap.Width * scale);
            var scaleHeight = (int)(bitmap.Height * scale);

            graph.FillRectangle(brush, new RectangleF(0, 0, width, height));
            graph.DrawImage(bitmap, new Rectangle(((int)width - scaleWidth) / 2, ((int)height - scaleHeight) / 2, scaleWidth, scaleHeight));

            return bmp;
        }



        private class ImageChooserForm : Form
        {
            public ImageChooserForm(string title, string file, string camera, string retake, string save, string confirm)
            {
                if (title == null || file == null || camera == null || retake == null || save == null || confirm == null)
                    InitializeComponent("Image selection", "File", "Camera", "Retake", this.save = "Save", this.confirm = "OK");
                else
                    InitializeComponent(title, file, camera, retake, this.save = save, this.confirm = confirm);
            }



            #region VARIABLES
            private IContainer components = null;
            private Panel panelToolBar;
            private Button buttonExit;
            private Button buttonMinimize;
            private Label label1;
            private Button buttonCamera;
            private Button buttonFile;
            private PictureBox picture;
            private ComboBox camsComboBox;
            private Button buttonAgain;
            private Button buttonSave;
            private Button button3;
            private Panel cameraPanel;
            public static object chosenImage = null;
            private FilterInfoCollection webcam;
            private VideoCaptureDevice cam;
            private string confirm, save;
            #endregion



            private void InitializeComponent(string title, string file, string camera, string retake, string save, string confirm)
            {
                panelToolBar = new Panel();
                buttonExit = new Button();
                buttonMinimize = new Button();
                label1 = new Label();
                buttonFile = new Button();
                buttonCamera = new Button();
                panelToolBar.SuspendLayout();
                SuspendLayout();
                // 
                // panelToolBar
                // 
                panelToolBar.BackColor = Color.FromArgb(68, 68, 68);
                panelToolBar.Controls.Add(label1);
                panelToolBar.Controls.Add(buttonMinimize);
                panelToolBar.Controls.Add(buttonExit);
                panelToolBar.ForeColor = Color.White;
                panelToolBar.Location = new Point(0, 0);
                panelToolBar.Name = "panelToolBar";
                panelToolBar.Size = new Size(300, 50);
                panelToolBar.TabIndex = 0;
                panelToolBar.MouseDown += new MouseEventHandler(moveYes);
                panelToolBar.MouseMove += new MouseEventHandler(moveDo);
                panelToolBar.MouseUp += new MouseEventHandler(moveNo);
                // 
                // buttonExit
                // 
                buttonExit.Cursor = Cursors.Hand;
                buttonExit.FlatAppearance.BorderSize = 0;
                buttonExit.FlatAppearance.MouseDownBackColor = Color.Black;
                buttonExit.FlatAppearance.MouseOverBackColor = Color.FromArgb(108, 108, 108);
                buttonExit.FlatStyle = FlatStyle.Flat;
                buttonExit.Font = new Font("Microsoft Sans Serif", 10F);
                buttonExit.ForeColor = Color.White;
                buttonExit.Location = new Point(265, 3);
                buttonExit.Name = "buttonExit";
                buttonExit.Size = new Size(32, 32);
                buttonExit.TabIndex = 0;
                buttonExit.Text = "x";
                buttonExit.UseVisualStyleBackColor = true;
                buttonExit.Click += new EventHandler(buttonExit_Click);
                // 
                // buttonMinimize
                // 
                buttonMinimize.Cursor = Cursors.Hand;
                buttonMinimize.FlatAppearance.BorderSize = 0;
                buttonMinimize.FlatAppearance.MouseDownBackColor = Color.Black;
                buttonMinimize.FlatAppearance.MouseOverBackColor = Color.FromArgb(108, 108, 108);
                buttonMinimize.FlatStyle = FlatStyle.Flat;
                buttonMinimize.Font = new Font("Microsoft Sans Serif", 10F);
                buttonMinimize.ForeColor = Color.White;
                buttonMinimize.Location = new Point(227, 3);
                buttonMinimize.Name = "buttonMinimize";
                buttonMinimize.Size = new Size(32, 32);
                buttonMinimize.TabIndex = 0;
                buttonMinimize.Text = "_";
                buttonMinimize.UseVisualStyleBackColor = true;
                buttonMinimize.Click += new EventHandler(buttonMinimize_Click);
                // 
                // label1
                // 
                label1.AutoSize = true;
                label1.Font = new Font("Microsoft Sans Serif", 16F);
                label1.Location = new Point(12, 10);
                label1.Name = "label1";
                label1.Size = new Size(165, 26);
                label1.TabIndex = 1;
                label1.Text = title;
                label1.MouseDown += new MouseEventHandler(moveYes);
                label1.MouseMove += new MouseEventHandler(moveDo);
                label1.MouseUp += new MouseEventHandler(moveNo);
                // 
                // buttonFile
                // 
                buttonFile.BackColor = Color.FromArgb(200, 200, 200);
                buttonFile.Cursor = Cursors.Hand;
                buttonFile.FlatAppearance.BorderSize = 0;
                buttonFile.FlatAppearance.MouseDownBackColor = Color.FromArgb(150, 150, 150);
                buttonFile.FlatAppearance.MouseOverBackColor = Color.White;
                buttonFile.FlatStyle = FlatStyle.Flat;
                buttonFile.Font = new Font("Microsoft Sans Serif", 14F);
                buttonFile.Image = Properties.Resources.file;
                buttonFile.ImageAlign = ContentAlignment.MiddleLeft;
                buttonFile.Location = new Point(12, 170);
                buttonFile.Name = "buttonFile";
                buttonFile.Padding = new Padding(15);
                buttonFile.Size = new Size(276, 100);
                buttonFile.TabIndex = 1;
                buttonFile.Text = file;
                buttonFile.TextAlign = ContentAlignment.MiddleRight;
                buttonFile.UseVisualStyleBackColor = false;
                buttonFile.Click += new EventHandler(buttonFile_Click);
                // 
                // buttonCamera
                // 
                buttonCamera.BackColor = Color.FromArgb(200, 200, 200);
                buttonCamera.Cursor = Cursors.Hand;
                buttonCamera.FlatAppearance.BorderSize = 0;
                buttonCamera.FlatAppearance.MouseDownBackColor = Color.FromArgb(150, 150, 150);
                buttonCamera.FlatAppearance.MouseOverBackColor = Color.White;
                buttonCamera.FlatStyle = FlatStyle.Flat;
                buttonCamera.Font = new Font("Microsoft Sans Serif", 14F);
                buttonCamera.Image = Properties.Resources.cam;
                buttonCamera.ImageAlign = ContentAlignment.MiddleLeft;
                buttonCamera.Location = new Point(12, 64);
                buttonCamera.Name = "buttonCamera";
                buttonCamera.Padding = new Padding(15);
                buttonCamera.Size = new Size(276, 100);
                buttonCamera.TabIndex = 1;
                buttonCamera.Text = camera;
                buttonCamera.TextAlign = ContentAlignment.MiddleRight;
                buttonCamera.UseVisualStyleBackColor = false;
                buttonCamera.Click += new EventHandler(buttonCamera_Click);
                picture = new PictureBox();
                camsComboBox = new ComboBox();
                buttonAgain = new Button();
                buttonSave = new Button();
                button3 = new Button();
                cameraPanel = new Panel();
                ((ISupportInitialize)(picture)).BeginInit();
                cameraPanel.SuspendLayout();
                SuspendLayout();
                // 
                // picture
                // 
                picture.Location = new Point(0, 0);
                picture.Name = "picture";
                picture.Size = new Size(300, 211);
                picture.TabIndex = 0;
                picture.TabStop = false;
                // 
                // camsComboBox
                // 
                camsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                camsComboBox.FormattingEnabled = true;
                camsComboBox.Location = new Point(0, 211);
                camsComboBox.Name = "camsComboBox";
                camsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                camsComboBox.Size = new Size(100, 21);
                camsComboBox.TabIndex = 1;
                // 
                // buttonAgain
                // 
                buttonAgain.Cursor = Cursors.Hand;
                buttonAgain.FlatAppearance.BorderSize = 0;
                buttonAgain.BackColor = Color.FromArgb(68, 68, 68);
                buttonAgain.FlatAppearance.MouseDownBackColor = Color.FromArgb(68, 68, 68);
                buttonAgain.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 30, 30);
                buttonAgain.ForeColor = Color.White;
                buttonAgain.FlatStyle = FlatStyle.Flat;
                buttonAgain.Location = new Point(100, 211);
                buttonAgain.Name = "buttonAgain";
                buttonAgain.Size = new Size(100, 21);
                buttonAgain.TabIndex = 2;
                buttonAgain.Text = retake;
                buttonAgain.UseVisualStyleBackColor = true;
                buttonAgain.Click += new EventHandler(buttonAgain_Click);
                // 
                // buttonSave
                // 
                buttonSave.Cursor = Cursors.Hand;
                buttonSave.BackColor = Color.FromArgb(68, 68, 68);
                buttonSave.FlatAppearance.BorderSize = 0;
                buttonSave.FlatAppearance.MouseDownBackColor = Color.FromArgb(68, 68, 68);
                buttonSave.ForeColor = Color.White;
                buttonSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 30, 30);
                buttonSave.FlatStyle = FlatStyle.Flat;
                buttonSave.Location = new Point(200, 211);
                buttonSave.Name = "buttonSave";
                buttonSave.Size = new Size(100, 21);
                buttonSave.TabIndex = 2;
                buttonSave.Text = save;
                buttonSave.UseVisualStyleBackColor = true;
                buttonSave.Click += new EventHandler(buttonSave_Click);
                // 
                // cameraPanel
                // 
                cameraPanel.Controls.Add(buttonSave);
                cameraPanel.Controls.Add(picture);
                cameraPanel.Controls.Add(buttonAgain);
                cameraPanel.Controls.Add(camsComboBox);
                cameraPanel.Location = new Point(0, 50);
                cameraPanel.Name = "cameraPanel";
                cameraPanel.Size = new Size(300, 232);
                cameraPanel.TabIndex = 4;
                cameraPanel.Visible = false;
                // 
                // Form1
                // 
                AutoScaleDimensions = new SizeF(6F, 13F);
                AutoScaleMode = AutoScaleMode.Font;
                BackColor = Color.FromArgb(160, 160, 160);
                ClientSize = new Size(300, 282);
                Controls.Add(buttonFile);
                Controls.Add(buttonCamera);
                Controls.Add(panelToolBar);
                Controls.Add(cameraPanel);
                FormBorderStyle = FormBorderStyle.None;
                Name = "Form1";
                FormClosing += new FormClosingEventHandler(Form_FormClosing);
                panelToolBar.ResumeLayout(false);
                panelToolBar.PerformLayout();
                ResumeLayout(false);
            }
            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }



            private Point delta;
            private bool move = false;
            private void moveYes(object sender, MouseEventArgs e)
            {
                delta = new Point(Cursor.Position.X - Location.X, Cursor.Position.Y - Location.Y);
                move = true;
                panelToolBar.Cursor = Cursors.SizeAll;
            }

            private void moveNo(object sender, MouseEventArgs e)
            {
                move = false;
                panelToolBar.Cursor = Cursors.Default;
            }

            private void moveDo(object sender, MouseEventArgs e)
            {
                if (move)
                    Location = new Point(Cursor.Position.X - delta.X, Cursor.Position.Y - delta.Y);
            }



            private void buttonExit_Click(object sender, EventArgs e)
            {
                chosenImage = null;
                Close();
            }

            private void buttonMinimize_Click(object sender, EventArgs e)
            {
                WindowState = FormWindowState.Minimized;
            }

            private void buttonFile_Click(object sender, EventArgs e)
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Filter = "PNG|*.png|JPEG|*jpg|BMP|*.bmp|Other type of file|*.*";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    chosenImage = Image.FromFile(file.FileName);
                    Close();
                }
            }



            private void buttonSave_Click(object sender, EventArgs e)
            {
                if (camsComboBox.Items.Count > 0)
                    if (cam.IsRunning)
                    {
                        cam.Stop();
                        buttonSave.Text = confirm;
                    }
                    else
                    {
                        Close();
                    }
            }

            private void buttonAgain_Click(object sender, EventArgs e)
            {
                buttonSave.Text = save;
                if (!cam.IsRunning)
                    cam.Start();
            }

            private void Cam_NewFrame(object sender, NewFrameEventArgs e)
            {
                var image = (Bitmap)e.Frame.Clone();
                chosenImage = (Bitmap)e.Frame.Clone();

                float width = picture.Width;
                float height = picture.Height;
                var brush = new SolidBrush(Color.Black);

                float scale = Math.Max(width / image.Width, height / image.Height);

                var bmp = new Bitmap((int)width, (int)height);
                var graph = Graphics.FromImage(bmp);

                graph.InterpolationMode = InterpolationMode.High;
                graph.CompositingQuality = CompositingQuality.HighQuality;
                graph.SmoothingMode = SmoothingMode.AntiAlias;

                var scaleWidth = (int)(image.Width * scale);
                var scaleHeight = (int)(image.Height * scale);

                graph.FillRectangle(brush, new RectangleF(0, 0, width, height));
                graph.DrawImage(image, new Rectangle(((int)width - scaleWidth) / 2, ((int)height - scaleHeight) / 2, scaleWidth, scaleHeight));

                picture.Image = bmp;
            }



            private void buttonCamera_Click(object sender, EventArgs e)
            {
                webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                foreach (FilterInfo dev in webcam)
                    camsComboBox.Items.Add(dev.Name);
                if (camsComboBox.Items.Count != 0)
                {
                    camsComboBox.SelectedItem = camsComboBox.Items[0];

                    cam = new VideoCaptureDevice(webcam[camsComboBox.SelectedIndex].MonikerString);
                    cam.NewFrame += Cam_NewFrame;
                    cam.Start();
                }

                buttonFile.Visible = false;
                buttonCamera.Visible = false;
                cameraPanel.Visible = true;
            }

            private void Form_FormClosing(object sender, FormClosingEventArgs e)
            {
                if (cam != null && cam.IsRunning)
                    new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                    {
                        cam.Stop();
                    })).Start();
            }
        }
    }
}
