using ItemService.Infra.DTO;
using ItemService.Infra.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItemService.Controllers;

[Route("api/item/[controller]")]
[ApiController]
public class RestaurantController : ControllerBase
{
    private IItemRepository _itemRepository;
    public RestaurantController(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    [HttpGet]
    [Route("restaurants/get/all")]
    public async Task<ActionResult<IReadOnlyList<RestaurantReadDto>>> GetALlRestaurants()
    {
        return Ok(_itemRepository.GetAllRestaurantsList());
    }
}
