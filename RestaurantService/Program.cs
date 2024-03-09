using Microsoft.EntityFrameworkCore;
using RestaurantService.Extensions;
using RestaurantService.HttpClientServices;
using RestaurantService.Infra.Data;
using RestaurantService.Infra.Repository;
using RestaurantService.RabbitMQClient;
using System;

var builder = WebApplication.CreateBuilder(args);

Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Program>()
                              .UseUrls("http://0.0.0.0:80"); // Configura para escutar em todas as interfaces IPv4 na porta 80
                });

// Add services to the container.
builder.Services.AddControllers();
var databaseConnectionString = builder.Configuration.GetConnectionString("databaseConnectionStringRestaurantServiceApi");

builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseMySql(databaseConnectionString, ServerVersion.AutoDetect(databaseConnectionString)));

builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<IItemServiceHttpClient, ItemServiceHttpClient>();
builder.Services.AddSingleton<IRabbitMqClient, RabbitMqClient>();
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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();
