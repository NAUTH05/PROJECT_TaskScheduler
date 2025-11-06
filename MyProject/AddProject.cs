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

namespace MyProject
{
    public partial class AddProject : Form
    {
        private bool isProjectNamePlaceholder = true;
        private bool isDescriptionPlaceholder = true;
        private string currentUserId;
        
        public string ProjectName { get; private set; }
        public string ProjectDescription { get; private set; }
        public DateTime Deadline { get; private set; }
        public string Status { get; private set; }
        public bool IsSuccess { get; private set; }

        public AddProject(string userId)
        {
            InitializeComponent();
            currentUserId = userId;
            InitializeForm();
        }

        private void InitializeForm()
        {
            cboStatus.SelectedIndex = 1;
            dtpDeadline.Value = DateTime.Now.AddDays(30);
            
            this.BackColor = Color.FromArgb(240, 240, 240);
            
            btnCreate.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(btnCreate.ClientRectangle, 5))
                {
                    btnCreate.Region = new Region(path);
                }
            };

            btnCancel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(btnCancel.ClientRectangle, 5))
                {
                    btnCancel.Region = new Region(path);
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

        private void txtProjectName_Enter(object sender, EventArgs e)
        {
            if (isProjectNamePlaceholder)
            {
                txtProjectName.Text = "";
                txtProjectName.ForeColor = Color.Black;
                isProjectNamePlaceholder = false;
            }
        }

        private void txtProjectName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProjectName.Text))
            {
                txtProjectName.Text = "Ví dụ: Triển khai ứng dụng TaskScheduler";
                txtProjectName.ForeColor = Color.Gray;
                isProjectNamePlaceholder = true;
            }
        }

        private void txtDescription_Enter(object sender, EventArgs e)
        {
            if (isDescriptionPlaceholder)
            {
                txtDescription.Text = "";
                txtDescription.ForeColor = Color.Black;
                isDescriptionPlaceholder = false;
            }
        }

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                txtDescription.Text = "Mô tả chi tiết về mục tiêu và phạm vi dự án.";
                txtDescription.ForeColor = Color.Gray;
                isDescriptionPlaceholder = true;
            }
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            if (isProjectNamePlaceholder || string.IsNullOrWhiteSpace(txtProjectName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên dự án.", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProjectName.Focus();
                return;
            }

            if (isDescriptionPlaceholder || string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Vui lòng nhập mô tả dự án.", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescription.Focus();
                return;
            }

            if (cboStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn trạng thái ban đầu.", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboStatus.Focus();
                return;
            }

            btnCreate.Enabled = false;
            btnCancel.Enabled = false;
            btnCreate.Text = "Đang tạo...";

            HttpClient client = new HttpClient();
            
            try
            {
                string statusValue = MapStatusToEnglish(cboStatus.SelectedItem.ToString());

                var projectData = new
                {
                    ProjectName = txtProjectName.Text.Trim(),
                    ProjectDescription = txtDescription.Text.Trim(),
                    StartDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    EndDate = dtpDeadline.Value.ToString("yyyy-MM-dd"),
                    Status = statusValue,
                    OwnerUserID = currentUserId
                };

                var json = JsonSerializer.Serialize(projectData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://nauth.fitlhu.com/api/projects", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var result = JsonSerializer.Deserialize<AddProjectApiResponse>(responseContent, options);

                    ProjectName = txtProjectName.Text.Trim();
                    ProjectDescription = txtDescription.Text.Trim();
                    Deadline = dtpDeadline.Value;
                    Status = cboStatus.SelectedItem.ToString();
                    IsSuccess = true;

                    MessageBox.Show($"Tạo dự án thành công!\n{result?.Message}", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var errorResult = JsonSerializer.Deserialize<AddProjectApiResponse>(responseContent, options);

                    MessageBox.Show($"Tạo dự án thất bại!\n{errorResult?.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    IsSuccess = false;
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Không thể kết nối đến server.\nVui lòng kiểm tra:\n- Server đang chạy\n- URL đúng\n\nChi tiết: {ex.Message}", 
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsSuccess = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsSuccess = false;
            }
            finally
            {
                client.Dispose();
                btnCreate.Enabled = true;
                btnCancel.Enabled = true;
                btnCreate.Text = "Tạo Dự Án";
            }
        }

        private string MapStatusToEnglish(string vietnameseStatus)
        {
            return vietnameseStatus switch
            {
                "Đang Tiến Hành (In Progress)" => "In Progress",
                "Chờ Làm (To Do)" => "To Do",
                "Hoàn Thành (Completed)" => "Completed",
                _ => "To Do"
            };
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    public class AddProjectApiResponse
    {
        public string Message { get; set; }
        public AddProjectData Data { get; set; }
    }

    public class AddProjectData
    {
        public string _id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public string OwnerUserID { get; set; }
    }
}