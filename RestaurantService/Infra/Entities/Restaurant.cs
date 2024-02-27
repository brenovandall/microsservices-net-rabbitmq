namespace RestaurantService.Infra.Entities;

public class Restaurant : BaseEntityWithUniqueIdentifier
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string WebSiteUrl { get; set; }
}
