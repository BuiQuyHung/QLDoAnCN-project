using Microsoft.EntityFrameworkCore;
using QLDoAnAPI.Models;

namespace QLDoAnAPI.Data
{
    public class QLDoAnChuyenNganhDbContext : DbContext
    {
        public QLDoAnChuyenNganhDbContext(DbContextOptions<QLDoAnChuyenNganhDbContext> options) : base(options)
        {
        }

        public DbSet<Khoa> Khoas { get; set; }
        public DbSet<BoMon> BoMons { get; set; }
        public DbSet<Nganh> Nganhs { get; set; }
        public DbSet<ChuyenNganh> ChuyenNganhs { get; set; }
        public DbSet<Lop> Lops { get; set; }
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<GiangVien> GiangViens { get; set; }
        public DbSet<DeTai> DeTais { get; set; }
        public DbSet<PhanCong> PhanCongs { get; set; }
        public DbSet<TienDo> TienDos { get; set; }
        public DbSet<DanhGia> DanhGias { get; set; }
        public DbSet<HoiDong> HoiDongs { get; set; }
        public DbSet<ThanhVienHoiDong> ThanhVienHoiDongs { get; set; }
        public DbSet<TaiKhoanDangNhap> TaiKhoanDangNhap { get; set; }
    
        public DbSet<DotDoAn> DotDoAns { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                       
            modelBuilder.Entity<BoMon>()
                .HasOne(bm => bm.Khoa)
                .WithMany(k => k.BoMons)
                .HasForeignKey(bm => bm.MaKhoa)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Nganh>()
                .HasOne(n => n.BoMon)
                .WithMany(bm => bm.Nganhs)
                .HasForeignKey(n => n.MaBoMon)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChuyenNganh>()
                .HasOne(cn => cn.Nganh)
                .WithMany(n => n.ChuyenNganhs) 
                .HasForeignKey(cn => cn.MaNganh)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Lop>()
                .HasOne(l => l.ChuyenNganh)
                .WithMany() // Một ChuyenNganh có thể có nhiều Lop, nhưng không cần thuộc tính Collection ở ChuyenNganh nếu không cần thiết
                .HasForeignKey(l => l.MaChuyenNganh)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SinhVien>()
                .HasOne(sv => sv.Lop)
                .WithMany() // Một Lop có thể có nhiều SinhVien, nhưng không cần thuộc tính Collection ở Lop nếu không cần thiết
                .HasForeignKey(sv => sv.MaLop)
                .OnDelete(DeleteBehavior.Restrict);

            // Mối quan hệ giữa DeTai và GiangVien (một GiangVien có thể hướng dẫn nhiều DeTai)
            modelBuilder.Entity<DeTai>()
                .HasOne(dt => dt.GiangVien)
                .WithMany() // Một GiangVien có thể có nhiều DeTai, không cần Collection ở GiangVien nếu không cần thiết
                .HasForeignKey(dt => dt.MaGV)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<GiangVien>()
            //     .HasOne(gv => gv.BoMon)
            //     .WithMany()
            //     .HasForeignKey(gv => gv.MaBoMon)
            //     .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PhanCong>()
                 .HasKey(pc => new { pc.MaDeTai, pc.MaSV });

            modelBuilder.Entity<ThanhVienHoiDong>()
                .HasKey(tv => new { tv.MaHoiDong, tv.MaGV });

            modelBuilder.Entity<PhanCong>()
                .HasKey(pc => new { pc.MaDeTai, pc.MaSV });

            modelBuilder.Entity<PhanCong>()
                .HasOne(pc => pc.DeTai)
                .WithMany() // Một DeTai có thể được phân công cho nhiều SinhVien
                .HasForeignKey(pc => pc.MaDeTai)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PhanCong>()
                .HasOne(pc => pc.SinhVien)
                .WithMany() // Một SinhVien có thể được phân công nhiều DeTai
                .HasForeignKey(pc => pc.MaSV)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TienDo>()
                .HasOne(td => td.DeTai)
                .WithMany()
                .HasForeignKey(td => td.MaDeTai)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TienDo>()
                .HasOne(td => td.SinhVien)
                .WithMany()
                .HasForeignKey(td => td.MaSV)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DanhGia>()
                .HasOne(dg => dg.DeTai)
                .WithMany()
                .HasForeignKey(dg => dg.MaDeTai)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DanhGia>()
                .HasOne(dg => dg.GiangVien)
                .WithMany()
                .HasForeignKey(dg => dg.MaGV)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ThanhVienHoiDong>()
                .HasKey(tv => new { tv.MaHoiDong, tv.MaGV });

            //modelBuilder.Entity<ThanhVienHoiDong>()
            //    .HasOne(tv => tv.HoiDong)
            //    .WithMany(hd => hd.ThanhVienHoiDongs)
            //    .HasForeignKey(tv => tv.MaHoiDong)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ThanhVienHoiDong>()
                .HasOne(tv => tv.GiangVien)
                .WithMany()
                .HasForeignKey(tv => tv.MaGV)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}