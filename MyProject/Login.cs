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
            
            LoadSavedToken();
        }

        private void LoadSavedToken()
        {
            if (AuthManager.LoadToken())
            {
                var mainForm = new MainForm(AuthManager.UserName, AuthManager.UserId);
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

                var response = await ApiHelper.PostAsync("login", loginData);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var result = JsonSerializer.Deserialize<LoginResponse>(responseContent, options);

                    AuthManager.Token = result?.Token;
                    AuthManager.UserId = result?.Data?._id;
                    AuthManager.UserName = result?.Data?.UserName ?? result?.Data?.Email;
                    AuthManager.Email = result?.Data?.Email;
                    AuthManager.SaveToken();

                    string welcomeName = result?.Data?.UserName ?? result?.Data?.Email;
                    MessageBox.Show($"Đăng nhập thành công!\nChào mừng {welcomeName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    var mainForm = new MainForm(AuthManager.UserName, AuthManager.UserId);
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
                    var errorResult = JsonSerializer.Deserialize<LoginResponse>(responseContent, options);

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

    public class LoginResponse
    {
        public string Message { get; set; }
        public string Token { get; set; }
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
