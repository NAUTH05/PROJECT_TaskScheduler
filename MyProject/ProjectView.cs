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
        private string currentUserId;
        private string currentUserName;
        private bool hasChanges = false;
        private bool isLoadingData = true;  // ADD THIS FLAG

        private List<TaskItem> currentTasks = new List<TaskItem>();
        private List<ProjectMember> projectMembers = new List<ProjectMember>();

        public ProjectView(string projectId, string projectName, string projectDescription,
                          string endDate, string status, string userId, string userName)
        {
            this.projectId = projectId;
            this.currentUserId = userId;
            this.currentUserName = userName;

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
            
            isLoadingData = false;  // SET FALSE AFTER LOADING
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
                
                // Debug: Show raw JSON
                // MessageBox.Show($"JSON Response:\n{json.Substring(0, Math.Min(500, json.Length))}", "Debug JSON", MessageBoxButtons.OK);
                
                var result = JsonSerializer.Deserialize<TasksApiResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                currentTasks = result?.Data ?? new List<TaskItem>();
                
                MessageBox.Show($"Đã load {currentTasks.Count} tasks thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
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
                    // Don't show message if no tasks
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
                        
                        // Get UserName from AssignedUserDetails if available
                        string assignedName = task.AssignedUserDetails?.UserName ?? task.AssignedToUserName ?? "Chưa giao";
                        task.AssignedToUserName = task.AssignedUserDetails?.UserName ?? task.AssignedToUserName; // Update property
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
                    var lbl = new Label
                    {
                        Text = $"{comment.UserName}: {comment.Content}",
                        AutoSize = true,
                        MaximumSize = new Size(flowComments.Width - 30, 0),
                        Margin = new Padding(5)
                    };
                    flowComments.Controls.Add(lbl);
                }
            }
            catch { }
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
                var dialog = new TaskDetailDialog(task, projectId);
                if (dialog.ShowDialog() == DialogResult.OK || dialog.TaskUpdated)
                {
                    hasChanges = true;
                    LoadTasksFromApi();
                }
            }
        }

        private async void cboProjectStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoadingData) return;  // PREVENT UPDATE DURING LOAD
            
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
            public string UserName { get; set; }
            public string Content { get; set; }
        }

        public class TasksApiResponse { public List<TaskItem> Data { get; set; } }
        public class MembersResponse { public List<ProjectMember> Members { get; set; } }
        public class CommentsResponse { public List<Comment> Data { get; set; } }
    }
}
