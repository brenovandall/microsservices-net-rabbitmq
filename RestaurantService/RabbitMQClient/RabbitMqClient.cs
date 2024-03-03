using RabbitMQ.Client;
using RestaurantService.Infra.DTO;
using System.Text;
using System.Text.Json;

namespace RestaurantService.RabbitMQClient;
public class RabbitMqClient : IRabbitMqClient
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    public RabbitMqClient(IConfiguration configuration)
    {
        _configuration = configuration;
        _connection = new ConnectionFactory() { HostName = _configuration["RabbitMQHost"], Port = Int32.Parse(_configuration["RabbitMQPort"]) }.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
    }
    public void PublishRestaurantAtRabbitMqQueue(RestaurantHttpRequest restaurantHttpRequest)
    {
        var requestToQueue = JsonSerializer.Serialize(restaurantHttpRequest);

        var requestBody = Encoding.UTF8.GetBytes(requestToQueue);

        _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: requestBody);
    }
}
