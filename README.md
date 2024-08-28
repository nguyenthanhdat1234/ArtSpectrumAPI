# ArtSpectrum  
## Mục lục

- [Giới thiệu](#giới-thiệu)
- [Các tính năng](#các-tính-năng)
- [Cài đặt](#cài-đặt)
- [Sử dụng](#sử-dụng)
- [API Endpoints](#api-endpoints)
- [Cấu trúc dự án](#cấu-trúc-dự-án)
- [Đóng góp](#đóng-góp)
- [Tác giả](#tác-giả)
- [Giấy phép](#giấy-phép)
- [Liên hệ](#liên-hệ)
- [Ghi nhận đóng góp](#ghi-nhận-đóng-góp)

## Giới thiệu
  - ArtSpectrum là một nền tảng trực tuyến giúp các nghệ sĩ giới thiệu, chia sẻ và bán tác phẩm nghệ thuật của họ. Dự án này được xây dựng trên ASP.NET Core API và cung cấp 
   các dịch vụ backend cho ứng dụng web, bao gồm quản lý tác phẩm nghệ thuật, hồ sơ nghệ sĩ, và xử lý giao dịch mua bán.
## Các tính năng
  - Danh sách các tính năng của dự án...

## Cài đặt
 ### Yêu cầu hệ thống
   * .NET 6.0 SDK hoặc cao hơn
   * SQL Server hoặc SQLite
   * Visual Studio 2022 hoặc Visual Studio Code

### Hướng dẫn cài đặt
  1. Clone dự án: - git clone https://github.com/nguyenthanhdat1234/ArtSpectrumAPI.git - cd artspectrum
  2. Cài đặt các gói phụ thuộc: - dotnet restore
  3. Cấu hình cơ sở dữ liệu: - dotnet ef database update
  4. Chạy ứng dụng: - dotnet run

## Sử dụng
 - ### Link:
   https://artspectrum.azurewebsites.net/index.html
  ### Tài liệu Postman
  - Bạn có thể nhập tệp cấu hình Postman từ thư mục docs/Postman để thử nghiệm các API endpoints.

  ### Xác thực
  - Để truy cập các API bảo mật, bạn cần đăng nhập và sử dụng JWT Token trong phần header của yêu cầu.
    #### Authorization: Bearer {your-token}

## API Endpoints
   #### Nghệ sĩ
    GET /api/artists - Lấy danh sách nghệ sĩ.
    POST /api/artists - Tạo một nghệ sĩ mới.
    PUT /api/artists/{id} - Cập nhật thông tin nghệ sĩ.
    DELETE /api/artists/{id} - Xóa nghệ sĩ.
  #### Tác phẩm nghệ thuật
    GET /api/artworks - Lấy danh sách tác phẩm nghệ thuật.
    POST /api/artworks - Tạo một tác phẩm nghệ thuật mới.
    PUT /api/artworks/{id} - Cập nhật thông tin tác phẩm nghệ thuật.
    DELETE /api/artworks/{id} - Xóa tác phẩm nghệ thuật.
  #### Giao dịch
    POST /api/transactions - Tạo giao dịch mua bán.

## Cấu trúc dự án
 artspectrum/
├── Controllers/        # Các controller cho API
├── Data/               # Cấu hình cơ sở dữ liệu và ngữ cảnh dữ liệu
├── Models/             # Các model dữ liệu
├── Services/           # Các dịch vụ logic nghiệp vụ
├── DTOs/               # Data Transfer Objects
├── Migrations/         # Thư mục chứa các tệp di trú của Entity Framework
├── appsettings.json    # Cấu hình ứng dụng
├── Program.cs          # Điểm khởi đầu của ứng dụng
└── Startup.cs          # Cấu hình middleware và dịch vụ

## Liên hệ
  - #### Email: datnguyen.010802@gmail.com
  - #### LinkedIn: https://www.linkedin.com/in/dat-nguyen-982782191/

