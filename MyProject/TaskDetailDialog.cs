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
        private string projectOwnerId;
        private string currentUserId;
        private bool isOwner;

        private bool isLoadingData = true;

        private List<ProjectMember> projectMembers = new List<ProjectMember>();
        private List<AssignedUser> assignedUsers = new List<AssignedUser>();
        private ProjectView.TaskItem currentTask;
        private Dictionary<string, Color> userColors = new Dictionary<string, Color>();

        public bool TaskUpdated { get; private set; }

        public TaskDetailDialog(ProjectView.TaskItem task, string projectId, string ownerId, string userId)
        {
            this.taskId = task.TaskID;
            this.projectId = projectId;
            this.currentTask = task;
            this.projectOwnerId = ownerId;
            this.currentUserId = userId;
            this.isOwner = (currentUserId == projectOwnerId);

            InitializeComponent();

            if (!isOwner)
            {
                btnAddMember.Visible = false;
            }

            InitializeData();
        }

        private async void InitializeData()
        {
            LoadProjectMembers();
            await LoadTaskDetailsFromApiAsync();
            await LoadAssignedUsersAsync();
            LoadTaskComments();

            isLoadingData = false;
        }

        private async Task LoadTaskDetailsFromApiAsync()
        {
            try
            {
                var response = await ApiHelper.GetAsync($"tasks/{taskId}");
                if (!response.IsSuccessStatusCode)
                {
                    LoadTaskData(currentTask);                     return;
                }

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<TaskDetailResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result?.Data != null)
                {
                    currentTask.TaskName = result.Data.TaskName;
                    currentTask.TaskDescription = result.Data.TaskDescription;
                    currentTask.DueDate = result.Data.DueDate;
                    currentTask.Priority = result.Data.Priority;
                    currentTask.Status = result.Data.Status;

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
                LoadTaskData(currentTask);
            }
        }

        private async void CboTrangThai_SelectedIndexChanged(object? sender, EventArgs e)
        {
            await UpdateTaskStatus();
        }

        private async void BtnSendComment_Click(object? sender, EventArgs e)
        {
            await SendComment();
        }

        private void BtnCloseTop_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadTaskData(ProjectView.TaskItem task)
        {
            this.Text = task.TaskName;
            lblDescription!.Text = task.TaskDescription ?? "Phác thảo các bảng và mối quan hệ.";

            DateTime.TryParse(task.DueDate, out var dueDate);
            lblDueDate!.Text = dueDate.ToString("dd/MM/yyyy");
            lblPriority!.Text = task.Priority ?? "High";

            cboTrangThai!.SelectedIndexChanged -= CboTrangThai_SelectedIndexChanged;
            cboTrangThai.SelectedItem = task.Status ?? "To Do";
            if (cboTrangThai.SelectedItem == null && cboTrangThai.Items.Count > 0)
            {
                cboTrangThai.SelectedIndex = 2;
            }
            cboTrangThai.SelectedIndexChanged += CboTrangThai_SelectedIndexChanged;

            DisplayAssignedMember();
        }

        private void DisplayAssignedMember()
        {
            pnlNguoiGiao!.Controls.Clear();

            if (assignedUsers == null || assignedUsers.Count == 0)
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

            int xOffset = 5;
            int yOffset = 5;
            int maxWidth = pnlNguoiGiao.Width - 10;

            foreach (var user in assignedUsers)
            {
                var chip = CreateMemberChip(user);

                if (xOffset + chip.Width > maxWidth && xOffset > 5)
                {
                    xOffset = 5;
                    yOffset += chip.Height + 5;
                }

                chip.Location = new Point(xOffset, yOffset);
                pnlNguoiGiao.Controls.Add(chip);

                xOffset += chip.Width + 5;
            }
        }

        private Panel CreateMemberChip(AssignedUser user)
        {
            var chipPanel = new Panel
            {
                Height = 28,
                AutoSize = true,
                BackColor = Color.FromArgb(88, 86, 214),
                Padding = new Padding(8, 5, 4, 5),
                Tag = user.UserID             };

            var lblName = new Label
            {
                Text = user.UserName ?? user.Email,
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
                Cursor = Cursors.Hand,
                Tag = user.UserID             };
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.Click += BtnRemove_Click;

            chipPanel.Controls.AddRange(new Control[] { lblName, btnRemove });
            chipPanel.Width = lblName.Width + btnRemove.Width + 20;

            return chipPanel;
        }

        private async void BtnRemove_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is string userIdToRemove)
            {
                await RemoveMemberAsync(userIdToRemove);
            }
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

        private async Task LoadAssignedUsersAsync()
        {
            try
            {
                var response = await ApiHelper.GetAsync($"tasks/{taskId}/assigned-users");
                if (!response.IsSuccessStatusCode)
                {
                    assignedUsers = new List<AssignedUser>();
                    DisplayAssignedMember();
                    return;
                }

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<AssignedUsersResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                assignedUsers = result?.Data ?? new List<AssignedUser>();
                DisplayAssignedMember();
            }
            catch
            {
                assignedUsers = new List<AssignedUser>();
                DisplayAssignedMember();
            }
        }

        private void BtnAddMember_Click(object sender, EventArgs e)
        {
            var assignedUserIds = assignedUsers.Select(u => u.UserID).ToList();
            var availableMembers = projectMembers.Where(m => !assignedUserIds.Contains(m.UserID)).ToList();

            if (availableMembers.Count == 0)
            {
                MessageBox.Show("Không có thành viên nào đã giao.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectForm = new Form
            {
                Text = "Chọn Thành Viên",
                Size = new Size(320, 260),
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

                    await LoadAssignedUsersAsync();

                    MessageBox.Show($"Đã giao task cho: {member.UserName}", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { }
        }

        private async Task RemoveMemberAsync(string userId)
        {
            var userToRemove = assignedUsers.FirstOrDefault(u => u.UserID == userId);
            if (userToRemove == null) return;

            var result = MessageBox.Show(
                $"Gỡ {userToRemove.UserName} khỏi task này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes) return;

            try
            {
                var response = await ApiHelper.DeleteAsync($"tasks/{taskId}/unassign-user/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    TaskUpdated = true;

                    await LoadAssignedUsersAsync();

                    MessageBox.Show($"Đã gỡ {userToRemove.UserName}!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(
                        $"Không thể gỡ người được giao!\n\n" +
                        $"Status: {response.StatusCode}\n" +
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
                                        var userName = comment.UserDetails?.UserName ?? comment.UserName ?? "Unknown User";
                    var userId = comment.UserDetails?.UserID ?? comment.CreatedByUserID ?? "";

                    var userColor = GetColorForUser(userId);

                    var commentPanel = new FlowLayoutPanel
                    {
                        AutoSize = true,
                        MaximumSize = new Size(flowComments.Width - 30, 0),
                        MinimumSize = new Size(flowComments.Width - 30, 25),
                        Margin = new Padding(5, 2, 5, 2),
                        Padding = new Padding(3),
                        FlowDirection = FlowDirection.LeftToRight,
                        WrapContents = true,
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    var userNameLabel = new Label
                    {
                        Text = userName + ": ",
                        AutoSize = true,
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        ForeColor = userColor,
                        Margin = new Padding(0)
                    };

                    var timestampLabel = new Label
                    {
                        Text = FormatCommentTimestamp(comment.CreatedAt),
                        AutoSize = true,
                        Font = new Font("Segoe UI", 7.5F, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        Margin = new Padding(5, 2, 0, 0)
                    };

                    commentPanel.Controls.Add(userNameLabel);
                    commentPanel.Controls.Add(timestampLabel);
                    commentPanel.SetFlowBreak(timestampLabel, true);

                    var contentBox = new RichTextBox
                    {
                        Text = comment.Content,
                        ReadOnly = true,
                        BorderStyle = BorderStyle.None,
                        ScrollBars = RichTextBoxScrollBars.None,
                        Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                        ForeColor = Color.Black,
                        BackColor = SystemColors.Control,
                        Margin = new Padding(0),
                        DetectUrls = true,
                        Cursor = Cursors.IBeam,
                        Width = flowComments.Width - 90,
                        Multiline = true,
                        WordWrap = true
                    };

                    using (Graphics g = contentBox.CreateGraphics())
                    {
                        SizeF size = g.MeasureString(comment.Content, contentBox.Font, contentBox.Width);
                        contentBox.Height = (int)Math.Ceiling(size.Height) + 10;
                    }

                    contentBox.LinkClicked += (s, e) =>
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = e.LinkText,
                                UseShellExecute = true
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Không thể mở link: {ex.Message}", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };

                    commentPanel.Controls.Add(contentBox);

                    if (userId == currentUserId && !string.IsNullOrEmpty(comment.CommentID))
                    {
                        var btnDelete = new Button
                        {
                            Text = "✕",
                            Size = new Size(22, 22),
                            BackColor = Color.FromArgb(220, 53, 69),
                            ForeColor = Color.White,
                            FlatStyle = FlatStyle.Flat,
                            Cursor = Cursors.Hand,
                            Tag = comment.CommentID,
                            Font = new Font("Segoe UI", 9, FontStyle.Bold),
                            Margin = new Padding(5, 0, 0, 0)
                        };
                        btnDelete.FlatAppearance.BorderSize = 0;
                        btnDelete.Click += async (s, e) => await DeleteComment(comment.CommentID);

                        commentPanel.Controls.Add(btnDelete);
                    }

                    flowComments.Controls.Add(commentPanel);
                }
            }
            catch { }
        }

        private Color GetColorForUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return Color.Gray;

            if (!userColors.ContainsKey(userId))
            {
                int hash = userId.GetHashCode();
                Random rnd = new Random(hash);

                int r = rnd.Next(50, 200);
                int g = rnd.Next(50, 200);
                int b = rnd.Next(50, 200);

                userColors[userId] = Color.FromArgb(r, g, b);
            }

            return userColors[userId];
        }

        private string FormatCommentTimestamp(string? createdAt)
        {
            if (string.IsNullOrEmpty(createdAt))
                return "";

            try
            {
                DateTime dt = DateTime.Parse(createdAt);
                return dt.ToString("dd/MM/yyyy HH:mm:ss");
            }
            catch
            {
                return createdAt;
            }
        }

        private async Task DeleteComment(string commentId)
        {
            try
            {
                var result = MessageBox.Show("Bạn có chắc muốn xóa bình luận này?", "Xác nhận xóa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;

                var response = await ApiHelper.DeleteAsync($"comments/{commentId}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Đã xóa bình luận!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTaskComments();
                    TaskUpdated = true;
                }
                else if (ApiHelper.IsUnauthorized(response))
                {
                    MessageBox.Show("Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AuthManager.Logout();
                    this.Close();
                }
                else if (ApiHelper.IsForbidden(response))
                {
                    MessageBox.Show("Bạn không có quyền xóa bình luận này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Không thể xóa bình luận!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            public string? CommentID { get; set; }
            public string? Content { get; set; }
            public string? CreatedAt { get; set; }
            public string? CreatedByUserID { get; set; }
            public string? TaskID { get; set; }
            public UserDetails? UserDetails { get; set; } 
                        public string? UserName { get; set; }
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

        public class AssignedUsersResponse
        {
            public string? Message { get; set; }
            public string? TaskId { get; set; }
            public int Count { get; set; }
            public List<AssignedUser>? Data { get; set; }
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
            public List<AssignedUser>? AssignedUsers { get; set; }
            public int AssignedUsersCount { get; set; }
        }

        public class AssignedUser
        {
            public string? AssignmentID { get; set; }
            public string? UserID { get; set; }
            public string? UserName { get; set; }
            public string? Email { get; set; }
            public string? AssignedAt { get; set; }
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
