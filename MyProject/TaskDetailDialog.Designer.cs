namespace MyProject
{
    partial class TaskDetailDialog
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
            btnCloseTop = new Button();
            lblDescriptionTitle = new Label();
            lblDescription = new Label();
            lblDueDateTitle = new Label();
            lblDueDate = new Label();
            lblPriorityTitle = new Label();
            lblPriority = new Label();
            lblTrangThaiTitle = new Label();
            cboTrangThai = new ComboBox();
            lblNguoiGiaoTitle = new Label();
            pnlNguoiGiao = new Panel();
            btnAddMember = new Button();
            lblCommentsTitle = new Label();
            flowComments = new FlowLayoutPanel();
            txtNewComment = new TextBox();
            btnSendComment = new Button();
            SuspendLayout();
            // 
            // btnCloseTop
            // 
            btnCloseTop.BackColor = Color.White;
            btnCloseTop.Cursor = Cursors.Hand;
            btnCloseTop.FlatAppearance.BorderSize = 0;
            btnCloseTop.FlatStyle = FlatStyle.Flat;
            btnCloseTop.Font = new Font("Segoe UI", 12F);
            btnCloseTop.ForeColor = Color.Gray;
            btnCloseTop.Location = new Point(460, 5);
            btnCloseTop.Name = "btnCloseTop";
            btnCloseTop.Size = new Size(30, 30);
            btnCloseTop.TabIndex = 15;
            btnCloseTop.Text = "X";
            btnCloseTop.UseVisualStyleBackColor = false;
            btnCloseTop.Click += BtnCloseTop_Click;
            // 
            // lblDescriptionTitle
            // 
            lblDescriptionTitle.AutoSize = true;
            lblDescriptionTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDescriptionTitle.ForeColor = Color.Gray;
            lblDescriptionTitle.Location = new Point(20, 50);
            lblDescriptionTitle.Name = "lblDescriptionTitle";
            lblDescriptionTitle.Size = new Size(41, 15);
            lblDescriptionTitle.TabIndex = 0;
            lblDescriptionTitle.Text = "Mô Tả";
            // 
            // lblDescription
            // 
            lblDescription.Font = new Font("Segoe UI", 9F);
            lblDescription.ForeColor = Color.FromArgb(44, 62, 80);
            lblDescription.Location = new Point(20, 70);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(200, 60);
            lblDescription.TabIndex = 1;
            // 
            // lblDueDateTitle
            // 
            lblDueDateTitle.AutoSize = true;
            lblDueDateTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDueDateTitle.ForeColor = Color.Gray;
            lblDueDateTitle.Location = new Point(20, 145);
            lblDueDateTitle.Name = "lblDueDateTitle";
            lblDueDateTitle.Size = new Size(88, 15);
            lblDueDateTitle.TabIndex = 2;
            lblDueDateTitle.Text = "Ngày Kết Thúc";
            // 
            // lblDueDate
            // 
            lblDueDate.AutoSize = true;
            lblDueDate.Font = new Font("Segoe UI", 9F);
            lblDueDate.ForeColor = Color.FromArgb(231, 76, 60);
            lblDueDate.Location = new Point(20, 165);
            lblDueDate.Name = "lblDueDate";
            lblDueDate.Size = new Size(0, 15);
            lblDueDate.TabIndex = 3;
            // 
            // lblPriorityTitle
            // 
            lblPriorityTitle.AutoSize = true;
            lblPriorityTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPriorityTitle.ForeColor = Color.Gray;
            lblPriorityTitle.Location = new Point(20, 195);
            lblPriorityTitle.Name = "lblPriorityTitle";
            lblPriorityTitle.Size = new Size(97, 15);
            lblPriorityTitle.TabIndex = 4;
            lblPriorityTitle.Text = "Mức Độ Ưu Tiên";
            // 
            // lblPriority
            // 
            lblPriority.AutoSize = true;
            lblPriority.Font = new Font("Segoe UI", 9F);
            lblPriority.ForeColor = Color.FromArgb(44, 62, 80);
            lblPriority.Location = new Point(20, 215);
            lblPriority.Name = "lblPriority";
            lblPriority.Size = new Size(0, 15);
            lblPriority.TabIndex = 5;
            // 
            // lblTrangThaiTitle
            // 
            lblTrangThaiTitle.AutoSize = true;
            lblTrangThaiTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTrangThaiTitle.ForeColor = Color.Gray;
            lblTrangThaiTitle.Location = new Point(250, 50);
            lblTrangThaiTitle.Name = "lblTrangThaiTitle";
            lblTrangThaiTitle.Size = new Size(64, 15);
            lblTrangThaiTitle.TabIndex = 6;
            lblTrangThaiTitle.Text = "Trạng Thái";
            // 
            // cboTrangThai
            // 
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.Font = new Font("Segoe UI", 9F);
            cboTrangThai.Items.AddRange(new object[] { "Done", "In Progress", "To Do", "Backlog", "In Review", "Testing", "Blocked", "Cancelled" });
            cboTrangThai.Location = new Point(250, 70);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(220, 23);
            cboTrangThai.TabIndex = 7;
            cboTrangThai.SelectedIndexChanged += CboTrangThai_SelectedIndexChanged;
            // 
            // lblNguoiGiaoTitle
            // 
            lblNguoiGiaoTitle.AutoSize = true;
            lblNguoiGiaoTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNguoiGiaoTitle.ForeColor = Color.Gray;
            lblNguoiGiaoTitle.Location = new Point(250, 115);
            lblNguoiGiaoTitle.Name = "lblNguoiGiaoTitle";
            lblNguoiGiaoTitle.Size = new Size(104, 15);
            lblNguoiGiaoTitle.TabIndex = 8;
            lblNguoiGiaoTitle.Text = "Người Được Giao";
            // 
            // pnlNguoiGiao
            // 
            pnlNguoiGiao.AutoScroll = true;
            pnlNguoiGiao.BackColor = Color.FromArgb(250, 250, 250);
            pnlNguoiGiao.BorderStyle = BorderStyle.FixedSingle;
            pnlNguoiGiao.Location = new Point(250, 135);
            pnlNguoiGiao.Name = "pnlNguoiGiao";
            pnlNguoiGiao.Size = new Size(220, 120);
            pnlNguoiGiao.TabIndex = 9;
            // 
            // btnAddMember
            // 
            btnAddMember.BackColor = Color.FromArgb(88, 86, 214);
            btnAddMember.Cursor = Cursors.Hand;
            btnAddMember.FlatAppearance.BorderSize = 0;
            btnAddMember.FlatStyle = FlatStyle.Flat;
            btnAddMember.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddMember.ForeColor = Color.White;
            btnAddMember.Location = new Point(250, 265);
            btnAddMember.Name = "btnAddMember";
            btnAddMember.Size = new Size(140, 28);
            btnAddMember.TabIndex = 10;
            btnAddMember.Text = "+ Thêm Người Mới";
            btnAddMember.UseVisualStyleBackColor = false;
            btnAddMember.Click += BtnAddMember_Click;
            // 
            // lblCommentsTitle
            // 
            lblCommentsTitle.AutoSize = true;
            lblCommentsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCommentsTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblCommentsTitle.Location = new Point(20, 335);
            lblCommentsTitle.Name = "lblCommentsTitle";
            lblCommentsTitle.Size = new Size(106, 19);
            lblCommentsTitle.TabIndex = 11;
            lblCommentsTitle.Text = "Bình Luận Task";
            // 
            // flowComments
            // 
            flowComments.AutoScroll = true;
            flowComments.BackColor = Color.FromArgb(250, 250, 250);
            flowComments.BorderStyle = BorderStyle.FixedSingle;
            flowComments.FlowDirection = FlowDirection.TopDown;
            flowComments.Location = new Point(20, 357);
            flowComments.Name = "flowComments";
            flowComments.Padding = new Padding(5);
            flowComments.Size = new Size(450, 183);
            flowComments.TabIndex = 12;
            flowComments.WrapContents = false;
            // 
            // txtNewComment
            // 
            txtNewComment.Font = new Font("Segoe UI", 9F);
            txtNewComment.Location = new Point(20, 555);
            txtNewComment.Name = "txtNewComment";
            txtNewComment.PlaceholderText = "Thêm bình luận hoặc liên kết...";
            txtNewComment.Size = new Size(360, 23);
            txtNewComment.TabIndex = 13;
            // 
            // btnSendComment
            // 
            btnSendComment.BackColor = Color.FromArgb(88, 86, 214);
            btnSendComment.Cursor = Cursors.Hand;
            btnSendComment.FlatAppearance.BorderSize = 0;
            btnSendComment.FlatStyle = FlatStyle.Flat;
            btnSendComment.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSendComment.ForeColor = Color.White;
            btnSendComment.Location = new Point(390, 555);
            btnSendComment.Name = "btnSendComment";
            btnSendComment.Size = new Size(80, 25);
            btnSendComment.TabIndex = 14;
            btnSendComment.Text = "Gửi";
            btnSendComment.UseVisualStyleBackColor = false;
            btnSendComment.Click += BtnSendComment_Click;
            // 
            // TaskDetailDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(500, 630);
            Controls.Add(btnSendComment);
            Controls.Add(txtNewComment);
            Controls.Add(flowComments);
            Controls.Add(lblCommentsTitle);
            Controls.Add(btnAddMember);
            Controls.Add(pnlNguoiGiao);
            Controls.Add(lblNguoiGiaoTitle);
            Controls.Add(cboTrangThai);
            Controls.Add(lblTrangThaiTitle);
            Controls.Add(lblPriority);
            Controls.Add(lblPriorityTitle);
            Controls.Add(lblDueDate);
            Controls.Add(lblDueDateTitle);
            Controls.Add(lblDescription);
            Controls.Add(lblDescriptionTitle);
            Controls.Add(btnCloseTop);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TaskDetailDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Task Details";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblDescriptionTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblDueDateTitle;
        private System.Windows.Forms.Label lblDueDate;
        private System.Windows.Forms.Label lblPriorityTitle;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.Label lblTrangThaiTitle;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblNguoiGiaoTitle;
        private System.Windows.Forms.Panel pnlNguoiGiao;
        private System.Windows.Forms.Button btnAddMember;
        private System.Windows.Forms.Label lblCommentsTitle;
        private System.Windows.Forms.FlowLayoutPanel flowComments;
        private System.Windows.Forms.TextBox txtNewComment;
        private System.Windows.Forms.Button btnSendComment;
        private Button btnCloseTop;
    }
}

