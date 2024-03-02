using Microsoft.EntityFrameworkCore;
using RestaurantService.Extensions;
using RestaurantService.HttpClientServices;
using RestaurantService.Infra.Data;
using RestaurantService.Infra.Repository;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
var databaseConnectionString = builder.Configuration.GetConnectionString("databaseConnectionStringRestaurantServiceApi");

builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseMySql(databaseConnectionString, ServerVersion.AutoDetect(databaseConnectionString)));

builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<IItemServiceHttpClient, ItemServiceHttpClient>();
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
