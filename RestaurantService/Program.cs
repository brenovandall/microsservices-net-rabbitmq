using Microsoft.EntityFrameworkCore;
using RestaurantService.Infra.Data;
using RestaurantService.Infra.Repository;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var databaseConnectionString = builder.Configuration.GetConnectionString("databaseConnectionStringRestaurantServiceApi");

builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseMySql(databaseConnectionString, ServerVersion.AutoDetect(databaseConnectionString)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();

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
