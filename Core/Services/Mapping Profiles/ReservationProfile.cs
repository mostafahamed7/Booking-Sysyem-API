using AutoMapper;
using Domain.Entites.Identity;
using Domain.Entites.Reservations_Mod;
using Shared.DTOs.Order_Module;
using Shared.DTOs.Reservation_DTOs;

namespace Services.Mapping_Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            // Create
            CreateMap<ReservationCreateDTO, Reservation>()
                .ForMember(dest => dest.ReservationRooms, opt => opt.MapFrom(src =>
                    src.RoomIds.Select(Rid => new ReservationRoom { RoomId = Rid }).ToList()
                ));

            // Return
            CreateMap<Reservation, ReservationReturnDTO>()
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel.Name))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src =>
                    src.ReservationRooms.Select(rr => rr.Room).ToList()
                ));

            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
