﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using ProjectManager.Data;
using System.Text;
using ProjectManager.Application.Interfaces;
using ProjectManager.Application.Services;

namespace ProjectManager.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectManagerDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
                configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            var jwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY")
    ?? configuration["JWT:SecretKey"];

            var key = Encoding.ASCII.GetBytes(jwtSecretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization();
            services.AddHttpContextAccessor();

            services.AddScoped<IUserInterface, UserService>();

            // Aqui você pode adicionar outros serviços ao contêiner de injeção de dependência
            // Exemplo:
            // services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}