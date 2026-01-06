using Business.Abstract.Products;
using Business.Abstract.Users;
using Business.Concrete.Products;
using Business.Concrete.Users;
using Business.DTOs;
using Business.Extensions; // Bunu ekle
using DataAccess.Abstract.Products;
using DataAccess.Abstract.Users;
using DataAccess.Concrete.EntityFramework.Products;
using DataAccess.Concrete.EntityFramework.Users;
using Infrastructure.Security.Abstract;
using Infrastructure.Security.Concrete;
using Infrastructure.Security.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presentation_API.Helpers;
using Presentation_API.Middlewares;
using System.Security.Claims;
using System.Text;
using System.Text.Json;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpLogging(_ => { });
builder.Services.AddHttpContextAccessor(); // Business katmanında token' a erişmem için

// 1. Handler'ı servislere ekle
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

// 2. Authorization servisini yapılandır
builder.Services.AddAuthorization(options =>
{
    // Buraya şimdilik manuel policy eklemene gerek yok, 
    // aşağıda yazacağımız Attribute ile dinamik halledeceğiz.
});


// 2. Helper Servisleri
builder.Services.AddSingleton<ISecurityHelper, SecurityHelper>();

// 3. Data Access (Repository) Kayıtları
builder.Services.AddScoped<IUserRepository, EfUserRepository>();
builder.Services.AddScoped<IAddressRepository, EfAddressRepository>();
builder.Services.AddScoped<IUserRoleRepository, EfUserRoleRepository>();
builder.Services.AddScoped<IRoleRepository, EfRoleRepository>();
builder.Services.AddScoped<IProductRepository, EfProductRepository>();
builder.Services.AddScoped<ICategoryRepository, EfCategoryRepository>();
builder.Services.AddScoped<IBrandRepostory, EfBrandRepository>();
builder.Services.AddScoped<IUserPermissionRepository, EfUserPermissionRepository>();

builder.Services.AddScoped<UserExtension>();
builder.Services.AddScoped<LogMiddleware>();


// 4. Business Servis Kayıtları
// UserManagement'ın tüm bağımlılıkları (Repository, ITokenHelper) DI tarafından çözülecektir.
builder.Services.AddScoped<IUserService, UserManagement>();
builder.Services.AddScoped<IUserRoleService, UserRoleManagement>();
builder.Services.AddScoped<IProductService, ProductManagement>();
builder.Services.AddScoped<IBrandService, BrandManagement>();
builder.Services.AddScoped<ICategoryService, CategoryManagement>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// 1. Token Ayarlarını Konfigürasyondan Oku (TokenOption)
var tokenOptionsSection = builder.Configuration.GetSection("TokenOptions");
var tokenOptions = tokenOptionsSection.Get<TokenOptions>()!;

// KRİTİK KONTROL: Eğer json'da hata varsa uygulama başlamasın, hata versin
if (tokenOptions == null || string.IsNullOrEmpty(tokenOptions.SecurityKey))
{
    throw new Exception("HATA: appsettings.json içinde 'TokenOptions' veya 'SecurityKey' bulunamadı!");
}

builder.Services.AddSingleton(tokenOptions);


// 2.ITokenHelper'ı Kayder (DI)
builder.Services.AddSingleton<ITokenHelper, JwtHelper>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true, // Bunu eklemeyi unutma, güvenliği sağlar
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
            RoleClaimType = ClaimTypes.Role // Roles="Admin" kontrolünün çalışması için bu kritik!
        };

        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";

                var unauthorizedResponse = new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "Yetkilendirme Gerekli. Lütfen geçerli bir Access Token ile istek yapın.",
                    Data = null
                };

                return context.Response.WriteAsync(JsonSerializer.Serialize(unauthorizedResponse));
            },
            // --- BURASI YENİ: Admin olmayanlar için (403 Forbidden) ---
            OnForbidden = context =>
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";

                var forbiddenResponse = new ApiResponseDto<object>
                {
                    Success = false,
                    Message = "Erişim Reddedildi. Bu işlem için yetkiniz bulunmamaktadır.",
                    Data = null
                };

                return context.Response.WriteAsync(JsonSerializer.Serialize(forbiddenResponse));
            }
        };
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Token'ı 'Bearer ' kelimesiyle başlatarak girin. Örn: 'Bearer eyJhbGciOiJIUzI1Ni...' \r\n",
    });

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

var app = builder.Build();

// --- HIZLI ERIŞIM (MINIMAL API) ---
var apiGroup = app.MapGroup("/api");

// 1. Marka Servisleri
apiGroup.MapGet("/Brands", (IBrandService brandService) =>
{
    var result = brandService.GetAll();
    return MinimalResultHandler.HandleDataResult(result);
});

apiGroup.MapGet("/Brand/{id:int}", (int id, IBrandService brandService) =>
{
    var result = brandService.GetBrand(id);
    return MinimalResultHandler.HandleDataResult(result);
});

// 2. Kategori Servisleri
apiGroup.MapGet("/Categories", (ICategoryService categoryService) =>
{
    var result = categoryService.GetAll();
    return MinimalResultHandler.HandleDataResult(result);
});

apiGroup.MapGet("/Categories/{id:int}", (int id, ICategoryService categoryService) =>
{
    var result = categoryService.GetById(id);
    return MinimalResultHandler.HandleDataResult(result);
});

// 3. Kategoriye Göre Ürünler (E-ticaretin en yoğun trafiği)
apiGroup.MapGet("/Categories/{id:int}/products", (int id, IProductService productService) =>
{
    var result = productService.GetProductsByCategoryId(id);
    return MinimalResultHandler.HandleDataResult(result);
});

// 4. Slug Üzerinden Ürün Getirme (SEO için kritik)
apiGroup.MapGet("/Categories/slug/{slug}/products", (string slug, IProductService productService) =>
{
    var result = productService.GetProductsByCategorySlug(slug);
    return MinimalResultHandler.HandleDataResult(result);
});

//5. Product üzerinden kategori getirme
apiGroup.MapGet("/Product/{id:int}/categories", (int id, ICategoryService categoryService) =>
{
    var result = categoryService.GetCategoryProductById(id);
    return MinimalResultHandler.HandleDataResult(result);
});
//5. id ile ürün çekme
apiGroup.MapGet("/Product/{id:int}", (int id, IProductService productService) =>
{
    var result = productService.GetById(id);
    return MinimalResultHandler.HandleDataResult(result);
});


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<LogMiddleware>();

app.UseHttpsRedirection();

//app.UseHttpLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

