using ItemService.Infra.Data;
using ItemService.Infra.DTO;
using ItemService.Infra.Entities;
using ItemService.Infra.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItemService.Controllers;

[Route("api/item/[controller]")]
[ApiController]
public class RestaurantController : ControllerBase
{
    private IItemRepository _itemRepository;
    private readonly AppDbContext _context;
    public RestaurantController(IItemRepository itemRepository, AppDbContext context)
    {
        _itemRepository = itemRepository;
        _context = context;
    }

    [HttpGet]
    [Route("restaurants/get/all")]
    public async Task<ActionResult<IReadOnlyList<RestaurantReadDto>>> GetALlRestaurants()
    {
        return Ok(_itemRepository.GetAllRestaurantsList());
    }

    [HttpPost]
    [Route("restaurants/add/new")]
    public async Task<ActionResult<RestaurantHttpRequest>> CreateNewRestaurantFromRestaurantService(RestaurantHttpRequest restaurantHttpRequest)
    {
        if (restaurantHttpRequest is not null)
        {
            var newRestaurantModel = new Restaurant
            {
                ExternalId = restaurantHttpRequest.Id,
                Name = restaurantHttpRequest.Name,
            };

            _context.Restaurants.Add(newRestaurantModel);
            _context.SaveChanges();

            return Ok(newRestaurantModel);
        }

        return BadRequest();
    }
}
