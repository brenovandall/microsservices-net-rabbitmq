using System.ComponentModel.DataAnnotations;

namespace ItemService.Infra.DTO;

public class ItemReadDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Restaurant { get; set; }
}
