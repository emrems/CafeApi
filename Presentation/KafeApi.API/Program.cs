﻿using KafeApi.Application.Interfaces;
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

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // Servis kaydı
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<ICategoriyServices, CategoryServices>();
        builder.Services.AddScoped<IMenuItemServices, MenuItemServices>();
        builder.Services.AddScoped<ITableRepository, TableRepository>();
        builder.Services.AddScoped<ITableServices, TableService>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

        // AutoMapper konfigürasyonu
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // FluentValidation konfigürasyonu
        builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryDto>();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdateCategoryDto>();

        builder.Services.AddValidatorsFromAssemblyContaining<CreateMenuItemDto>();

        builder.Services.AddValidatorsFromAssemblyContaining<UpdateMenuItemDto>();

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