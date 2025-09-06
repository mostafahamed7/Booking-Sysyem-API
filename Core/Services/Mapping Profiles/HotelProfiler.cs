using AutoMapper;
using Domain.Entites;
using Shared.DTOs;

namespace Services.Mapping_Profiles
{
    public class HotelProfiler : Profile
    {
        public HotelProfiler()
        {
            CreateMap<Hotel, HotelReturnDTO>()
                .ForMember(D => D.PictureUrl, O => O.MapFrom<PictureUrlResolverHotel>());

            CreateMap<Hotel, CreateHotelDTO>();
            CreateMap<CreateHotelDTO, Hotel>();

            CreateMap<Hotel, UpdatedHotelDTO>();
            CreateMap<UpdatedHotelDTO, Hotel>();


            CreateMap<Hotel, HotelSearchResultDTO>()
            .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Stars))
            .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.PictureUrl))
            .ForMember(dest => dest.RoomType, opt => opt.Ignore())
            .ForMember(dest => dest.Price, opt => opt.Ignore())
            .ForMember(dest => dest.Provider, opt => opt.MapFrom(_ => "LocalDB"));

            CreateMap<Room, HotelSearchResultDTO>()
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomType))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Provider, opt => opt.MapFrom(_ => "LocalDB"));

            CreateMap<HotelReturnDTO, HotelSearchResultDTO>()
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Stars))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.PictureUrl))
                .ForMember(dest => dest.RoomType, opt => opt.Ignore())
                .ForMember(dest => dest.Price, opt => opt.Ignore())
                .ForMember(dest => dest.Provider, opt => opt.MapFrom(_ => "LocalDB"));
        }
    }
}
