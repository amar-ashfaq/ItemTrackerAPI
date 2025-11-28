using AutoMapper;
using ItemTrackerAPI.DTOs;
using ItemTrackerAPI.Entities;

namespace ItemTrackerAPI
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<ItemEntity, ItemReadDto>();
            CreateMap<ItemCreateDto, ItemEntity>();
        }
    }
}
