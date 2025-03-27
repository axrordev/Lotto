using Lotto.Data.DbContexts;
using Lotto.WebApi.Extensions;
using Lotto.WebApi.MapperConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;

var wwwrootPath = Path.GetFullPath("wwwroot");
if (!Directory.Exists(wwwrootPath))
{
    Directory.CreateDirectory(wwwrootPath);
}

var builder = WebApplication.CreateBuilder(args);



// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


// Xizmatlarni ro'yxatdan o'tkazish
builder.Services.AddControllers();

// PostgreSQL bilan bog'lanish
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper konfiguratsiyasi
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Xotira keshini qo'shish
builder.Services.AddMemoryCache();

// API xizmatlari va biznes xizmatlarini ro'yxatdan o'tkazish
builder.Services.AddApiServices();
builder.Services.AddServices(builder.Configuration);

// Validatorlarni qo'shish
builder.Services.AddValidators();

// Swagger konfiguratsiyasi va xizmatlari
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

// Xatolikni boshqarish (Exception middleware'lari)
builder.Services.AddExceptions();

// JWT autentifikatsiyasini qo'shish
builder.Services.AddJwt(builder.Configuration);

// HTTP kontekst uchun qo'llanma
builder.Services.AddHttpContextAccessor();

// CORS siyosatini qo'shish (frontend URL'ini qo'shing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("https://axidel.netlify.app")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Ilovani yaratish
var app = builder.Build();

// CORS siyosatini ishlatish
app.UseCors("AllowSpecificOrigin");

// Qo'shimcha yordamchi funksiyalarni sozlash
app.AddInjectHelper();
app.AddPathInitializer();

// Statik fayllarni serve qilish
app.UseStaticFiles(); // Bu qatorni qo'shing

// Swagger uchun yo'l konfiguratsiyasi
app.UseSwagger(c => c.RouteTemplate = "swagger/{documentName}/swagger.json");
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lotto API V1");
    c.RoutePrefix = "swagger";
});

// Xatoliklarni boshqarish uchun maxsus yo'l
app.UseExceptionHandler("/error");

// HTTPS orqali ulanishni majburiy qilish
app.UseHttpsRedirection();

// Autentifikatsiya va avtorizatsiya middleware'larini qo'shish
app.UseAuthentication();
app.UseAuthorization();

// Controller'larni xaritalash
app.MapControllers();

// Ilovani ishga tushirish
app.Run();
