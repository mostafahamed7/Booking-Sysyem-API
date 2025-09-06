using AutoMapper;
using Domain.Entites;
using Shared.DTOs;

namespace Services.Mapping_Profiles
{
    public class BasketProfiler : Profile
    {
        public BasketProfiler()
        {
            CreateMap<Basket, BasketDTO>().ReverseMap();

            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
        }
    }
}
