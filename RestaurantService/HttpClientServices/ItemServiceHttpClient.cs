using Microsoft.AspNetCore.Http.HttpResults;
using RestaurantService.Infra.DTO;
using System.Text;
using System.Text.Json;

namespace RestaurantService.HttpClientServices;

public class ItemServiceHttpClient : IItemServiceHttpClient
{
    private readonly HttpClient _httpClient;
    public ItemServiceHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async void SendRestaurantForItemServiceRestaurant(RestaurantHttpRequest restaurantHttpRequest)
    {
        var httpContent = new StringContent(JsonSerializer.Serialize(restaurantHttpRequest),Encoding.UTF8, "application/json");

        await _httpClient.PostAsync("https://localhost:7105/api/item/restaurant/restaurants/add/new", httpContent);
    }
}
