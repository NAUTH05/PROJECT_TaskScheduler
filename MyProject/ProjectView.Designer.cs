namespace MyProject
{
    partial class ProjectView
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
            panelHeader = new Panel();
            lblUserAvatar = new Label();
            lblUserName = new Label();
            lblDashboardTitle = new Label();
            btnBack = new Button();
            panelProjectInfo = new Panel();
            cboProjectStatus = new ComboBox();
            lblProjectName = new Label();
            lblProjectDeadline = new Label();
            lblProjectDescription = new Label();
            lblTasksHeader = new Label();
            btnAddTask = new Button();
            panelToDo = new Panel();
            lblToDo = new Label();
            flowToDoTasks = new FlowLayoutPanel();
            panelInProgress = new Panel();
            lblInProgress = new Label();
            flowInProgressTasks = new FlowLayoutPanel();
            panelDone = new Panel();
            lblDone = new Label();
            flowDoneTasks = new FlowLayoutPanel();
            panelHeader.SuspendLayout();
            panelProjectInfo.SuspendLayout();
            panelToDo.SuspendLayout();
            panelInProgress.SuspendLayout();
            panelDone.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.White;
            panelHeader.Controls.Add(lblUserAvatar);
            panelHeader.Controls.Add(lblUserName);
            panelHeader.Controls.Add(lblDashboardTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1200, 80);
            panelHeader.TabIndex = 0;
            // 
            // lblUserAvatar
            // 
            lblUserAvatar.BackColor = Color.FromArgb(88, 86, 214);
            lblUserAvatar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblUserAvatar.ForeColor = Color.White;
            lblUserAvatar.Location = new Point(1130, 20);
            lblUserAvatar.Name = "lblUserAvatar";
            lblUserAvatar.Size = new Size(50, 50);
            lblUserAvatar.TabIndex = 2;
            lblUserAvatar.Text = "MU";
            lblUserAvatar.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Segoe UI", 10F);
            lblUserName.ForeColor = Color.Gray;
            lblUserName.Location = new Point(980, 35);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(154, 19);
            lblUserName.TabIndex = 1;
            lblUserName.Text = "Chào mừng, Mock User";
            // 
            // lblDashboardTitle
            // 
            lblDashboardTitle.AutoSize = true;
            lblDashboardTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblDashboardTitle.ForeColor = Color.FromArgb(88, 86, 214);
            lblDashboardTitle.Location = new Point(30, 25);
            lblDashboardTitle.Name = "lblDashboardTitle";
            lblDashboardTitle.Size = new Size(346, 37);
            lblDashboardTitle.TabIndex = 0;
            lblDashboardTitle.Text = "TaskScheduler Dashboard";
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.Transparent;
            btnBack.Cursor = Cursors.Hand;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 11F);
            btnBack.ForeColor = Color.FromArgb(88, 86, 214);
            btnBack.Location = new Point(30, 100);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(200, 35);
            btnBack.TabIndex = 1;
            btnBack.Text = "← Quay lại Dashboard";
            btnBack.TextAlign = ContentAlignment.MiddleLeft;
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // panelProjectInfo
            // 
            panelProjectInfo.BackColor = Color.White;
            panelProjectInfo.Controls.Add(cboProjectStatus);
            panelProjectInfo.Controls.Add(lblProjectName);
            panelProjectInfo.Controls.Add(lblProjectDeadline);
            panelProjectInfo.Controls.Add(lblProjectDescription);
            panelProjectInfo.Location = new Point(40, 155);
            panelProjectInfo.Name = "panelProjectInfo";
            panelProjectInfo.Size = new Size(1120, 180);
            panelProjectInfo.TabIndex = 2;
            // 
            // cboProjectStatus
            // 
            cboProjectStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProjectStatus.Font = new Font("Segoe UI", 10F);
            cboProjectStatus.FormattingEnabled = true;
            cboProjectStatus.Items.AddRange(new object[] { "To Do", "In Progress", "Completed" });
            cboProjectStatus.Location = new Point(955, 25);
            cboProjectStatus.Name = "cboProjectStatus";
            cboProjectStatus.Size = new Size(140, 25);
            cboProjectStatus.TabIndex = 3;
            cboProjectStatus.SelectedIndexChanged += cboProjectStatus_SelectedIndexChanged;
            // 
            // lblProjectName
            // 
            lblProjectName.AutoSize = true;
            lblProjectName.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblProjectName.Location = new Point(25, 20);
            lblProjectName.Name = "lblProjectName";
            lblProjectName.Size = new Size(397, 32);
            lblProjectName.TabIndex = 0;
            lblProjectName.Text = "Phát triển TaskScheduler Backend";
            // 
            // lblProjectDeadline
            // 
            lblProjectDeadline.AutoSize = true;
            lblProjectDeadline.Font = new Font("Segoe UI", 10F);
            lblProjectDeadline.ForeColor = Color.Gray;
            lblProjectDeadline.Location = new Point(25, 65);
            lblProjectDeadline.Name = "lblProjectDeadline";
            lblProjectDeadline.Size = new Size(225, 19);
            lblProjectDeadline.TabIndex = 1;
            lblProjectDeadline.Text = "Ngày kết thúc dự kiến: 30/11/2025";
            // 
            // lblProjectDescription
            // 
            lblProjectDescription.Font = new Font("Segoe UI", 10F);
            lblProjectDescription.Location = new Point(25, 100);
            lblProjectDescription.Name = "lblProjectDescription";
            lblProjectDescription.Size = new Size(1070, 60);
            lblProjectDescription.TabIndex = 2;
            lblProjectDescription.Text = "Thiết lập API và Database, đảm bảo tính năng xác thực và lưu trữ dữ liệu an toàn.";
            // 
            // lblTasksHeader
            // 
            lblTasksHeader.AutoSize = true;
            lblTasksHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTasksHeader.Location = new Point(35, 360);
            lblTasksHeader.Name = "lblTasksHeader";
            lblTasksHeader.Size = new Size(331, 25);
            lblTasksHeader.TabIndex = 3;
            lblTasksHeader.Text = "Danh Sách Nhiệm Vụ (Project Tasks)";
            // 
            // btnAddTask
            // 
            btnAddTask.BackColor = Color.FromArgb(46, 204, 113);
            btnAddTask.Cursor = Cursors.Hand;
            btnAddTask.FlatAppearance.BorderSize = 0;
            btnAddTask.FlatStyle = FlatStyle.Flat;
            btnAddTask.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddTask.ForeColor = Color.White;
            btnAddTask.Location = new Point(1015, 355);
            btnAddTask.Name = "btnAddTask";
            btnAddTask.Size = new Size(145, 35);
            btnAddTask.TabIndex = 4;
            btnAddTask.Text = "+ Nhiệm Vụ Mới";
            btnAddTask.UseVisualStyleBackColor = false;
            btnAddTask.Click += btnAddTask_Click;
            // 
            // panelToDo
            // 
            panelToDo.BackColor = Color.FromArgb(255, 240, 240);
            panelToDo.Controls.Add(lblToDo);
            panelToDo.Controls.Add(flowToDoTasks);
            panelToDo.Location = new Point(40, 410);
            panelToDo.Name = "panelToDo";
            panelToDo.Size = new Size(360, 360);
            panelToDo.TabIndex = 5;
            // 
            // lblToDo
            // 
            lblToDo.BackColor = Color.Transparent;
            lblToDo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblToDo.ForeColor = Color.FromArgb(231, 76, 60);
            lblToDo.Location = new Point(15, 15);
            lblToDo.Name = "lblToDo";
            lblToDo.Size = new Size(330, 25);
            lblToDo.TabIndex = 0;
            lblToDo.Text = "Chờ Làm (To Do)";
            // 
            // flowToDoTasks
            // 
            flowToDoTasks.AutoScroll = true;
            flowToDoTasks.FlowDirection = FlowDirection.TopDown;
            flowToDoTasks.Location = new Point(15, 50);
            flowToDoTasks.Name = "flowToDoTasks";
            flowToDoTasks.Size = new Size(330, 295);
            flowToDoTasks.TabIndex = 1;
            flowToDoTasks.WrapContents = false;
            // 
            // panelInProgress
            // 
            panelInProgress.BackColor = Color.FromArgb(255, 250, 220);
            panelInProgress.Controls.Add(lblInProgress);
            panelInProgress.Controls.Add(flowInProgressTasks);
            panelInProgress.Location = new Point(420, 410);
            panelInProgress.Name = "panelInProgress";
            panelInProgress.Size = new Size(360, 360);
            panelInProgress.TabIndex = 6;
            // 
            // lblInProgress
            // 
            lblInProgress.BackColor = Color.Transparent;
            lblInProgress.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblInProgress.ForeColor = Color.FromArgb(241, 196, 15);
            lblInProgress.Location = new Point(15, 15);
            lblInProgress.Name = "lblInProgress";
            lblInProgress.Size = new Size(330, 25);
            lblInProgress.TabIndex = 0;
            lblInProgress.Text = "Đang Tiến Hành (In Progress)";
            // 
            // flowInProgressTasks
            // 
            flowInProgressTasks.AutoScroll = true;
            flowInProgressTasks.FlowDirection = FlowDirection.TopDown;
            flowInProgressTasks.Location = new Point(15, 50);
            flowInProgressTasks.Name = "flowInProgressTasks";
            flowInProgressTasks.Size = new Size(330, 295);
            flowInProgressTasks.TabIndex = 1;
            flowInProgressTasks.WrapContents = false;
            // 
            // panelDone
            // 
            panelDone.BackColor = Color.FromArgb(230, 255, 240);
            panelDone.Controls.Add(lblDone);
            panelDone.Controls.Add(flowDoneTasks);
            panelDone.Location = new Point(800, 410);
            panelDone.Name = "panelDone";
            panelDone.Size = new Size(360, 360);
            panelDone.TabIndex = 7;
            // 
            // lblDone
            // 
            lblDone.BackColor = Color.Transparent;
            lblDone.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblDone.ForeColor = Color.FromArgb(46, 204, 113);
            lblDone.Location = new Point(15, 15);
            lblDone.Name = "lblDone";
            lblDone.Size = new Size(330, 25);
            lblDone.TabIndex = 0;
            lblDone.Text = "Hoàn Thành (Done)";
            // 
            // flowDoneTasks
            // 
            flowDoneTasks.AutoScroll = true;
            flowDoneTasks.FlowDirection = FlowDirection.TopDown;
            flowDoneTasks.Location = new Point(15, 50);
            flowDoneTasks.Name = "flowDoneTasks";
            flowDoneTasks.Size = new Size(330, 295);
            flowDoneTasks.TabIndex = 1;
            flowDoneTasks.WrapContents = false;
            // 
            // ProjectView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(1200, 800);
            Controls.Add(panelDone);
            Controls.Add(panelInProgress);
            Controls.Add(panelToDo);
            Controls.Add(btnAddTask);
            Controls.Add(lblTasksHeader);
            Controls.Add(panelProjectInfo);
            Controls.Add(btnBack);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "ProjectView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Chi Tiết Dự Án";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelProjectInfo.ResumeLayout(false);
            panelProjectInfo.PerformLayout();
            panelToDo.ResumeLayout(false);
            panelInProgress.ResumeLayout(false);
            panelDone.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblUserAvatar;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblDashboardTitle;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel panelProjectInfo;
        private System.Windows.Forms.ComboBox cboProjectStatus;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.Label lblProjectDeadline;
        private System.Windows.Forms.Label lblProjectDescription;
        private System.Windows.Forms.Label lblTasksHeader;
        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.Panel panelToDo;
        private System.Windows.Forms.Label lblToDo;
        private System.Windows.Forms.FlowLayoutPanel flowToDoTasks;
        private System.Windows.Forms.Panel panelInProgress;
        private System.Windows.Forms.Label lblInProgress;
        private System.Windows.Forms.FlowLayoutPanel flowInProgressTasks;
        private System.Windows.Forms.Panel panelDone;
        private System.Windows.Forms.Label lblDone;
        private System.Windows.Forms.FlowLayoutPanel flowDoneTasks;
    }
}