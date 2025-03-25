using BookingCare.Business.Services;
using BookingCare.Data.Data;
using BookingCare.Data.Models;
using BookingCare.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
});
builder.Services.AddScoped<DoctorRepository>();

//builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
//{
//    // Tùy ch?nh yêu c?u m?t kh?u
//    options.Password.RequireDigit = false; // Không yêu c?u ch? s?
//    options.Password.RequireLowercase = true;
//    options.Password.RequireUppercase = false; // Không yêu c?u ch? hoa
//    options.Password.RequireNonAlphanumeric = false; // Không yêu c?u ký t? ??c bi?t
//    options.Password.RequiredLength = 6; // ?? dài t?i thi?u là 6
//    options.Password.RequiredUniqueChars = 1;
//})
//    .AddEntityFrameworkStores<AppnDbContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<ClinicRepository>();
builder.Services.AddScoped<ClinicService>();


builder.Services.AddScoped<PatientService>();
// If PatientRepository is also used, it should be registered as well
builder.Services.AddScoped<PatientRepository>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
