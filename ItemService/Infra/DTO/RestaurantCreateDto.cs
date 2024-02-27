using ItemService.Infra.Entities;
using System.ComponentModel.DataAnnotations;

namespace ItemService.Infra.DTO;

public class RestaurantCreateDto
{
    [Required]
    public int ExternalId { get; set; }
    [Required]
    [StringLength(70, ErrorMessage = "Max length { 70 } and min length { 3 }")]
    [MinLength(3, ErrorMessage = "Max length { 70 } and min length { 3 }")]
    public string Name { get; set; }
    public ICollection<Item> ItemsOfRestaurant { get; set; } = new List<Item>();
}
