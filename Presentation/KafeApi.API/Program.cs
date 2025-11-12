using KafeApi.Application.Interfaces;
using KafeApi.Application.Mapper;
using KafeApi.Application.Services.Abstract;
using KafeApi.Application.Services.Concrete;
using KafeApi.Persistance.Context;
using KafeApi.Persistance.Repository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;
using FluentValidation;
using KafeApi.Application.Dtos.CategoryDto;
using KafeApi.Application.Dtos.MenuItemDto;
using KafeApi.Application.Dtos.TableDtos;
using KafeApi.Application.Dtos.OrderDtos;
using KafeApi.Application.Dtos.OrderItemDtos;
using KafeApi.Application.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using KafeApi.Persistance.Context.Identity;
using Microsoft.AspNetCore.Identity;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Identity DbContext ekle
        builder.Services.AddDbContext<AppIdentityAppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        // Identity yapılandırması
        builder.Services.AddIdentity<AppIdentityUser, AppIdentityRole>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequireDigit = true;
            opt.Password.RequiredLength = 6;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequireUppercase = true;
            opt.Password.RequireLowercase = false;
        }).AddEntityFrameworkStores<AppIdentityAppDbContext>().AddDefaultTokenProviders();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        //builder.Services.AddIdentity<AppIdentityUser, IdentityRole>()
        //.AddEntityFrameworkStores<AppDbContext>()
        //.AddDefaultTokenProviders();


        // Servis kaydı
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<ICategoriyServices, CategoryServices>();
        builder.Services.AddScoped<IMenuItemServices, MenuItemServices>();
        builder.Services.AddScoped<ITableRepository, TableRepository>();
        builder.Services.AddScoped<ITableServices, TableService>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IOrderItemService, OrderItemService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAuthService, Authservice>();
        builder.Services.AddScoped<TokenHelpers>();

        // AutoMapper konfigürasyonu
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // FluentValidation konfigürasyonu
        builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryDto>();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdateCategoryDto>();

        builder.Services.AddValidatorsFromAssemblyContaining<CreateMenuItemDto>();

        builder.Services.AddValidatorsFromAssemblyContaining<UpdateMenuItemDto>();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateTableDto>();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdateTableDto>();

        builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderDto>();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdateOrderDto>();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdateOrderItemDto>();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdateOrderItemDto>();


        // OpenAPI servisini ekle
        builder.Services.AddOpenApi();

        // jwt yapılandırması
        builder.Services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

            };
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpContextAccessor();

        var app = builder.Build();
       

        // HTTP istek hattını yapılandır (Configure the HTTP request pipeline)
        // Her şey app.Build() sonrasında burada olmalı

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.MapScalarApiReference(opt =>
        {
            opt.Title = "KafeApi API Reference";
            opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
            opt.Theme = ScalarTheme.BluePlanet;

            // 👇 Bu satırı ekle
           // opt.Servers = new[] { new ScalarServer("http://localhost:5000") };
        });


        app.UseHttpsRedirection();
        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}