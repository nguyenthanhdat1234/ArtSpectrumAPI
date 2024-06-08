using ArtSpectrum.Repository.Models;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using ArtSpectrum.Utils;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<ArtSpectrumDBContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// REGISTER a JWT authentication 
builder.Services.AddScoped<GenerateJwtTokenFilter>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


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


builder.Services.AddTransient<GenerateJSONWebTokenHelper>();
// Add a implementation "Repositories"
builder.Services.AddTransient<IBaseRepository<User>, UserRepository>();
builder.Services.AddTransient<IBaseRepository<Artist>, ArtistRepository>();
builder.Services.AddTransient<IBaseRepository<Painting>, PaintingRepository>();
builder.Services.AddTransient<IBaseRepository<Sale>, SaleRepository>();
builder.Services.AddTransient<IBaseRepository<Order>, OrderRepository>();
builder.Services.AddTransient<IBaseRepository<OrderDetail>, OrderDetailRepository>();
builder.Services.AddTransient<IBaseRepository<Cart>, CartRepository>();
builder.Services.AddTransient<IBaseRepository<Review>, ReviewRepository>();
builder.Services.AddTransient<IBaseRepository<Category>, CategoryRepository>();
builder.Services.AddTransient<IBaseRepository<PaintingCategory>, PaintingCategoryRepository>();
builder.Services.AddTransient<IBaseRepository<Blog>, BlogRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();


// Add a implement "Service"
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IPaintingService, PaintingService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPaintingCategoryService, PaintingCategoryService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IBlogService, BlogService>();


// Middlewares & Filters
builder.Services.AddScoped<ExceptionMiddleware>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add Cors Policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger(options =>
{
    options.RouteTemplate = "swagger/{documentName}/swagger.json";
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ArtSpectrum");

    c.RoutePrefix = "";
    c.EnableTryItOutByDefault();
});

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
