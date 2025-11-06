using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        private bool isLoadingStatus = false;
        private bool hasChanges = false;
        
        public class TaskItem
        {
            [System.Text.Json.Serialization.JsonPropertyName("TaskID")]
            public string TaskID { get; set; }
            
            [System.Text.Json.Serialization.JsonPropertyName("ProjectID")]
            public string ProjectID { get; set; }
            
            [System.Text.Json.Serialization.JsonPropertyName("TaskName")]
            public string TaskName { get; set; }
            
            [System.Text.Json.Serialization.JsonPropertyName("TaskDescription")]
            public string TaskDescription { get; set; }
            
            [System.Text.Json.Serialization.JsonPropertyName("DueDate")]
            public string DueDate { get; set; }
            
            [System.Text.Json.Serialization.JsonPropertyName("Priority")]
            public string Priority { get; set; }
            
            [System.Text.Json.Serialization.JsonPropertyName("Status")]
            public string Status { get; set; }
            
            [System.Text.Json.Serialization.JsonPropertyName("AssignedToUserID")]
            public string AssignedToUserID { get; set; }
        }

        public class TasksApiResponse
        {
            public string Message { get; set; }
            public int Count { get; set; }
            public List<string> TaskIds { get; set; }
            public List<TaskItem> Data { get; set; }
        }

        public class TaskApiResponse
        {
            public string Message { get; set; }
            public string TaskId { get; set; }
            public TaskItem Data { get; set; }
        }

        public ProjectView(string projectId, string projectName, string projectDescription, 
                          string endDate, string status, string userId, string userName)
        {
            InitializeComponent();
            
            this.projectId = projectId;
            this.currentUserId = userId;
            this.currentUserName = userName;
            
            lblProjectName.Text = projectName;
            lblProjectDescription.Text = projectDescription;
            lblProjectDeadline.Text = $"Ngày kết thúc dự kiến: {endDate}";
            
            isLoadingStatus = true;
            cboProjectStatus.SelectedItem = status;
            isLoadingStatus = false;
            
            lblUserName.Text = $"Chào mừng, {userName}";
            lblUserAvatar.Text = GetInitials(userName);
            
            InitializeStyles();
            LoadTasksFromApi();
            
            this.FormClosed += ProjectView_FormClosed;
        }

        private void ProjectView_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (hasChanges)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void InitializeStyles()
        {
            lblUserAvatar.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(lblUserAvatar.ClientRectangle, 25))
                {
                    lblUserAvatar.Region = new Region(path);
                }
            };

            panelProjectInfo.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(panelProjectInfo.ClientRectangle, 8))
                {
                    panelProjectInfo.Region = new Region(path);
                }
            };

            btnAddTask.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(btnAddTask.ClientRectangle, 5))
                {
                    btnAddTask.Region = new Region(path);
                }
            };

            panelToDo.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(panelToDo.ClientRectangle, 8))
                {
                    panelToDo.Region = new Region(path);
                }
            };

            panelInProgress.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(panelInProgress.ClientRectangle, 8))
                {
                    panelInProgress.Region = new Region(path);
                }
            };

            panelDone.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(panelDone.ClientRectangle, 8))
                {
                    panelDone.Region = new Region(path);
                }
            };

            if (!string.IsNullOrEmpty(cboProjectStatus.Text))
            {
                cboProjectStatus.BackColor = GetStatusBackgroundColor(cboProjectStatus.Text);
                cboProjectStatus.ForeColor = Color.White;
            }
        }

        private System.Drawing.Drawing2D.GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            float r = radius;
            path.AddArc(rect.X, rect.Y, r, r, 180, 90);
            path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90);
            path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
            path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90);
            path.CloseFigure();
            return path;
        }

        private string GetInitials(string name)
        {
            if (string.IsNullOrEmpty(name)) return "??";
            var parts = name.Split(' ');
            if (parts.Length >= 2)
                return $"{parts[0][0]}{parts[parts.Length - 1][0]}".ToUpper();
            return name.Length >= 2 ? name.Substring(0, 2).ToUpper() : name.ToUpper();
        }

        private Color GetStatusBackgroundColor(string status)
        {
            return status switch
            {
                "Planning" => Color.FromArgb(155, 89, 182),
                "To Do" => Color.FromArgb(231, 76, 60),
                "In Progress" => Color.FromArgb(241, 196, 15),
                "On Hold" => Color.FromArgb(243, 156, 18),
                "Completed" => Color.FromArgb(46, 204, 113),
                "Cancelled" => Color.FromArgb(149, 165, 166),
                _ => Color.Gray
            };
        }

        private Color GetPriorityColor(string priority)
        {
            return priority switch
            {
                "High" => Color.FromArgb(231, 76, 60),
                "Medium" => Color.FromArgb(241, 196, 15),
                "Low" => Color.FromArgb(46, 204, 113),
                _ => Color.Gray
            };
        }

        private async void LoadTasksFromApi()
        {
            try
            {
                var response = await ApiHelper.GetAsync($"tasks?ProjectID={projectId}");
                
                if (ApiHelper.IsUnauthorized(response))
                {
                    MessageBox.Show("Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại!", 
                        "Hết phiên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AuthManager.Logout();
                    this.Close();
                    return;
                }
                
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<TasksApiResponse>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    flowToDoTasks.Controls.Clear();
                    flowInProgressTasks.Controls.Clear();
                    flowDoneTasks.Controls.Clear();

                    if (result?.Data != null && result.Data.Count > 0)
                    {
                        foreach (var task in result.Data)
                        {
                            var taskCard = CreateTaskCard(task);
                            
                            switch (task.Status)
                            {
                                case "To Do":
                                    flowToDoTasks.Controls.Add(taskCard);
                                    break;
                                case "In Progress":
                                    flowInProgressTasks.Controls.Add(taskCard);
                                    break;
                                case "Done":
                                    flowDoneTasks.Controls.Add(taskCard);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        ShowEmptyTasksState();
                    }
                }
                else
                {
                    MessageBox.Show($"Không thể tải danh sách nhiệm vụ.\nMã lỗi: {response.StatusCode}", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Không thể kết nối đến server.\nChi tiết: {ex.Message}", 
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải nhiệm vụ: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowEmptyTasksState()
        {
            var emptyLabel = new Label
            {
                Text = "Chưa có nhiệm vụ nào.\nNhấn '+ Nhiệm Vụ Mới' để thêm!",
                Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                ForeColor = Color.Gray,
                AutoSize = false,
                Size = new Size(300, 60),
                TextAlign = ContentAlignment.MiddleCenter
            };
            flowToDoTasks.Controls.Add(emptyLabel);
        }

        private Panel CreateTaskCard(TaskItem task)
        {
            var card = new Panel
            {
                Width = 310,
                Height = 110,
                BackColor = Color.White,
                Margin = new Padding(5),
                Padding = new Padding(10),
                Cursor = Cursors.Hand,
                Tag = task
            };

            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("📋 Chuyển sang To Do", null, async (s, e) => await ChangeTaskStatus(task, card, "To Do"));
            contextMenu.Items.Add("⏳ Chuyển sang In Progress", null, async (s, e) => await ChangeTaskStatus(task, card, "In Progress"));
            contextMenu.Items.Add("✅ Chuyển sang Done", null, async (s, e) => await ChangeTaskStatus(task, card, "Done"));
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add("🗑️ Xóa nhiệm vụ", null, async (s, e) => await DeleteTask(task.TaskID, card));
            
            card.ContextMenuStrip = contextMenu;

            var lblTaskName = new Label
            {
                Text = task.TaskName,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(10, 10),
                Size = new Size(200, 40),
                AutoSize = false,
                ContextMenuStrip = contextMenu
            };

            DateTime dueDate;
            DateTime.TryParse(task.DueDate, out dueDate);
            var lblDueDate = new Label
            {
                Text = $"Hạn: {dueDate:dd/MM/yyyy}",
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.Gray,
                Location = new Point(10, 55),
                AutoSize = true,
                ContextMenuStrip = contextMenu
            };

            var lblPriority = new Label
            {
                Text = task.Priority,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = GetPriorityColor(task.Priority),
                Location = new Point(220, 10),
                Size = new Size(50, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblPriority.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(lblPriority.ClientRectangle, 3))
                {
                    lblPriority.Region = new Region(path);
                }
            };

            var lblUserInitials = new Label
            {
                Text = string.IsNullOrEmpty(task.AssignedToUserID) ? "?" : GetInitials(currentUserName),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(88, 86, 214),
                Location = new Point(220, 75),
                Size = new Size(30, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblUserInitials.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(lblUserInitials.ClientRectangle, 15))
                {
                    lblUserInitials.Region = new Region(path);
                }
            };

            var btnChangeStatus = new Button
            {
                Text = "⋮",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.Gray,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(25, 25),
                Location = new Point(275, 10),
                Cursor = Cursors.Hand
            };
            btnChangeStatus.FlatAppearance.BorderSize = 0;
            btnChangeStatus.Click += (s, e) => contextMenu.Show(btnChangeStatus, new Point(0, btnChangeStatus.Height));

            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(card.ClientRectangle, 8))
                {
                    card.Region = new Region(path);
                }
            };

            card.Controls.AddRange(new Control[] { lblTaskName, lblDueDate, lblPriority, lblUserInitials, btnChangeStatus });

            card.Click += (s, e) => ShowTaskDetails(task);
            lblTaskName.Click += (s, e) => ShowTaskDetails(task);
            lblDueDate.Click += (s, e) => ShowTaskDetails(task);

            return card;
        }

        private async System.Threading.Tasks.Task ChangeTaskStatus(TaskItem task, Panel taskCard, string newStatus)
        {
            try
            {
                taskCard.Enabled = false;

                var updateData = new { Status = newStatus };
                var response = await ApiHelper.PutAsync($"tasks/{task.TaskID}", updateData);
                
                if (ApiHelper.IsUnauthorized(response))
                {
                    MessageBox.Show("Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại!", 
                        "Hết phiên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AuthManager.Logout();
                    this.Close();
                    return;
                }
                
                if (response.IsSuccessStatusCode)
                {
                    hasChanges = true;
                    task.Status = newStatus;
                    
                    var currentParent = taskCard.Parent as FlowLayoutPanel;
                    currentParent?.Controls.Remove(taskCard);

                    FlowLayoutPanel targetFlow = newStatus switch
                    {
                        "To Do" => flowToDoTasks,
                        "In Progress" => flowInProgressTasks,
                        "Done" => flowDoneTasks,
                        _ => flowToDoTasks
                    };

                    targetFlow.Controls.Add(taskCard);
                    taskCard.Enabled = true;

                    this.Text = $"✓ Đã chuyển sang {newStatus}";
                    await System.Threading.Tasks.Task.Delay(2000);
                    this.Text = "Chi Tiết Dự Án";
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật trạng thái nhiệm vụ.", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    taskCard.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật trạng thái: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                taskCard.Enabled = true;
            }
        }

        private void ShowTaskDetails(TaskItem task)
        {
            var detailsMessage = $"Task: {task.TaskName}\n\n" +
                               $"Mô tả: {task.TaskDescription}\n\n" +
                               $"Hạn: {DateTime.Parse(task.DueDate):dd/MM/yyyy}\n" +
                               $"Ưu tiên: {task.Priority}\n" +
                               $"Trạng thái: {task.Status}\n\n" +
                               $"Task ID: {task.TaskID}";
            
            MessageBox.Show(detailsMessage, "Chi Tiết Nhiệm Vụ", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async System.Threading.Tasks.Task DeleteTask(string taskId, Panel taskCard)
        {
            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa nhiệm vụ này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    taskCard.Enabled = false;

                    var response = await ApiHelper.DeleteAsync($"tasks/{taskId}");
                    
                    if (ApiHelper.IsUnauthorized(response))
                    {
                        MessageBox.Show("Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại!", 
                            "Hết phiên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        AuthManager.Logout();
                        this.Close();
                        return;
                    }
                    
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        hasChanges = true;
                        
                        var parentFlow = taskCard.Parent as FlowLayoutPanel;
                        parentFlow?.Controls.Remove(taskCard);
                        taskCard.Dispose();

                        MessageBox.Show("Xóa nhiệm vụ thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Xóa nhiệm vụ thất bại!\n{responseContent}",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        taskCard.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa nhiệm vụ: {ex.Message}",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    taskCard.Enabled = true;
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private async void cboProjectStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoadingStatus) return;

            if (cboProjectStatus.SelectedItem != null)
            {
                string newStatus = cboProjectStatus.SelectedItem.ToString();
                cboProjectStatus.BackColor = GetStatusBackgroundColor(newStatus);
                
                try
                {
                    var updateData = new { Status = newStatus };
                    var response = await ApiHelper.PutAsync($"projects/{projectId}", updateData);
                    
                    if (ApiHelper.IsUnauthorized(response))
                    {
                        MessageBox.Show("Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại!", 
                            "Hết phiên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        AuthManager.Logout();
                        this.Close();
                        return;
                    }
                    
                    var responseContent = await response.Content.ReadAsStringAsync();
                    
                    if (response.IsSuccessStatusCode)
                    {
                        hasChanges = true;
                        MessageBox.Show($"Cập nhật trạng thái dự án thành: {newStatus}", 
                            "Cập Nhật Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Không thể cập nhật trạng thái dự án.\nResponse: {responseContent}", 
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật trạng thái: {ex.Message}", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
