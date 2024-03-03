using AutoMapper;
using ItemService.Infra.DTO;
using ItemService.Infra.Entities;
using ItemService.Infra.Repository;
using System.Text.Json;

namespace ItemService.EventProcessor;

public class ExecuteEvent : IExecuteEvent
{
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public ExecuteEvent(IMapper mapper, IServiceScopeFactory serviceScopeFactory)
    {
        _mapper = mapper;
        _serviceScopeFactory = serviceScopeFactory;

    }
    public void Execute(string message)
    {
        using var scope = _serviceScopeFactory.CreateScope();

        var itemRepository = scope.ServiceProvider.GetRequiredService<IItemRepository>();

        var restaurantHttpResponse = JsonSerializer.Deserialize<RestaurantHttpRequest>(message);

        if (!itemRepository.IsExternalRestaureantExisted(restaurantHttpResponse.Id))
        {
            var newRestaurant = new Restaurant
            {
                ExternalId = restaurantHttpResponse.Id,
                Name = restaurantHttpResponse.Name,
                ItemsOfRestaurant = new List<Item>()
            };

            if(newRestaurant is not null)
            {
                var mappedRestaurantToCreate = _mapper.Map<Restaurant, RestaurantCreateDto>(newRestaurant);

                itemRepository.CreateRestaurant(mappedRestaurantToCreate);
            }
        }
    }
}
