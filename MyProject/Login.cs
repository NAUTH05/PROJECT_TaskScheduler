using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MyProject
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            txtUsernameOrEmail.KeyDown += TextBox_KeyDown;
            txtPassword.KeyDown += TextBox_KeyDown;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txtUsernameOrEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

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

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsernameOrEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            HttpClient client = new HttpClient();
            
            try
            {
                var userInput = txtUsernameOrEmail.Text.Trim();
                object loginData;
                
                if (IsValidEmail(userInput))
                {
                    loginData = new
                    {
                        email = userInput,
                        password = txtPassword.Text
                    };
                }
                else
                {
                    loginData = new
                    {
                        userName = userInput,
                        password = txtPassword.Text
                    };
                }

                var json = JsonSerializer.Serialize(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://nauth.fitlhu.com/api/login", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var result = JsonSerializer.Deserialize<ApiResponse>(responseContent, options);

                    string welcomeName = result?.Data?.UserName ?? result?.Data?.Email;
                    MessageBox.Show($"Đăng nhập thành công!\nChào mừng {welcomeName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    string userName = result?.Data?.UserName ?? result?.Data?.Email ?? "User";
                    string userId = result?.Data?._id ?? "";
                    
                    var mainForm = new MainForm(userName, userId);
                    this.Hide();

                    mainForm.FormClosed += (s, args) =>
                    {
                        this.Show();
                        txtUsernameOrEmail.Clear();
                        txtPassword.Clear();
                        txtUsernameOrEmail.Focus();
                    };
                    
                    mainForm.Show();
                }
                else
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var errorResult = JsonSerializer.Deserialize<ApiResponse>(responseContent, options);

                    MessageBox.Show($"Đăng nhập thất bại!\n{errorResult?.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            finally
            {
                client.Dispose();
            }
        }

        private void btnLogin_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var registerForm = new Register();
            registerForm.ShowDialog();

            if (registerForm.DialogResult == DialogResult.OK)
            {
                txtUsernameOrEmail.Clear();
                txtPassword.Clear();
                txtUsernameOrEmail.Focus();
            }
        }
    }

    public class ApiResponse
    {
        public string Message { get; set; }
        public UserData Data { get; set; }
    }

    public class UserData
    {
        public string _id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
