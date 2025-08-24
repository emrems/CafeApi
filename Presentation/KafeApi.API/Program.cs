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

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Servisleri konteynere ekle (Add services to the container)
        // Her şey app.Build() öncesinde burada olmalı

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // Servis kaydı
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<ICategoriyServices, CategoryServices>();
        builder.Services.AddScoped<IMenuItemServices, MenuItemServices>();

        // AutoMapper konfigürasyonu
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // OpenAPI servisini ekle
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // HTTP istek hattını yapılandır (Configure the HTTP request pipeline)
        // Her şey app.Build() sonrasında burada olmalı

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.MapScalarApiReference(
            opt =>
            {
                opt.Title = "KafeApi API Reference";
                opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
                opt.Theme = ScalarTheme.BluePlanet;
            }
        );

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}