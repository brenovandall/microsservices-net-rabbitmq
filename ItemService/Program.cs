using ItemService.EventProcessor;
using ItemService.Infra.Data;
using ItemService.Infra.Repository;
using ItemService.RabbitMQClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Program>()
                              .UseUrls("http://0.0.0.0:80"); // Configura para escutar em todas as interfaces IPv4 na porta 80
                });

// Add services to the container.

var applicationConnectionString = builder.Configuration.GetConnectionString("databaseConnectionStringRestaurantServiceApi");

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySql(applicationConnectionString, ServerVersion.AutoDetect(applicationConnectionString)));

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddHostedService<RabbitMqSubscriber>();
builder.Services.AddSingleton<IExecuteEvent, ExecuteEvent>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
