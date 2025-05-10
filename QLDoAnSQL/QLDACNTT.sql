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
    ChoPhepBaoVe BIT, -- 1: cho ph�p, 0: kh�ng cho ph�p 
    FOREIGN KEY (MaDeTai) REFERENCES DeTai(MaDeTai),
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    FOREIGN KEY (MaGVCham) REFERENCES GiangVien(MaGV)
);

-- T?o index ?? t?ng t?c truy v?n (th??ng xuy�n l?c theo MaDeTai, MaSV)
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
    -- R�ng bu?c: N?u VaiTro l� 'SinhVien', MaNguoiDung ph?i b?ng MaSV, ng??c l?i l� NULL
    CONSTRAINT CK_TaiKhoan_SinhVien CHECK (
        (VaiTro <> 'SinhVien' AND MaSV IS NULL) OR
        (VaiTro = 'SinhVien' AND MaSV IS NOT NULL AND MaNguoiDung = MaSV)
    ),
    -- R�ng bu?c: N?u VaiTro l� 'GiangVien', MaNguoiDung ph?i b?ng MaGV, ng??c l?i l� NULL
    CONSTRAINT CK_TaiKhoan_GiangVien CHECK (
        (VaiTro <> 'GiangVien' AND MaGV IS NULL) OR
        (VaiTro = 'GiangVien' AND MaGV IS NOT NULL AND MaNguoiDung = MaGV)
    ),
    -- R�ng bu?c: N?u VaiTro l� 'Admin' th� MaNguoiDung, MaSV, MaGV ph?i NULL
    CONSTRAINT CK_TaiKhoan_Admin CHECK (
        (VaiTro <> 'Admin' ) OR
        (VaiTro = 'Admin' AND MaNguoiDung IS NULL AND MaSV IS NULL AND MaGV IS NULL)
    ),
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    FOREIGN KEY (MaGV) REFERENCES GiangVien(MaGV)
);

-----------------------------------
INSERT INTO Khoa (MaKhoa, TenKhoa) VALUES
('CNTT', N'C�ng ngh? th�ng tin'),
('KT', N'Kinh t?'),
('QTKD', N'Qu?n tr? kinh doanh'),
('NH', N'Ng�n h�ng'),
('SP', N'S? ph?m');

INSERT INTO BoMon (MaBoMon, TenBoMon, MaKhoa) VALUES
('CNPM', N'C�ng ngh? ph?n m?m', 'CNTT'),
('HTTT', N'H? th?ng th�ng tin', 'CNTT'),
('KHMT', N'Khoa h?c m�y t�nh', 'CNTT');

INSERT INTO Nganh (MaNganh, TenNganh, MaBoMon) VALUES
('CNPM', N'C�ng ngh? ph?n m?m', 'CNPM'),
('HTTT', N'H? th?ng th�ng tin', 'HTTT'),
('KHMT', N'Khoa h?c m�y t�nh', 'KHMT');

INSERT INTO ChuyenNganh (MaChuyenNganh, TenChuyenNganh, MaNganh) VALUES
('PTUD', N'Ph�t tri?n ?ng d?ng', 'CNPM'),
('TTNT', N'Tr� tu? nh�n t?o', 'KHMT'),
('PTDL', N'Ph�n t�ch d? li?u', 'HTTT');

INSERT INTO DotDoAn (MaDotDoAn, TenDotDoAn, KhoaHoc, NgayBatDau, SoTuanThucHien) VALUES
('DA1', N'?? �n chuy�n ng�nh 1', '2021-2025', '2024-01-15', 15),
('DA2', N'?? �n chuy�n ng�nh 2', '2022-2026', '2024-05-20', 15),
('DA3', N'?? �n chuy�n ng�nh 3', '2023-2027', '2025-01-15', 15);

INSERT INTO Lop (MaLop, TenLop, KhoaHoc, MaChuyenNganh, MaDotDoAn, MaGVHuongDan) VALUES
('125214', N'SEK19.4', 'K19', 'PTUD', 'DA1', 'GV001'),
('125215', N'SEK19.5', 'K19', 'TTNT', 'DA1', 'GV002'),
('125216', N'SEK19.6', 'K19', 'PTDL', 'DA1', 'GV003'),
('125217', N'SEK19.7', 'K19', 'PTUD', 'DA3', 'GV001'),
('125218', N'SEK19.8', 'K19', 'TTNT', 'DA3', 'GV002');

INSERT INTO SinhVien (MaSV, HoTen, Email, SoDienThoai, NgaySinh, MaLop) VALUES
('SV001', N'Nguy?n V?n An', 'vana@example.com', '0912345678', '2000-05-10', '125214'),
('SV002', N'Tr?n Th? B�nh', 'thib@example.com', '0987654321', '2000-08-15', '125214'),
('SV003', N'L� Minh C??ng', 'minhc@example.com', '0933322211', '2001-02-20', '125214'),
('SV004', N'Ph?m Thu D??ng', 'thud@example.com', '0977788899', '2001-06-25', '125214'),
('SV005', N'Ho�ng Anh Y?n', 'anhe@example.com', '0922233344', '2002-01-01', '125214');

INSERT INTO GiangVien (MaGV, HoTen, ChuyenNganh, Email, SoDienThoai) VALUES
('GV001', N'Nguy?n V?n An', N'C�ng ngh? ph?n m?m', 'annguyen@example.com', '0901234567'),
('GV002', N'Tr?n Th? B�nh', N'Tr� tu? nh�n t?o', 'binhtran@example.com', '0902345678'),
('GV003', N'L� Ho�ng Ch�u', N'H? th?ng th�ng tin', 'chaule@example.com', '0903456789'),
('GV004', N'Ph?m Th? Di?u', N'M?ng m�y t�nh', 'dieupham@example.com', '0904567890'),
('GV005', N'Ho�ng Ng?c ??c', N'An to�n th�ng tin', 'duchoang@example.com', '0905678901');

INSERT INTO DeTai (MaDeTai, TenDeTai, MoTa, ChuyenNganh, ThoiGianThucHien, MaGVHuongDan, TrangThai, MaSVDeXuat, MaDotDoAn) VALUES
('DT001', N'X�y d?ng h? th?ng qu?n l� th? vi?n', N'H? th?ng qu?n l� c�c ch?c n?ng c?a th? vi?n', 'PTUD', 16, 'GV001', N'?� duy?t', NULL, 'DA1'),
('DT002', N'Ph�t tri?n ?ng d?ng nh?n di?n khu�n m?t', N'?ng d?ng nh?n di?n v� theo d�i khu�n m?t', 'TTNT', 16, 'GV002', N'?� duy?t', NULL, 'DA1'),
('DT003', N'X�y d?ng kho d? li?u cho si�u th?', N'X�y d?ng h? th?ng ETL v� kho d? li?u', 'PTDL', 16, 'GV003', N'?� duy?t', NULL, 'DA1'),
('DT004', N'Nghi�n c?u c�c thu?t to�n m�y h?c', N'T�m hi?u v� c�i ??t c�c thu?t to�n h?c m�y', 'TTNT', 15, 'GV002', N'Ch? duy?t', 'SV005', 'DA3'),
('DT005', N'X�y d?ng ?ng d?ng web b�n h�ng', N'X�y d?ng website b�n h�ng tr?c tuy?n', 'PTUD', 15, 'GV001', N'?� duy?t', NULL, 'DA3');

INSERT INTO PhanCong (MaDeTai, MaSV, MaDotDoAn) VALUES
('DT001', 'SV001', 'DA1'),
('DT002', 'SV002', 'DA1'),
('DT003', 'SV003', 'DA1'),
('DT004', 'SV005', 'DA3'),
('DT005', 'SV004', 'DA3');

INSERT INTO BaoCaoTienDo (MaDeTai, MaSV, TuanSo, NgayNop, LoaiBaoCao, TepDinhKem, GhiChuSV, MaGVCham, DiemSo, NhanXetGV, ChoPhepBaoVe) VALUES
('DT001', 'SV001', 1, '2024-01-22', N'Tu?n 1', N'baocao1.pdf', N'B�o c�o tu?n 1', 'GV001', 8.5, N'T?t', 1),
('DT001', 'SV001', 8, '2024-03-11', N'Tu?n 8', N'baocao8.pdf', N'B�o c�o gi?a k?', 'GV001', 9.0, N'Xu?t s?c', 1),
('DT002', 'SV002', 1, '2024-01-22', N'Tu?n 1', N'baocao1.pdf', N'B�o c�o tu?n 1', 'GV002', 7.0, N'Kh�', 1),
('DT002', 'SV002', 8, '2024-03-11', N'Tu?n 8', N'baocao8.pdf', N'B�o c�o gi?a k?', 'GV002', 8.0, N'T?t', 1),
('DT003', 'SV003', 1, '2024-01-22', N'Tu?n 1', N'baocao1.pdf', N'B�o c�o tu?n 1', 'GV003', 6.5, N'Trung b�nh', 0);

INSERT INTO HoiDong (MaHoiDong, TenHoiDong, NgayBaoVe, MaDotDoAn) VALUES
('HD1', N'H?i ??ng b?o v? 1', '2024-05-20', 'DA1'),
('HD2', N'H?i ??ng b?o v? 2', '2024-05-21', 'DA1'),
('HD3', N'H?i ??ng b?o v? 3', '2025-05-20', 'DA3'),
('HD4', N'H?i ??ng b?o v? 4', '2025-05-21', 'DA3'),
('HD5', N'H?i ??ng b?o v? 5', '2026-05-20', 'DA3');

INSERT INTO ThanhVienHoiDong (MaHoiDong, MaGV, VaiTro) VALUES
('HD1', 'GV001', N'Ch? t?ch'),
('HD1', 'GV002', N'Th? k�'),
('HD1', 'GV003', N'?y vi�n'),
('HD2', 'GV004', N'Ch? t?ch'),
('HD2', 'GV005', N'?y vi�n');

INSERT INTO LogHoatDong (TenDangNhap, ThoiGian, HanhDong, ChiTiet) VALUES
('admin', GETDATE(), N'Th�m m?i', N'Th�m sinh vi�n Nguy?n V?n A'),
('admin', GETDATE(), N'C?p nh?t', N'C?p nh?t th�ng tin ?? t�i DT001'),
('GV001', GETDATE(), N'?�nh gi�', N'?�nh gi� b�o c�o tu?n 1 c?a SV001'),
('SV001', GETDATE(), N'N?p b�o c�o', N'N?p b�o c�o tu?n 1'),
('admin', GETDATE(), N'X�a', N'X�a sinh vi�n Ho�ng Anh E');

INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro, MaNguoiDung, MaSV, MaGV) VALUES
('admin', '123456', 'Admin', NULL, NULL, NULL),
('GV001', '123456', 'GiangVien', 'GV001', NULL, 'GV001'),
('SV001', '123456', 'SinhVien', 'SV001', 'SV001', NULL),
('GV002', '123456', 'GiangVien', 'GV002', NULL, 'GV002'),
('SV002', '123456', 'SinhVien', 'SV002', 'SV002', NULL);
