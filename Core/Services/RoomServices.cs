using AutoMapper;
using Domain.Contracts;
using Domain.Entites;
using Services.Abstraction;
using Services.Specifications;
using Shared;
using Shared.DTOs;

namespace Services
{
    public class RoomServices(IUnitOfWork unitOfWork, IMapper mapper) : IRoomServices
    {
        public async Task<IEnumerable<RoomReturnDTO>> GetAllRoomsAsync(RoomSpceParams parameters)
        {
            var rooms = await unitOfWork.GetRepository<Room, int>().GetAllAsync(new RoomWithHotelName(parameters));

            var resultRooms = mapper.Map<IEnumerable<RoomReturnDTO>>(rooms);

            return resultRooms;
        }

        public async Task<RoomReturnDTO> GetRoomByIdAsync(int id)
        {
            if(id <= 0)
                throw new ArgumentException("Invalid room ID.");
            if(id == null)
                throw new ArgumentNullException(nameof(id));

            var room = await unitOfWork.GetRepository<Room, int>().GetByIdAsync(new RoomWithHotelName(id));

            var resultRoom = mapper.Map<RoomReturnDTO>(room);

            return resultRoom;
        }

        public async Task<IEnumerable<HotelReturnDTO>> GetAllHotelsAsync()
        {
            var hotels = await unitOfWork.GetRepository<Hotel, int>().GetAllAsync();

            var resultHotels = mapper.Map<IEnumerable<HotelReturnDTO>>(hotels);

            return resultHotels;
        }

        public async Task<RoomReturnDTO> CreateRoomAsync(CreatedRoomDTO createdRoomDTO, int id)
        {
            if (createdRoomDTO == null)
                throw new ArgumentNullException(nameof(createdRoomDTO));
            
            if (id <= 0)
                throw new ArgumentException("Invalid room ID.");

            var roomRepo = unitOfWork.GetRepository<Room, int>();
            var existingRoom = await roomRepo.GetByIdAsync(id);

            if (existingRoom != null)
                throw new InvalidOperationException($"Room with ID {id} already exists.");
            

            var pictures = await unitOfWork.GetRepository<RoomPictures, int>().GetAllAsync();

            var room = mapper.Map<Room>(createdRoomDTO);

            room.ID = id;

            if (pictures != null && pictures.Any())
            {
                room.RoomPictures = pictures.ToList();
            }

            await unitOfWork.GetRepository<Room, int>().AddAsync(room);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<RoomReturnDTO>(room);

        }

        public bool UpdateRoom(UpdatedRoomDTO roomDTO)
        {
            if(roomDTO == null)
                throw new ArgumentNullException(nameof(roomDTO));

            if(roomDTO.Id <= 0)
                throw new ArgumentException("Invalid room ID.");

            var existingRoom = unitOfWork.GetRepository<Room, int>().GetByIdAsync(roomDTO.Id).Result;

            if(existingRoom == null)
                throw new KeyNotFoundException($"Room with ID {roomDTO.Id} not found.");

            mapper.Map(roomDTO, existingRoom);

            unitOfWork.GetRepository<Room, int>().Update(existingRoom);

            unitOfWork.SaveChangesAsync();

            return true;
        }
        public bool DeleteRoom(int id)
        {
            if(id <= 0)
                throw new ArgumentException("Invalid room ID.");

            var existingRoom = unitOfWork.GetRepository<Room, int>().GetByIdAsync(id).Result;
            if(existingRoom == null)
                throw new KeyNotFoundException($"Room with ID {id} not found.");

            unitOfWork.GetRepository<Room, int>().Delete(existingRoom);
            unitOfWork.SaveChangesAsync();
            return true;
        }




    }
}
