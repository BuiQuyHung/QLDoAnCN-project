using QLDoAnAPI.Data;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cấu hình CORS (giữ nguyên)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<QLDoAnChuyenNganhDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QLDoAnDatabase"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure();
        }));

// Đăng ký Repository
builder.Services.AddScoped<IKhoaRepository, KhoaRepository>();
builder.Services.AddScoped<IBoMonRepository, BoMonRepository>();
builder.Services.AddScoped<INganhRepository, NganhRepository>();
builder.Services.AddScoped<IChuyenNganhRepository, ChuyenNganhRepository>();
builder.Services.AddScoped<ILopRepository, LopRepository>();
builder.Services.AddScoped<ISinhVienRepository, SinhVienRepository>();
builder.Services.AddScoped<IGiangVienRepository, GiangVienRepository>();
builder.Services.AddScoped<IDeTaiRepository, DeTaiRepository>();
builder.Services.AddScoped<IPhanCongRepository, PhanCongRepository>();
builder.Services.AddScoped<ITienDoRepository, TienDoRepository>();
builder.Services.AddScoped<IDanhGiaRepository, DanhGiaRepository>();
builder.Services.AddScoped<IHoiDongRepository, HoiDongRepository>();
builder.Services.AddScoped<IThanhVienHoiDongRepository, ThanhVienHoiDongRepository>();
builder.Services.AddScoped<ITaiKhoanRepository, TaiKhoanRepository>();
// ...

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
//app.UseAuthentication(); // Thêm middleware xác thực

//app.UseAuthorization(); // Thêm middleware phân quyền

app.UseCors("AllowSpecificOrigin"); // Sử dụng policy CORS

//app.UseAuthorization();

app.MapControllers();

app.Run();