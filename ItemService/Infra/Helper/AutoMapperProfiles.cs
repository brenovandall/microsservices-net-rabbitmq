using AutoMapper;
using ItemService.Infra.DTO;
using ItemService.Infra.Entities;

namespace ItemService.Infra.Helper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<RestaurantCreateDto, Restaurant>();
        CreateMap<Restaurant, RestaurantReadDto>();
        CreateMap<Restaurant, RestaurantCreateDto>();
        CreateMap<Item, ItemReadDto>();
    }
}
