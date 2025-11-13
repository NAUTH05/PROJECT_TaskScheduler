namespace MyProject
{
    partial class TaskForm
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
            panelMain = new Panel();
            lblTitle = new Label();
            btnCreate = new Button();
            lblTaskName = new Label();
            btnCancel = new Button();
            txtTaskName = new TextBox();
            cboStatus = new ComboBox();
            lblDescription = new Label();
            lblStatus = new Label();
            txtDescription = new TextBox();
            cboPriority = new ComboBox();
            lblDueDate = new Label();
            lblPriority = new Label();
            dtpDueDate = new DateTimePicker();
            lblAssignTo = new Label();
            cboAssignTo = new ComboBox();
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.White;
            panelMain.Controls.Add(lblTitle);
            panelMain.Controls.Add(btnCreate);
            panelMain.Controls.Add(lblTaskName);
            panelMain.Controls.Add(btnCancel);
            panelMain.Controls.Add(txtTaskName);
            panelMain.Controls.Add(cboStatus);
            panelMain.Controls.Add(lblDescription);
            panelMain.Controls.Add(lblStatus);
            panelMain.Controls.Add(txtDescription);
            panelMain.Controls.Add(cboPriority);
            panelMain.Controls.Add(lblDueDate);
            panelMain.Controls.Add(lblPriority);
            panelMain.Controls.Add(dtpDueDate);
            panelMain.Controls.Add(lblAssignTo);
            panelMain.Controls.Add(cboAssignTo);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(540, 580);
            panelMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Location = new Point(30, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(249, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Thêm Nhiệm Vụ Mới";
            // 
            // btnCreate
            // 
            btnCreate.BackColor = Color.FromArgb(46, 204, 113);
            btnCreate.Cursor = Cursors.Hand;
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCreate.ForeColor = Color.White;
            btnCreate.Location = new Point(410, 500);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(100, 45);
            btnCreate.TabIndex = 12;
            btnCreate.Text = "Tạo Nhiệm Vụ";
            btnCreate.UseVisualStyleBackColor = false;
            btnCreate.Click += btnCreate_Click;
            // 
            // lblTaskName
            // 
            lblTaskName.AutoSize = true;
            lblTaskName.Font = new Font("Segoe UI", 10F);
            lblTaskName.ForeColor = Color.FromArgb(44, 62, 80);
            lblTaskName.Location = new Point(30, 90);
            lblTaskName.Name = "lblTaskName";
            lblTaskName.Size = new Size(105, 19);
            lblTaskName.TabIndex = 1;
            lblTaskName.Text = "Tên Nhiệm Vụ *";
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.White;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderColor = Color.LightGray;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.Gray;
            btnCancel.Location = new Point(296, 500);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 45);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtTaskName
            // 
            txtTaskName.Font = new Font("Segoe UI", 11F);
            txtTaskName.ForeColor = Color.Gray;
            txtTaskName.Location = new Point(30, 115);
            txtTaskName.Name = "txtTaskName";
            txtTaskName.Size = new Size(480, 27);
            txtTaskName.TabIndex = 2;
            txtTaskName.Text = "Ví dụ: Viết API cho CRUD Task";
            txtTaskName.Enter += txtTaskName_Enter;
            txtTaskName.Leave += txtTaskName_Leave;
            // 
            // cboStatus
            // 
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.Font = new Font("Segoe UI", 11F);
            cboStatus.FormattingEnabled = true;
            cboStatus.Items.AddRange(new object[] { "Backlog", "To Do", "In Progress", "In Review", "Testing", "Blocked", "Completed", "Cancelled" });
            cboStatus.Location = new Point(290, 415);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new Size(220, 28);
            cboStatus.TabIndex = 10;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 10F);
            lblDescription.ForeColor = Color.FromArgb(44, 62, 80);
            lblDescription.Location = new Point(30, 165);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(123, 19);
            lblDescription.TabIndex = 3;
            lblDescription.Text = "Mô Tả Nhiệm Vụ *";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 10F);
            lblStatus.ForeColor = Color.FromArgb(44, 62, 80);
            lblStatus.Location = new Point(290, 390);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(72, 19);
            lblStatus.TabIndex = 9;
            lblStatus.Text = "Trạng Thái";
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Segoe UI", 10F);
            txtDescription.ForeColor = Color.Gray;
            txtDescription.Location = new Point(30, 190);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(480, 100);
            txtDescription.TabIndex = 4;
            txtDescription.Text = "Chi tiết yêu cầu và tiêu chí hoàn thành.";
            txtDescription.Enter += txtDescription_Enter;
            txtDescription.Leave += txtDescription_Leave;
            // 
            // cboPriority
            // 
            cboPriority.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPriority.Font = new Font("Segoe UI", 11F);
            cboPriority.FormattingEnabled = true;
            cboPriority.Items.AddRange(new object[] { "Low", "Medium", "High", "Urgent" });
            cboPriority.Location = new Point(30, 415);
            cboPriority.Name = "cboPriority";
            cboPriority.Size = new Size(220, 28);
            cboPriority.TabIndex = 8;
            // 
            // lblDueDate
            // 
            lblDueDate.AutoSize = true;
            lblDueDate.Font = new Font("Segoe UI", 10F);
            lblDueDate.ForeColor = Color.FromArgb(44, 62, 80);
            lblDueDate.Location = new Point(30, 315);
            lblDueDate.Name = "lblDueDate";
            lblDueDate.Size = new Size(106, 19);
            lblDueDate.TabIndex = 5;
            lblDueDate.Text = "Ngày Hết Hạn *";
            // 
            // lblPriority
            // 
            lblPriority.AutoSize = true;
            lblPriority.Font = new Font("Segoe UI", 10F);
            lblPriority.ForeColor = Color.FromArgb(44, 62, 80);
            lblPriority.Location = new Point(30, 390);
            lblPriority.Name = "lblPriority";
            lblPriority.Size = new Size(109, 19);
            lblPriority.TabIndex = 7;
            lblPriority.Text = "Mức Độ Ưu Tiên";
            // 
            // dtpDueDate
            // 
            dtpDueDate.CalendarFont = new Font("Segoe UI", 10F);
            dtpDueDate.Font = new Font("Segoe UI", 11F);
            dtpDueDate.Format = DateTimePickerFormat.Short;
            dtpDueDate.Location = new Point(30, 340);
            dtpDueDate.Name = "dtpDueDate";
            dtpDueDate.Size = new Size(220, 27);
            dtpDueDate.TabIndex = 6;
            // 
            // lblAssignTo
            // 
            lblAssignTo.AutoSize = true;
            lblAssignTo.Font = new Font("Segoe UI", 10F);
            lblAssignTo.ForeColor = Color.FromArgb(44, 62, 80);
            lblAssignTo.Location = new Point(290, 315);
            lblAssignTo.Name = "lblAssignTo";
            lblAssignTo.Size = new Size(96, 19);
            lblAssignTo.TabIndex = 13;
            lblAssignTo.Text = "Người Giao *";
            // 
            // cboAssignTo
            // 
            cboAssignTo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboAssignTo.Font = new Font("Segoe UI", 11F);
            cboAssignTo.FormattingEnabled = true;
            cboAssignTo.Location = new Point(290, 340);
            cboAssignTo.Name = "cboAssignTo";
            cboAssignTo.Size = new Size(220, 28);
            cboAssignTo.TabIndex = 14;
            // 
            // TaskForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(540, 580);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TaskForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Thêm Nhiệm Vụ Mới";
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTaskName;
        private System.Windows.Forms.TextBox txtTaskName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDueDate;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.ComboBox cboPriority;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label lblAssignTo;
        private System.Windows.Forms.ComboBox cboAssignTo;
    }
}