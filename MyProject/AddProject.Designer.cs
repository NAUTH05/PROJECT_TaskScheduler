namespace MyProject
{
    partial class AddProject
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblProjectName = new Label();
            txtProjectName = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblDeadline = new Label();
            dtpDeadline = new DateTimePicker();
            lblStatus = new Label();
            cboStatus = new ComboBox();
            btnCancel = new Button();
            btnCreate = new Button();
            panelMain = new Panel();
            panelMain.SuspendLayout();
            SuspendLayout();
            
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Location = new Point(30, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(188, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Tạo Dự Án Mới";
            
            lblProjectName.AutoSize = true;
            lblProjectName.Font = new Font("Segoe UI", 10F);
            lblProjectName.ForeColor = Color.FromArgb(44, 62, 80);
            lblProjectName.Location = new Point(30, 90);
            lblProjectName.Name = "lblProjectName";
            lblProjectName.Size = new Size(73, 19);
            lblProjectName.TabIndex = 1;
            lblProjectName.Text = "Tên Dự Án";
            
            txtProjectName.Font = new Font("Segoe UI", 11F);
            txtProjectName.ForeColor = Color.Gray;
            txtProjectName.Location = new Point(30, 115);
            txtProjectName.Name = "txtProjectName";
            txtProjectName.Size = new Size(460, 27);
            txtProjectName.TabIndex = 2;
            txtProjectName.Text = "Ví dụ: Triển khai ứng dụng TaskScheduler";
            txtProjectName.Enter += txtProjectName_Enter;
            txtProjectName.Leave += txtProjectName_Leave;
            
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 10F);
            lblDescription.ForeColor = Color.FromArgb(44, 62, 80);
            lblDescription.Location = new Point(30, 165);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(91, 19);
            lblDescription.TabIndex = 3;
            lblDescription.Text = "Mô Tả Dự ÁN";
            
            txtDescription.Font = new Font("Segoe UI", 10F);
            txtDescription.ForeColor = Color.Gray;
            txtDescription.Location = new Point(30, 190);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(460, 80);
            txtDescription.TabIndex = 4;
            txtDescription.Text = "Mô tả chi tiết về mục tiêu và phạm vi dự án.";
            txtDescription.Enter += txtDescription_Enter;
            txtDescription.Leave += txtDescription_Leave;
            
            lblDeadline.AutoSize = true;
            lblDeadline.Font = new Font("Segoe UI", 10F);
            lblDeadline.ForeColor = Color.FromArgb(44, 62, 80);
            lblDeadline.Location = new Point(30, 295);
            lblDeadline.Name = "lblDeadline";
            lblDeadline.Size = new Size(150, 19);
            lblDeadline.TabIndex = 5;
            lblDeadline.Text = "Ngày Kết Thúc Dự Kiến";
            
            dtpDeadline.CalendarFont = new Font("Segoe UI", 10F);
            dtpDeadline.Font = new Font("Segoe UI", 11F);
            dtpDeadline.Format = DateTimePickerFormat.Short;
            dtpDeadline.Location = new Point(30, 320);
            dtpDeadline.Name = "dtpDeadline";
            dtpDeadline.Size = new Size(460, 27);
            dtpDeadline.TabIndex = 6;
            
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 10F);
            lblStatus.ForeColor = Color.FromArgb(44, 62, 80);
            lblStatus.Location = new Point(30, 370);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(128, 19);
            lblStatus.TabIndex = 7;
            lblStatus.Text = "Trạng Thái Ban Đầu";
            
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.Font = new Font("Segoe UI", 11F);
            cboStatus.FormattingEnabled = true;
            cboStatus.Items.AddRange(new object[] { "Đang Tiến Hành (In Progress)", "Chờ Làm (To Do)", "Hoàn Thành (Completed)" });
            cboStatus.Location = new Point(30, 395);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new Size(460, 28);
            cboStatus.TabIndex = 8;
            
            btnCancel.BackColor = Color.White;
            btnCancel.FlatAppearance.BorderColor = Color.LightGray;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Gray;
            btnCancel.Location = new Point(276, 460);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 40);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            
            btnCreate.BackColor = Color.FromArgb(88, 86, 214);
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCreate.ForeColor = Color.White;
            btnCreate.Location = new Point(370, 460);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(120, 40);
            btnCreate.TabIndex = 10;
            btnCreate.Text = "Tạo Dự Án";
            btnCreate.UseVisualStyleBackColor = false;
            btnCreate.Click += btnCreate_Click;
            
            panelMain.BackColor = Color.White;
            panelMain.Controls.Add(lblTitle);
            panelMain.Controls.Add(btnCreate);
            panelMain.Controls.Add(lblProjectName);
            panelMain.Controls.Add(btnCancel);
            panelMain.Controls.Add(txtProjectName);
            panelMain.Controls.Add(cboStatus);
            panelMain.Controls.Add(lblDescription);
            panelMain.Controls.Add(lblStatus);
            panelMain.Controls.Add(txtDescription);
            panelMain.Controls.Add(dtpDeadline);
            panelMain.Controls.Add(lblDeadline);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(520, 530);
            panelMain.TabIndex = 11;
            
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(520, 530);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddProject";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Tạo Dự Án Mới";
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDeadline;
        private System.Windows.Forms.DateTimePicker dtpDeadline;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Panel panelMain;
    }
}