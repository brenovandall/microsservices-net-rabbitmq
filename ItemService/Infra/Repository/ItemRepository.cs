using ItemService.Infra.DTO;

namespace ItemService.Infra.Repository;

public class ItemRepository : IItemRepository
{
    public Task<RestaurantReadDto> CreateRestaurant(RestaurantCreateDto restaurant)
    {
        throw new NotImplementedException();
    }

    public Task<ItemReadDto> CreateRestaurant(int restaurantId, ItemCreateDto item)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<RestaurantReadDto>> GetAllRestaurantsList()
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<ItemReadDto>> GetItemsFromRestaurant(int restaurantId)
    {
        throw new NotImplementedException();
    }

    public Task<ItemReadDto> GetOnlyOneRestaurantById(int restaurantId, int id)
    {
        throw new NotImplementedException();
    }

    public bool IsExternalRestaureantExisted(int externalRestaurantId)
    {
        throw new NotImplementedException();
    }

    public bool IsRestaureantExisted(int restaurantId)
    {
        throw new NotImplementedException();
    }
}
