using AutoMapper;
using ItemService.Infra.Data;
using ItemService.Infra.DTO;
using ItemService.Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Text.Json;

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
        _context.SaveChanges();

        var itemToReturn = new ItemReadDto
        {
            Name = newItem.Name,
            Price = newItem.Price,
            Restaurant = idDoRestaurante.Name
        };

        if(itemToReturn != null) return itemToReturn;

        return null;

    }

    public async Task<IReadOnlyList<RestaurantReadDto>> GetAllRestaurantsList()
    {
        var listOfRestaurants = await _context.Restaurants.ToListAsync();

        var mappedList = _mapper.Map<IReadOnlyList<Restaurant>, IReadOnlyList<RestaurantReadDto>>(listOfRestaurants);

        return mappedList;
    }

    public async Task<IReadOnlyList<ItemReadDto>> GetItemsFromRestaurant(int restaurantId)
    {
        if(restaurantId != null)
        {
            var listOfItemsFromRestaurant = _context.Items.Where(x => x.RestaurantId == restaurantId).ToList();

            if(listOfItemsFromRestaurant is not null) return _mapper.Map<IReadOnlyList<Item>, IReadOnlyList<ItemReadDto>>(listOfItemsFromRestaurant);
        }

        return null;
    }

    public async Task<ItemReadDto> GetOnlyOneItemById(int restaurantId, int id)
    {
        var itemToRequest = await _context.Items.FirstAsync(x => x.RestaurantId == restaurantId && x.Id == id);
        var restaurantOfItem = await _context.Restaurants.FirstOrDefaultAsync(x => x.Id == restaurantId);

        var itemToReturn = new ItemReadDto
        {
            Name = itemToRequest.Name,
            Price = itemToRequest.Price,
            Restaurant = restaurantOfItem.Name
        };

        if (itemToReturn is not null) return itemToReturn;

        return null;
    }

    public bool IsExternalRestaureantExisted(int externalRestaurantId)
    {
        return _context.Restaurants.Any(x => x.ExternalId == externalRestaurantId);
    }

    public bool IsRestaureantExisted(int restaurantId)
    {
        return _context.Restaurants.Any(x => x.Id == restaurantId);
    }
}
