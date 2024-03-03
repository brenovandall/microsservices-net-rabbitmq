using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantService.HttpClientServices;
using RestaurantService.Infra.Data;
using RestaurantService.Infra.DTO;
using RestaurantService.Infra.Repository;
using RestaurantService.RabbitMQClient;

namespace RestaurantService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantRepository _restaurantRepository;
    private IItemServiceHttpClient _itemServiceHttpClient;
    private readonly ApplicationDbContext _context;
    private IRabbitMqClient _rabbitMqClient;
    public RestaurantController(IRestaurantRepository restaurantRepository, IItemServiceHttpClient itemServiceHttpClient, ApplicationDbContext context, IRabbitMqClient rabbitMqClient)
    {
        _restaurantRepository = restaurantRepository;
        _itemServiceHttpClient = itemServiceHttpClient;
        _context = context;
        _rabbitMqClient = rabbitMqClient;
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

            var restaurantIdForTheRequest = await _context.Restaurants.FirstOrDefaultAsync(x => x.Name == response.Result.Name && x.Address == response.Result.Address && x.WebSiteUrl == response.Result.WebSiteUrl);

            if (response is not null)
            {
                var objectToHttpRequest = new RestaurantHttpRequest
                {
                    Id = restaurantIdForTheRequest.Id,
                    Name = response.Result.Name,
                    Address = response.Result.Address,
                    WebSiteUrl = response.Result.WebSiteUrl
                };

                _rabbitMqClient.PublishRestaurantAtRabbitMqQueue(objectToHttpRequest);

                return Ok(objectToHttpRequest);
            };

            ModelState.AddModelError("Not create", "Something wrong");
            return NotFound(ModelState);
        }

        ModelState.AddModelError("Not create", "Something wrong");
        return NotFound(ModelState);
    }
}
