namespace Kadrlar
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.kadrlarRoyhatiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.organizatsiyaniTanlashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.servisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.organizatsiyaSozlamalariToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bazaBoyichaQidirishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hotiraniBoshatishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.litsenziyaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dasturHaqidaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chqishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.кадрларToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.чиқишToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.бекорҚилишToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.notifyOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kadrlarRoyhatiToolStripMenuItem,
            this.organizatsiyaniTanlashToolStripMenuItem,
            this.servisToolStripMenuItem,
            this.chqishToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(662, 23);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // kadrlarRoyhatiToolStripMenuItem
            // 
            this.kadrlarRoyhatiToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.kadrlarRoyhatiToolStripMenuItem.Name = "kadrlarRoyhatiToolStripMenuItem";
            this.kadrlarRoyhatiToolStripMenuItem.Size = new System.Drawing.Size(65, 19);
            this.kadrlarRoyhatiToolStripMenuItem.Text = "Кадрлар";
            this.kadrlarRoyhatiToolStripMenuItem.Click += new System.EventHandler(this.kadrlarRoyhatiToolStripMenuItem_Click);
            // 
            // organizatsiyaniTanlashToolStripMenuItem
            // 
            this.organizatsiyaniTanlashToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.organizatsiyaniTanlashToolStripMenuItem.Name = "organizatsiyaniTanlashToolStripMenuItem";
            this.organizatsiyaniTanlashToolStripMenuItem.Size = new System.Drawing.Size(111, 19);
            this.organizatsiyaniTanlashToolStripMenuItem.Text = "Организациялар";
            this.organizatsiyaniTanlashToolStripMenuItem.Click += new System.EventHandler(this.organizatsiyaniTanlashToolStripMenuItem_Click);
            // 
            // servisToolStripMenuItem
            // 
            this.servisToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.servisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.organizatsiyaSozlamalariToolStripMenuItem,
            this.bazaBoyichaQidirishToolStripMenuItem,
            this.hotiraniBoshatishToolStripMenuItem,
            this.litsenziyaToolStripMenuItem,
            this.dasturHaqidaToolStripMenuItem});
            this.servisToolStripMenuItem.Name = "servisToolStripMenuItem";
            this.servisToolStripMenuItem.Size = new System.Drawing.Size(72, 19);
            this.servisToolStripMenuItem.Text = "Қўшимча";
            // 
            // organizatsiyaSozlamalariToolStripMenuItem
            // 
            this.organizatsiyaSozlamalariToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.organizatsiyaSozlamalariToolStripMenuItem.Name = "organizatsiyaSozlamalariToolStripMenuItem";
            this.organizatsiyaSozlamalariToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.organizatsiyaSozlamalariToolStripMenuItem.Text = "Организация созламалари";
            this.organizatsiyaSozlamalariToolStripMenuItem.Click += new System.EventHandler(this.organizatsiyaniTanlashToolStripMenuItem_Click);
            // 
            // bazaBoyichaQidirishToolStripMenuItem
            // 
            this.bazaBoyichaQidirishToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bazaBoyichaQidirishToolStripMenuItem.Name = "bazaBoyichaQidirishToolStripMenuItem";
            this.bazaBoyichaQidirishToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.bazaBoyichaQidirishToolStripMenuItem.Text = "База бўйича қидириш";
            this.bazaBoyichaQidirishToolStripMenuItem.Click += new System.EventHandler(this.kadrlarRoyhatiToolStripMenuItem_Click);
            // 
            // hotiraniBoshatishToolStripMenuItem
            // 
            this.hotiraniBoshatishToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.hotiraniBoshatishToolStripMenuItem.Name = "hotiraniBoshatishToolStripMenuItem";
            this.hotiraniBoshatishToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.hotiraniBoshatishToolStripMenuItem.Text = "Ҳотирани бўшатиш";
            this.hotiraniBoshatishToolStripMenuItem.Click += new System.EventHandler(this.hotiraniBoshatishToolStripMenuItem_Click);
            // 
            // litsenziyaToolStripMenuItem
            // 
            this.litsenziyaToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.litsenziyaToolStripMenuItem.Name = "litsenziyaToolStripMenuItem";
            this.litsenziyaToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.litsenziyaToolStripMenuItem.Text = "Лицензия созламалари";
            this.litsenziyaToolStripMenuItem.Click += new System.EventHandler(this.litsenziyaToolStripMenuItem_Click);
            // 
            // dasturHaqidaToolStripMenuItem
            // 
            this.dasturHaqidaToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.dasturHaqidaToolStripMenuItem.Name = "dasturHaqidaToolStripMenuItem";
            this.dasturHaqidaToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.dasturHaqidaToolStripMenuItem.Text = "Дастур ҳақида";
            this.dasturHaqidaToolStripMenuItem.Click += new System.EventHandler(this.dasturHaqidaToolStripMenuItem_Click);
            // 
            // chqishToolStripMenuItem
            // 
            this.chqishToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.chqishToolStripMenuItem.Name = "chqishToolStripMenuItem";
            this.chqishToolStripMenuItem.Size = new System.Drawing.Size(58, 19);
            this.chqishToolStripMenuItem.Text = "Чиқиш";
            this.chqishToolStripMenuItem.Click += new System.EventHandler(this.chqishToolStripMenuItem_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Дастурга қайтиш учун шу тугмачани сичқонча билан икки марта босинг!  ";
            this.notifyIcon.BalloonTipTitle = "Сизнинг кадрлар дастурингиз";
            this.notifyIcon.ContextMenuStrip = this.notifyOptions;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Кадрлар дастури";
            this.notifyIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDown);
            // 
            // notifyOptions
            // 
            this.notifyOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.кадрларToolStripMenuItem,
            this.чиқишToolStripMenuItem,
            this.бекорҚилишToolStripMenuItem});
            this.notifyOptions.Name = "notifyOptions";
            this.notifyOptions.Size = new System.Drawing.Size(165, 118);
            // 
            // кадрларToolStripMenuItem
            // 
            this.кадрларToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("кадрларToolStripMenuItem.Image")));
            this.кадрларToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.кадрларToolStripMenuItem.Name = "кадрларToolStripMenuItem";
            this.кадрларToolStripMenuItem.Size = new System.Drawing.Size(164, 38);
            this.кадрларToolStripMenuItem.Text = "Кадрлар";
            this.кадрларToolStripMenuItem.Click += new System.EventHandler(this.openAppToolStripMenuItem_Click);
            // 
            // чиқишToolStripMenuItem
            // 
            this.чиқишToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("чиқишToolStripMenuItem.Image")));
            this.чиқишToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.чиқишToolStripMenuItem.Name = "чиқишToolStripMenuItem";
            this.чиқишToolStripMenuItem.Size = new System.Drawing.Size(164, 38);
            this.чиқишToolStripMenuItem.Text = "Чиқиш";
            this.чиқишToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // бекорҚилишToolStripMenuItem
            // 
            this.бекорҚилишToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("бекорҚилишToolStripMenuItem.Image")));
            this.бекорҚилишToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.бекорҚилишToolStripMenuItem.Name = "бекорҚилишToolStripMenuItem";
            this.бекорҚилишToolStripMenuItem.Size = new System.Drawing.Size(164, 38);
            this.бекорҚилишToolStripMenuItem.Text = "Бекор қилиш";
            this.бекорҚилишToolStripMenuItem.Click += new System.EventHandler(this.cancelToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 401);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Кадрлар (www.timest.org)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.notifyOptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem kadrlarRoyhatiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem organizatsiyaniTanlashToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chqishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem servisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem organizatsiyaSozlamalariToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bazaBoyichaQidirishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hotiraniBoshatishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem litsenziyaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dasturHaqidaToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyOptions;
        private System.Windows.Forms.ToolStripMenuItem кадрларToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem чиқишToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem бекорҚилишToolStripMenuItem;
    }
}

