using AutoMapper;

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
