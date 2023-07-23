using CleanArchitecture.Application.Services.Implementations;
using CleanArchitecture.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Application.CQRS.ProductFiles.Commands;
using CleanArchitecture.Application.Dtos.Validators;
using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Domain.Settings;
using CleanArchitecture.Infrastructure.IdentityServices;
using CleanArchitecture.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CleanArchitecture.API.Utilities;
using CleanArchitecture.API.Utilities.Exceptions;
using CleanArchitecture.API.Utilities.Api;
using System.Net;
using System.Security.Claims;
using CleanArchitecture.API.Mapping;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CleanArchitecture.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //var testConnectionString = "Server=(localdb)\\mssqllocaldb;Database=CleanArchitecture.Test;Trusted_Connection=True";
            var testConnectionString = "Data Source=.;Initial Catalog=CleanArchitectureDb;Integrated Security=True;TrustServerCertificate=True;";

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(testConnectionString);
            });

            services.AddIdentity<IdentityUser, IdentityRole>(identityOptions =>
            {
                identityOptions.Password.RequireDigit = true;
                identityOptions.Password.RequiredLength = 6;
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequireUppercase = false;
                identityOptions.Password.RequireLowercase = false;
            })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            var _jwtSettings = new JwtSettings()
            {
                Audience = "MyWebsite",
                Issuer = "MyWebsite",
                SecretKey = "LongerThan-16Char-SecretKey",
                Encryptkey = "16CharEncryptKey",
                ExpirationMinutes = 60,
                NotBeforeMinutes = 0
            };

            services.AddSingleton(_jwtSettings);

            services.AddScoped<IJwtService, JwtService>();
            services.AddSingleton<WebApplicationFactory<Program>>();
        }
    }
}
