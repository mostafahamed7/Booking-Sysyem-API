using AutoMapper;
using Domain.Entites;
using Shared.DTOs;

namespace Services.Mapping_Profiles
{
    public class Room_Profiler : Profile
    {
        public Room_Profiler()
        {
            CreateMap<Room, RoomReturnDTO>()
                .ForMember(D => D.HotelName, O => O.MapFrom(S => S.Hotel.Name))
                .ForMember(D => D.PictureUrl, O => O.MapFrom<PictureUrlResolver>());

            CreateMap<RoomPictures, RoomPicturesDTO>();
            CreateMap<RoomPicturesDTO, RoomPictures>();

            CreateMap<Room, CreatedRoomDTO>()
                .ForMember(D => D.Hotel, O => O.MapFrom(S => S.Hotel.Name))
                .ForMember(D => D.Id, O => O.Ignore());

            CreateMap<CreatedRoomDTO, Room>()
                .ForMember(D => D.Hotel, O => O.Ignore())
                .ForMember(D => D.RoomPictures, O => O.Ignore());

            CreateMap<Room, UpdatedRoomDTO>();

        }
    }
}
