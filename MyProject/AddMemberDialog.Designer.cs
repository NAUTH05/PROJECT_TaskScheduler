namespace MyProject
{
    partial class AddMemberDialog
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
            lblTitle = new Label();
            lblAvailableTitle = new Label();
            lblCurrentMembersTitle = new Label();
            flowCurrentMembers = new FlowLayoutPanel();
            txtSearch = new TextBox();
            lblInstruction = new Label();
            flowUsers = new FlowLayoutPanel();
            btnClose = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Location = new Point(30, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(289, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Quản Lý Thành Viên Dự Án";
            // 
            // lblAvailableTitle
            // 
            lblAvailableTitle.AutoSize = true;
            lblAvailableTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblAvailableTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblAvailableTitle.Location = new Point(30, 310);
            lblAvailableTitle.Name = "lblAvailableTitle";
            lblAvailableTitle.Size = new Size(178, 21);
            lblAvailableTitle.TabIndex = 3;
            lblAvailableTitle.Text = "Thêm Thành Viên Mới";
            // 
            // lblCurrentMembersTitle
            // 
            lblCurrentMembersTitle.AutoSize = true;
            lblCurrentMembersTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCurrentMembersTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblCurrentMembersTitle.Location = new Point(30, 75);
            lblCurrentMembersTitle.Name = "lblCurrentMembersTitle";
            lblCurrentMembersTitle.Size = new Size(164, 21);
            lblCurrentMembersTitle.TabIndex = 1;
            lblCurrentMembersTitle.Text = "Thành Viên Hiện Tại";
            // 
            // flowCurrentMembers
            // 
            flowCurrentMembers.AutoScroll = true;
            flowCurrentMembers.BackColor = Color.FromArgb(250, 250, 250);
            flowCurrentMembers.FlowDirection = FlowDirection.TopDown;
            flowCurrentMembers.Location = new Point(30, 110);
            flowCurrentMembers.Name = "flowCurrentMembers";
            flowCurrentMembers.Padding = new Padding(10);
            flowCurrentMembers.Size = new Size(501, 180);
            flowCurrentMembers.TabIndex = 2;
            flowCurrentMembers.WrapContents = false;
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 11F);
            txtSearch.Location = new Point(30, 345);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Tìm kiếm người dùng";
            txtSearch.Size = new Size(501, 27);
            txtSearch.TabIndex = 4;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            // 
            // lblInstruction
            // 
            lblInstruction.Font = new Font("Segoe UI", 9F);
            lblInstruction.ForeColor = Color.Gray;
            lblInstruction.Location = new Point(30, 385);
            lblInstruction.Name = "lblInstruction";
            lblInstruction.Size = new Size(380, 20);
            lblInstruction.TabIndex = 5;
            lblInstruction.Text = "Chọn người dùng để thêm vào dự án này";
            // 
            // flowUsers
            // 
            flowUsers.AutoScroll = true;
            flowUsers.BackColor = Color.FromArgb(250, 250, 250);
            flowUsers.FlowDirection = FlowDirection.TopDown;
            flowUsers.Location = new Point(30, 415);
            flowUsers.Name = "flowUsers";
            flowUsers.Padding = new Padding(10);
            flowUsers.Size = new Size(501, 180);
            flowUsers.TabIndex = 6;
            flowUsers.WrapContents = false;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(149, 165, 166);
            btnClose.Cursor = Cursors.Hand;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(451, 603);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(80, 35);
            btnClose.TabIndex = 7;
            btnClose.Text = "Đóng";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += BtnClose_Click;
            // 
            // AddMemberDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(552, 650);
            Controls.Add(btnClose);
            Controls.Add(flowUsers);
            Controls.Add(lblInstruction);
            Controls.Add(txtSearch);
            Controls.Add(lblAvailableTitle);
            Controls.Add(flowCurrentMembers);
            Controls.Add(lblCurrentMembersTitle);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddMemberDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Quản Lý Thành Viên Dự Án";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        // Designer-owned control fields
        private System.Windows.Forms.Label lblCurrentMembersTitle;
        private System.Windows.Forms.FlowLayoutPanel flowCurrentMembers;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.FlowLayoutPanel flowUsers;
        private System.Windows.Forms.Button btnClose;

        private void BtnClose_Click(object? sender, System.EventArgs e)
        {
            this.Close();
        }
        private Label lblTitle;
        private Label lblAvailableTitle;
    }
}
