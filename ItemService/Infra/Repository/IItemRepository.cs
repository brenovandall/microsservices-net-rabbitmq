﻿using ItemService.Infra.DTO;
using ItemService.Infra.Entities;

namespace ItemService.Infra.Repository;

public interface IItemRepository
{
    public Task<IReadOnlyList<RestaurantReadDto>> GetAllRestaurantsList();
    public Task<RestaurantReadDto> CreateRestaurant(RestaurantCreateDto restaurant);
    public bool IsRestaureantExisted(int restaurantId);
    public bool IsExternalRestaureantExisted(int externalRestaurantId);
    public Task<IReadOnlyList<ItemReadDto>> GetItemsFromRestaurant(int restaurantId);
    public Task<ItemReadDto> GetOnlyOneItemById(int restaurantId, int id);
    public Task<ItemReadDto> CreateItem(int restaurantId, ItemCreateDto item);
}
