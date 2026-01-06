namespace Presentation_Forms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtUsername = new TextBox();
            lblUsername = new Label();
            lblPassword = new Label();
            txtPassword = new TextBox();
            label3 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(452, 168);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(304, 27);
            txtUsername.TabIndex = 0;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(452, 145);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(92, 20);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Kullanıcı Adı";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(452, 203);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(39, 20);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Şifre";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(452, 226);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(304, 27);
            txtPassword.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(706, 256);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 5;
            label3.Text = "label3";
            // 
            // button1
            // 
            button1.Location = new Point(452, 289);
            button1.Name = "button1";
            button1.Size = new Size(304, 29);
            button1.TabIndex = 6;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblUsername);
            Controls.Add(txtUsername);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUsername;
        private Label lblUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label label3;
        private Button button1;
    }
}
