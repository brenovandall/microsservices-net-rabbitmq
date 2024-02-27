using AutoMapper;
using RestaurantService.Infra.DTO;
using RestaurantService.Infra.Entities;

namespace RestaurantService.Infra.Helper;

public class RestaurantAutoMapperProfiles : Profile
{
    public RestaurantAutoMapperProfiles()
    {
        CreateMap<Restaurant, RestaurantReadDto>();
        CreateMap<RestaurantReadDto, Restaurant>();
    }
}
