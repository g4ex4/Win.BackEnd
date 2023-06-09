using Application;
using Application.Common.Mappings;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistance;
using System.Reflection;
using System.Text;
using Application.JWT;
using Win.WebApi.Middleware;
using Application.Managers;
using Application.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Application.Repositories;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Application.Common.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<IdentityDbContext>();
builder.Services.AddSingleton<SMTPConfig>(provider => BindConfiguration(provider));
builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddMemoryCache();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<EmailService>();
builder.Services.AddTransient<RegisterUserHandler>();
builder.Services.AddTransient<CheckCodeHandler>();
builder.Services.AddTransient<LoginUserHandler>();
builder.Services.AddTransient<EmailHandler>();
builder.Services.AddTransient<AuthManager>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    cfg.AddProfile(new AssemblyMappingProfile(typeof(ICourseDbContext).Assembly));
    cfg.AddProfile(new AssemblyMappingProfile(typeof(IUserCourseDbContext).Assembly));
    cfg.AddProfile(new AssemblyMappingProfile(typeof(ISubDbContext).Assembly));
});
builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
})
    .AddEntityFrameworkStores<BaseDbContext>();
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var SecretKey = builder.Configuration.GetSection("JwtSettings:SecretKey").Value;
var Issuer = builder.Configuration.GetSection("JwtSettings:Issuer").Value;
var Audience = builder.Configuration.GetSection("JwtSettings:Audience").Value;
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

builder.Services.AddScoped(typeof(EmailService));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "jwt_token_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "This Enter JWT Token"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
       {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        },
        new string[] {}
       }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = Issuer,
            ValidateAudience = true,
            ValidAudience = Audience,
            ValidateLifetime = true,
            IssuerSigningKey = signingKey,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExceptionHandler();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
SMTPConfig? BindConfiguration(IServiceProvider provider)
{
    var envName = builder.Environment.EnvironmentName;

    var config = new ConfigurationBuilder()
        .AddJsonFile($"appsettings.json")
        .Build();

    var configService = config.Get<SMTPConfig>();
    return configService;
}