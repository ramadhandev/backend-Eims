using EIMS.Data;
using EIMS.Models;
using EIMS.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Add koneksi
builder.Services.AddDbContext<EimsDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectToDb")));
builder.Services.AddScoped<CardTypeServices>();
builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DocumentRqService>();
builder.Services.AddScoped<UserDocumentService>();
builder.Services.AddScoped<IncidentReportServices>();
builder.Services.AddScoped<InvestigationServices>();
builder.Services.AddScoped<PermitRequirementServices>();
builder.Services.AddScoped<TrainingServices>();
builder.Services.AddScoped<HseCardServices>();
builder.Services.AddScoped<PermitToWorkServices>();
builder.Services.AddScoped<ApprovalServices>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddHttpContextAccessor();



// Untuk mengizinkan file upload besar (jika diperlukan)
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10485760; // 10MB
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJs",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // alamat frontend
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowNextJs");
    app.UseStaticFiles();

    app.UseAuthorization();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
