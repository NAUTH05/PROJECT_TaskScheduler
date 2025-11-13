namespace MyProject
{
    partial class ProjectView
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSubHeader;
        private System.Windows.Forms.Label lblUserIdDisplay;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel panelProjectInfo;
        private System.Windows.Forms.Label lblProjectTitle;
        private System.Windows.Forms.Label lblProjectDescription;
        private System.Windows.Forms.Label lblProjectDeadline;
        private System.Windows.Forms.Label lblMembersList;
        private System.Windows.Forms.ComboBox cboProjectStatus;
        private System.Windows.Forms.Button btnAddMember;
        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.DataGridView dgvTasks;
        private System.Windows.Forms.Label lblComments;
        private System.Windows.Forms.FlowLayoutPanel flowComments;
        private System.Windows.Forms.TextBox txtNewComment;
        private System.Windows.Forms.Button btnSendComment;

        private void InitializeComponent()
        {
            lblHeader = new Label();
            lblSubHeader = new Label();
            lblUserIdDisplay = new Label();
            btnBack = new Button();
            panelProjectInfo = new Panel();
            lblProjectTitle = new Label();
            lblProjectDescription = new Label();
            lblProjectDeadline = new Label();
            lblMembersList = new Label();
            cboProjectStatus = new ComboBox();
            btnAddMember = new Button();
            btnAddTask = new Button();
            dgvTasks = new DataGridView();
            lblComments = new Label();
            flowComments = new FlowLayoutPanel();
            txtNewComment = new TextBox();
            btnSendComment = new Button();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            panelProjectInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTasks).BeginInit();
            SuspendLayout();
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblHeader.Location = new Point(30, 20);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(229, 32);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "Task Scheduler Pro";
            // 
            // lblSubHeader
            // 
            lblSubHeader.AutoSize = true;
            lblSubHeader.Font = new Font("Segoe UI", 10F);
            lblSubHeader.ForeColor = Color.Gray;
            lblSubHeader.Location = new Point(30, 55);
            lblSubHeader.Name = "lblSubHeader";
            lblSubHeader.Size = new Size(232, 19);
            lblSubHeader.TabIndex = 1;
            lblSubHeader.Text = "Quản lý dự án và công việc hiệu quả";
            // 
            // lblUserIdDisplay
            // 
            lblUserIdDisplay.AutoSize = true;
            lblUserIdDisplay.Font = new Font("Segoe UI", 9F);
            lblUserIdDisplay.ForeColor = Color.Gray;
            lblUserIdDisplay.Location = new Point(1200, 30);
            lblUserIdDisplay.Name = "lblUserIdDisplay";
            lblUserIdDisplay.Size = new Size(59, 15);
            lblUserIdDisplay.TabIndex = 2;
            lblUserIdDisplay.Text = "User ID: ...";
            // 
            // btnBack
            // 
            btnBack.Cursor = Cursors.Hand;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 10F);
            btnBack.ForeColor = Color.FromArgb(88, 86, 214);
            btnBack.Location = new Point(30, 90);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(200, 30);
            btnBack.TabIndex = 3;
            btnBack.Text = "<- Quay lại Dashboard";
            btnBack.Click += btnBack_Click;
            // 
            // panelProjectInfo
            // 
            panelProjectInfo.BackColor = Color.White;
            panelProjectInfo.Controls.Add(lblProjectTitle);
            panelProjectInfo.Controls.Add(lblProjectDescription);
            panelProjectInfo.Controls.Add(lblProjectDeadline);
            panelProjectInfo.Controls.Add(lblMembersList);
            panelProjectInfo.Controls.Add(cboProjectStatus);
            panelProjectInfo.Controls.Add(btnAddMember);
            panelProjectInfo.Location = new Point(30, 140);
            panelProjectInfo.Name = "panelProjectInfo";
            panelProjectInfo.Size = new Size(1340, 130);
            panelProjectInfo.TabIndex = 4;
            // 
            // lblProjectTitle
            // 
            lblProjectTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblProjectTitle.Location = new Point(20, 20);
            lblProjectTitle.Name = "lblProjectTitle";
            lblProjectTitle.Size = new Size(800, 30);
            lblProjectTitle.TabIndex = 0;
            lblProjectTitle.Text = "Tên Dự Án";
            // 
            // lblProjectDescription
            // 
            lblProjectDescription.Font = new Font("Segoe UI", 10F);
            lblProjectDescription.ForeColor = Color.Gray;
            lblProjectDescription.Location = new Point(20, 55);
            lblProjectDescription.Name = "lblProjectDescription";
            lblProjectDescription.Size = new Size(1000, 20);
            lblProjectDescription.TabIndex = 1;
            lblProjectDescription.Text = "Mô tả dự án";
            // 
            // lblProjectDeadline
            // 
            lblProjectDeadline.AutoSize = true;
            lblProjectDeadline.Font = new Font("Segoe UI", 9F);
            lblProjectDeadline.Location = new Point(20, 85);
            lblProjectDeadline.Name = "lblProjectDeadline";
            lblProjectDeadline.Size = new Size(96, 15);
            lblProjectDeadline.TabIndex = 2;
            lblProjectDeadline.Text = "Ngày kết thúc: ...";
            // 
            // lblMembersList
            // 
            lblMembersList.Font = new Font("Segoe UI", 9F);
            lblMembersList.Location = new Point(200, 85);
            lblMembersList.Name = "lblMembersList";
            lblMembersList.Size = new Size(800, 20);
            lblMembersList.TabIndex = 3;
            lblMembersList.Text = "Thành viên: ...";
            // 
            // cboProjectStatus
            // 
            cboProjectStatus.BackColor = Color.FromArgb(52, 152, 219);
            cboProjectStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProjectStatus.Font = new Font("Segoe UI", 10F);
            cboProjectStatus.ForeColor = Color.White;
            cboProjectStatus.Items.AddRange(new object[] { "Planning", "Active", "In Progress", "On Hold", "Completed", "Cancelled" });
            cboProjectStatus.Location = new Point(1050, 20);
            cboProjectStatus.Name = "cboProjectStatus";
            cboProjectStatus.Size = new Size(130, 25);
            cboProjectStatus.TabIndex = 4;
            cboProjectStatus.SelectedIndexChanged += cboProjectStatus_SelectedIndexChanged;
            // 
            // btnAddMember
            // 
            btnAddMember.BackColor = Color.FromArgb(46, 204, 113);
            btnAddMember.Cursor = Cursors.Hand;
            btnAddMember.FlatAppearance.BorderSize = 0;
            btnAddMember.FlatStyle = FlatStyle.Flat;
            btnAddMember.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddMember.ForeColor = Color.White;
            btnAddMember.Location = new Point(1190, 20);
            btnAddMember.Name = "btnAddMember";
            btnAddMember.Size = new Size(130, 32);
            btnAddMember.TabIndex = 5;
            btnAddMember.Text = "+ Thêm Thành Viên";
            btnAddMember.UseVisualStyleBackColor = false;
            btnAddMember.Click += btnAddMember_Click;
            // 
            // btnAddTask
            // 
            btnAddTask.BackColor = Color.FromArgb(88, 86, 214);
            btnAddTask.Cursor = Cursors.Hand;
            btnAddTask.FlatAppearance.BorderSize = 0;
            btnAddTask.FlatStyle = FlatStyle.Flat;
            btnAddTask.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddTask.ForeColor = Color.White;
            btnAddTask.Location = new Point(1240, 285);
            btnAddTask.Name = "btnAddTask";
            btnAddTask.Size = new Size(130, 35);
            btnAddTask.TabIndex = 5;
            btnAddTask.Text = "+ Nhiệm Vụ Mới";
            btnAddTask.UseVisualStyleBackColor = false;
            btnAddTask.Click += btnAddTask_Click;
            // 
            // dgvTasks
            // 
            dgvTasks.AllowUserToAddRows = false;
            dgvTasks.AllowUserToDeleteRows = false;
            dgvTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTasks.BackgroundColor = Color.White;
            dgvTasks.BorderStyle = BorderStyle.None;
            dgvTasks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTasks.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn7 });
            dgvTasks.Location = new Point(30, 330);
            dgvTasks.Name = "dgvTasks";
            dgvTasks.ReadOnly = true;
            dgvTasks.RowHeadersVisible = false;
            dgvTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTasks.Size = new Size(1340, 300);
            dgvTasks.TabIndex = 6;
            // 
            // lblComments
            // 
            lblComments.AutoSize = true;
            lblComments.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblComments.Location = new Point(30, 650);
            lblComments.Name = "lblComments";
            lblComments.Size = new Size(138, 21);
            lblComments.TabIndex = 7;
            lblComments.Text = "Bình Luận Dự Án";
            // 
            // flowComments
            // 
            flowComments.AutoScroll = true;
            flowComments.BackColor = Color.FromArgb(250, 250, 250);
            flowComments.FlowDirection = FlowDirection.TopDown;
            flowComments.Location = new Point(30, 685);
            flowComments.Name = "flowComments";
            flowComments.Size = new Size(1340, 100);
            flowComments.TabIndex = 8;
            flowComments.WrapContents = false;
            // 
            // txtNewComment
            // 
            txtNewComment.Font = new Font("Segoe UI", 10F);
            txtNewComment.Location = new Point(30, 800);
            txtNewComment.Name = "txtNewComment";
            txtNewComment.PlaceholderText = "Thêm bình luận hoặc liên kết...";
            txtNewComment.Size = new Size(1260, 25);
            txtNewComment.TabIndex = 9;
            // 
            // btnSendComment
            // 
            btnSendComment.BackColor = Color.FromArgb(88, 86, 214);
            btnSendComment.Cursor = Cursors.Hand;
            btnSendComment.FlatAppearance.BorderSize = 0;
            btnSendComment.FlatStyle = FlatStyle.Flat;
            btnSendComment.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSendComment.ForeColor = Color.White;
            btnSendComment.Location = new Point(1300, 800);
            btnSendComment.Name = "btnSendComment";
            btnSendComment.Size = new Size(70, 30);
            btnSendComment.TabIndex = 10;
            btnSendComment.Text = "Bình Luận";
            btnSendComment.UseVisualStyleBackColor = false;
            btnSendComment.Click += btnSendComment_Click;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "TÊN TASK";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "TRẠNG THÁI";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "NGƯỢC ĐƯỢC GIAO";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "NGÀY KẾT THÚC";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "ƯU TIÊN";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.HeaderText = "MÔ TẢ";
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // ProjectView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(1400, 900);
            Controls.Add(lblHeader);
            Controls.Add(lblSubHeader);
            Controls.Add(lblUserIdDisplay);
            Controls.Add(btnBack);
            Controls.Add(panelProjectInfo);
            Controls.Add(btnAddTask);
            Controls.Add(dgvTasks);
            Controls.Add(lblComments);
            Controls.Add(flowComments);
            Controls.Add(txtNewComment);
            Controls.Add(btnSendComment);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "ProjectView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Chi Tiết Dự Án";
            panelProjectInfo.ResumeLayout(false);
            panelProjectInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTasks).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    }
}
