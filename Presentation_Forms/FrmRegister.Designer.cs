namespace Presentation_Forms
{
    partial class FrmRegister
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
            components = new System.ComponentModel.Container();
            txtFirstName = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtLastName = new TextBox();
            label3 = new Label();
            txtEmail = new TextBox();
            label4 = new Label();
            txtPassword = new TextBox();
            label5 = new Label();
            txtUsername = new TextBox();
            btnSave = new Button();
            label6 = new Label();
            label7 = new Label();
            txtPasswordRef = new TextBox();
            btnLogin = new Button();
            label8 = new Label();
            lblPassword = new Label();
            txtLoginPassword = new TextBox();
            lblUsername = new Label();
            txtLoginUsername = new TextBox();
            timerBackMove = new System.Windows.Forms.Timer(components);
            label9 = new Label();
            lblNext0 = new Label();
            label10 = new Label();
            label11 = new Label();
            pictBack = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictBack).BeginInit();
            SuspendLayout();
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(49, 43);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(298, 27);
            txtFirstName.TabIndex = 0;
            txtFirstName.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(49, 20);
            label1.Name = "label1";
            label1.Size = new Size(28, 20);
            label1.TabIndex = 1;
            label1.Text = "Ad";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(49, 78);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 3;
            label2.Text = "Soyad";
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(49, 101);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(298, 27);
            txtLastName.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(50, 193);
            label3.Name = "label3";
            label3.Size = new Size(58, 20);
            label3.TabIndex = 5;
            label3.Text = "E-mail*";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(50, 216);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(298, 27);
            txtEmail.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(51, 246);
            label4.Name = "label4";
            label4.Size = new Size(45, 20);
            label4.TabIndex = 7;
            label4.Text = "Şifre*";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(51, 269);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(298, 27);
            txtPassword.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(51, 299);
            label5.Name = "label5";
            label5.Size = new Size(89, 20);
            label5.TabIndex = 9;
            label5.Text = "Şifre Tekrar*";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(52, 156);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(298, 27);
            txtUsername.TabIndex = 8;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(51, 399);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(299, 29);
            btnSave.TabIndex = 10;
            btnSave.Text = "Kayıt Ol";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(13, 108);
            label6.Name = "label6";
            label6.Size = new Size(0, 20);
            label6.TabIndex = 11;
            label6.Click += label6_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(50, 133);
            label7.Name = "label7";
            label7.Size = new Size(98, 20);
            label7.TabIndex = 13;
            label7.Text = "Kullanıcı Adı*";
            // 
            // txtPasswordRef
            // 
            txtPasswordRef.Location = new Point(52, 322);
            txtPasswordRef.Name = "txtPasswordRef";
            txtPasswordRef.Size = new Size(298, 27);
            txtPasswordRef.TabIndex = 12;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(459, 287);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(304, 29);
            btnLogin.TabIndex = 19;
            btnLogin.Text = "Giriş Yap";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = SystemColors.HotTrack;
            label8.Location = new Point(646, 253);
            label8.Name = "label8";
            label8.Size = new Size(117, 20);
            label8.TabIndex = 18;
            label8.Text = "Şifremi Unuttum";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(459, 201);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(39, 20);
            lblPassword.TabIndex = 17;
            lblPassword.Text = "Şifre";
            // 
            // txtLoginPassword
            // 
            txtLoginPassword.Location = new Point(459, 224);
            txtLoginPassword.Multiline = true;
            txtLoginPassword.Name = "txtLoginPassword";
            txtLoginPassword.Size = new Size(304, 27);
            txtLoginPassword.TabIndex = 16;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(459, 143);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(173, 20);
            lblUsername.TabIndex = 15;
            lblUsername.Text = "Kullanıcı Adı veya E-Mail";
            // 
            // txtLoginUsername
            // 
            txtLoginUsername.Location = new Point(459, 166);
            txtLoginUsername.Name = "txtLoginUsername";
            txtLoginUsername.Size = new Size(304, 27);
            txtLoginUsername.TabIndex = 14;
            // 
            // timerBackMove
            // 
            timerBackMove.Interval = 20;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(70, 360);
            label9.Name = "label9";
            label9.Size = new Size(187, 20);
            label9.TabIndex = 21;
            label9.Text = "Zaten Hesabın var mı ? -->";
            // 
            // lblNext0
            // 
            lblNext0.AutoSize = true;
            lblNext0.ForeColor = SystemColors.HotTrack;
            lblNext0.Location = new Point(251, 360);
            lblNext0.Name = "lblNext0";
            lblNext0.Size = new Size(73, 20);
            lblNext0.TabIndex = 22;
            lblNext0.Text = "GİRİŞ YAP";
            lblNext0.Click += lblNext0_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(590, 322);
            label10.Name = "label10";
            label10.Size = new Size(39, 20);
            label10.TabIndex = 23;
            label10.Text = "veya";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = SystemColors.ButtonFace;
            label11.BorderStyle = BorderStyle.Fixed3D;
            label11.ForeColor = SystemColors.HotTrack;
            label11.Location = new Point(577, 352);
            label11.Name = "label11";
            label11.Size = new Size(71, 22);
            label11.TabIndex = 24;
            label11.Text = "KAYIT OL";
            label11.Click += label11_Click;
            // 
            // pictBack
            // 
            pictBack.Image = Properties.Resources.image_removebg_preview__10_;
            pictBack.Location = new Point(391, -2);
            pictBack.Name = "pictBack";
            pictBack.Size = new Size(860, 463);
            pictBack.SizeMode = PictureBoxSizeMode.Zoom;
            pictBack.TabIndex = 25;
            pictBack.TabStop = false;
            // 
            // FrmRegister
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(827, 457);
            Controls.Add(pictBack);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(lblNext0);
            Controls.Add(label9);
            Controls.Add(btnLogin);
            Controls.Add(label8);
            Controls.Add(lblPassword);
            Controls.Add(txtLoginPassword);
            Controls.Add(lblUsername);
            Controls.Add(txtLoginUsername);
            Controls.Add(label7);
            Controls.Add(txtPasswordRef);
            Controls.Add(label6);
            Controls.Add(btnSave);
            Controls.Add(label5);
            Controls.Add(txtUsername);
            Controls.Add(label4);
            Controls.Add(txtPassword);
            Controls.Add(label3);
            Controls.Add(txtEmail);
            Controls.Add(label2);
            Controls.Add(txtLastName);
            Controls.Add(label1);
            Controls.Add(txtFirstName);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "FrmRegister";
            Text = "Kayıt Ol";
            Load += FrmRegister_Load;
            ((System.ComponentModel.ISupportInitialize)pictBack).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFirstName;
        private Label label1;
        private Label label2;
        private TextBox txtLastName;
        private Label label3;
        private TextBox txtEmail;
        private Label label4;
        private TextBox txtPassword;
        private Label label5;
        private TextBox txtUsername;
        private Button btnSave;
        private Label label6;
        private Label label7;
        private TextBox txtPasswordRef;
        private Button btnLogin;
        private Label label8;
        private Label lblPassword;
        private TextBox txtLoginPassword;
        private Label lblUsername;
        private TextBox txtLoginUsername;
        private System.Windows.Forms.Timer timerBackMove;
        private Label label9;
        private Label lblNext0;
        private Label label10;
        private Label label11;
        private PictureBox pictBack;
    }
}