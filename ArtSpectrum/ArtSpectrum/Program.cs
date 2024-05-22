using ArtSpectrum.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ArtSpectrum.Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using ArtSpectrum.Policies;
using ArtSpectrum.Repository.Repositores.Interface;
using ArtSpectrum.Repository.Repositores.Implementation;
using ArtSpectrum.Services.Implementation;
using ArtSpectrum.Services.Interface;
using ArtSpectrum.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ArtSpectrumDBContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//Add Validation && not use "ModelStateInvalidFilter"

builder.Services.Configure<ApiBehaviorOptions>(opts =>
{
    opts.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Add filter "ValidateRequestFilter"

builder.Services.AddControllersWithViews(opts =>
{
    opts.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));

    // Set order very high to allow other filters to include their own validation results.
    opts.Filters.Add<ValidateRequestFilter>(int.MaxValue - 100);
});

// Primary services
builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Add a implementation "Repositories"
builder.Services.AddTransient<IBaseRepository<User>, UserRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();


// Add a implement "Service"
builder.Services.AddScoped<IUserService, UserService>();


// Middlewares & Filters
builder.Services.AddScoped<ExceptionMiddleware>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
