using System.ComponentModel.DataAnnotations;

namespace RestaurantService.Infra.DTO;

public class RestaurantCreateDto
{
    [Required]
    [StringLength(70, ErrorMessage = "Max length { 70 } and min length { 3 }")]
    [MinLength(3, ErrorMessage = "Max length { 70 } and min length { 3 }")]
    public string Name { get; set; }
    [Required]
    [StringLength(70, ErrorMessage = "Max length { 70 } and min length { 3 }")]
    [MinLength(3, ErrorMessage = "Max length { 70 } and min length { 3 }")]
    public string Address { get; set; }
    [Required]
    public string WebSiteUrl { get; set; }
}
