using QLDoAnAPI.Data;
using QLDoAnAPI.Interfaces;
using QLDoAnAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using YourNamespace.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

builder.Services.AddAuthorization();

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
builder.Services.AddScoped<IDotDoAnRepository, DotDoAnRepository>();
// ...

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowSpecificOrigin"); 

app.MapControllers();

app.Run();