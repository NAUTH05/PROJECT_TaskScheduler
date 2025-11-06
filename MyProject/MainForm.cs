using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;

namespace MyProject
{
    public partial class MainForm : Form
    {
        private string currentUserName = "Mock User";
        private string currentUserId = "";
        private FlowLayoutPanel flowProjectsList;
        private List<ProjectData> currentProjects = new List<ProjectData>();
        private List<ProjectData> recentProjects = new List<ProjectData>();
        private const int MAX_RECENT_PROJECTS = 10;

        public MainForm()
        {
            InitializeComponent();
            InitializeUI();
        }

        public MainForm(string userName, string userId) : this()
        {
            currentUserName = userName;
            currentUserId = userId;
            lblWelcome.Text = $"Chào mừng, {userName}";
            lblUserName.Text = GetInitials(userName);
            LoadProjectsFromApi();
        }

        private void InitializeUI()
        {
            lblStat1Value.Text = "0";
            lblStat2Value.Text = "0";
            lblStat3Value.Text = "0";
            
            lblUserName.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(lblUserName.ClientRectangle, 25))
                {
                    lblUserName.Region = new Region(path);
                }
            };

            SetRoundedCorners(panelStat1, 10);
            SetRoundedCorners(panelStat2, 10);
            SetRoundedCorners(panelStat3, 10);

            panelStat1.Paint += (s, e) => DrawLeftBorder(e.Graphics, panelStat1, Color.FromArgb(52, 152, 219), 4);
            panelStat2.Paint += (s, e) => DrawLeftBorder(e.Graphics, panelStat2, Color.FromArgb(241, 196, 15), 4);
            panelStat3.Paint += (s, e) => DrawLeftBorder(e.Graphics, panelStat3, Color.FromArgb(46, 204, 113), 4);

            flowProjectsList = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(10)
            };
            panelProjectsList.Controls.Add(flowProjectsList);

            lblTasksTitle.Text = "Dự Án Gần Đây";
            
            lblToDoTitle.Text = "In Progress Gần Đây";
            lblInProgressTitle.Text = "To Do Gần Đây";
            lblDoneTitle.Text = "Completed Gần Đây";
            
            panelToDo.Controls.Clear();
            panelInProgress.Controls.Clear();
            panelDone.Controls.Clear();

            UpdateRecentProjectsDisplay();
        }

        private async void LoadProjectsFromApi()
        {
            if (string.IsNullOrEmpty(currentUserId))
            {
                LoadSampleProjects();
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync($"https://nauth.fitlhu.com/api/projects?OwnerUserID={currentUserId}");
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonSerializer.Deserialize<ProjectsApiResponse>(responseContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        flowProjectsList.Controls.Clear();
                        currentProjects.Clear();

                        if (result?.Data != null && result.Data.Count > 0)
                        {
                            currentProjects = result.Data;
                            
                            foreach (var project in result.Data)
                            {
                                DateTime endDate;
                                DateTime.TryParse(project.EndDate, out endDate);

                                int progress = CalculateProgress(project.Status);

                                AddProjectItem(
                                    project.ProjectName,
                                    endDate.ToString("dd/MM/yyyy"),
                                    progress,
                                    project.Status,
                                    new[] { GetInitials(currentUserName) },
                                    project.ProjectID
                                );
                            }

                            UpdateStatistics();
                        }
                        else
                        {
                            ShowEmptyState();
                            UpdateStatistics();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Không thể tải danh sách dự án.\nMã lỗi: {response.StatusCode}", 
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LoadSampleProjects();
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Không thể kết nối đến server.\nChi tiết: {ex.Message}", 
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadSampleProjects();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dự án: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadSampleProjects();
            }
        }

        private int CalculateProgress(string status)
        {
            return status switch
            {
                "In Progress" => 50,
                "Completed" => 100,
                "To Do" => 0,
                _ => 0
            };
        }

        private void ShowEmptyState()
        {
            var emptyLabel = new Label
            {
                Text = "Chưa có dự án nào.\nNhấn nút '+ Dự Án Mới' để tạo dự án đầu tiên!",
                Font = new Font("Segoe UI", 12F, FontStyle.Italic),
                ForeColor = Color.Gray,
                AutoSize = false,
                Size = new Size(flowProjectsList.Width - 40, 100),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            flowProjectsList.Controls.Add(emptyLabel);
        }

        private string GetInitials(string name)
        {
            var parts = name.Split(' ');
            if (parts.Length >= 2)
                return $"{parts[0][0]}{parts[parts.Length - 1][0]}".ToUpper();
            return name.Length >= 2 ? name.Substring(0, 2).ToUpper() : name.ToUpper();
        }

        private void SetRoundedCorners(Panel panel, int radius)
        {
            panel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(panel.ClientRectangle, radius))
                {
                    panel.Region = new Region(path);
                }
            };
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

        private void DrawLeftBorder(Graphics g, Panel panel, Color color, int width)
        {
            using (var pen = new Pen(color, width))
            {
                g.DrawLine(pen, 0, 0, 0, panel.Height);
            }
        }

        private void LoadSampleProjects()
        {
            AddProjectItem("Phát triển TaskScheduler Backend", "30/11/2025", 60, "In Progress",
                new[] { "ML", "A" });

            AddProjectItem("Thiết kế UI/UX cho ứng dụng", "15/10/2025", 100, "Completed",
                new[] { "ML", "B" });

            AddProjectItem("Marketing và giới thiệu sản phẩm", "31/01/2026", 10, "To Do",
                new[] { "A", "B" });
        }

        private void AddProjectItem(string title, string deadline, int progress, string status, string[] members)
        {
            AddProjectItem(title, deadline, progress, status, members, null);
        }

        private void AddProjectItem(string title, string deadline, int progress, string status, string[] members, string projectId)
        {
            var projectPanel = new Panel
            {
                Width = flowProjectsList.Width - 40,
                Height = 80,
                BackColor = Color.White,
                Margin = new Padding(5, 5, 5, 10),
                Padding = new Padding(15, 10, 15, 10),
                Tag = projectId
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(88, 86, 214),
                AutoSize = true,
                Location = new Point(15, 10),
                Cursor = Cursors.Hand,
                Tag = projectId
            };
            
            lblTitle.Click += (s, e) => 
            {
                AddToRecentProjects(projectId, title, deadline, status);
                
                var projectView = new ProjectView(
                    projectId,
                    title,
                    "Thiết lập API và Database, đảm bảo tính năng xác thực và lưu trữ dữ liệu an toàn.",
                    deadline,
                    status,
                    currentUserId,
                    currentUserName
                );
                
                var dialogResult = projectView.ShowDialog();
                
                if (dialogResult == DialogResult.OK)
                {
                    this.Text = "TaskScheduler Dashboard - Đang tải lại...";
                    this.Cursor = Cursors.WaitCursor;
                    LoadProjectsFromApi();
                    this.Text = "TaskScheduler Dashboard";
                    this.Cursor = Cursors.Default;
                }
            };

            var lblDeadline = new Label
            {
                Text = $"Kết thúc: {deadline}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(15, 40)
            };

            var progressBar = new ProgressBar
            {
                Location = new Point(450, 25),
                Size = new Size(200, 20),
                Value = progress,
                Style = ProgressBarStyle.Continuous
            };

            var lblProgress = new Label
            {
                Text = $"{progress}%",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(660, 25)
            };

            int xPos = 750;
            foreach (var member in members)
            {
                var lblMember = new Label
                {
                    Text = member,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    Size = new Size(35, 35),
                    Location = new Point(xPos, 20),
                    BackColor = Color.FromArgb(88, 86, 214),
                    ForeColor = Color.White,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                lblMember.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    using (var path = GetRoundedRectPath(lblMember.ClientRectangle, 17))
                    {
                        lblMember.Region = new Region(path);
                    }
                };

                projectPanel.Controls.Add(lblMember);
                xPos += 40;
            }

            var lblStatus = new Label
            {
                Text = status,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(projectPanel.Width - 170, 25),
                Padding = new Padding(10, 5, 10, 5),
                BackColor = GetStatusColor(status),
                ForeColor = Color.White
            };

            lblStatus.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(lblStatus.ClientRectangle, 5))
                {
                    lblStatus.Region = new Region(path);
                }
            };

            if (!string.IsNullOrEmpty(projectId))
            {
                var btnDelete = new Button
                {
                    Text = "🗑️",
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    Size = new Size(35, 35),
                    Location = new Point(projectPanel.Width - 50, 22),
                    BackColor = Color.FromArgb(231, 76, 60),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    Tag = projectId
                };

                btnDelete.FlatAppearance.BorderSize = 0;
                
                var tooltip = new ToolTip();
                tooltip.SetToolTip(btnDelete, "Xóa dự án");

                btnDelete.Click += async (s, e) =>
                {
                    var result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa dự án '{title}'?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (result == DialogResult.Yes)
                    {
                        await DeleteProject(projectId, projectPanel);
                    }
                };

                btnDelete.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    using (var path = GetRoundedRectPath(btnDelete.ClientRectangle, 17))
                    {
                        btnDelete.Region = new Region(path);
                    }
                };

                projectPanel.Controls.Add(btnDelete);
            }

            projectPanel.Controls.AddRange(new Control[] { lblTitle, lblDeadline, progressBar, lblProgress, lblStatus });

            projectPanel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(projectPanel.ClientRectangle, 8))
                {
                    projectPanel.Region = new Region(path);
                }
            };

            flowProjectsList.Controls.Add(projectPanel);
        }

        private void AddToRecentProjects(string projectId, string title, string deadline, string status)
        {
            if (string.IsNullOrEmpty(projectId)) return;

            var project = currentProjects.FirstOrDefault(p => p.ProjectID == projectId);
            if (project == null) return;

            recentProjects.RemoveAll(p => p.ProjectID == projectId);
            recentProjects.Insert(0, project);

            if (recentProjects.Count > MAX_RECENT_PROJECTS)
            {
                recentProjects.RemoveAt(recentProjects.Count - 1);
            }

            UpdateRecentProjectsDisplay();
        }

        private void UpdateRecentProjectsDisplay()
        {
            panelToDo.Controls.Clear();
            panelInProgress.Controls.Clear();
            panelDone.Controls.Clear();

            if (recentProjects.Count == 0)
            {
                var emptyLabel = new Label
                {
                    Text = "Chưa có dự án gần đây\n\nNhấn vào dự án để xem chi tiết",
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = false,
                    Size = new Size(panelTaskColumns.Width - 40, 100),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Location = new Point(20, 80)
                };
                panelToDo.Controls.Add(emptyLabel);
                return;
            }

            var inProgressProjects = recentProjects.Where(p => p.Status == "In Progress").ToList();
            var toDoProjects = recentProjects.Where(p => p.Status == "To Do").ToList();
            var completedProjects = recentProjects.Where(p => p.Status == "Completed").ToList();

            AddRecentProjectsToPanel(panelToDo, inProgressProjects, "In Progress");
            AddRecentProjectsToPanel(panelInProgress, toDoProjects, "To Do");
            AddRecentProjectsToPanel(panelDone, completedProjects, "Completed");
        }

        private void AddRecentProjectsToPanel(Panel targetPanel, List<ProjectData> projects, string statusType)
        {
            if (projects.Count == 0)
            {
                var emptyLabel = new Label
                {
                    Text = $"Chưa có dự án\n{statusType} gần đây",
                    Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = false,
                    Size = new Size(targetPanel.Width - 30, 60),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Location = new Point(15, 50)
                };
                targetPanel.Controls.Add(emptyLabel);
                return;
            }

            int yPos = 50;
            foreach (var project in projects.Take(3))
            {
                DateTime endDate;
                DateTime.TryParse(project.EndDate, out endDate);

                var recentCard = new Panel
                {
                    Width = targetPanel.Width - 30,
                    Height = 80,
                    BackColor = Color.FromArgb(250, 250, 250),
                    Location = new Point(15, yPos),
                    Cursor = Cursors.Hand,
                    Tag = project.ProjectID
                };

                var lblName = new Label
                {
                    Text = project.ProjectName.Length > 25 ? project.ProjectName.Substring(0, 25) + "..." : project.ProjectName,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(88, 86, 214),
                    Location = new Point(10, 10),
                    Size = new Size(recentCard.Width - 20, 25),
                    Cursor = Cursors.Hand
                };

                var lblDate = new Label
                {
                    Text = $"Hạn: {endDate:dd/MM/yyyy}",
                    Font = new Font("Segoe UI", 8F),
                    ForeColor = Color.Gray,
                    Location = new Point(10, 40),
                    AutoSize = true,
                    Cursor = Cursors.Hand
                };

                var lblStatusSmall = new Label
                {
                    Text = project.Status,
                    Font = new Font("Segoe UI", 7F, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = GetStatusColor(project.Status),
                    Location = new Point(10, 58),
                    Size = new Size(80, 18),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                lblStatusSmall.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    using (var path = GetRoundedRectPath(lblStatusSmall.ClientRectangle, 3))
                    {
                        lblStatusSmall.Region = new Region(path);
                    }
                };

                EventHandler clickHandler = (s, e) =>
                {
                    var projectView = new ProjectView(
                        project.ProjectID,
                        project.ProjectName,
                        project.ProjectDescription,
                        endDate.ToString("dd/MM/yyyy"),
                        project.Status,
                        currentUserId,
                        currentUserName
                    );
                    
                    if (projectView.ShowDialog() == DialogResult.OK)
                    {
                        LoadProjectsFromApi();
                    }
                };

                recentCard.Click += clickHandler;
                lblName.Click += clickHandler;
                lblDate.Click += clickHandler;

                recentCard.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    using (var path = GetRoundedRectPath(recentCard.ClientRectangle, 5))
                    {
                        recentCard.Region = new Region(path);
                    }
                };

                recentCard.Controls.AddRange(new Control[] { lblName, lblDate, lblStatusSmall });
                targetPanel.Controls.Add(recentCard);

                yPos += 90;
            }
        }

        private async Task DeleteProject(string projectId, Panel projectPanel)
        {
            try
            {
                projectPanel.Enabled = false;

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.DeleteAsync($"https://nauth.fitlhu.com/api/projects/{projectId}");
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        currentProjects.RemoveAll(p => p.ProjectID == projectId);
                        recentProjects.RemoveAll(p => p.ProjectID == projectId);
                        
                        flowProjectsList.Controls.Remove(projectPanel);
                        projectPanel.Dispose();

                        UpdateStatistics();
                        UpdateRecentProjectsDisplay();

                        if (flowProjectsList.Controls.Count == 0)
                        {
                            ShowEmptyState();
                        }

                        MessageBox.Show("Xóa dự án thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        var errorResult = JsonSerializer.Deserialize<ProjectsApiResponse>(responseContent, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        MessageBox.Show($"Xóa dự án thất bại!\n{errorResult?.Message ?? responseContent}",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        projectPanel.Enabled = true;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Không thể kết nối đến server.\nChi tiết: {ex.Message}",
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                projectPanel.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa dự án: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                projectPanel.Enabled = true;
            }
        }

        private Color GetStatusColor(string status)
        {
            return status switch
            {
                "To Do" => Color.FromArgb(231, 76, 60),
                "In Progress" => Color.FromArgb(241, 196, 15),
                "Completed" => Color.FromArgb(46, 204, 113),
                _ => Color.Gray
            };
        }

        private void btnAddProject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentUserId))
            {
                MessageBox.Show("Không tìm thấy thông tin người dùng. Vui lòng đăng nhập lại.", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var addProjectForm = new AddProject(currentUserId))
            {
                if (addProjectForm.ShowDialog() == DialogResult.OK && addProjectForm.IsSuccess)
                {
                    LoadProjectsFromApi();
                }
            }
        }

        private void UpdateStatistics()
        {
            if (currentProjects == null || currentProjects.Count == 0)
            {
                lblStat1Value.Text = "0";
                lblStat2Value.Text = "0";
                lblStat3Value.Text = "0";
                return;
            }

            int totalProjects = currentProjects.Count;
            int inProgressCount = currentProjects.Count(p => p.Status == "In Progress");
            int completedCount = currentProjects.Count(p => p.Status == "Completed");

            lblStat1Value.Text = totalProjects.ToString();
            lblStat2Value.Text = inProgressCount.ToString();
            lblStat3Value.Text = completedCount.ToString();
        }
    }

    public class ProjectsApiResponse
    {
        public string Message { get; set; }
        public List<ProjectData> Data { get; set; }
    }

    public class ProjectData
    {
        [System.Text.Json.Serialization.JsonPropertyName("ProjectID")]
        public string ProjectID { get; set; }
        
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public string OwnerUserID { get; set; }
    }
}
