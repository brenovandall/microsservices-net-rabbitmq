using RestaurantService.Infra.DTO;

namespace RestaurantService.RabbitMQClient;

public interface IRabbitMqClient
{
    void PublishRestaurantAtRabbitMqQueue(RestaurantHttpRequest restaurantHttpRequest);
}
