using RestaurantService.Infra.DTO;

namespace RestaurantService.HttpClientServices;

public interface IItemServiceHttpClient
{
    public void SendRestaurantForItemServiceRestaurant(RestaurantHttpRequest restaurantHttpRequest);
}
