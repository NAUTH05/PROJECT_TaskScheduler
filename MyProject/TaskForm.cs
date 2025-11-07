using System;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace MyProject
{
    public partial class TaskForm : Form
    {
        private string projectId;
        private string currentUserId;
        private bool isTaskNamePlaceholder = true;
        private bool isDescriptionPlaceholder = true;

        public bool IsSuccess { get; private set; }

        public TaskForm(string projectId, string userId)
        {
            InitializeComponent();
            this.projectId = projectId;
            this.currentUserId = userId;
            InitializeForm();
        }

        private void InitializeForm()
        {
            cboPriority.SelectedIndex = 1;
            cboStatus.SelectedIndex = 0;
            dtpDueDate.Value = DateTime.Now.AddDays(7);
            
            txtTaskName.Text = "Ví dụ: Viết API cho CRUD Task";
            txtTaskName.ForeColor = System.Drawing.Color.Gray;
            
            txtDescription.Text = "Chi tiết yêu cầu và tiêu chí hoàn thành.";
            txtDescription.ForeColor = System.Drawing.Color.Gray;
            
            this.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
        }

        private void txtTaskName_Enter(object sender, EventArgs e)
        {
            if (isTaskNamePlaceholder)
            {
                txtTaskName.Text = "";
                txtTaskName.ForeColor = System.Drawing.Color.Black;
                isTaskNamePlaceholder = false;
            }
        }

        private void txtTaskName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskName.Text))
            {
                txtTaskName.Text = "Ví dụ: Viết API cho CRUD Task";
                txtTaskName.ForeColor = System.Drawing.Color.Gray;
                isTaskNamePlaceholder = true;
            }
        }

        private void txtDescription_Enter(object sender, EventArgs e)
        {
            if (isDescriptionPlaceholder)
            {
                txtDescription.Text = "";
                txtDescription.ForeColor = System.Drawing.Color.Black;
                isDescriptionPlaceholder = false;
            }
        }

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                txtDescription.Text = "Chi tiết yêu cầu và tiêu chí hoàn thành.";
                txtDescription.ForeColor = System.Drawing.Color.Gray;
                isDescriptionPlaceholder = true;
            }
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            if (isTaskNamePlaceholder || string.IsNullOrWhiteSpace(txtTaskName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhiệm vụ.", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTaskName.Focus();
                return;
            }

            if (isDescriptionPlaceholder || string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Vui lòng nhập mô tả nhiệm vụ.", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescription.Focus();
                return;
            }

            if (cboPriority.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn mức độ ưu tiên.", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboPriority.Focus();
                return;
            }

            if (cboStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn trạng thái.", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboStatus.Focus();
                return;
            }

            btnCreate.Enabled = false;
            btnCancel.Enabled = false;
            btnCreate.Text = "Đang tạo...";

            try
            {
                var taskData = new
                {
                    ProjectID = projectId,
                    TaskName = txtTaskName.Text.Trim(),
                    TaskDescription = txtDescription.Text.Trim(),
                    DueDate = dtpDueDate.Value.ToString("yyyy-MM-dd"),
                    Priority = cboPriority.SelectedItem.ToString(),
                    Status = cboStatus.SelectedItem.ToString(),
                    AssignedToUserID = currentUserId
                };

                var response = await ApiHelper.PostAsync("tasks", taskData);
                
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
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var result = JsonSerializer.Deserialize<TaskApiResponse>(responseContent, options);

                    IsSuccess = true;

                    MessageBox.Show($"Tạo nhiệm vụ thành công!\n{result?.Message}", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Tạo nhiệm vụ thất bại!\n{responseContent}", "Lỗi", 
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
                btnCreate.Enabled = true;
                btnCancel.Enabled = true;
                btnCreate.Text = "Tạo Nhiệm Vụ";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public class TaskApiResponse
        {
            public string Message { get; set; }
            public string TaskId { get; set; }
            public TaskData Data { get; set; }
        }

        public class TaskData
        {
            public string TaskID { get; set; }
            public string ProjectID { get; set; }
            public string TaskName { get; set; }
            public string TaskDescription { get; set; }



            public string DueDate { get; set; }
            public string Priority { get; set; }
            public string Status { get; set; }
            public string AssignedToUserID { get; set; }
        }
    }
}
