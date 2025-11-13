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
    public partial class AddMemberDialog : Form
    {
        private string projectId;
        private TextBox? txtSearch;
        private FlowLayoutPanel? flowUsers;
        private FlowLayoutPanel? flowCurrentMembers;
        private Label? lblInstruction;
        private Label? lblCurrentMembersTitle;
        private Button? btnClose;
        private List<UserSearchResult> availableUsers = new List<UserSearchResult>();
        private List<ProjectMember> currentMembers = new List<ProjectMember>();

        public bool MemberAdded { get; private set; }
        public bool MemberRemoved { get; private set; }

        public AddMemberDialog(string projectId)
        {
            this.projectId = projectId;
            InitializeComponent();
            LoadCurrentMembers();
            LoadAvailableUsers();
        }

        private void InitializeComponent()
        {
            this.Text = "Quản Lý Thành Viên Dự Án";
            this.Size = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            var lblTitle = new Label
            {
                Text = "Quản Lý Thành Viên Dự Án",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Location = new Point(30, 30),
                AutoSize = true,
                ForeColor = Color.FromArgb(44, 62, 80)
            };

            lblCurrentMembersTitle = new Label
            {
                Text = "Thành Viên Hiện Tại",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(30, 75),
                AutoSize = true,
                ForeColor = Color.FromArgb(44, 62, 80)
            };

            flowCurrentMembers = new FlowLayoutPanel
            {
                Location = new Point(30, 110),
                Size = new Size(380, 180),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.FromArgb(250, 250, 250),
                Padding = new Padding(10)
            };

            var lblAvailableTitle = new Label
            {
                Text = "Thêm Thành Viên Mới",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(30, 310),
                AutoSize = true,
                ForeColor = Color.FromArgb(44, 62, 80)
            };

            txtSearch = new TextBox
            {
                Location = new Point(30, 345),
                Size = new Size(380, 30),
                Font = new Font("Segoe UI", 11F),
                PlaceholderText = "Tìm kiếm người dùng..."
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;

            lblInstruction = new Label
            {
                Text = "Chọn người dùng đã thêm vào dự án này.",
                Font = new Font("Segoe UI", 9F),
                Location = new Point(30, 385),
                Size = new Size(380, 20),
                ForeColor = Color.Gray
            };

            flowUsers = new FlowLayoutPanel
            {
                Location = new Point(30, 415),
                Size = new Size(380, 180),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.FromArgb(250, 250, 250),
                Padding = new Padding(10)
            };

            btnClose = new Button
            {
                Text = "Đóng",
                Location = new Point(790, 605),
                Size = new Size(80, 35),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lblTitle, lblCurrentMembersTitle, flowCurrentMembers, lblAvailableTitle, txtSearch, lblInstruction, flowUsers, btnClose });
        }

        private async void LoadAvailableUsers()
        {
            try
            {
                var response = await ApiHelper.GetAsync($"projects/{projectId}/available-users");
                
                if (ApiHelper.IsUnauthorized(response))
                {
                    MessageBox.Show("Phiên đăng nhập đã hết hạn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AuthManager.Logout();
                    this.Close();
                    return;
                }

                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<AvailableUsersResponse>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    availableUsers = result?.Data ?? new List<UserSearchResult>();
                    DisplayUsers(availableUsers);
                }
                else
                {
                    MessageBox.Show("Không thể tải danh sách người dùng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadCurrentMembers()
        {
            try
            {
                var response = await ApiHelper.GetAsync($"projects/{projectId}/members");
                if (!response.IsSuccessStatusCode) return;

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<MembersResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                currentMembers = result?.Members ?? new List<ProjectMember>();
                DisplayCurrentMembers();
            }
            catch { }
        }

        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            var searchText = txtSearch?.Text.ToLower().Trim() ?? "";
            if (string.IsNullOrEmpty(searchText))
            {
                DisplayUsers(availableUsers);
            }
            else
            {
                var filtered = availableUsers.Where(u => 
                    u.UserName.ToLower().Contains(searchText) || 
                    u.Email.ToLower().Contains(searchText)
                ).ToList();
                DisplayUsers(filtered);
            }
        }

        private void DisplayUsers(List<UserSearchResult> users)
        {
            flowUsers!.Controls.Clear();

            if (users == null || users.Count == 0)
            {
                var emptyLabel = new Label
                {
                    Text = "Không có người dùng khả dụng.",
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Margin = new Padding(10)
                };
                flowUsers.Controls.Add(emptyLabel);
                return;
            }

            foreach (var user in users)
            {
                var userPanel = CreateUserCard(user);
                flowUsers.Controls.Add(userPanel);
            }
        }

        private void DisplayCurrentMembers()
        {
            flowCurrentMembers!.Controls.Clear();

            if (currentMembers == null || currentMembers.Count == 0)
            {
                var emptyLabel = new Label
                {
                    Text = "Chưa có thành viên nào.",
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Margin = new Padding(10)
                };
                flowCurrentMembers.Controls.Add(emptyLabel);
                return;
            }

            foreach (var member in currentMembers)
            {
                var memberCard = CreateMemberCard(member);
                flowCurrentMembers.Controls.Add(memberCard);
            }
        }

        private Panel CreateUserCard(UserSearchResult user)
        {
            var panel = new Panel
            {
                Size = new Size(420, 60),
                BackColor = Color.White,
                Margin = new Padding(5),
                Padding = new Padding(10)
            };

            var lblInitial = new Label
            {
                Text = GetInitials(user.UserName),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = GetRandomColor(user.UserId),
                Size = new Size(40, 40),
                Location = new Point(10, 10),
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblInitial.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(lblInitial.ClientRectangle, 20))
                {
                    lblInitial.Region = new Region(path);
                }
            };

            var lblName = new Label
            {
                Text = user.UserName,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Location = new Point(60, 12),
                AutoSize = true,
                ForeColor = Color.FromArgb(44, 62, 80)
            };

            var lblEmail = new Label
            {
                Text = user.Email,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(60, 32),
                AutoSize = true,
                ForeColor = Color.Gray
            };

            var btnAdd = new Button
            {
                Text = "Thêm",
                Size = new Size(70, 30),
                Location = new Point(330, 15),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(88, 86, 214),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = user
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += BtnAdd_Click;

            panel.Controls.AddRange(new Control[] { lblInitial, lblName, lblEmail, btnAdd });
            return panel;
        }

        private async void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (sender is Button btnAdd && btnAdd.Tag is UserSearchResult user)
            {
                await AddMemberAsync(user, btnAdd);
            }
        }

        private Panel CreateMemberCard(ProjectMember member)
        {
            var panel = new Panel
            {
                Size = new Size(340, 60),
                BackColor = Color.White,
                Margin = new Padding(5),
                Padding = new Padding(10)
            };

            var lblInitial = new Label
            {
                Text = GetInitials(member.UserName),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = GetRandomColor(member.UserID),
                Size = new Size(40, 40),
                Location = new Point(10, 10),
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblInitial.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(lblInitial.ClientRectangle, 20))
                {
                    lblInitial.Region = new Region(path);
                }
            };

            var lblName = new Label
            {
                Text = member.UserName,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Location = new Point(60, 12),
                AutoSize = true,
                ForeColor = Color.FromArgb(44, 62, 80)
            };

            var lblEmail = new Label
            {
                Text = member.Email,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(60, 32),
                AutoSize = true,
                ForeColor = Color.Gray
            };

            var btnRemove = new Button
            {
                Text = "x Gỡ",
                Size = new Size(60, 30),
                Location = new Point(260, 15),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = new Tuple<ProjectMember, Panel>(member, panel)
            };
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.Click += BtnRemove_Click;

            panel.Controls.AddRange(new Control[] { lblInitial, lblName, lblEmail, btnRemove });
            return panel;
        }

        private async void BtnRemove_Click(object? sender, EventArgs e)
        {
            if (sender is Button btnRemove && btnRemove.Tag is Tuple<ProjectMember, Panel> tuple)
            {
                await RemoveMemberAsync(tuple.Item1, tuple.Item2);
            }
        }

        private async Task AddMemberAsync(UserSearchResult user, Button btnAdd)
        {
            try
            {
                btnAdd.Enabled = false;
                btnAdd.Text = "Đang thêm...";

                var requestData = new
                {
                    userId = user.UserId,
                    role = "member"
                };

                var response = await ApiHelper.PostAsync($"projects/{projectId}/members", requestData);

                if (ApiHelper.IsUnauthorized(response))
                {
                    MessageBox.Show("Phiên đăng nhập đã hết hạn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AuthManager.Logout();
                    this.Close();
                    return;
                }

                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Đã thêm {user.UserName} vào dự án!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MemberAdded = true;
                    
                    availableUsers.Remove(user);
                    DisplayUsers(availableUsers);
                    LoadCurrentMembers();
                }
                else
                {
                    var error = JsonSerializer.Deserialize<ErrorResponse>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    MessageBox.Show($"Không thể thêm thành viên!\n{error?.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnAdd.Enabled = true;
                    btnAdd.Text = "Thêm";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnAdd.Enabled = true;
                btnAdd.Text = "Thêm";
            }
        }

        private async Task RemoveMemberAsync(ProjectMember member, Panel panel)
        {
            var result = MessageBox.Show(
                $"Bạn có chắc muốn gỡ {member.UserName} khỏi dự án?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes) return;

            try
            {
                var response = await ApiHelper.DeleteAsync($"projects/{projectId}/members/{member.UserID}");

                if (ApiHelper.IsUnauthorized(response))
                {
                    MessageBox.Show("Phiên đăng nhập đã hết hạn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AuthManager.Logout();
                    this.Close();
                    return;
                }

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Đã gỡ {member.UserName} khỏi dự án!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MemberRemoved = true;
                    
                    currentMembers.Remove(member);
                    DisplayCurrentMembers();
                    
                    LoadAvailableUsers();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Không thể gỡ thành viên!\n{error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetInitials(string name)
        {
            if (string.IsNullOrEmpty(name)) return "?";
            var parts = name.Split(' ');
            if (parts.Length >= 2)
                return $"{parts[0][0]}{parts[parts.Length - 1][0]}".ToUpper();
            return name.Length >= 2 ? name.Substring(0, 2).ToUpper() : name.ToUpper();
        }

        private Color GetRandomColor(string seed)
        {
            var colors = new[]
            {
                Color.FromArgb(88, 86, 214),
                Color.FromArgb(46, 204, 113),
                Color.FromArgb(231, 76, 60),
                Color.FromArgb(241, 196, 15),
                Color.FromArgb(155, 89, 182),
                Color.FromArgb(52, 152, 219)
            };
            var hash = seed.GetHashCode();
            return colors[Math.Abs(hash) % colors.Length];
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

        public class AvailableUsersResponse
        {
            public string? Message { get; set; }
            public int Count { get; set; }
            public List<UserSearchResult>? Data { get; set; }
        }

        public class MembersResponse
        {
            public string? Message { get; set; }
            public List<ProjectMember>? Members { get; set; }
        }

        public class ProjectMember
        {
            public string? MemberID { get; set; }
            public string? UserID { get; set; }
            public string? UserName { get; set; }
            public string? Email { get; set; }
            public string? Role { get; set; }
        }

        public class UserSearchResult
        {
            public string? UserId { get; set; }
            public string? UserName { get; set; }
            public string? Email { get; set; }
        }

        public class ErrorResponse
        {
            public string? Message { get; set; }
        }
    }
}
