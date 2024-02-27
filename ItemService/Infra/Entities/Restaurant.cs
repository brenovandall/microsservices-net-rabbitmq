namespace ItemService.Infra.Entities;

public class Restaurant : BaseEntityWithUniqueIdentifier
{
    public int ExternalId { get; set; }
    public string Name { get; set; }
    public ICollection<Item> ItemsOfRestaurant { get; set; } = new List<Item>();
}
