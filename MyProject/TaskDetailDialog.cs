using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

namespace MyProject
{
    public partial class TaskDetailDialog : Form
    {
        private string taskId;
        private string projectId;
        
        private bool isLoadingData = true; // Prevent auto-update on load
        
        // Left column controls
        private Label? lblDescriptionTitle;
        private Label? lblDescription;
        private Label? lblDueDateTitle;
        private Label? lblDueDate;
        private Label? lblPriorityTitle;
        private Label? lblPriority;
        
        // Right column controls
        private Label? lblTrangThaiTitle;
        private ComboBox? cboTrangThai;
        private Label? lblNguoiGiaoTitle;
        private Panel? pnlNguoiGiao; // Container for assigned member chip
        private Button? btnAddMember;
        
        // Comments section
        private Label? lblCommentsTitle;
        private FlowLayoutPanel? flowComments;
        private TextBox? txtNewComment;
        private Button? btnSendComment;
        private Button? btnClose;

        private List<ProjectMember> projectMembers = new List<ProjectMember>();
        private ProjectView.TaskItem currentTask;

        public bool TaskUpdated { get; private set; }

        public TaskDetailDialog(ProjectView.TaskItem task, string projectId)
        {
            this.taskId = task.TaskID;
            this.projectId = projectId;
            this.currentTask = task;
            
            InitializeComponent();
            InitializeData(); // Change to async initialization
        }

        private async void InitializeData()
        {
            LoadProjectMembers();
            await LoadTaskDetailsFromApiAsync(); // Make async
            LoadTaskComments();
            
            isLoadingData = false; // Set false AFTER all data loaded
        }

        private async Task LoadTaskDetailsFromApiAsync()
        {
            try
            {
                var response = await ApiHelper.GetAsync($"tasks/{taskId}");
                if (!response.IsSuccessStatusCode)
                {
                    LoadTaskData(currentTask); // Fallback to passed task
                    return;
                }

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<TaskDetailResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Data != null)
                {
                    // Update currentTask with fresh data
                    currentTask.TaskName = result.Data.TaskName;
                    currentTask.TaskDescription = result.Data.TaskDescription;
                    currentTask.DueDate = result.Data.DueDate;
                    currentTask.Priority = result.Data.Priority;
                    currentTask.Status = result.Data.Status;
                    // currentTask.TaskType = result.Data.TaskType;
                    
                    // Get UserName from AssignedUserDetails
                    currentTask.AssignedToUserName = result.Data.AssignedUserDetails?.UserName;
                    
                    LoadTaskData(currentTask);
                }
                else
                {
                    LoadTaskData(currentTask);
                }
            }
            catch
            {
                LoadTaskData(currentTask); // Fallback on error
            }
        }

        private void InitializeComponent()
        {
            this.Size = new Size(500, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            // Close button
            var btnCloseTop = new Button
            {
                Text = "×",
                Size = new Size(30, 30),
                Location = new Point(460, 5),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.Gray,
                Cursor = Cursors.Hand,
                BackColor = Color.White
            };
            btnCloseTop.FlatAppearance.BorderSize = 0;
            btnCloseTop.Click += (s, e) => this.Close();

            // Left Column - Task Info
            lblDescriptionTitle = new Label
            {
                Text = "Mô Tả",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(20, 50),
                AutoSize = true,
                ForeColor = Color.Gray
            };

            lblDescription = new Label
            {
                Location = new Point(20, 70),
                Size = new Size(200, 60),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(44, 62, 80)
            };

            lblDueDateTitle = new Label
            {
                Text = "Ngày kết thúc",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(20, 145),
                AutoSize = true,
                ForeColor = Color.Gray
            };

            lblDueDate = new Label
            {
                Location = new Point(20, 165),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(231, 76, 60)
            };

            lblPriorityTitle = new Label
            {
                Text = "Độ ưu tiên",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(20, 195),
                AutoSize = true,
                ForeColor = Color.Gray
            };

            lblPriority = new Label
            {
                Location = new Point(20, 215),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(44, 62, 80)
            };

            // Right Column - Status & Assignment
            lblTrangThaiTitle = new Label
            {
                Text = "Trạng Thái",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(250, 50),
                AutoSize = true,
                ForeColor = Color.Gray
            };

            cboTrangThai = new ComboBox
            {
                Location = new Point(250, 70),
                Size = new Size(220, 28),
                Font = new Font("Segoe UI", 9F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboTrangThai.Items.AddRange(new object[] { 
                "Done", "In Progress", "To Do", "Backlog", 
                "In Review", "Testing", "Blocked", "Cancelled" 
            });
            cboTrangThai.SelectedIndexChanged += CboTrangThai_SelectedIndexChanged;

            lblNguoiGiaoTitle = new Label
            {
                Text = "Người được giao",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(250, 115),
                AutoSize = true,
                ForeColor = Color.Gray
            };

            pnlNguoiGiao = new Panel
            {
                Location = new Point(250, 135),
                Size = new Size(220, 40),
                BackColor = Color.FromArgb(250, 250, 250),
                BorderStyle = BorderStyle.FixedSingle
            };

            btnAddMember = new Button
            {
                Text = "+ Thêm Người Mới",
                Size = new Size(140, 28),
                Location = new Point(250, 185),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(88, 86, 214),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnAddMember.FlatAppearance.BorderSize = 0;
            btnAddMember.Click += BtnAddMember_Click;

            // Comments Section
            lblCommentsTitle = new Label
            {
                Text = "Bình Luận Task",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(20, 310),
                AutoSize = true,
                ForeColor = Color.FromArgb(44, 62, 80)
            };

            flowComments = new FlowLayoutPanel
            {
                Location = new Point(20, 340),
                Size = new Size(450, 120),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.FromArgb(250, 250, 250),
                Padding = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle
            };

            txtNewComment = new TextBox
            {
                Location = new Point(20, 475),
                Size = new Size(360, 25),
                Font = new Font("Segoe UI", 9F),
                PlaceholderText = "Thêm bình luận hoặc liên kết..."
            };

            btnSendComment = new Button
            {
                Text = "Gửi",
                Size = new Size(80, 25),
                Location = new Point(390, 475),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(88, 86, 214),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };
            btnSendComment.FlatAppearance.BorderSize = 0;
            btnSendComment.Click += BtnSendComment_Click;

            this.Controls.AddRange(new Control[] {
                btnCloseTop, 
                lblDescriptionTitle, lblDescription,
                lblDueDateTitle, lblDueDate, 
                lblPriorityTitle, lblPriority,
                lblTrangThaiTitle, cboTrangThai,
                lblNguoiGiaoTitle, pnlNguoiGiao, btnAddMember,
                lblCommentsTitle, flowComments,
                txtNewComment, btnSendComment
            });
        }

        private async void CboTrangThai_SelectedIndexChanged(object? sender, EventArgs e)
        {
            await UpdateTaskStatus();
        }

        private async void BtnSendComment_Click(object? sender, EventArgs e)
        {
            await SendComment();
        }

        private void LoadTaskData(ProjectView.TaskItem task)
        {
            this.Text = task.TaskName;
            lblDescription!.Text = task.TaskDescription ?? "Phác thảo các bảng và mối quan hệ.";
            
            DateTime.TryParse(task.DueDate, out var dueDate);
            lblDueDate!.Text = dueDate.ToString("dd/MM/yyyy");
            lblPriority!.Text = task.Priority ?? "High";
            
            // Set status without triggering event
            cboTrangThai!.SelectedIndexChanged -= CboTrangThai_SelectedIndexChanged;
            cboTrangThai.SelectedItem = task.Status ?? "To Do";
            if (cboTrangThai.SelectedItem == null && cboTrangThai.Items.Count > 0)
            {
                cboTrangThai.SelectedIndex = 2; // Default to "To Do"
            }
            cboTrangThai.SelectedIndexChanged += CboTrangThai_SelectedIndexChanged;
            
            // Display assigned member
            DisplayAssignedMember();
        }

        private void DisplayAssignedMember()
        {
            pnlNguoiGiao!.Controls.Clear();
            
            if (string.IsNullOrEmpty(currentTask.AssignedToUserName))
            {
                var lblEmpty = new Label
                {
                    Text = "Chưa giao",
                    Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    Location = new Point(5, 10),
                    AutoSize = true
                };
                pnlNguoiGiao.Controls.Add(lblEmpty);
                return;
            }

            // Create member chip
            var chip = CreateMemberChip(currentTask.AssignedToUserName);
            chip.Location = new Point(5, 5);
            pnlNguoiGiao.Controls.Add(chip);
        }

        private Panel CreateMemberChip(string userName)
        {
            var chipPanel = new Panel
            {
                Height = 28,
                AutoSize = true,
                BackColor = Color.FromArgb(88, 86, 214),
                Padding = new Padding(8, 5, 4, 5)
            };

            var lblName = new Label
            {
                Text = userName,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(8, 6)
            };

            var btnRemove = new Button
            {
                Text = "×",
                Size = new Size(20, 18),
                Location = new Point(lblName.Right + 5, 5),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(231, 76, 60),
                Cursor = Cursors.Hand
            };
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.Click += BtnRemove_Click;

            chipPanel.Controls.AddRange(new Control[] { lblName, btnRemove });
            chipPanel.Width = lblName.Width + btnRemove.Width + 20;
            
            return chipPanel;
        }

        private async void BtnRemove_Click(object? sender, EventArgs e)
        {
            await RemoveMemberAsync();
        }

        private async void LoadProjectMembers()
        {
            try
            {
                var response = await ApiHelper.GetAsync($"projects/{projectId}/members");
                if (!response.IsSuccessStatusCode) return;

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<MembersResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                projectMembers = result?.Members ?? new List<ProjectMember>();
            }
            catch { }
        }

        private void BtnAddMember_Click(object sender, EventArgs e)
        {
            // Check if already assigned
            if (!string.IsNullOrEmpty(currentTask.AssignedToUserName))
            {
                MessageBox.Show("Task đã được giao. Vui lòng gỡ người hiện tại trước.", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var availableMembers = projectMembers.ToList();

            if (availableMembers.Count == 0)
            {
                MessageBox.Show("Không có thành viên nào đã giao.", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Simple selection dialog
            var selectForm = new Form
            {
                Text = "Chọn Thành Viên",
                Size = new Size(320, 250),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.White
            };

            var listBox = new ListBox
            {
                Location = new Point(20, 20),
                Size = new Size(270, 150),
                Font = new Font("Segoe UI", 10F)
            };

            foreach (var member in availableMembers)
            {
                listBox.Items.Add(member.UserName);
            }

            var btnOk = new Button
            {
                Text = "Giao Task",
                Location = new Point(190, 180),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.OK
            };
            btnOk.FlatAppearance.BorderSize = 0;

            var btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(80, 180),
                Size = new Size(100, 35),
                BackColor = Color.White,
                ForeColor = Color.Gray,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.Cancel
            };
            btnCancel.FlatAppearance.BorderColor = Color.LightGray;

            selectForm.Controls.AddRange(new Control[] { listBox, btnOk, btnCancel });
            selectForm.AcceptButton = btnOk;
            selectForm.CancelButton = btnCancel;

            if (selectForm.ShowDialog() == DialogResult.OK && listBox.SelectedItem != null)
            {
                var selectedMember = projectMembers.FirstOrDefault(m => m.UserName == listBox.SelectedItem.ToString());
                if (selectedMember != null)
                {
                    AssignMemberAsync(selectedMember);
                }
            }
        }

        private async void AssignMemberAsync(ProjectMember member)
        {
            try
            {
                var response = await ApiHelper.PutAsync($"tasks/{taskId}/assign", new { userId = member.UserID });
                
                if (response.IsSuccessStatusCode)
                {
                    TaskUpdated = true;
                    
                    // Parse response to get assigned user details
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<AssignResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    
                    // Update with UserName from response or use member.UserName
                    currentTask.AssignedToUserName = result?.Data?.AssignedToUser?.FullName ?? member.UserName;
                    
                    DisplayAssignedMember();
                    MessageBox.Show($"Đã giao task cho: {currentTask.AssignedToUserName}", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { }
        }

        private async Task RemoveMemberAsync()
        {
            var result = MessageBox.Show(
                $"Gỡ {currentTask.AssignedToUserName} khỏi task này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes) return;

            try
            {
                // Debug: Show taskId
                // MessageBox.Show($"Task ID: {taskId}", "Debug");
                
                // API expects PUT with empty body
                var response = await ApiHelper.PutAsync($"tasks/{taskId}/unassign", null);
                
                // Debug: Show status code
                var statusCode = (int)response.StatusCode;
                
                if (response.IsSuccessStatusCode)
                {
                    TaskUpdated = true;
                    currentTask.AssignedToUserName = null;
                    DisplayAssignedMember();
                    MessageBox.Show("Đã gỡ người được giao!", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Show detailed error
                    var errorContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(
                        $"Không thể gỡ người được giao!\n\n" +
                        $"Status: {response.StatusCode} ({statusCode})\n" +
                        $"Task ID: {taskId}\n\n" +
                        $"Response:\n{errorContent}", 
                        "Lỗi API", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception: {ex.GetType().Name}\n\nMessage: {ex.Message}\n\nStackTrace:\n{ex.StackTrace}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadTaskComments()
        {
            try
            {
                var response = await ApiHelper.GetAsync($"tasks/{taskId}/comments");
                if (!response.IsSuccessStatusCode) return;

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<CommentsResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                flowComments!.Controls.Clear();
                var comments = result?.Data ?? new List<Comment>();

                if (comments.Count == 0)
                {
                    var emptyLabel = new Label
                    {
                        Text = "Chưa có bình luận nào.",
                        Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Margin = new Padding(5)
                    };
                    flowComments.Controls.Add(emptyLabel);
                    return;
                }

                foreach (var comment in comments)
                {
                    var commentPanel = new Panel
                    {
                        Width = flowComments.Width - 20,
                        AutoSize = true,
                        MinimumSize = new Size(flowComments.Width - 20, 40),
                        BackColor = Color.White,
                        Margin = new Padding(3),
                        Padding = new Padding(8)
                    };

                    // Get UserName from UserDetails or fallback to old field
                    string userName = comment.UserDetails?.UserName ?? comment.UserName ?? "Unknown";

                    var lblUser = new Label
                    {
                        Text = userName + " • " + DateTime.Now.ToString("dd/MM/yyyy"),
                        Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                        Location = new Point(5, 5),
                        AutoSize = true,
                        ForeColor = Color.FromArgb(88, 86, 214)
                    };

                    var lblContent = new Label
                    {
                        Text = comment.Content,
                        Font = new Font("Segoe UI", 9F),
                        Location = new Point(5, 22),
                        Size = new Size(commentPanel.Width - 15, 0),
                        AutoSize = true,
                        ForeColor = Color.FromArgb(44, 62, 80)
                    };

                    commentPanel.Controls.AddRange(new Control[] { lblUser, lblContent });
                    commentPanel.Height = lblContent.Bottom + 8;
                    flowComments.Controls.Add(commentPanel);
                }
            }
            catch { }
        }

        private async Task UpdateTaskStatus()
        {
            if (isLoadingData) return;
            if (cboTrangThai?.SelectedItem == null) return;
            
            try
            {
                var newStatus = cboTrangThai.SelectedItem.ToString();
                var response = await ApiHelper.PutAsync($"tasks/{taskId}", new { Status = newStatus });
                
                if (response.IsSuccessStatusCode)
                {
                    TaskUpdated = true;
                    currentTask.Status = newStatus;
                    MessageBox.Show($"Cập nhật trạng thái: {newStatus}", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { }
        }

        private async Task SendComment()
        {
            if (string.IsNullOrWhiteSpace(txtNewComment?.Text)) return;
            
            try
            {
                var response = await ApiHelper.PostAsync($"tasks/{taskId}/comments", new { content = txtNewComment.Text.Trim() });
                if (response.IsSuccessStatusCode)
                {
                    txtNewComment.Clear();
                    LoadTaskComments();
                }
            }
            catch { }
        }

        public class Comment
        {
            public string? UserName { get; set; } // For backward compatibility
            public string? Content { get; set; }
            public UserDetails? UserDetails { get; set; } // NEW in v2.0.0
        }

        public class UserDetails
        {
            public string? UserID { get; set; }
            public string? UserName { get; set; }
            public string? Email { get; set; }
            public string? FullName { get; set; }
        }

        public class CommentsResponse
        {
            public List<Comment>? Data { get; set; }
        }

        public class ProjectMember
        {
            public string? UserID { get; set; }
            public string? UserName { get; set; }
        }

        public class MembersResponse
        {
            public List<ProjectMember>? Members { get; set; }
        }

        public class TaskDetailResponse
        {
            public string? Message { get; set; }
            public string? TaskId { get; set; }
            public TaskData? Data { get; set; }
        }

        public class TaskData
        {
            public string? TaskID { get; set; }
            public string? TaskName { get; set; }
            public string? TaskDescription { get; set; }
            public string? DueDate { get; set; }
            public string? Priority { get; set; }
            public string? Status { get; set; }
            public string? AssignedToUserID { get; set; }
            public AssignedUserDetails? AssignedUserDetails { get; set; }
        }

        public class AssignedUserDetails
        {
            public string? UserID { get; set; }
            public string? UserName { get; set; }
            public string? Email { get; set; }
        }

        public class AssignResponse
        {
            public string? Message { get; set; }
            public AssignData? Data { get; set; }
        }

        public class AssignData
        {
            public string? TaskID { get; set; }
            public string? TaskName { get; set; }
            public string? AssignedToUserID { get; set; }
            public AssignedToUser? AssignedToUser { get; set; }
        }

        public class AssignedToUser
        {
            public string? UserID { get; set; }
            public string? Email { get; set; }
            public string? FullName { get; set; }
        }
    }
}
