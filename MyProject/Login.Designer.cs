namespace MyProject
{
    partial class Login
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
            txtUsernameOrEmail = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            btnRegister = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // txtUsernameOrEmail
            // 
            txtUsernameOrEmail.Location = new Point(12, 38);
            txtUsernameOrEmail.Name = "txtUsernameOrEmail";
            txtUsernameOrEmail.PlaceholderText = "Username hoặc Email";
            txtUsernameOrEmail.Size = new Size(281, 23);
            txtUsernameOrEmail.TabIndex = 0;
            txtUsernameOrEmail.TextChanged += txtUsernameOrEmail_TextChanged;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(12, 96);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(281, 23);
            txtPassword.TabIndex = 1;
            txtPassword.TextChanged += txtPassword_TextChanged;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = SystemColors.Highlight;
            btnLogin.ForeColor = SystemColors.ControlLightLight;
            btnLogin.Location = new Point(12, 138);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(281, 47);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Đăng Nhập";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            btnLogin.KeyPress += btnLogin_KeyPress;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(12, 191);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(281, 35);
            btnRegister.TabIndex = 3;
            btnRegister.Text = "Đăng Ký Tài Khoản Mới";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(120, 15);
            label1.TabIndex = 2;
            label1.Text = "Username hoặc Email";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 78);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 2;
            label2.Text = "Mật Khẩu";
            // 
            // Login
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(305, 240);
            Controls.Add(btnRegister);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(txtUsernameOrEmail);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng Nhập";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUsernameOrEmail;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegister;
        private Label label1;
        private Label label2;
    }
}
