namespace ItemService.Infra.Entities;

public class Item : BaseEntityWithUniqueIdentifier
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
}
