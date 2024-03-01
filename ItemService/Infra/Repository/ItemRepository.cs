using AutoMapper;
using ItemService.Infra.Data;
using ItemService.Infra.DTO;
using ItemService.Infra.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemService.Infra.Repository;

public class ItemRepository : IItemRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public ItemRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<RestaurantReadDto> CreateRestaurant(RestaurantCreateDto restaurant)
    {
        if(restaurant is not null)
        {
            var mappedRestaurant = _mapper.Map<RestaurantCreateDto, Restaurant>(restaurant);

            if(mappedRestaurant is not null)
            {
                var restaurantToReturn = _mapper.Map<Restaurant, RestaurantReadDto>(mappedRestaurant);

                return restaurantToReturn;
            }

            return null;
        }

        return null;
    }

    public async Task<ItemReadDto> CreateItem(int restaurantId, ItemCreateDto item)
    {
        var idDoRestaurante = await _context.Restaurants.FirstOrDefaultAsync(item => item.Id == restaurantId);

        var newItem = new Item
        {
            Name = item.Name,
            Price = item.Price,
            RestaurantId = item.RestaurantId,
            Restaurant = idDoRestaurante
        };

        idDoRestaurante.ItemsOfRestaurant.Add(newItem);

        var itemToReturn = new ItemReadDto
        {
            Name = newItem.Name,
            Price = newItem.Price,
            Restaurant = idDoRestaurante.Name
        };

        if(itemToReturn != null) return itemToReturn;

        return null;

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
