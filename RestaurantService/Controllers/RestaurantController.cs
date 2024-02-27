using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantService.Infra.DTO;
using RestaurantService.Infra.Repository;

namespace RestaurantService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantRepository _restaurantRepository;
    public RestaurantController(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    [HttpGet]
    [Route("restaurants/list")]
    public async Task<ActionResult<IReadOnlyList<RestaurantReadDto>>> GetAllRestaurants()
    {
        var listOfRestaurants = _restaurantRepository.GetRestaurantList();

        if(listOfRestaurants is not null) return Ok(listOfRestaurants);

        ModelState.AddModelError("Get", "Something worng");
        return NotFound(ModelState);
    }

    [HttpGet]
    [Route("restaurants/restaurant/{id}")]
    public async Task<ActionResult<RestaurantReadDto>> GetRestaurantById([FromRoute] int id)
    {
        var restaurantResult = _restaurantRepository.GetOnlyOneRestaurantById(id);

        if(restaurantResult is not null) return Ok(restaurantResult);

        ModelState.AddModelError("Get", "Something wrong");
        return NotFound(ModelState);
    }

    [HttpPost]
    [Route("restaurants/restaurant/add/new/restaurant")]
    public async Task<ActionResult<RestaurantReadDto>> CreateNewRestaurant([FromQuery] RestaurantCreateDto restaurant)
    {
        if(restaurant is not null)
        {
            var response = _restaurantRepository.CreateRestaurant(restaurant);

            if(response is not null) return Ok(response);

            ModelState.AddModelError("Not create", "Something wrong");
            return NotFound(ModelState);
        }

        ModelState.AddModelError("Not create", "Something wrong");
        return NotFound(ModelState);
    }
}
