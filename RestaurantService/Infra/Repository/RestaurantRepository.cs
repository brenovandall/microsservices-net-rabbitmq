using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantService.Infra.Data;
using RestaurantService.Infra.DTO;
using RestaurantService.Infra.Entities;

namespace RestaurantService.Infra.Repository;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    public RestaurantRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RestaurantReadDto> CreateRestaurant(RestaurantCreateDto restaurant)
    {
        if(restaurant is not null)
        {
            var newRestaurantModel = new Restaurant
            {
                Name = restaurant.Name,
                Address = restaurant.Address,   
                WebSiteUrl = restaurant.WebSiteUrl
            };

            await _context.Restaurants.AddAsync(newRestaurantModel);
            await _context.SaveChangesAsync();

            var mappedRestaurantToRead = _mapper.Map<Restaurant, RestaurantReadDto>(newRestaurantModel);

            if(mappedRestaurantToRead is not null) return mappedRestaurantToRead;
        }

        return null;
    }

    public async Task<RestaurantReadDto> GetOnlyOneRestaurantById(int id)
    {
        var restaurantById = await _context.Restaurants.FirstOrDefaultAsync(item => item.Id == id);

        var mappedRestaurantSearchedById = _mapper.Map<Restaurant, RestaurantReadDto>(restaurantById);

        return mappedRestaurantSearchedById;
    }

    public async Task<IReadOnlyList<RestaurantReadDto>> GetRestaurantList()
    {
        var listOfRestaurants = await _context.Restaurants.ToListAsync();

        var mappedListOfRestaurants = _mapper.Map<IReadOnlyList<Restaurant>, IReadOnlyList<RestaurantReadDto>>(listOfRestaurants);

        return mappedListOfRestaurants;
    }
}
