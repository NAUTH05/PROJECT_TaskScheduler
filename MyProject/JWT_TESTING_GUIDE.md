# ?? H??ng d?n Test JWT Authentication

## ? Quick Test Checklist

### 1. **Test Login Flow** (5 phút)

```
1. Kh?i ??ng app
2. ??ng nh?p v?i tài kho?n h?p l?
   ? Token ???c l?u vào token.dat
   ? Vào dashboard thành công
   ? Hi?n th? username ? góc ph?i

3. ?óng app hoàn toàn
4. M? l?i app
   ? T? ??ng vào dashboard (không c?n login)
   ? D? li?u load ???c

5. Click vào avatar ? Ch?n "Có" ?? logout
   ? Token.dat b? xóa
   ? Quay v? màn hình login
```

### 2. **Test CRUD Operations** (10 phút)

#### **A. Projects**
```
1. Login ? Dashboard

2. T?o d? án m?i:
   - Click "+ D? Án M?i"
   - Nh?p thông tin
   - Click "T?o D? Án"
   ? D? án ???c t?o thành công
   ? D? án hi?n trong danh sách
   ? Th?ng kê c?p nh?t

3. C?p nh?t tr?ng thái d? án:
   - Click vào d? án
   - ??i tr?ng thái (dropdown)
   ? C?p nh?t thành công
   ? Thông báo hi?n th?

4. Xóa d? án:
   - Click nút ???
   - Xác nh?n "Có"
   ? D? án b? xóa
   ? Th?ng kê c?p nh?t
```

#### **B. Tasks**
```
1. Click vào m?t d? án ? M? ProjectView

2. T?o task m?i:
   - Click "+ Nhi?m V? M?i"
   - Nh?p thông tin
   - Click "T?o Nhi?m V?"
   ? Task ???c t?o
   ? Task hi?n ?úng c?t (To Do/In Progress/Done)

3. Di chuy?n task:
   - Right-click vào task
   - Ch?n "Chuy?n sang..."
   ? Task di chuy?n sang c?t m?i
   ? Thông báo thành công

4. Xóa task:
   - Right-click ? "Xóa nhi?m v?"
   - Xác nh?n "Có"
   ? Task b? xóa
```

### 3. **Test Token Expiration** (Manual)

#### **Option 1: Thay ??i token trong token.dat**
```
1. Login thành công
2. ?óng app
3. M? token.dat (Notepad)
4. Xóa ho?c s?a token
5. L?u file
6. M? l?i app
   ? Thông báo "Phiên ??ng nh?p ?ã h?t h?n"
   ? Auto logout
   ? Quay v? login
```

#### **Option 2: Wait 7 days** ?
```
(Token t? ??ng h?t h?n sau 7 ngày)
```

### 4. **Test Error Handling** (5 phút)

```
1. T?t backend server
2. Th? t?o project/task
   ? Hi?n th? "Không th? k?t n?i ??n server"
   ? App không crash

3. B?t l?i server
4. Th? l?i
   ? Ho?t ??ng bình th??ng
```

## ?? Common Issues & Solutions

### Issue 1: "Không th? k?t n?i ??n server"
**Nguyên nhân**: Backend ch?a ch?y ho?c URL sai  
**Gi?i pháp**: 
- Ki?m tra backend ?ang ch?y t?i `https://nauth.fitlhu.com`
- Ki?m tra `ApiHelper.cs` ? BaseUrl

### Issue 2: Token không l?u
**Nguyên nhân**: Quy?n write file  
**Gi?i pháp**:
- Ch?y app v?i quy?n admin
- Ki?m tra folder có quy?n write

### Issue 3: "Phiên ??ng nh?p ?ã h?t h?n" ngay sau login
**Nguyên nhân**: Backend không tr? v? token ho?c token format sai  
**Gi?i pháp**:
- Check response t? API login
- Debug `AuthManager.Token` có giá tr? không

### Issue 4: Auto-login không ho?t ??ng
**Nguyên nhân**: token.dat b? xóa ho?c corrupt  
**Gi?i pháp**:
- Ki?m tra file token.dat t?n t?i
- Login l?i ?? t?o token m?i

## ?? Debug Tips

### 1. Ki?m tra Token
```csharp
// Thêm vào Login.cs sau khi login thành công
MessageBox.Show($"Token: {AuthManager.Token?.Substring(0, 20)}...");
```

### 2. Ki?m tra API Response
```csharp
// Thêm vào ApiHelper.cs
System.Diagnostics.Debug.WriteLine($"Response Status: {response.StatusCode}");
System.Diagnostics.Debug.WriteLine($"Response Body: {await response.Content.ReadAsStringAsync()}");
```

### 3. Ki?m tra Token File
```
M? th? m?c: bin\Debug\net9.0-windows\
Tìm file: token.dat
M? b?ng Notepad ? Xem có token không
```

## ?? Expected Behavior

### ? Successful Scenarios

| Action | Expected Result |
|--------|----------------|
| First login | Token saved, dashboard shown |
| Reopen app | Auto login, no login screen |
| Create project | 201 Created, project in list |
| Update status | 200 OK, status changed |
| Delete item | 200 OK, item removed |
| Logout | Token deleted, back to login |

### ? Error Scenarios

| Action | Expected Result |
|--------|----------------|
| Invalid credentials | "??ng nh?p th?t b?i" |
| Token expired | "Phiên ??ng nh?p ?ã h?t h?n" ? Auto logout |
| Server offline | "Không th? k?t n?i ??n server" |
| Network timeout | Error message, no crash |

## ?? Testing Priority

**HIGH PRIORITY** (Must test):
1. ? Login ? Token save ? Auto login
2. ? Create project with JWT
3. ? Create task with JWT
4. ? Logout ? Token delete

**MEDIUM PRIORITY** (Should test):
5. ? Update operations (project/task status)
6. ? Delete operations
7. ? Token expiration handling

**LOW PRIORITY** (Nice to have):
8. ? Error messages
9. ? Server offline handling
10. ? UI feedback

## ?? Test Report Template

```
=== JWT Authentication Test Report ===
Date: __________
Tester: __________

1. Login Flow: [ PASS / FAIL ]
   - Notes: _______________

2. CRUD Operations: [ PASS / FAIL ]
   - Create Project: [ PASS / FAIL ]
   - Create Task: [ PASS / FAIL ]
   - Update Status: [ PASS / FAIL ]
   - Delete: [ PASS / FAIL ]

3. Token Management: [ PASS / FAIL ]
   - Auto login: [ PASS / FAIL ]
   - Logout: [ PASS / FAIL ]
   - Token expiration: [ PASS / FAIL ]

4. Error Handling: [ PASS / FAIL ]
   - Server offline: [ PASS / FAIL ]
   - Invalid token: [ PASS / FAIL ]

Overall Status: [ PASS / FAIL ]

Issues Found:
1. _______________
2. _______________

Suggestions:
1. _______________
2. _______________
```

## ?? Ready to Test!

**Estimated Time**: 20-30 phút  
**Prerequisites**: Backend API ?ang ch?y  
**Tools Needed**: App compiled, Notepad (?? check token.dat)

**Start testing now!** ??
