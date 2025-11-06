namespace MyProject
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
            panelHeader = new Panel();
            lblUserName = new Label();
            lblWelcome = new Label();
            lblTitle = new Label();
            panelStats = new Panel();
            panelStat3 = new Panel();
            lblStat3Value = new Label();
            lblStat3Title = new Label();
            panelStat2 = new Panel();
            lblStat2Value = new Label();
            lblStat2Title = new Label();
            panelStat1 = new Panel();
            lblStat1Value = new Label();
            lblStat1Title = new Label();
            panelProjects = new Panel();
            btnAddProject = new Button();
            lblMyProjects = new Label();
            panelProjectsList = new Panel();
            panelTasks = new Panel();
            lblTasksTitle = new Label();
            panelTaskColumns = new Panel();
            panelDone = new Panel();
            lblDoneTitle = new Label();
            panelInProgress = new Panel();
            lblInProgressTitle = new Label();
            panelToDo = new Panel();
            lblToDoTitle = new Label();
            panelHeader.SuspendLayout();
            panelStats.SuspendLayout();
            panelStat3.SuspendLayout();
            panelStat2.SuspendLayout();
            panelStat1.SuspendLayout();
            panelProjects.SuspendLayout();
            panelTasks.SuspendLayout();
            panelTaskColumns.SuspendLayout();
            panelDone.SuspendLayout();
            panelInProgress.SuspendLayout();
            panelToDo.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.White;
            panelHeader.Controls.Add(lblUserName);
            panelHeader.Controls.Add(lblWelcome);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Padding = new Padding(30, 20, 30, 20);
            panelHeader.Size = new Size(1201, 80);
            panelHeader.TabIndex = 0;
            // 
            // lblUserName
            // 
            lblUserName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserName.BackColor = Color.FromArgb(88, 86, 214);
            lblUserName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblUserName.ForeColor = Color.White;
            lblUserName.Location = new Point(1091, 20);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(50, 50);
            lblUserName.TabIndex = 2;
            lblUserName.Text = "MU";
            lblUserName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblWelcome
            // 
            lblWelcome.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblWelcome.Font = new Font("Segoe UI", 10F);
            lblWelcome.ForeColor = Color.Gray;
            lblWelcome.Location = new Point(901, 30);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(180, 30);
            lblWelcome.TabIndex = 1;
            lblWelcome.Text = "Chào mừng, Mock User";
            lblWelcome.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(88, 86, 214);
            lblTitle.Location = new Point(30, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(346, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "TaskScheduler Dashboard";
            // 
            // panelStats
            // 
            panelStats.BackColor = Color.WhiteSmoke;
            panelStats.Controls.Add(panelStat3);
            panelStats.Controls.Add(panelStat2);
            panelStats.Controls.Add(panelStat1);
            panelStats.Dock = DockStyle.Top;
            panelStats.Location = new Point(0, 80);
            panelStats.Name = "panelStats";
            panelStats.Padding = new Padding(30, 30, 30, 20);
            panelStats.Size = new Size(1201, 150);
            panelStats.TabIndex = 1;
            // 
            // panelStat3
            // 
            panelStat3.BackColor = Color.White;
            panelStat3.Controls.Add(lblStat3Value);
            panelStat3.Controls.Add(lblStat3Title);
            panelStat3.Location = new Point(820, 30);
            panelStat3.Name = "panelStat3";
            panelStat3.Size = new Size(350, 100);
            panelStat3.TabIndex = 2;
            // 
            // lblStat3Value
            // 
            lblStat3Value.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblStat3Value.ForeColor = Color.FromArgb(46, 204, 113);
            lblStat3Value.Location = new Point(15, 40);
            lblStat3Value.Name = "lblStat3Value";
            lblStat3Value.Size = new Size(320, 50);
            lblStat3Value.TabIndex = 1;
            lblStat3Value.Text = "2";
            // 
            // lblStat3Title
            // 
            lblStat3Title.Font = new Font("Segoe UI", 11F);
            lblStat3Title.ForeColor = Color.Gray;
            lblStat3Title.Location = new Point(15, 10);
            lblStat3Title.Name = "lblStat3Title";
            lblStat3Title.Size = new Size(320, 25);
            lblStat3Title.TabIndex = 0;
            lblStat3Title.Text = "Nhiệm vụ Hoàn thành";
            // 
            // panelStat2
            // 
            panelStat2.BackColor = Color.White;
            panelStat2.Controls.Add(lblStat2Value);
            panelStat2.Controls.Add(lblStat2Title);
            panelStat2.Location = new Point(425, 30);
            panelStat2.Name = "panelStat2";
            panelStat2.Size = new Size(350, 100);
            panelStat2.TabIndex = 1;
            // 
            // lblStat2Value
            // 
            lblStat2Value.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblStat2Value.ForeColor = Color.FromArgb(241, 196, 15);
            lblStat2Value.Location = new Point(15, 40);
            lblStat2Value.Name = "lblStat2Value";
            lblStat2Value.Size = new Size(320, 50);
            lblStat2Value.TabIndex = 1;
            lblStat2Value.Text = "2";
            // 
            // lblStat2Title
            // 
            lblStat2Title.Font = new Font("Segoe UI", 11F);
            lblStat2Title.ForeColor = Color.Gray;
            lblStat2Title.Location = new Point(15, 10);
            lblStat2Title.Name = "lblStat2Title";
            lblStat2Title.Size = new Size(320, 25);
            lblStat2Title.TabIndex = 0;
            lblStat2Title.Text = "Nhiệm vụ Chờ làm";
            // 
            // panelStat1
            // 
            panelStat1.BackColor = Color.White;
            panelStat1.Controls.Add(lblStat1Value);
            panelStat1.Controls.Add(lblStat1Title);
            panelStat1.Location = new Point(30, 30);
            panelStat1.Name = "panelStat1";
            panelStat1.Size = new Size(350, 100);
            panelStat1.TabIndex = 0;
            // 
            // lblStat1Value
            // 
            lblStat1Value.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblStat1Value.ForeColor = Color.FromArgb(52, 152, 219);
            lblStat1Value.Location = new Point(15, 40);
            lblStat1Value.Name = "lblStat1Value";
            lblStat1Value.Size = new Size(320, 50);
            lblStat1Value.TabIndex = 1;
            lblStat1Value.Text = "3";
            // 
            // lblStat1Title
            // 
            lblStat1Title.Font = new Font("Segoe UI", 11F);
            lblStat1Title.ForeColor = Color.Gray;
            lblStat1Title.Location = new Point(15, 10);
            lblStat1Title.Name = "lblStat1Title";
            lblStat1Title.Size = new Size(320, 25);
            lblStat1Title.TabIndex = 0;
            lblStat1Title.Text = "Tổng Dự án";
            // 
            // panelProjects
            // 
            panelProjects.BackColor = Color.WhiteSmoke;
            panelProjects.Controls.Add(btnAddProject);
            panelProjects.Controls.Add(lblMyProjects);
            panelProjects.Controls.Add(panelProjectsList);
            panelProjects.Dock = DockStyle.Top;
            panelProjects.Location = new Point(0, 230);
            panelProjects.Name = "panelProjects";
            panelProjects.Padding = new Padding(30, 20, 30, 20);
            panelProjects.Size = new Size(1201, 405);
            panelProjects.TabIndex = 2;
            // 
            // btnAddProject
            // 
            btnAddProject.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddProject.BackColor = Color.FromArgb(88, 86, 214);
            btnAddProject.FlatAppearance.BorderSize = 0;
            btnAddProject.FlatStyle = FlatStyle.Flat;
            btnAddProject.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddProject.ForeColor = Color.White;
            btnAddProject.Location = new Point(1041, 20);
            btnAddProject.Name = "btnAddProject";
            btnAddProject.Size = new Size(130, 40);
            btnAddProject.TabIndex = 2;
            btnAddProject.Text = "+ Dự Án Mới";
            btnAddProject.UseVisualStyleBackColor = false;
            btnAddProject.Click += btnAddProject_Click;
            // 
            // lblMyProjects
            // 
            lblMyProjects.AutoSize = true;
            lblMyProjects.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblMyProjects.ForeColor = Color.FromArgb(44, 62, 80);
            lblMyProjects.Location = new Point(30, 20);
            lblMyProjects.Name = "lblMyProjects";
            lblMyProjects.Size = new Size(159, 30);
            lblMyProjects.TabIndex = 1;
            lblMyProjects.Text = "Dự Án Của Tôi";
            // 
            // panelProjectsList
            // 
            panelProjectsList.AutoScroll = true;
            panelProjectsList.BackColor = Color.White;
            panelProjectsList.Dock = DockStyle.Bottom;
            panelProjectsList.Location = new Point(30, 66);
            panelProjectsList.Name = "panelProjectsList";
            panelProjectsList.Padding = new Padding(10);
            panelProjectsList.Size = new Size(1141, 319);
            panelProjectsList.TabIndex = 0;
            // 
            // panelTasks
            // 
            panelTasks.BackColor = Color.WhiteSmoke;
            panelTasks.Controls.Add(lblTasksTitle);
            panelTasks.Controls.Add(panelTaskColumns);
            panelTasks.Dock = DockStyle.Fill;
            panelTasks.Location = new Point(0, 635);
            panelTasks.Name = "panelTasks";
            panelTasks.Padding = new Padding(30, 20, 30, 30);
            panelTasks.Size = new Size(1201, 352);
            panelTasks.TabIndex = 3;
            // 
            // lblTasksTitle
            // 
            lblTasksTitle.AutoSize = true;
            lblTasksTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTasksTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTasksTitle.Location = new Point(30, 20);
            lblTasksTitle.Name = "lblTasksTitle";
            lblTasksTitle.Size = new Size(208, 30);
            lblTasksTitle.TabIndex = 1;
            lblTasksTitle.Text = "Nhiệm Vụ Gần Đây";
            // 
            // panelTaskColumns
            // 
            panelTaskColumns.Controls.Add(panelDone);
            panelTaskColumns.Controls.Add(panelInProgress);
            panelTaskColumns.Controls.Add(panelToDo);
            panelTaskColumns.Dock = DockStyle.Bottom;
            panelTaskColumns.Location = new Point(30, 72);
            panelTaskColumns.Name = "panelTaskColumns";
            panelTaskColumns.Size = new Size(1141, 250);
            panelTaskColumns.TabIndex = 0;
            // 
            // panelDone
            // 
            panelDone.BackColor = Color.White;
            panelDone.Controls.Add(lblDoneTitle);
            panelDone.Location = new Point(780, 0);
            panelDone.Name = "panelDone";
            panelDone.Padding = new Padding(15);
            panelDone.Size = new Size(360, 250);
            panelDone.TabIndex = 2;
            // 
            // lblDoneTitle
            // 
            lblDoneTitle.Dock = DockStyle.Top;
            lblDoneTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblDoneTitle.ForeColor = Color.FromArgb(46, 204, 113);
            lblDoneTitle.Location = new Point(15, 15);
            lblDoneTitle.Name = "lblDoneTitle";
            lblDoneTitle.Size = new Size(330, 30);
            lblDoneTitle.TabIndex = 0;
            lblDoneTitle.Text = "Hoàn Thành (Done)";
            // 
            // panelInProgress
            // 
            panelInProgress.BackColor = Color.White;
            panelInProgress.Controls.Add(lblInProgressTitle);
            panelInProgress.Location = new Point(390, 0);
            panelInProgress.Name = "panelInProgress";
            panelInProgress.Padding = new Padding(15);
            panelInProgress.Size = new Size(360, 250);
            panelInProgress.TabIndex = 1;
            // 
            // lblInProgressTitle
            // 
            lblInProgressTitle.Dock = DockStyle.Top;
            lblInProgressTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblInProgressTitle.ForeColor = Color.FromArgb(241, 196, 15);
            lblInProgressTitle.Location = new Point(15, 15);
            lblInProgressTitle.Name = "lblInProgressTitle";
            lblInProgressTitle.Size = new Size(330, 30);
            lblInProgressTitle.TabIndex = 0;
            lblInProgressTitle.Text = "Đang Tiến Hành (In Progress)";
            // 
            // panelToDo
            // 
            panelToDo.BackColor = Color.White;
            panelToDo.Controls.Add(lblToDoTitle);
            panelToDo.Location = new Point(0, 0);
            panelToDo.Name = "panelToDo";
            panelToDo.Padding = new Padding(15);
            panelToDo.Size = new Size(360, 250);
            panelToDo.TabIndex = 0;
            // 
            // lblToDoTitle
            // 
            lblToDoTitle.Dock = DockStyle.Top;
            lblToDoTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblToDoTitle.ForeColor = Color.FromArgb(231, 76, 60);
            lblToDoTitle.Location = new Point(15, 15);
            lblToDoTitle.Name = "lblToDoTitle";
            lblToDoTitle.Size = new Size(330, 30);
            lblToDoTitle.TabIndex = 0;
            lblToDoTitle.Text = "Chờ Làm (To Do)";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1201, 987);
            Controls.Add(panelTasks);
            Controls.Add(panelProjects);
            Controls.Add(panelStats);
            Controls.Add(panelHeader);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TaskScheduler Dashboard";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelStats.ResumeLayout(false);
            panelStat3.ResumeLayout(false);
            panelStat2.ResumeLayout(false);
            panelStat1.ResumeLayout(false);
            panelProjects.ResumeLayout(false);
            panelProjects.PerformLayout();
            panelTasks.ResumeLayout(false);
            panelTasks.PerformLayout();
            panelTaskColumns.ResumeLayout(false);
            panelDone.ResumeLayout(false);
            panelInProgress.ResumeLayout(false);
            panelToDo.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Panel panelStat1;
        private System.Windows.Forms.Label lblStat1Title;
        private System.Windows.Forms.Label lblStat1Value;
        private System.Windows.Forms.Panel panelStat2;
        private System.Windows.Forms.Label lblStat2Value;
        private System.Windows.Forms.Label lblStat2Title;
        private System.Windows.Forms.Panel panelStat3;
        private System.Windows.Forms.Label lblStat3Value;
        private System.Windows.Forms.Label lblStat3Title;
        private System.Windows.Forms.Panel panelProjects;
        private System.Windows.Forms.Label lblMyProjects;
        private System.Windows.Forms.Panel panelProjectsList;
        private System.Windows.Forms.Button btnAddProject;
        private System.Windows.Forms.Panel panelTasks;
        private System.Windows.Forms.Label lblTasksTitle;
        private System.Windows.Forms.Panel panelTaskColumns;
        private System.Windows.Forms.Panel panelToDo;
        private System.Windows.Forms.Label lblToDoTitle;
        private System.Windows.Forms.Panel panelInProgress;
        private System.Windows.Forms.Label lblInProgressTitle;
        private System.Windows.Forms.Panel panelDone;
        private System.Windows.Forms.Label lblDoneTitle;
    }
}