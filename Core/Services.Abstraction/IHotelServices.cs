using Domain.Entites;
using Shared.DTOs;

namespace Services.Abstraction
{
    public interface IHotelServices
    {
        public Task<HotelReturnDTO> GetHotelByIdAsync(int id);
        public Task<IEnumerable<HotelReturnDTO>> GetAllHotelsAsync();
        public Task<IEnumerable<RoomReturnDTO>> GetAvilabaleRoomsByHotelIdAsync(int hotelId);
        public Task<HotelReturnDTO> CreateHotelAsync(HotelReturnDTO hotelDTO);
        bool UpdateHotel(UpdateHotelDTO hotelDTO);
        bool DeleteHotel(int id);

        public Task<IEnumerable<HotelReturnDTO>> SearchHotelsAsync(HotelSearchCriteria criteria);
    }
}
