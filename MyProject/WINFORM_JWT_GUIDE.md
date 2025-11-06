# Hướng dẫn tích hợp JWT với .NET WinForm

## 📌 Cách hoạt động

1. **Login/Register** → Backend trả về `token`
2. **Lưu token** trong WinForm (biến static hoặc file)
3. **Gửi token** trong header cho mọi request tiếp theo

---

## 🔐 1. API Authentication Endpoints

### Register (Không cần token)

```
POST http://localhost:3300/api/register
Content-Type: application/json

{
  "userName": "testuser",
  "email": "test@email.com",
  "password": "123456"
}
```

**Response:**

```json
{
  "message": "Đăng ký thành công",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "data": {
    "_id": "abc123",
    "userName": "testuser",
    "email": "test@email.com"
  }
}
```

### Login (Không cần token)

```
POST http://localhost:3300/api/login
Content-Type: application/json

{
  "userName": "testuser",
  "password": "123456"
}
```

**Response:**

```json
{
  "message": "Đăng nhập thành công",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "data": {
    "_id": "abc123",
    "userName": "testuser",
    "email": "test@email.com"
  }
}
```

---

## 🔒 2. Protected APIs (Cần token)

Tất cả các API sau đây **BẮT BUỘC** phải có token trong header:

- `GET/POST /api/projects`
- `GET/PUT/DELETE /api/projects/:id`
- `GET/POST /api/tasks`
- `GET/PUT/DELETE /api/tasks/:id`

---

## 💻 3. Code mẫu cho WinForm C#

### A. Class để quản lý Token

```csharp
// AuthManager.cs
public static class AuthManager
{
    public static string Token { get; set; }
    public static string UserId { get; set; }
    public static string UserName { get; set; }

    public static bool IsLoggedIn()
    {
        return !string.IsNullOrEmpty(Token);
    }

    public static void Logout()
    {
        Token = null;
        UserId = null;
        UserName = null;
    }
}
```

### B. Login Form

```csharp
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;

public partial class LoginForm : Form
{
    private static readonly HttpClient client = new HttpClient();

    private async void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            var loginData = new
            {
                userName = txtUsername.Text,
                password = txtPassword.Text
            };

            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(
                "http://localhost:3300/api/login",
                content
            );

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(responseString);

            if (response.IsSuccessStatusCode)
            {
                // Lưu token
                AuthManager.Token = result.token;
                AuthManager.UserId = result.data._id;
                AuthManager.UserName = result.data.userName;

                MessageBox.Show("Đăng nhập thành công!");

                // Mở form chính
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(result.message.ToString());
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi: " + ex.Message);
        }
    }
}
```

### C. Gọi API có bảo vệ (với Token)

```csharp
// ApiHelper.cs
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public static class ApiHelper
{
    private static readonly HttpClient client = new HttpClient();

    // GET request
    public static async Task<string> GetAsync(string url)
    {
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", AuthManager.Token);

        var response = await client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }

    // POST request
    public static async Task<string> PostAsync(string url, object data)
    {
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", AuthManager.Token);

        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(url, content);
        return await response.Content.ReadAsStringAsync();
    }

    // PUT request
    public static async Task<string> PutAsync(string url, object data)
    {
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", AuthManager.Token);

        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PutAsync(url, content);
        return await response.Content.ReadAsStringAsync();
    }

    // DELETE request
    public static async Task<string> DeleteAsync(string url)
    {
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", AuthManager.Token);

        var response = await client.DeleteAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
}
```

### D. Ví dụ sử dụng trong MainForm

```csharp
// MainForm.cs
private async void btnLoadProjects_Click(object sender, EventArgs e)
{
    try
    {
        // Kiểm tra đã login chưa
        if (!AuthManager.IsLoggedIn())
        {
            MessageBox.Show("Vui lòng đăng nhập!");
            return;
        }

        // Gọi API với token
        string response = await ApiHelper.GetAsync(
            "http://localhost:3300/api/projects"
        );

        var result = JsonConvert.DeserializeObject<dynamic>(response);

        // Hiển thị dữ liệu
        dataGridView1.DataSource = result.data;
    }
    catch (HttpRequestException ex)
    {
        if (ex.Message.Contains("401") || ex.Message.Contains("403"))
        {
            MessageBox.Show("Token hết hạn, vui lòng đăng nhập lại!");
            AuthManager.Logout();
            // Quay về login form
        }
        else
        {
            MessageBox.Show("Lỗi: " + ex.Message);
        }
    }
}

private async void btnCreateProject_Click(object sender, EventArgs e)
{
    try
    {
        var projectData = new
        {
            ProjectName = txtProjectName.Text,
            ProjectDescription = txtDescription.Text,
            StartDate = dtpStartDate.Value.ToString("yyyy-MM-dd"),
            EndDate = dtpEndDate.Value.ToString("yyyy-MM-dd"),
            Status = cboStatus.SelectedItem.ToString(),
            OwnerUserID = AuthManager.UserId
        };

        string response = await ApiHelper.PostAsync(
            "http://localhost:3300/api/projects",
            projectData
        );

        var result = JsonConvert.DeserializeObject<dynamic>(response);
        MessageBox.Show(result.message.ToString());
    }
    catch (Exception ex)
    {
        MessageBox.Show("Lỗi: " + ex.Message);
    }
}
```

---

## 📦 4. Cài đặt NuGet Packages cho WinForm

```
Install-Package Newtonsoft.Json
```

Hoặc qua NuGet Package Manager:

1. Tools → NuGet Package Manager → Manage NuGet Packages for Solution
2. Tìm "Newtonsoft.Json"
3. Install

---

## ⚠️ 5. Xử lý lỗi Token

### Token hết hạn (403 Forbidden)

```csharp
try
{
    // API call
}
catch (HttpRequestException ex)
{
    if (ex.StatusCode == System.Net.HttpStatusCode.Forbidden)
    {
        MessageBox.Show("Phiên đăng nhập hết hạn!");
        AuthManager.Logout();
        // Redirect to login
        LoginForm loginForm = new LoginForm();
        loginForm.Show();
        this.Close();
    }
}
```

### Token không tồn tại (401 Unauthorized)

```csharp
if (!AuthManager.IsLoggedIn())
{
    MessageBox.Show("Vui lòng đăng nhập!");
    // Redirect to login
}
```

---

## 🔑 6. Lưu Token vào file (Optional - để auto login)

```csharp
// Lưu token
public static void SaveToken(string token)
{
    File.WriteAllText("token.txt", token);
}

// Đọc token
public static string LoadToken()
{
    if (File.Exists("token.txt"))
    {
        return File.ReadAllText("token.txt");
    }
    return null;
}

// Sử dụng khi khởi động app
private void MainForm_Load(object sender, EventArgs e)
{
    string savedToken = LoadToken();
    if (!string.IsNullOrEmpty(savedToken))
    {
        AuthManager.Token = savedToken;
        // Kiểm tra token còn hợp lệ không
    }
}
```

---

## 📝 7. Test API bằng Postman trước

### Test Login

```
POST http://localhost:3300/api/login
Body (JSON):
{
  "userName": "testuser",
  "password": "123456"
}
```

### Copy token từ response

### Test API có bảo vệ

```
GET http://localhost:3300/api/projects
Headers:
Authorization: Bearer YOUR_TOKEN_HERE
```

---

## ✅ 8. Checklist Implementation

- [ ] Tạo AuthManager class
- [ ] Tạo LoginForm với logic lưu token
- [ ] Tạo ApiHelper với các method GET/POST/PUT/DELETE
- [ ] Thêm token vào header cho mọi request
- [ ] Xử lý lỗi 401/403 (redirect về login)
- [ ] Test login → lấy token → gọi API protected
- [ ] (Optional) Lưu token vào file

---

## 🎯 9. Lưu ý quan trọng

1. **Token expires sau 7 ngày** (có thể thay đổi trong .env)
2. **Không share token** giữa nhiều user
3. **Logout** phải clear token
4. **Login lại** khi token hết hạn
5. **HTTPS** trong production (không để lộ token)

---

## 📞 Support

Nếu gặp lỗi:

- 401: Token không có hoặc sai format
- 403: Token hết hạn hoặc không hợp lệ
- 400: Thiếu thông tin bắt buộc
- 500: Lỗi server
