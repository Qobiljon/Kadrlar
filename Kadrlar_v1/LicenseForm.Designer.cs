namespace Kadrlar
{
    partial class LicenseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LicenseForm));
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSignIn = new System.Windows.Forms.Button();
            this.buttonSignUp = new System.Windows.Forms.Button();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.saveCheckBox = new System.Windows.Forms.CheckBox();
            this.panelLicense = new System.Windows.Forms.Panel();
            this.buttonLicense = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.codeTextBox = new System.Windows.Forms.TextBox();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.panelDone = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelLogin.SuspendLayout();
            this.panelLicense.SuspendLayout();
            this.panelDone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // emailTextBox
            // 
            this.emailTextBox.Location = new System.Drawing.Point(3, 64);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(254, 20);
            this.emailTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Электрон почта";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(3, 103);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(254, 20);
            this.passwordTextBox.TabIndex = 1;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(108, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Парол";
            // 
            // buttonSignIn
            // 
            this.buttonSignIn.Location = new System.Drawing.Point(3, 154);
            this.buttonSignIn.Name = "buttonSignIn";
            this.buttonSignIn.Size = new System.Drawing.Size(87, 30);
            this.buttonSignIn.TabIndex = 3;
            this.buttonSignIn.Text = "Кириш";
            this.buttonSignIn.UseVisualStyleBackColor = true;
            this.buttonSignIn.Click += new System.EventHandler(this.buttonSignIn_Click);
            // 
            // buttonSignUp
            // 
            this.buttonSignUp.Location = new System.Drawing.Point(96, 154);
            this.buttonSignUp.Name = "buttonSignUp";
            this.buttonSignUp.Size = new System.Drawing.Size(161, 30);
            this.buttonSignUp.TabIndex = 4;
            this.buttonSignUp.Text = "Рўйхатдан ўтиш";
            this.buttonSignUp.UseVisualStyleBackColor = true;
            this.buttonSignUp.Click += new System.EventHandler(this.buttonSignUp_Click);
            // 
            // panelLogin
            // 
            this.panelLogin.Controls.Add(this.saveCheckBox);
            this.panelLogin.Controls.Add(this.buttonSignUp);
            this.panelLogin.Controls.Add(this.emailTextBox);
            this.panelLogin.Controls.Add(this.buttonSignIn);
            this.panelLogin.Controls.Add(this.passwordTextBox);
            this.panelLogin.Controls.Add(this.label2);
            this.panelLogin.Controls.Add(this.label1);
            this.panelLogin.Location = new System.Drawing.Point(12, 12);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(260, 237);
            this.panelLogin.TabIndex = 3;
            // 
            // saveCheckBox
            // 
            this.saveCheckBox.AutoSize = true;
            this.saveCheckBox.Location = new System.Drawing.Point(20, 131);
            this.saveCheckBox.Name = "saveCheckBox";
            this.saveCheckBox.Size = new System.Drawing.Size(226, 17);
            this.saveCheckBox.TabIndex = 2;
            this.saveCheckBox.Text = "Киритилган маълумотларни сақлансин";
            this.saveCheckBox.UseVisualStyleBackColor = true;
            // 
            // panelLicense
            // 
            this.panelLicense.Controls.Add(this.buttonLicense);
            this.panelLicense.Controls.Add(this.label3);
            this.panelLicense.Controls.Add(this.codeTextBox);
            this.panelLicense.Controls.Add(this.buttonLogout);
            this.panelLicense.Location = new System.Drawing.Point(12, 12);
            this.panelLicense.Name = "panelLicense";
            this.panelLicense.Size = new System.Drawing.Size(260, 237);
            this.panelLicense.TabIndex = 4;
            // 
            // buttonLicense
            // 
            this.buttonLicense.Location = new System.Drawing.Point(3, 121);
            this.buttonLicense.Name = "buttonLicense";
            this.buttonLicense.Size = new System.Drawing.Size(254, 23);
            this.buttonLicense.TabIndex = 5;
            this.buttonLicense.Text = "Тасдиқлаш";
            this.buttonLicense.UseVisualStyleBackColor = true;
            this.buttonLicense.Click += new System.EventHandler(this.buttonLicense_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "Итимос, лицензион паролингизни \r\nшу ерга киритинг!";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // codeTextBox
            // 
            this.codeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.codeTextBox.Location = new System.Drawing.Point(3, 80);
            this.codeTextBox.MaxLength = 16;
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.Size = new System.Drawing.Size(254, 35);
            this.codeTextBox.TabIndex = 4;
            this.codeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonLogout
            // 
            this.buttonLogout.Location = new System.Drawing.Point(3, 203);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(254, 31);
            this.buttonLogout.TabIndex = 6;
            this.buttonLogout.Text = "Системадан чиқиш";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // panelDone
            // 
            this.panelDone.Controls.Add(this.button1);
            this.panelDone.Controls.Add(this.pictureBox1);
            this.panelDone.Location = new System.Drawing.Point(12, 12);
            this.panelDone.Name = "panelDone";
            this.panelDone.Size = new System.Drawing.Size(260, 237);
            this.panelDone.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 203);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(254, 31);
            this.button1.TabIndex = 7;
            this.button1.Text = "Системадан чиқиш";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(80, 68);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // LicenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.panelLicense);
            this.Controls.Add(this.panelDone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "demo";
            this.Text = "Лицензия";
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.panelLicense.ResumeLayout(false);
            this.panelLicense.PerformLayout();
            this.panelDone.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSignIn;
        private System.Windows.Forms.Button buttonSignUp;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Panel panelLicense;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox codeTextBox;
        private System.Windows.Forms.Button buttonLicense;
        private System.Windows.Forms.Panel panelDone;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox saveCheckBox;
        private System.Windows.Forms.Button button1;
    }
}