using ItemService.Infra.DTO;
using ItemService.Infra.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ItemService.Controllers;

[Route("api/item/restaurant/{restaurantId}/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly IItemRepository _itemRepository;
    public ItemController(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ItemReadDto>>> GetItensFromRestaurant(int restaurantId)
    {
        if (!_itemRepository.IsRestaureantExisted(restaurantId)) return NotFound();

        var itens = _itemRepository.GetItemsFromRestaurant(restaurantId);

        return Ok(itens);
    }

    [HttpGet]
    [Route("{ItemId}")]
    public async Task<ActionResult<ItemReadDto>> GetItemFromRestaurant(int restaurantId, int ItemId)
    {
        if (!_itemRepository.IsRestaureantExisted(restaurantId)) return NotFound();

        var item = _itemRepository.GetOnlyOneItemById(restaurantId, ItemId);

        if (item is not null) return Ok(item);

        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<ItemReadDto>> CreateItemForRestaurant(int restaurantId, ItemCreateDto item)
    {
        if (item is not null && _itemRepository.IsRestaureantExisted(restaurantId)) return Ok(_itemRepository.CreateItem(restaurantId, item));

        return BadRequest();
    }
}
