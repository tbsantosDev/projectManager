using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using DotNetEnv;
using ProjectManager.Infra.Ioc;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Usar o método de extensão para adicionar dependências
builder.Services.AddProjectManagerDependencies(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
