namespace MyProject
{
    partial class Register
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
            txtUsername = new TextBox();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            txtConfirmPassword = new TextBox();
            btnRegister = new Button();
            btnBackToLogin = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(12, 58);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Nhập username";
            txtUsername.Size = new Size(281, 23);
            txtUsername.TabIndex = 0;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(12, 106);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "example@email.com";
            txtEmail.Size = new Size(281, 23);
            txtEmail.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(12, 154);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(281, 23);
            txtPassword.TabIndex = 2;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Location = new Point(12, 202);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '*';
            txtConfirmPassword.Size = new Size(281, 23);
            txtConfirmPassword.TabIndex = 3;
            // 
            // btnRegister
            // 
            btnRegister.BackColor = SystemColors.Highlight;
            btnRegister.ForeColor = SystemColors.ControlLightLight;
            btnRegister.Location = new Point(12, 244);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(281, 47);
            btnRegister.TabIndex = 4;
            btnRegister.Text = "Đăng Ký";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += btnRegister_Click;
            // 
            // btnBackToLogin
            // 
            btnBackToLogin.Location = new Point(12, 297);
            btnBackToLogin.Name = "btnBackToLogin";
            btnBackToLogin.Size = new Size(281, 35);
            btnBackToLogin.TabIndex = 5;
            btnBackToLogin.Text = "Quay Lại Đăng Nhập";
            btnBackToLogin.UseVisualStyleBackColor = true;
            btnBackToLogin.Click += btnBackToLogin_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 40);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 2;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 88);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 2;
            label2.Text = "Email";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 136);
            label3.Name = "label3";
            label3.Size = new Size(58, 15);
            label3.TabIndex = 2;
            label3.Text = "Mật Khẩu";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 184);
            label4.Name = "label4";
            label4.Size = new Size(112, 15);
            label4.TabIndex = 2;
            label4.Text = "Xác Nhận Mật Khẩu";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(102, 9);
            label5.Name = "label5";
            label5.Size = new Size(97, 25);
            label5.TabIndex = 6;
            label5.Text = "ĐĂNG KÝ";
            label5.Click += label5_Click;
            // 
            // Register
            // 
            AcceptButton = btnRegister;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(305, 350);
            Controls.Add(label5);
            Controls.Add(btnBackToLogin);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnRegister);
            Controls.Add(txtConfirmPassword);
            Controls.Add(txtPassword);
            Controls.Add(txtEmail);
            Controls.Add(txtUsername);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Register";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng Ký Tài Khoản";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUsername;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private Button btnRegister;
        private Button btnBackToLogin;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}
