# 📚 Quản Lý Sách WPF

<p align="center">
  <img src="icon.ico" alt="QuanLySach Logo" width="80">
</p>

**Quản Lý Sách WPF** là ứng dụng desktop hiện đại để quản lý kho sách, danh mục, đơn hàng và khách hàng. Ứng dụng được xây dựng trên **.NET 9** và **WPF**, tuân thủ nguyên tắc **Clean Architecture**, sử dụng **EF Core** cho truy cập dữ liệu, chạy **SQL Server** trong Docker để lưu trữ và dùng **ClosedXML** để nhập dữ liệu từ Excel.

---

## 🚀 Tính năng

* **Quản lý kho**: CRUD cho sách và danh mục
* **Xử lý đơn hàng**: Tạo, theo dõi đơn hàng và xem lịch sử
* **Quản lý khách hàng**: Đăng ký, cập nhật và tìm kiếm thông tin khách
* **Nhập khẩu Excel**: Nhập hàng loạt danh mục và sách qua file Excel
* **Xác thực bảo mật**: Mật khẩu được băm SHA-256 cho admin và người dùng
* **Tự động migrations**: EF Core tự động áp dụng migrations khi khởi động

---

## 🛠️ Kiến trúc dự án

| Lớp              | Công nghệ                       |
| ---------------- | ------------------------------- |
| Giao diện        | .NET 9 WPF                      |
| Host             | Microsoft.Extensions.Hosting    |
| Business Logic   | Dịch vụ theo Clean Architecture |
| Truy cập dữ liệu | EF Core (Code First)            |
| Cơ sở dữ liệu    | SQL Server (Docker Compose)     |
| Xử lý Excel      | ClosedXML                       |

---

## ⚙️ Yêu cầu cài đặt

* [.NET 9 SDK](https://dotnet.microsoft.com/download)
* [Docker & Docker Compose](https://docs.docker.com/compose/)
* Thông tin đăng nhập SQL Server (`sa` password) đã được cấu hình trong `docker-compose.yml` và `appsettings.json`

---

## 📝 Cài đặt & Chạy

1. **Clone repository**

   ```bash
   git clone https://github.com/hoangsnowy/quanlysach_wpf.git
   cd quanlysach_wpf
   ```

2. **Cấu hình SQL Server**

   * Chỉnh `docker-compose.yml`:

     ```yaml
     services:
       sqlserver:
         environment:
           SA_PASSWORD: "YourStrong!Pass"
     ```
   * Chỉnh `appsettings.json`:

     ```json
     "ConnectionStrings": {
       "QuanLyCafeDatabase": "Server=localhost,1433;Database=QuanLyCafe;User Id=sa;Password=YourStrong!Pass;TrustServerCertificate=True;"
     }
     ```

3. **Khởi động SQL Server container**

   ```bash
   docker compose up -d
   ```

4. **Tạo & áp migrations**

   ```bash
   dotnet ef migrations add InitialCreate \
     --project QuanLySach.DAL.EF \
     --startup-project QuanLySach
   dotnet ef database update \
     --project QuanLySach.DAL.EF \
     --startup-project QuanLySach
   ```

5. **Build & chạy ứng dụng**

   ```bash
   dotnet run --project QuanLySach/QuanLySach.csproj
   ```

---

## 🎯 Cách sử dụng

1. **Đăng nhập** với tài khoản admin đã seed:

   * **Username**: `admin`
   * **Password**: `Admin@123`
2. **Điều hướng** trong giao diện để quản lý sách, danh mục, đơn hàng và khách hàng.
3. **Nhập khẩu** dữ liệu từ Excel qua menu **Settings** để load hàng loạt.

---

## 🤝 Đóng góp

Mọi đóng góp đều được hoan nghênh! Vui lòng mở issue hoặc gửi pull request. Hãy fork repo, làm việc trên nhánh tính năng và gửi PR để mình review.

---

## 📄 Giấy phép

Dự án được cấp phép theo **MIT License**
