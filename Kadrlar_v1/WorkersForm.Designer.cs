namespace Kadrlar
{
    partial class WorkersForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkersForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button11 = new System.Windows.Forms.Button();
            this.qidirishGroupBox = new System.Windows.Forms.GroupBox();
            this.button12 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.keywordsTextBox = new System.Windows.Forms.TextBox();
            this.ayolRadioButton = new System.Windows.Forms.RadioButton();
            this.erkakRadioButton = new System.Windows.Forms.RadioButton();
            this.barchaRadioButton = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ishchiMalumotiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.malumotVaraqasiniOzgartirishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ishchiniShahsiyVaraqaFayliniYasashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ishchiniBazadanOchirishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.qidirishGroupBox.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Lime;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(9, 10);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(625, 454);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.doubleClickWorker);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.selectWorker);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(7, 477);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(462, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ишчи икки марта сичқонча билан босилганда уни тўлиқ информацияси панелига ўтилади" +
    "";
            // 
            // button3
            // 
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.button3.ForeColor = System.Drawing.Color.Red;
            this.button3.Location = new System.Drawing.Point(473, 468);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.button3.Size = new System.Drawing.Size(161, 32);
            this.button3.TabIndex = 3;
            this.button3.Text = "Ўзгартириш";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.button3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.button11);
            this.groupBox1.Controls.Add(this.qidirishGroupBox);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button9);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Location = new System.Drawing.Point(638, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(158, 500);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.radioButton2.Location = new System.Drawing.Point(29, 322);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(99, 17);
            this.radioButton2.TabIndex = 16;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Ишлаётганлар";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(29, 305);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(92, 17);
            this.radioButton1.TabIndex = 16;
            this.radioButton1.Text = "Вакансиялар";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(9, 58);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(144, 17);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "Вакансиялар базасида";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.SystemColors.Menu;
            this.button11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button11.ForeColor = System.Drawing.Color.Black;
            this.button11.Image = ((System.Drawing.Image)(resources.GetObject("button11.Image")));
            this.button11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button11.Location = new System.Drawing.Point(4, 267);
            this.button11.Margin = new System.Windows.Forms.Padding(0);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(149, 35);
            this.button11.TabIndex = 12;
            this.button11.Text = "Ҳисобот";
            this.button11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // qidirishGroupBox
            // 
            this.qidirishGroupBox.Controls.Add(this.button12);
            this.qidirishGroupBox.Controls.Add(this.label4);
            this.qidirishGroupBox.Controls.Add(this.keywordsTextBox);
            this.qidirishGroupBox.Controls.Add(this.ayolRadioButton);
            this.qidirishGroupBox.Controls.Add(this.erkakRadioButton);
            this.qidirishGroupBox.Controls.Add(this.barchaRadioButton);
            this.qidirishGroupBox.Location = new System.Drawing.Point(4, 342);
            this.qidirishGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.qidirishGroupBox.Name = "qidirishGroupBox";
            this.qidirishGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.qidirishGroupBox.Size = new System.Drawing.Size(149, 154);
            this.qidirishGroupBox.TabIndex = 14;
            this.qidirishGroupBox.TabStop = false;
            // 
            // button12
            // 
            this.button12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.button12.ForeColor = System.Drawing.Color.Black;
            this.button12.Image = ((System.Drawing.Image)(resources.GetObject("button12.Image")));
            this.button12.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button12.Location = new System.Drawing.Point(66, 87);
            this.button12.Margin = new System.Windows.Forms.Padding(2);
            this.button12.Name = "button12";
            this.button12.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.button12.Size = new System.Drawing.Size(79, 58);
            this.button12.TabIndex = 2;
            this.button12.Text = "Қидириш";
            this.button12.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Калит сўзлар (қидирув)";
            // 
            // keywordsTextBox
            // 
            this.keywordsTextBox.Location = new System.Drawing.Point(7, 26);
            this.keywordsTextBox.Multiline = true;
            this.keywordsTextBox.Name = "keywordsTextBox";
            this.keywordsTextBox.Size = new System.Drawing.Size(137, 59);
            this.keywordsTextBox.TabIndex = 7;
            // 
            // ayolRadioButton
            // 
            this.ayolRadioButton.AutoSize = true;
            this.ayolRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ayolRadioButton.Location = new System.Drawing.Point(11, 125);
            this.ayolRadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.ayolRadioButton.Name = "ayolRadioButton";
            this.ayolRadioButton.Size = new System.Drawing.Size(44, 17);
            this.ayolRadioButton.TabIndex = 5;
            this.ayolRadioButton.Text = "Аёл";
            this.ayolRadioButton.UseVisualStyleBackColor = true;
            // 
            // erkakRadioButton
            // 
            this.erkakRadioButton.AutoSize = true;
            this.erkakRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.erkakRadioButton.Location = new System.Drawing.Point(11, 108);
            this.erkakRadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.erkakRadioButton.Name = "erkakRadioButton";
            this.erkakRadioButton.Size = new System.Drawing.Size(54, 17);
            this.erkakRadioButton.TabIndex = 4;
            this.erkakRadioButton.Text = "Эркак";
            this.erkakRadioButton.UseVisualStyleBackColor = true;
            // 
            // barchaRadioButton
            // 
            this.barchaRadioButton.AutoSize = true;
            this.barchaRadioButton.Checked = true;
            this.barchaRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.barchaRadioButton.Location = new System.Drawing.Point(11, 92);
            this.barchaRadioButton.Margin = new System.Windows.Forms.Padding(2);
            this.barchaRadioButton.Name = "barchaRadioButton";
            this.barchaRadioButton.Size = new System.Drawing.Size(55, 17);
            this.barchaRadioButton.TabIndex = 3;
            this.barchaRadioButton.TabStop = true;
            this.barchaRadioButton.Text = "Барча";
            this.barchaRadioButton.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(4, 210);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(149, 27);
            this.button2.TabIndex = 8;
            this.button2.Text = "Бўшатилганлар";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button6
            // 
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button6.ForeColor = System.Drawing.Color.Black;
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(4, 237);
            this.button6.Margin = new System.Windows.Forms.Padding(0);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(149, 29);
            this.button6.TabIndex = 8;
            this.button6.Text = "Кадрни ўчириш";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button5.ForeColor = System.Drawing.Color.Black;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(4, 167);
            this.button5.Margin = new System.Windows.Forms.Padding(0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(149, 43);
            this.button5.TabIndex = 8;
            this.button5.Text = "Кадрни бўшатиш";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button9
            // 
            this.button9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button9.ForeColor = System.Drawing.Color.Black;
            this.button9.Image = ((System.Drawing.Image)(resources.GetObject("button9.Image")));
            this.button9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button9.Location = new System.Drawing.Point(4, 121);
            this.button9.Margin = new System.Windows.Forms.Padding(0);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(149, 43);
            this.button9.TabIndex = 8;
            this.button9.Text = "Ўзгартириш";
            this.button9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.editWorkerInfo);
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(4, 78);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 43);
            this.button1.TabIndex = 2;
            this.button1.Text = "Маълумоти";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.showWorkerInfo);
            // 
            // button4
            // 
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(4, 10);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(149, 43);
            this.button4.TabIndex = 2;
            this.button4.Text = "Янги кадр";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.createWorker);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ishchiMalumotiToolStripMenuItem,
            this.malumotVaraqasiniOzgartirishToolStripMenuItem,
            this.ishchiniShahsiyVaraqaFayliniYasashToolStripMenuItem,
            this.ishchiniBazadanOchirishToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(301, 92);
            // 
            // ishchiMalumotiToolStripMenuItem
            // 
            this.ishchiMalumotiToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            this.ishchiMalumotiToolStripMenuItem.Name = "ishchiMalumotiToolStripMenuItem";
            this.ishchiMalumotiToolStripMenuItem.Size = new System.Drawing.Size(300, 22);
            this.ishchiMalumotiToolStripMenuItem.Text = "💼 Кадр маълумоти";
            this.ishchiMalumotiToolStripMenuItem.Click += new System.EventHandler(this.showWorkerInfo);
            // 
            // malumotVaraqasiniOzgartirishToolStripMenuItem
            // 
            this.malumotVaraqasiniOzgartirishToolStripMenuItem.ForeColor = System.Drawing.Color.Fuchsia;
            this.malumotVaraqasiniOzgartirishToolStripMenuItem.Name = "malumotVaraqasiniOzgartirishToolStripMenuItem";
            this.malumotVaraqasiniOzgartirishToolStripMenuItem.Size = new System.Drawing.Size(300, 22);
            this.malumotVaraqasiniOzgartirishToolStripMenuItem.Text = "📝 Маълумот варақасини ўзгартириш";
            this.malumotVaraqasiniOzgartirishToolStripMenuItem.Click += new System.EventHandler(this.editWorkerInfo);
            // 
            // ishchiniShahsiyVaraqaFayliniYasashToolStripMenuItem
            // 
            this.ishchiniShahsiyVaraqaFayliniYasashToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.ishchiniShahsiyVaraqaFayliniYasashToolStripMenuItem.Name = "ishchiniShahsiyVaraqaFayliniYasashToolStripMenuItem";
            this.ishchiniShahsiyVaraqaFayliniYasashToolStripMenuItem.Size = new System.Drawing.Size(300, 22);
            this.ishchiniShahsiyVaraqaFayliniYasashToolStripMenuItem.Text = "🔖 Кадр маълумоти билан шаблон тўлдириш";
            this.ishchiniShahsiyVaraqaFayliniYasashToolStripMenuItem.Click += new System.EventHandler(this.fillTemplateFile);
            // 
            // ishchiniBazadanOchirishToolStripMenuItem
            // 
            this.ishchiniBazadanOchirishToolStripMenuItem.ForeColor = System.Drawing.Color.Red;
            this.ishchiniBazadanOchirishToolStripMenuItem.Name = "ishchiniBazadanOchirishToolStripMenuItem";
            this.ishchiniBazadanOchirishToolStripMenuItem.Size = new System.Drawing.Size(300, 22);
            this.ishchiniBazadanOchirishToolStripMenuItem.Text = "✘ Кадрни базадан ўчириш";
            this.ishchiniBazadanOchirishToolStripMenuItem.Click += new System.EventHandler(this.deleteWorker);
            // 
            // WorkersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 510);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "WorkersForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "paid";
            this.Text = "Кадрлар рўйхати";
            this.Load += new System.EventHandler(this.WorkersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.qidirishGroupBox.ResumeLayout(false);
            this.qidirishGroupBox.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.GroupBox qidirishGroupBox;
        private System.Windows.Forms.RadioButton ayolRadioButton;
        private System.Windows.Forms.RadioButton erkakRadioButton;
        private System.Windows.Forms.RadioButton barchaRadioButton;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ishchiniBazadanOchirishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ishchiMalumotiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem malumotVaraqasiniOzgartirishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ishchiniShahsiyVaraqaFayliniYasashToolStripMenuItem;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox keywordsTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}