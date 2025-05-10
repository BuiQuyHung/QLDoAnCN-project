CREATE DATABASE QLDoAnCNTT;
USE QLDoAnCNTT;
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
    NgayBatDau DATE NOT NULL,
    SoTuanThucHien INT NOT NULL CHECK (SoTuanThucHien > 0),
    NgayKetThuc DATE
);

CREATE TABLE Lop (
    MaLop VARCHAR(10) PRIMARY KEY,
    TenLop NVARCHAR(100) NOT NULL,
    KhoaHoc VARCHAR(10),
    MaChuyenNganh VARCHAR(10) FOREIGN KEY REFERENCES ChuyenNganh(MaChuyenNganh),
    MaDotDoAn VARCHAR(10), 
    MaGVHuongDan VARCHAR(10), 
    FOREIGN KEY (MaDotDoAn) REFERENCES DotDoAn(MaDotDoAn),
    FOREIGN KEY (MaGVHuongDan) REFERENCES GiangVien(MaGV)
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
    ChuyenNganh NVARCHAR(100),
    ThoiGianThucHien INT,
    MaGVHuongDan VARCHAR(10), 
    TrangThai VARCHAR(50) DEFAULT N'Ch? duy?t',
    MaSVDeXuat VARCHAR(10), 
    MaDotDoAn VARCHAR(10),
    FOREIGN KEY (MaGVHuongDan) REFERENCES GiangVien(MaGV),
    FOREIGN KEY (MaSVDeXuat) REFERENCES SinhVien(MaSV),
    FOREIGN KEY (MaDotDoAn) REFERENCES DotDoAn(MaDotDoAn)
);

CREATE TABLE PhanCong (
    MaDeTai VARCHAR(10),
    MaSV VARCHAR(10),
    NgayPhanCong DATE DEFAULT GETDATE(),
    MaDotDoAn VARCHAR(10),
    PRIMARY KEY (MaDeTai, MaSV),
    FOREIGN KEY (MaDeTai) REFERENCES DeTai(MaDeTai),
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    FOREIGN KEY (MaDotDoAn) REFERENCES DotDoAn(MaDotDoAn)
);

CREATE TABLE BaoCaoTienDo (
    MaBaoCao INT PRIMARY KEY IDENTITY,
    MaDeTai VARCHAR(10) NOT NULL,
    MaSV VARCHAR(10) NOT NULL,
    TuanSo INT NOT NULL, 
    NgayNop DATETIME DEFAULT GETDATE(),
    LoaiBaoCao VARCHAR(50) NOT NULL, 
    TepDinhKem NVARCHAR(255),
    GhiChuSV NVARCHAR(500), 
    MaGVCham VARCHAR(10), 
    DiemSo FLOAT,
    NhanXetGV TEXT,
    ChoPhepBaoVe BIT, -- 1: cho phép, 0: không cho phép 
    FOREIGN KEY (MaDeTai) REFERENCES DeTai(MaDeTai),
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    FOREIGN KEY (MaGVCham) REFERENCES GiangVien(MaGV)
);

-- T?o index ?? t?ng t?c truy v?n (th??ng xuyên l?c theo MaDeTai, MaSV)
CREATE INDEX IX_BaoCaoTienDo_DeTai_SV ON BaoCaoTienDo (MaDeTai, MaSV);

CREATE TABLE HoiDong (
    MaHoiDong VARCHAR(10) PRIMARY KEY,
    TenHoiDong NVARCHAR(100) NOT NULL,
    NgayBaoVe DATE,
    MaDotDoAn VARCHAR(10),
    FOREIGN KEY (MaDotDoAn) REFERENCES DotDoAn(MaDotDoAn)
);

CREATE TABLE ThanhVienHoiDong (
    MaHoiDong VARCHAR(10),
    MaGV VARCHAR(10),
    VaiTro VARCHAR(50) NOT NULL,
    PRIMARY KEY (MaHoiDong, MaGV),
    FOREIGN KEY (MaHoiDong) REFERENCES HoiDong(MaHoiDong),
    FOREIGN KEY (MaGV) REFERENCES GiangVien(MaGV)
);

CREATE TABLE LogHoatDong (
    MaLog INT PRIMARY KEY IDENTITY,
    TenDangNhap VARCHAR(50) NOT NULL,
    ThoiGian DATETIME DEFAULT GETDATE(),
    HanhDong NVARCHAR(200) NOT NULL,
    ChiTiet NVARCHAR(MAX) 
    FOREIGN KEY (TenDangNhap) REFERENCES TaiKhoan(TenDangNhap)
);

CREATE TABLE TaiKhoan (
    TenDangNhap VARCHAR(50) PRIMARY KEY,
    MatKhau VARCHAR(100) NOT NULL,
    VaiTro VARCHAR(20) NOT NULL CHECK (VaiTro IN ('Admin', 'GiangVien', 'SinhVien')),
    MaNguoiDung VARCHAR(10),
    MaSV VARCHAR(10),  
    MaGV VARCHAR(10),  
    -- Ràng bu?c: N?u VaiTro là 'SinhVien', MaNguoiDung ph?i b?ng MaSV, ng??c l?i là NULL
    CONSTRAINT CK_TaiKhoan_SinhVien CHECK (
        (VaiTro <> 'SinhVien' AND MaSV IS NULL) OR
        (VaiTro = 'SinhVien' AND MaSV IS NOT NULL AND MaNguoiDung = MaSV)
    ),
    -- Ràng bu?c: N?u VaiTro là 'GiangVien', MaNguoiDung ph?i b?ng MaGV, ng??c l?i là NULL
    CONSTRAINT CK_TaiKhoan_GiangVien CHECK (
        (VaiTro <> 'GiangVien' AND MaGV IS NULL) OR
        (VaiTro = 'GiangVien' AND MaGV IS NOT NULL AND MaNguoiDung = MaGV)
    ),
    -- Ràng bu?c: N?u VaiTro là 'Admin' thì MaNguoiDung, MaSV, MaGV ph?i NULL
    CONSTRAINT CK_TaiKhoan_Admin CHECK (
        (VaiTro <> 'Admin' ) OR
        (VaiTro = 'Admin' AND MaNguoiDung IS NULL AND MaSV IS NULL AND MaGV IS NULL)
    ),
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    FOREIGN KEY (MaGV) REFERENCES GiangVien(MaGV)
);

-----------------------------------
INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES
('CNTT', N'Công ngh? thông tin'),
('KT', N'Kinh t?'),
('QTKD', N'Qu?n tr? kinh doanh'),
('NH', N'Ngân hàng'),
('SP', N'S? ph?m');

INSERT INTO BoMon (MaBoMon, TenBoMon, MaKhoa) VALUES
('CNPM', N'Công ngh? ph?n m?m', 'CNTT'),
('HTTT', N'H? th?ng thông tin', 'CNTT'),
('KHMT', N'Khoa h?c máy tính', 'CNTT');

INSERT INTO Nganh (MaNganh, TenNganh, MaBoMon) VALUES
('CNPM', N'Công ngh? ph?n m?m', 'CNPM'),
('HTTT', N'H? th?ng thông tin', 'HTTT'),
('KHMT', N'Khoa h?c máy tính', 'KHMT');

INSERT INTO ChuyenNganh (MaChuyenNganh, TenChuyenNganh, MaNganh) VALUES
('PTUD', N'Phát tri?n ?ng d?ng', 'CNPM'),
('TTNT', N'Trí tu? nhân t?o', 'KHMT'),
('PTDL', N'Phân tích d? li?u', 'HTTT');

INSERT INTO DotDoAn (MaDotDoAn, TenDotDoAn, KhoaHoc, NgayBatDau, SoTuanThucHien) VALUES
('DA1', N'?? án chuyên ngành 1', '2021-2025', '2024-01-15', 15),
('DA2', N'?? án chuyên ngành 2', '2022-2026', '2024-05-20', 15),
('DA3', N'?? án chuyên ngành 3', '2023-2027', '2025-01-15', 15);

INSERT INTO Lop (MaLop, TenLop, KhoaHoc, MaChuyenNganh, MaDotDoAn, MaGVHuongDan) VALUES
('125214', N'SEK19.4', 'K19', 'PTUD', 'DA1', 'GV001'),
('125215', N'SEK19.5', 'K19', 'TTNT', 'DA1', 'GV002'),
('125216', N'SEK19.6', 'K19', 'PTDL', 'DA1', 'GV003'),
('125217', N'SEK19.7', 'K19', 'PTUD', 'DA3', 'GV001'),
('125218', N'SEK19.8', 'K19', 'TTNT', 'DA3', 'GV002');

INSERT INTO SinhVien (MaSV, HoTen, Email, SoDienThoai, NgaySinh, MaLop) VALUES
('SV001', N'Nguy?n V?n An', 'vana@example.com', '0912345678', '2000-05-10', '125214'),
('SV002', N'Tr?n Th? Bình', 'thib@example.com', '0987654321', '2000-08-15', '125214'),
('SV003', N'Lê Minh C??ng', 'minhc@example.com', '0933322211', '2001-02-20', '125214'),
('SV004', N'Ph?m Thu D??ng', 'thud@example.com', '0977788899', '2001-06-25', '125214'),
('SV005', N'Hoàng Anh Y?n', 'anhe@example.com', '0922233344', '2002-01-01', '125214');

INSERT INTO GiangVien (MaGV, HoTen, ChuyenNganh, Email, SoDienThoai) VALUES
('GV001', N'Nguy?n V?n An', N'Công ngh? ph?n m?m', 'annguyen@example.com', '0901234567'),
('GV002', N'Tr?n Th? Bình', N'Trí tu? nhân t?o', 'binhtran@example.com', '0902345678'),
('GV003', N'Lê Hoàng Châu', N'H? th?ng thông tin', 'chaule@example.com', '0903456789'),
('GV004', N'Ph?m Th? Di?u', N'M?ng máy tính', 'dieupham@example.com', '0904567890'),
('GV005', N'Hoàng Ng?c ??c', N'An toàn thông tin', 'duchoang@example.com', '0905678901');

INSERT INTO DeTai (MaDeTai, TenDeTai, MoTa, ChuyenNganh, ThoiGianThucHien, MaGVHuongDan, TrangThai, MaSVDeXuat, MaDotDoAn) VALUES
('DT001', N'Xây d?ng h? th?ng qu?n lý th? vi?n', N'H? th?ng qu?n lý các ch?c n?ng c?a th? vi?n', 'PTUD', 16, 'GV001', N'?ã duy?t', NULL, 'DA1'),
('DT002', N'Phát tri?n ?ng d?ng nh?n di?n khuôn m?t', N'?ng d?ng nh?n di?n và theo dõi khuôn m?t', 'TTNT', 16, 'GV002', N'?ã duy?t', NULL, 'DA1'),
('DT003', N'Xây d?ng kho d? li?u cho siêu th?', N'Xây d?ng h? th?ng ETL và kho d? li?u', 'PTDL', 16, 'GV003', N'?ã duy?t', NULL, 'DA1'),
('DT004', N'Nghiên c?u các thu?t toán máy h?c', N'Tìm hi?u và cài ??t các thu?t toán h?c máy', 'TTNT', 15, 'GV002', N'Ch? duy?t', 'SV005', 'DA3'),
('DT005', N'Xây d?ng ?ng d?ng web bán hàng', N'Xây d?ng website bán hàng tr?c tuy?n', 'PTUD', 15, 'GV001', N'?ã duy?t', NULL, 'DA3');

INSERT INTO PhanCong (MaDeTai, MaSV, MaDotDoAn) VALUES
('DT001', 'SV001', 'DA1'),
('DT002', 'SV002', 'DA1'),
('DT003', 'SV003', 'DA1'),
('DT004', 'SV005', 'DA3'),
('DT005', 'SV004', 'DA3');

INSERT INTO BaoCaoTienDo (MaDeTai, MaSV, TuanSo, NgayNop, LoaiBaoCao, TepDinhKem, GhiChuSV, MaGVCham, DiemSo, NhanXetGV, ChoPhepBaoVe) VALUES
('DT001', 'SV001', 1, '2024-01-22', N'Tu?n 1', N'baocao1.pdf', N'Báo cáo tu?n 1', 'GV001', 8.5, N'T?t', 1),
('DT001', 'SV001', 8, '2024-03-11', N'Tu?n 8', N'baocao8.pdf', N'Báo cáo gi?a k?', 'GV001', 9.0, N'Xu?t s?c', 1),
('DT002', 'SV002', 1, '2024-01-22', N'Tu?n 1', N'baocao1.pdf', N'Báo cáo tu?n 1', 'GV002', 7.0, N'Khá', 1),
('DT002', 'SV002', 8, '2024-03-11', N'Tu?n 8', N'baocao8.pdf', N'Báo cáo gi?a k?', 'GV002', 8.0, N'T?t', 1),
('DT003', 'SV003', 1, '2024-01-22', N'Tu?n 1', N'baocao1.pdf', N'Báo cáo tu?n 1', 'GV003', 6.5, N'Trung bình', 0);

INSERT INTO HoiDong (MaHoiDong, TenHoiDong, NgayBaoVe, MaDotDoAn) VALUES
('HD1', N'H?i ??ng b?o v? 1', '2024-05-20', 'DA1'),
('HD2', N'H?i ??ng b?o v? 2', '2024-05-21', 'DA1'),
('HD3', N'H?i ??ng b?o v? 3', '2025-05-20', 'DA3'),
('HD4', N'H?i ??ng b?o v? 4', '2025-05-21', 'DA3'),
('HD5', N'H?i ??ng b?o v? 5', '2026-05-20', 'DA3');

INSERT INTO ThanhVienHoiDong (MaHoiDong, MaGV, VaiTro) VALUES
('HD1', 'GV001', N'Ch? t?ch'),
('HD1', 'GV002', N'Th? ký'),
('HD1', 'GV003', N'?y viên'),
('HD2', 'GV004', N'Ch? t?ch'),
('HD2', 'GV005', N'?y viên');

INSERT INTO LogHoatDong (TenDangNhap, ThoiGian, HanhDong, ChiTiet) VALUES
('admin', GETDATE(), N'Thêm m?i', N'Thêm sinh viên Nguy?n V?n A'),
('admin', GETDATE(), N'C?p nh?t', N'C?p nh?t thông tin ?? tài DT001'),
('GV001', GETDATE(), N'?ánh giá', N'?ánh giá báo cáo tu?n 1 c?a SV001'),
('SV001', GETDATE(), N'N?p báo cáo', N'N?p báo cáo tu?n 1'),
('admin', GETDATE(), N'Xóa', N'Xóa sinh viên Hoàng Anh E');

INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro, MaNguoiDung, MaSV, MaGV) VALUES
('admin', '123456', 'Admin', NULL, NULL, NULL),
('GV001', '123456', 'GiangVien', 'GV001', NULL, 'GV001'),
('SV001', '123456', 'SinhVien', 'SV001', 'SV001', NULL),
('GV002', '123456', 'GiangVien', 'GV002', NULL, 'GV002'),
('SV002', '123456', 'SinhVien', 'SV002', 'SV002', NULL);
