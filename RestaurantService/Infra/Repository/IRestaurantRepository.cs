using RestaurantService.Infra.DTO;

namespace RestaurantService.Infra.Repository;

public interface IRestaurantRepository
{
    public Task<IReadOnlyList<RestaurantReadDto>> GetRestaurantList();
    public Task<RestaurantReadDto> GetOnlyOneRestaurantById(int id);
    public Task<RestaurantReadDto> CreateRestaurant(RestaurantCreateDto restaurant);
}
