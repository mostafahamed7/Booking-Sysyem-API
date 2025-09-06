using Shared;
using Shared.DTOs;

namespace Services.Abstraction
{
    public interface IRoomServices
    {
        public Task<RoomReturnDTO> GetRoomByIdAsync(int id);

        public Task<IEnumerable<RoomReturnDTO>> GetAllRoomsAsync(RoomSpceParams parameters);

        public Task<RoomReturnDTO> CreateRoomAsync(CreatedRoomDTO createdRoomDTO, int id);

        // Get All Hotels
        public Task<IEnumerable<HotelReturnDTO>> GetAllHotelsAsync();

        bool UpdateRoom(UpdatedRoomDTO roomDTO);

        bool DeleteRoom(int id);

    }
}
