using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MyProject
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();

            txtUsername.KeyDown += TextBox_KeyDown;
            txtEmail.KeyDown += TextBox_KeyDown;
            txtPassword.KeyDown += TextBox_KeyDown;
            txtConfirmPassword.KeyDown += TextBox_KeyDown;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRegister_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtUsername.Text.Trim().Length < 3)
            {
                MessageBox.Show("Username phải có ít nhất 3 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (!IsValidEmail(txtEmail.Text.Trim()))
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }
            
            try
            {
                var registerData = new
                {
                    userName = txtUsername.Text.Trim(),
                    email = txtEmail.Text.Trim(),
                    password = txtPassword.Text
                };

                var response = await ApiHelper.PostAsync("register", registerData);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var result = JsonSerializer.Deserialize<RegisterApiResponse>(responseContent, options);

                    AuthManager.Token = result?.Token;
                    AuthManager.UserId = result?.Data?._id;
                    AuthManager.UserName = result?.Data?.UserName;
                    AuthManager.Email = result?.Data?.Email;
                    AuthManager.SaveToken();

                    MessageBox.Show($"Đăng ký thành công!\n{result?.Message}\n\nBạn có thể đăng nhập ngay bây giờ.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var errorResult = JsonSerializer.Deserialize<RegisterApiResponse>(responseContent, options);

                    MessageBox.Show($"Đăng ký thất bại!\n{errorResult?.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Không thể kết nối đến server.\nVui lòng kiểm tra:\n- Server đang chạy\n- URL đúng\n\nChi tiết: {ex.Message}", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }

    public class RegisterApiResponse
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public RegisterUserData Data { get; set; }
    }

    public class RegisterUserData
    {
        public string _id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
