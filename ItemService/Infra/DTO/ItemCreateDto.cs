using System.ComponentModel.DataAnnotations;

namespace ItemService.Infra.DTO;

public class ItemCreateDto
{
    [Required]
    [StringLength(70, ErrorMessage = "Max length { 70 } and min length { 3 }")]
    [MinLength(3, ErrorMessage = "Max length { 70 } and min length { 3 }")]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int RestaurantId { get; set; }
}
