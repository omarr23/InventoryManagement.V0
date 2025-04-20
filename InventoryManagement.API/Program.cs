using InventoryManagement.BLL.Interfaces;
using InventoryManagement.BLL.Services;
using InventoryManagement.DAL;
using InventoryManagement.DAL.Interfaces;
using InventoryManagement.DAL.Models;
using InventoryManagement.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.API.Seeding;

var builder = WebApplication.CreateBuilder(args);

// =============================================
// 📦 1. Database & Identity Configuration
// =============================================
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<InventoryDbContext>()
    .AddDefaultTokenProviders();

// =============================================
// 🔐 2. Authentication & Authorization
// =============================================
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// =============================================
// 🛠️ 3. App Services & Repositories
// =============================================
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// =============================================
// 🌐 4. MVC + Swagger
// =============================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// =============================================
// 🧪 5. Seed Roles on Startup
// =============================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RoleSeeder.SeedRolesAsync(services);
}

// =============================================
// 🚦 6. Middleware Pipeline
// =============================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
