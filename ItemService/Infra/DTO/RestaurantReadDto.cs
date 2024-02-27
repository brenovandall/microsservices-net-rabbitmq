using ItemService.Infra.Entities;

namespace ItemService.Infra.DTO;

public class RestaurantReadDto
{
    public int ExternalId { get; set; }
    public string Name { get; set; }
    public ICollection<Item> ItemsOfRestaurant { get; set; }
}
