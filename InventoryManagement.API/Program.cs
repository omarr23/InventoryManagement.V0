using InventoryManagement.BLL.manager;
using InventoryManagement.DAL;
using InventoryManagement.DAL.Interfaces;
using InventoryManagement.DAL.Models;
using InventoryManagement.DAL.Repositories;
using InventoryManagement.API.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.DAL.Repository.UserRepository;
using InventoryManagement.BLL.manager.user;
using InventoryManagement.BLL.manager.auth;
using InventoryManagement.DAL.Repository.CompanyRepository;
using InventoryManagement.BLL.manager.company;
using InventoryManagement.BLL.manager.services;
using InventoryManagement.BLL.manager.ProductService;
using InventoryManagement.DAL.Repository.ProductRepository;
using InventoryManagement.BLL.manager.InventoryService;
using InventoryManagement.DAL.Repository.InventoryRepository;
using InventoryManagement.BLL.manager.SupplierService;
using InventoryManagement.DAL.Repository.SupplierRepository;
using InventoryManagement.BLL.manager.PaymentService;
using InventoryManagement.DAL.Repository.PaymentRepository;
using InventoryManagement.BLL.manager.SupplierProductService;
using InventoryManagement.DAL.Repository.SupplierProductRepository;
using InventoryManagement.BLL.manager.InventoryProductService;
using InventoryManagement.DAL.Repository.InventoryProductRepository;
using InventoryManagement.API.Middleware;
using Microsoft.Extensions.Caching.Memory;
using InventoryManagement.BLL.DTO.Common;


var builder = WebApplication.CreateBuilder(args);

// =============================================
//  1. Database & Identity Configuration
// =============================================
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<InventoryDbContext>()
    .AddDefaultTokenProviders();

// =============================================
//  2. JWT Authentication & Authorization
// =============================================
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
        ),
        ClockSkew = TimeSpan.Zero
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context => Task.CompletedTask,
        OnTokenValidated = context => Task.CompletedTask,
        OnMessageReceived = context => Task.CompletedTask,
        OnChallenge = context => Task.CompletedTask
    };
});

builder.Services.AddAuthorization();

// =============================================
//  3. App Services & Repositories
// =============================================
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ISupplierProductService, SupplierProductService>();
builder.Services.AddScoped<ISupplierProductRepository, SupplierProductRepository>();
builder.Services.AddScoped<IInventoryProductRepository, InventoryProductRepository>();
builder.Services.AddScoped<IInventoryProductService, InventoryProductService>();
builder.Services.AddMemoryCache();
// =============================================
//  4. MVC + Swagger + JWT Bearer Auto-Prepend
// =============================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "InventoryManagement.API",
        Version = "v1"
    });

    // Auto-prepend "Bearer" in Swagger UI
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer", // Important for auto-prepend
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Just paste the token below."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// =============================================
//  5. Seed Roles on Startup
// =============================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RoleSeeder.SeedRolesAsync(services);
}

// =============================================
//  6. Middleware Pipeline
// =============================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<UnauthorizedMessageMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
