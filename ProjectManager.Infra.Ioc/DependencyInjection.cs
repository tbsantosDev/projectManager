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
                options.UseNpgsql(connectionString, b =>
                    b.MigrationsAssembly("ProjectManager.Infra.Data"));
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

            // Configuração do CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", builder =>
                {
                    builder.WithOrigins("http://localhost:3000") // Substitua pelo domínio que você quer permitir
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });

                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            services.AddAuthorization();
            services.AddHttpContextAccessor();

            services.AddScoped<IUserInterface, UserService>();
            services.AddScoped<ITeamInterface, TeamService>();
            services.AddScoped<IProjectInterface, ProjectService>();
            services.AddScoped<ITeamMemberInterface, TeamMemberService>();
            services.AddScoped<ITaskInterface, TaskService>();
            services.AddScoped<ICommentInterface, CommentService>();
            services.AddScoped<ICostInterface, CostService>();

            return services;
        }
    }
}