using Application;
using Application.Common.Mappings;
using Application.Courses.Commands.CreateCommands;
using Application.Courses.Queries.GetCourseDetails;
using Application.Courses.Queries.GetCourseList;
using Application.Interfaces;
using Domain.Entities;
using Jose;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistance;
using System.Reflection;
using Win.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistance(builder.Configuration);

//var connectionString = builder.Configuration.GetConnectionString("SqlServerDbContextConnection");
//builder.Services.AddDbContext<BaseDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    cfg.AddProfile(new AssemblyMappingProfile(typeof(ICourseDbContext).Assembly));
    cfg.AddProfile(new AssemblyMappingProfile(typeof(IEmployeeDbContext).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var SecretKey = builder.Configuration.GetSection("JwtSettings:SecretKey").Value;
var Issuer = builder.Configuration.GetSection("JwtSettings:Issuer").Value;
var Audience = builder.Configuration.GetSection("JwtSettings:Audience").Value;
//var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
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
            //IssuerSigningKey = signingKey,
            ValidateIssuerSigningKey = true
        };
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
