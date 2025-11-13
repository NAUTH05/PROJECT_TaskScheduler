using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyProject
{
    public partial class ProjectView : Form
    {
        private string projectId;
        private string projectOwnerId;
        private string currentUserId;
        private string currentUserName;
        private bool hasChanges = false;
        private bool isLoadingData = true;          private bool isOwner = false;

        private List<TaskItem> currentTasks = new List<TaskItem>();
        private List<ProjectMember> projectMembers = new List<ProjectMember>();
        private Dictionary<string, Color> userColors = new Dictionary<string, Color>();

        public ProjectView(string projectId, string projectName, string projectDescription,
                          string endDate, string status, string userId, string userName, string ownerId)
        {
            this.projectId = projectId;
            this.projectOwnerId = ownerId;
            this.currentUserId = userId;
            this.currentUserName = userName;
            this.isOwner = (userId == ownerId);

            InitializeComponent();

            lblProjectTitle.Text = projectName;
            lblProjectDescription.Text = projectDescription;
            lblProjectDeadline.Text = $"Ngày hạn: {endDate}";
            lblUserIdDisplay.Text = $"User ID: {userId}";

            cboProjectStatus.SelectedItem = status;
            cboProjectStatus.SelectedIndexChanged += cboProjectStatus_SelectedIndexChanged;

            dgvTasks.CellDoubleClick += DgvTasks_CellDoubleClick;

            LoadProjectData();
            this.FormClosed += (s, e) =>
            {
                if (hasChanges) this.DialogResult = DialogResult.OK;
            };

            isLoadingData = false;  
                        if (!isOwner)
            {
                btnAddMember.Visible = false;
            }
        }

        private async void LoadProjectData()
        {
            await LoadTasksFromApi();
            await LoadProjectMembers();
            await LoadComments();
        }

        private async Task LoadTasksFromApi()
        {
            try
            {
                var response = await ApiHelper.GetAsync($"tasks?ProjectID={projectId}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"API Error: {response.StatusCode}\n\nResponse:\n{errorContent}",
                        "Lỗi tải tasks", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var json = await response.Content.ReadAsStringAsync();

                                
                var result = JsonSerializer.Deserialize<TasksApiResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                currentTasks = result?.Data ?? new List<TaskItem>();

                
                PopulateTaskGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception: {ex.GetType().Name}\n\nMessage: {ex.Message}\n\nStackTrace:\n{ex.StackTrace}",
                    "Lỗi khi tải tasks", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateTaskGrid()
        {
            try
            {
                dgvTasks.Rows.Clear();

                if (currentTasks == null || currentTasks.Count == 0)
                {
                                        return;
                }

                foreach (var task in currentTasks)
                {
                    try
                    {
                        DateTime.TryParse(task.DueDate, out var dueDate);
                        var row = dgvTasks.Rows[dgvTasks.Rows.Add()];
                        row.Cells[0].Value = task.TaskName;
                        row.Cells[1].Value = task.Status;

                                                string assignedName = "Chưa giao";
                        if (task.AssignedUsers != null && task.AssignedUsers.Count > 0)
                        {
                                                        assignedName = string.Join(", ", task.AssignedUsers.Select(u => u.UserName ?? u.Email));
                        }
                        else if (task.AssignedUserDetails != null)
                        {
                                                        assignedName = task.AssignedUserDetails.UserName ?? task.AssignedToUserName ?? "Chưa giao";
                        }
                        else if (!string.IsNullOrEmpty(task.AssignedToUserName))
                        {
                            assignedName = task.AssignedToUserName;
                        }

                        row.Cells[2].Value = assignedName;

                        row.Cells[3].Value = dueDate.ToString("dd/MM/yyyy");
                        row.Cells[4].Value = task.Priority;
                        row.Cells[5].Value = task.TaskDescription;
                        row.Tag = task;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi thêm task '{task?.TaskName ?? "N/A"}': {ex.Message}", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi PopulateTaskGrid: {ex.Message}\n\n{ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadProjectMembers()
        {
            try
            {
                var response = await ApiHelper.GetAsync($"projects/{projectId}/members");
                if (!response.IsSuccessStatusCode) return;

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<MembersResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                projectMembers = result?.Members ?? new List<ProjectMember>();
                var names = projectMembers.Select(m => m.UserName).Take(5);
                lblMembersList.Text = $"Thành viên: {string.Join(", ", names)}";
                if (projectMembers.Count > 5) lblMembersList.Text += $" và {projectMembers.Count - 5} người khác";
            }
            catch { }
        }

        private async Task LoadComments()
        {
            try
            {
                var response = await ApiHelper.GetAsync($"projects/{projectId}/comments");
                if (!response.IsSuccessStatusCode) return;

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<CommentsResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                flowComments.Controls.Clear();
                foreach (var comment in result?.Data ?? new List<Comment>())
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
                        Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold),
                        ForeColor = userColor,
                        Margin = new Padding(0)
                    };

                                        var timestampLabel = new Label
                    {
                        Text = FormatCommentTimestamp(comment.CreatedAt),
                        AutoSize = true,
                        Font = new Font(this.Font.FontFamily, 7.5F, FontStyle.Italic),
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
                        Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Regular),
                        ForeColor = Color.Black,
                        BackColor = SystemColors.Control,
                        Margin = new Padding(0),
                        DetectUrls = true,                         Cursor = Cursors.IBeam,
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
                            Font = new Font(this.Font.FontFamily, 9, FontStyle.Bold),
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
                return createdAt;             }
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
                    await LoadComments();
                    hasChanges = true;
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
        private void btnAddMember_Click(object sender, EventArgs e)
        {
            var dialog = new AddMemberDialog(projectId);
            if (dialog.ShowDialog() == DialogResult.OK || dialog.MemberAdded || dialog.MemberRemoved)
            {
                hasChanges = true;
                LoadProjectMembers();
            }
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            var taskForm = new TaskForm(projectId, currentUserId);
            if (taskForm.ShowDialog() == DialogResult.OK)
            {
                hasChanges = true;
                LoadTasksFromApi();
            }
        }

        private async void btnSendComment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewComment.Text)) return;
            try
            {
                var response = await ApiHelper.PostAsync($"projects/{projectId}/comments", new { content = txtNewComment.Text.Trim() });
                if (response.IsSuccessStatusCode)
                {
                    txtNewComment.Clear();
                    await LoadComments();
                    hasChanges = true;
                }
            }
            catch { }
        }

        private void btnBack_Click(object sender, EventArgs e) => this.Close();

        private void DgvTasks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvTasks.Rows[e.RowIndex];
            var task = row.Tag as TaskItem;
            if (task != null)
            {
                var dialog = new TaskDetailDialog(task, projectId, projectOwnerId, currentUserId);
                if (dialog.ShowDialog() == DialogResult.OK || dialog.TaskUpdated)
                {
                    hasChanges = true;
                    LoadTasksFromApi();
                }
            }
        }

        private async void cboProjectStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoadingData) return;  
            if (cboProjectStatus.SelectedItem != null)
            {
                string newStatus = cboProjectStatus.SelectedItem.ToString();
                try
                {
                    var updateData = new { Status = newStatus };
                    var response = await ApiHelper.PutAsync($"projects/{projectId}", updateData);
                    if (response.IsSuccessStatusCode)
                    {
                        hasChanges = true;
                        MessageBox.Show($"Cập nhật trạng thái: {newStatus}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch { }
            }
        }

        public class TaskItem
        {
            public string TaskID { get; set; }
            public string TaskName { get; set; }
            public string TaskDescription { get; set; }
            public string DueDate { get; set; }
            public string Priority { get; set; }
            public string Status { get; set; }
            public string AssignedToUserName { get; set; }
            public AssignedUserDetails AssignedUserDetails { get; set; }
            public List<AssignedUser> AssignedUsers { get; set; }
            public int AssignedUsersCount { get; set; }
        }

        public class AssignedUser
        {
            public string AssignmentID { get; set; }
            public string UserID { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string AssignedAt { get; set; }
        }

        public class AssignedUserDetails
        {
            public string UserID { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
        }

        public class ProjectMember
        {
            public string UserID { get; set; }
            public string UserName { get; set; }
        }

        public class Comment
        {
            public string CommentID { get; set; }
            public string Content { get; set; }
            public string CreatedAt { get; set; }
            public string CreatedByUserID { get; set; }
            public string ProjectID { get; set; }
            public UserDetails UserDetails { get; set; }

                        public string UserName { get; set; }
        }

        public class UserDetails
        {
            public string UserID { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string FullName { get; set; }
        }

        public class TasksApiResponse { public List<TaskItem> Data { get; set; } }
        public class MembersResponse { public List<ProjectMember> Members { get; set; } }
        public class CommentsResponse { public List<Comment> Data { get; set; } }
    }
}


