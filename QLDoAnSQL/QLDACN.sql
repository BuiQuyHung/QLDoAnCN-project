CREATE DATABASE QLDoAnChuyenNganh;
USE QLDoAnChuyenNganh;
GO

CREATE TABLE Khoa (
    MaKhoa VARCHAR(10) PRIMARY KEY,
    TenKhoa NVARCHAR(100) NOT NULL
);

CREATE TABLE BoMon (
    MaBoMon VARCHAR(10) PRIMARY KEY,
    TenBoMon NVARCHAR(100) NOT NULL,
    MaKhoa VARCHAR(10) FOREIGN KEY REFERENCES Khoa(MaKhoa)
);

CREATE TABLE Nganh (
    MaNganh VARCHAR(10) PRIMARY KEY,
    TenNganh NVARCHAR(100) NOT NULL,
    MaBoMon VARCHAR(10) FOREIGN KEY REFERENCES BoMon(MaBoMon)
);

CREATE TABLE ChuyenNganh (
    MaChuyenNganh VARCHAR(10) PRIMARY KEY,
    TenChuyenNganh NVARCHAR(100) NOT NULL,
    MaNganh VARCHAR(10) FOREIGN KEY REFERENCES Nganh(MaNganh)
);

CREATE TABLE DotDoAn (
    MaDotDoAn VARCHAR(10) PRIMARY KEY,
    TenDotDoAn NVARCHAR(200) NOT NULL,
    KhoaHoc VARCHAR(10) NOT NULL,
    NgayBatDau DATE,
    NgayKetThuc DATE,
    SoTuanThucHien INT
);

CREATE TABLE Lop (
    MaLop VARCHAR(10) PRIMARY KEY,
    TenLop NVARCHAR(100) NOT NULL,
    MaChuyenNganh VARCHAR(10) FOREIGN KEY REFERENCES ChuyenNganh(MaChuyenNganh),
    MaDotDoAn VARCHAR(10) FOREIGN KEY REFERENCES DotDoAn(MaDotDoAn)
);

CREATE TABLE SinhVien (
    MaSV VARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    SoDienThoai VARCHAR(20),
    NgaySinh DATE,
    MaLop VARCHAR(10) FOREIGN KEY REFERENCES Lop(MaLop)
);

CREATE TABLE GiangVien (
    MaGV VARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    ChuyenNganh NVARCHAR(100),
    Email VARCHAR(100) UNIQUE NOT NULL,
    SoDienThoai VARCHAR(20)
);

CREATE TABLE DeTai (
    MaDeTai VARCHAR(10) PRIMARY KEY,
    TenDeTai NVARCHAR(200) NOT NULL,
    MoTa TEXT,
    ThoiGianThucHien INT,
    MaGV VARCHAR(10) FOREIGN KEY REFERENCES GiangVien(MaGV),
    TrangThai VARCHAR(50) DEFAULT N'Chờ duyệt',
    MaDotDoAn VARCHAR(10) FOREIGN KEY REFERENCES DotDoAn(MaDotDoAn)
);

CREATE TABLE PhanCong (
    MaDeTai VARCHAR(10),
    MaSV VARCHAR(10),
    NgayPhanCong DATE DEFAULT GETDATE(),
    MaDotDoAn VARCHAR(10) FOREIGN KEY REFERENCES DotDoAn(MaDotDoAn),
    PRIMARY KEY (MaDeTai, MaSV)
    FOREIGN KEY (MaDeTai) REFERENCES DeTai(MaDeTai),
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
);

CREATE TABLE TienDo (
    MaTienDo INT PRIMARY KEY IDENTITY,
    MaDeTai VARCHAR(10) FOREIGN KEY REFERENCES DeTai(MaDeTai),
    MaSV VARCHAR(10) FOREIGN KEY REFERENCES SinhVien(MaSV),
    NgayNop DATETIME DEFAULT GETDATE(),
    LoaiBaoCao VARCHAR(50) NOT NULL,
    TepDinhKem NVARCHAR(255),
    GhiChu NVARCHAR(500)
);

CREATE TABLE DanhGia (
    MaDanhGia INT PRIMARY KEY IDENTITY,
    MaDeTai VARCHAR(10) FOREIGN KEY REFERENCES DeTai(MaDeTai),
    MaGV VARCHAR(10) FOREIGN KEY REFERENCES GiangVien(MaGV),
    DiemSo FLOAT NOT NULL,
    NhanXet TEXT
);

CREATE TABLE HoiDong (
    MaHoiDong VARCHAR(10) PRIMARY KEY,
    TenHoiDong NVARCHAR(100) NOT NULL,
    NgayBaoVe DATE,
    MaDotDoAn VARCHAR(10) FOREIGN KEY REFERENCES DotDoAn(MaDotDoAn)
);

CREATE TABLE ThanhVienHoiDong (
    MaHoiDong VARCHAR(10),
    MaGV VARCHAR(10),
    VaiTro VARCHAR(50) NOT NULL,
    PRIMARY KEY (MaHoiDong, MaGV),
    FOREIGN KEY (MaHoiDong) REFERENCES HoiDong(MaHoiDong),
    FOREIGN KEY (MaGV) REFERENCES GiangVien(MaGV)
);

CREATE TABLE TaiKhoan (
    TenDangNhap VARCHAR(50) PRIMARY KEY,
    MatKhau VARCHAR(100) NOT NULL,
    VaiTro VARCHAR(20) NOT NULL,
    MaGV VARCHAR(10) FOREIGN KEY REFERENCES GiangVien(MaGV),
    MaSV VARCHAR(10) FOREIGN KEY REFERENCES SinhVien(MaSV)
);

CREATE TABLE TaiKhoanDangNhap (
    TenDangNhap VARCHAR(50) PRIMARY KEY,
    MatKhau VARCHAR(100) NOT NULL,
    VaiTro VARCHAR(20) NOT NULL,
    MaNguoiDung VARCHAR(10) NOT NULL
);

CREATE TABLE Log (
    MaLog INT PRIMARY KEY IDENTITY,
    TenDangNhap VARCHAR(50) FOREIGN KEY REFERENCES TaiKhoan(TenDangNhap),
    ThoiGian DATETIME DEFAULT GETDATE(),
    HanhDong NVARCHAR(200) NOT NULL,
    MoTa NVARCHAR(500)
);
DELETE FROM TaiKhoan;
--------------------------------------------------------------------------
INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro, MaGV, MaSV) VALUES
('admin1', '12345', 'ADMIN', 'GV01', NULL),
('sv07', 'password2', 'SV', NULL, 'SV07'),
('gv02', 'password3', 'GV', 'GV02', NULL),
('sv08', 'password4', 'SV', NULL, 'SV08'),
('admin123', 'password5', 'ADMIN', NULL, NULL);

INSERT INTO TaiKhoanDangNhap (TenDangNhap, MatKhau, VaiTro, MaNguoiDung) VALUES
('admin', '12345', 'ADMIN', 'AD01'),
('teacher1', 'teacherpass', 'GV', 'GV01'),
('teacher2', 'teacherpass2', 'GV', 'GV02'),
('student1', 'studentpass', 'SV', 'SV08'),
('student2', 'studentpass2', 'SV', 'SV06');

INSERT INTO Khoa (MaKhoa, TenKhoa)
VALUES
('CNTT01', N'Công nghệ thông tin'),
('KT01', N'Kinh tế');

INSERT INTO BoMon (MaBoMon, TenBoMon, MaKhoa)
VALUES
('BM01', N'Khoa học máy tính', 'CNTT01'),
('BM02', N'Hệ thống thông tin', 'CNTT01'),
('BM03', N'Công nghệ phần mềm', 'CNTT01');

INSERT INTO Nganh (MaNganh, TenNganh, MaBoMon)
VALUES
('NG01', N'Khoa học máy tính', 'BM01'),
('NG02', N'Công nghệ thông tin', 'BM02'),
('NG03', N'Kỹ thuật phần mềm', 'BM03');

INSERT INTO ChuyenNganh (MaChuyenNganh, TenChuyenNganh, MaNganh)
VALUES
('CN01', N'Khoa học dữ liệu', 'NG01'),
('CN02', N'Trí tuệ nhân tạo', 'NG01'),
('CN03', N'Mạng máy tính', 'NG02'),
('CN04', N'Công nghệ thông tin', 'NG02'),
('CN05', N'Thiết kế đồ họa', 'NG02'),
('CN06', N'Kiểm thử phần mềm', 'NG03'),
('CN07', N'Công nghệ di động', 'NG03'),
('CN08', N'Công nghệ web', 'NG03');

INSERT INTO Lop (MaLop, TenLop, KhoaHoc, MaChuyenNganh)
VALUES
('125214', 'SEK19.4', 'K19', 'CN08'),
('125215', 'SEK19.5', 'K19', 'CN07'),
('125216', 'SEK19.6', 'K19', 'CN06');

INSERT INTO SinhVien (MaSV, HoTen, Email, SoDienThoai, NgaySinh, MaLop)
VALUES
('SV06', N'Nguyễn Văn Anh', 'ann.nguyen@abc.com', '0123456789', '2000-05-01', '125214'),
('SV07', N'Trần Thị Bao', 'binhn.tran@abc.com', '0123456788', '2000-06-01', '125214'),
('SV08', N'Lê Thị Cúc', 'camn.le@abc.com', '0123456787', '2000-07-01', '125214'),
('SV09', N'Phạm Văn Dương Anh', 'duongn.pham@abc.com', '0123456786', '2000-08-01', '125215'),
('SV10', N'Vũ Thị Yến', 'enn.vu@abc.com', '0123456785', '2000-09-01', '125215');

INSERT INTO GiangVien (MaGV, HoTen, ChuyenNganh, Email, SoDienThoai)
VALUES
('GV01', N'Phan Văn Huy', N'Khoa học máy tính', 'f.phan@abc.com', '0123456781'),
('GV02', N'Nguyễn Thị Giang', N'Mạng máy tính', 'g.nguyen@abc.com', '0123456782'),
('GV03', N'Lê Minh Hường', N'Công nghệ Webn', 'h.le@abc.com', '0123456783'),
('GV04', N'Trần Thị Nụ', N'Công nghệ Web', 'i.tran@abc.com', '0123456784'),
('GV05', N'Vũ Minh Khôi', N'Công nghệ Web', 'k.vu@abc.com', '0123456780');

INSERT INTO DotDoAn (MaDotDoAn, TenDotDoAn, KhoaHoc, NgayBatDau, NgayKetThuc, SoTuanThucHien)
VALUES
('DDA2025A', N'Đồ án tốt nghiệp khóa 2021-2025 đợt 1', 'K21', '2025-05-15', '2025-08-15', 13),
('DDA2025B', N'Đồ án tốt nghiệp khóa 2021-2025 đợt 2', 'K21', '2025-09-01', '2025-12-15', 15),
('DAK2026A', N'Đồ án kiến tập khóa 2022-2026 đợt 1', 'K22', '2026-01-10', '2026-03-30', 11),
('DATN2024B', N'Đồ án tốt nghiệp khóa 2020-2024 bổ sung', 'K20', '2025-06-01', '2025-08-30', 12),
('DAK2026B', N'Đồ án kiến tập khóa 2022-2026 đợt 2', 'K22', '2026-04-15', '2026-07-15', 13),
('DDA2026A', N'Đồ án tốt nghiệp khóa 2022-2026 đợt 1', 'K22', '2026-05-15', '2026-08-15', 13),
('DDA2024C', N'Đồ án tốt nghiệp khóa 2020-2024 đợt cuối', 'K20', '2025-10-01', '2025-12-31', 14),
('DAK2027A', N'Đồ án kiến tập khóa 2023-2027 đợt 1', 'K23', '2027-01-15', '2027-04-05', 12),
('DDA2027A', N'Đồ án tốt nghiệp khóa 2023-2027 đợt 1', 'K23', '2027-05-20', '2027-08-20', 13),
('DATN2025A', N'Đồ án tốt nghiệp khóa 2021-2025 đặc biệt', 'K21', '2025-11-01', '2026-02-15', 16);

INSERT INTO DeTai (MaDeTai, TenDeTai, MoTa, ChuyenNganh, ThoiGianThucHien, MaGV, TrangThai)
VALUES ('DTCNTT01', N'Xây dựng hệ thống quản lý thư viện trực tuyến', N'Đồ án tập trung vào việc phát triển một ứng dụng web cho phép quản lý sách, độc giả và các hoạt động mượn trả sách của thư viện.', N'Công nghệ phần mềm', 15, 'GV01', N'Đã duyệt');
INSERT INTO DeTai (MaDeTai, TenDeTai, MoTa, ChuyenNganh, ThoiGianThucHien, MaGV, TrangThai)
VALUES ('DTCNTT02', N'Phát triển ứng dụng di động hỗ trợ học tập trực tuyến', N'Đồ án nhằm mục tiêu xây dựng một ứng dụng di động đa nền tảng, cung cấp các chức năng hỗ trợ sinh viên trong quá trình học tập trực tuyến như xem tài liệu, làm bài kiểm tra, tương tác với giảng viên.', N'Hệ thống thông tin', 16, 'GV02', N'Chờ duyệt');
INSERT INTO DeTai (MaDeTai, TenDeTai, MoTa, ChuyenNganh, ThoiGianThucHien, MaGV, TrangThai)
VALUES ('DTCNTT03', N'Nghiên cứu và triển khai hệ thống khuyến nghị sản phẩm dựa trên Machine Learning', N'Đồ án tập trung vào việc nghiên cứu các thuật toán Machine Learning và xây dựng một hệ thống có khả năng đưa ra các gợi ý sản phẩm phù hợp cho người dùng dựa trên lịch sử tương tác và sở thích của họ.', N'Khoa học máy tính', 18, 'GV03', N'Đã duyệt');
INSERT INTO DeTai (MaDeTai, TenDeTai, MoTa, ChuyenNganh, ThoiGianThucHien, MaGV, TrangThai)
VALUES ('DTCNTT04', N'Xây dựng website thương mại điện tử bán hàng trực tuyến', N'Đồ án nhằm mục tiêu xây dựng một website thương mại điện tử hoàn chỉnh với các chức năng như quản lý sản phẩm, giỏ hàng, thanh toán, quản lý đơn hàng và tài khoản người dùng.', N'Công nghệ phần mềm', 14, 'GV01', N'Đang thực hiện');
INSERT INTO DeTai (MaDeTai, TenDeTai, MoTa, ChuyenNganh, ThoiGianThucHien, MaGV, TrangThai)
VALUES ('DTCNTT05', N'Phân tích và thiết kế hệ thống quản lý bệnh viện', N'Đồ án tập trung vào việc phân tích nghiệp vụ của một bệnh viện và thiết kế một hệ thống thông tin quản lý toàn diện, bao gồm quản lý bệnh nhân, hồ sơ bệnh án, lịch hẹn, thuốc men và nhân viên.', N'Hệ thống thông tin', 17, 'GV04', N'Chờ duyệt');

INSERT INTO DeTai (MaDeTai, TenDeTai, MoTa, ChuyenNganh, ThoiGianThucHien, MaGV, TrangThai)
VALUES ('DTCNTT01', N'Xây dựng hệ thống quản lý thư viện trực tuyến', N'Đồ án tập trung vào việc phát triển một ứng dụng web cho phép quản lý sách, độc giả và các hoạt động mượn trả sách của thư viện.', N'Công nghệ phần mềm', 15, 'GV001', N'Đã duyệt');
INSERT INTO DeTai (MaDeTai, TenDeTai, MoTa, ChuyenNganh, ThoiGianThucHien, MaGV, TrangThai)
VALUES ('DTCNTT02', N'Phát triển ứng dụng di động hỗ trợ học tập trực tuyến', N'Đồ án nhằm mục tiêu xây dựng một ứng dụng di động đa nền tảng, cung cấp các chức năng hỗ trợ sinh viên trong quá trình học tập trực tuyến như xem tài liệu, làm bài kiểm tra, tương tác với giảng viên.', N'Hệ thống thông tin', 16, 'GV002', N'Chờ duyệt');
INSERT INTO DeTai (MaDeTai, TenDeTai, MoTa, ChuyenNganh, ThoiGianThucHien, MaGV, TrangThai)
VALUES ('DTCNTT03', N'Nghiên cứu và triển khai hệ thống khuyến nghị sản phẩm dựa trên Machine Learning', N'Đồ án tập trung vào việc nghiên cứu các thuật toán Machine Learning và xây dựng một hệ thống có khả năng đưa ra các gợi ý sản phẩm phù hợp cho người dùng dựa trên lịch sử tương tác và sở thích của họ.', N'Khoa học máy tính', 18, 'GV003', N'Đã duyệt');
INSERT INTO DeTai (MaDeTai, TenDeTai, MoTa, ChuyenNganh, ThoiGianThucHien, MaGV, TrangThai)
VALUES ('DTCNTT04', N'Xây dựng website thương mại điện tử bán hàng trực tuyến', N'Đồ án nhằm mục tiêu xây dựng một website thương mại điện tử hoàn chỉnh với các chức năng như quản lý sản phẩm, giỏ hàng, thanh toán, quản lý đơn hàng và tài khoản người dùng.', N'Công nghệ phần mềm', 14, 'GV001', N'Đang thực hiện');
INSERT INTO DeTai (MaDeTai, TenDeTai, MoTa, ChuyenNganh, ThoiGianThucHien, MaGV, TrangThai)
VALUES ('DTCNTT05', N'Phân tích và thiết kế hệ thống quản lý bệnh viện', N'Đồ án tập trung vào việc phân tích nghiệp vụ của một bệnh viện và thiết kế một hệ thống thông tin quản lý toàn diện, bao gồm quản lý bệnh nhân, hồ sơ bệnh án, lịch hẹn, thuốc men và nhân viên.', N'Hệ thống thông tin', 17, 'GV004', N'Chờ duyệt');

INSERT INTO PhanCong (MaDeTai, MaSV, NgayPhanCong)
VALUES
('DTCNTT01', 'SV01', '2025-04-15'),
('DTCNTT02', 'SV02', '2025-04-10'),
('DTCNTT01', 'SV03', '2025-04-18'),
('DTCNTT03', 'SV04', '2025-03-25'),
('DTCNTT04', 'SV05', '2025-04-01'),
('DTCNTT02', 'SV06', '2025-04-20'),
('DTCNTT05', 'SV07', '2025-03-15'),
('DTCNTT03', 'SV08', '2025-04-05'),
('DTCNTT04', 'SV09', '2025-04-21'),
('DTCNTT01', 'SV10', '2025-04-12');

INSERT INTO TienDo (MaDeTai, MaSV, NgayNop, LoaiBaoCao, TepDinhKem, GhiChu) VALUES
('DTCNTT01', 'SV06', GETDATE(), 'Bao cao tuan 1', 'Link Google Doc', N'Hoàn thành phần cơ sở lý thuyết.'),
('DTCNTT02', 'SV07', DATEADD(day, -7, GETDATE()), 'Bao cao tien do', 'Link Google Doc', N'Đang xây dựng giao diện người dùng.'),
('DTCNTT03', 'SV08', GETDATE(), 'Bao cao tuan 2', 'Link Google Doc', N'Triển khai thuật toán khuyến nghị.'),
('DTCNTT04', 'SV09', DATEADD(day, -10, GETDATE()), 'Bao cao giua ky', 'Link Google Doc', N'Đã hoàn thành 50% chức năng.'),
('DTCNTT05', 'SV10', GETDATE(), 'Bao cao tuan 1', 'Link Google Doc', N'Nghiên cứu nghiệp vụ quản lý bệnh viện.'),
('DTCNTT01', 'SV07', DATEADD(day, -3, GETDATE()), 'Bao cao tuan 3', 'Link Google Doc', N'Kết nối với API bên ngoài.'),
('DTCNTT02', 'SV08', GETDATE(), 'Bao cao tien do', 'Link Google Doc', N'Thử nghiệm các chức năng chính.'),
('DTCNTT03', 'SV09', DATEADD(day, -5, GETDATE()), 'Bao cao tuan 1', 'Link Google Doc', N'Tìm hiểu các thư viện Machine Learning.'),
('DTCNTT04', 'SV10', GETDATE(), 'Bao cao giua ky', 'Link Google Doc', N'Sửa lỗi và tối ưu giao diện.'),
('DTCNTT05', 'SV06', DATEADD(day, -2, GETDATE()), 'Bao cao tuan 2', 'Link Google Doc', N'Phân tích cơ sở dữ liệu.');

INSERT INTO DanhGia (MaDeTai, MaGV, DiemSo, NhanXet) VALUES
('DTCNTT01', 'GV01', 8.5, N'Đồ án có tính ứng dụng cao.'),
('DTCNTT02', 'GV02', 7.8, N'Cần cải thiện về giao diện.'),
('DTCNTT03', 'GV03', 9.0, N'Ý tưởng sáng tạo, triển khai tốt.'),
('DTCNTT04', 'GV01', 7.0, N'Chưa đáp ứng đủ yêu cầu.'),
('DTCNTT05', 'GV04', 8.0, N'Phân tích nghiệp vụ tốt.'),
('DTCNTT01', 'GV03', 8.8, N'Báo cáo chi tiết và rõ ràng.'),
('DTCNTT02', 'GV04', 7.5, N'Cần xem xét lại kiến trúc hệ thống.'),
('DTCNTT03', 'GV05', 9.2, N'Đề tài có tiềm năng phát triển.'),
('DTCNTT04', 'GV02', 6.5, N'Khả năng trình bày còn yếu.'),
('DTCNTT05', 'GV01', 8.2, N'Hoàn thành đúng thời hạn.');

INSERT INTO HoiDong (MaHoiDong, TenHoiDong, NgayBaoVe) VALUES
('HD01', N'Hội đồng bảo vệ đồ án 1', '2025-05-10'),
('HD02', N'Hội đồng bảo vệ đồ án 2', '2025-05-15'),
('HD03', N'Hội đồng bảo vệ đồ án 3', '2025-05-20'),
('HD04', N'Hội đồng bảo vệ đồ án 4', '2025-05-25'),
('HD05', N'Hội đồng bảo vệ đồ án 5', '2025-05-30'),
('HD06', N'Hội đồng nghiệm thu 1', '2025-06-05'),
('HD07', N'Hội đồng nghiệm thu 2', '2025-06-10'),
('HD08', N'Hội đồng đánh giá tiến độ', '2025-05-05'),
('HD09', N'Hội đồng bảo vệ dự kiến', '2025-05-01'),
('HD10', N'Hội đồng xét duyệt đề tài', '2025-04-25');

INSERT INTO ThanhVienHoiDong (MaHoiDong, MaGV, VaiTro) VALUES
('HD01', 'GV01', N'Chủ tịch'),
('HD01', 'GV02', N'Thư ký'),
('HD01', 'GV03', N'Ủy viên'),
('HD02', 'GV03', N'Chủ tịch'),
('HD02', 'GV04', N'Thư ký'),
('HD02', 'GV05', N'Ủy viên'),
('HD03', 'GV05', N'Chủ tịch'),
('HD03', 'GV01', N'Thư ký'),
('HD03', 'GV02', N'Ủy viên'),
('HD04', 'GV04', N'Chủ tịch');
INSERT INTO ThanhVienHoiDong (MaHoiDong, MaGV, VaiTro) VALUES
('HD04', 'GV05', N'Thư ký'),
('HD05', 'GV01', N'Chủ tịch'),
('HD05', 'GV03', N'Thư ký'),
('HD06', 'GV02', N'Chủ tịch'),
('HD06', 'GV04', N'Thư ký'),
('HD07', 'GV05', N'Chủ tịch'),
('HD07', 'GV01', N'Thư ký'),
('HD08', 'GV03', N'Chủ tịch'),
('HD09', 'GV04', N'Chủ tịch'),
('HD10', 'GV05', N'Chủ tịch');

INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro, MaNguoiDung) VALUES
('svanh', '123456', 'SinhVien', 'SV06'),
('svbao', 'abcdef', 'SinhVien', 'SV07'),
('svcuc', 'qwerty', 'SinhVien', 'SV08'),
('svduong', 'asdfgh', 'SinhVien', 'SV09'),
('svyen', 'zxcvbn', 'SinhVien', 'SV10'),
('gvphuy', 'mnbvcxz', 'GiangVien', 'GV01'),
('gvgiang', 'lkjhgfd', 'GiangVien', 'GV02'),
('gvhuong', 'poiuytr', 'GiangVien', 'GV03'),
('gvnu', 'yuiopkl', 'GiangVien', 'GV04'),
('gvminh', 'hjklyui', 'GiangVien', 'GV05');