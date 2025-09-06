using AutoMapper;
using Domain.Contracts;
using Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using Shared.DTOs;

namespace Presentation.Controllers
{
    public class RoomsController(IServiceManger serviceManger, IUnitOfWork unitOfWork, IMapper mapper) : ApiController
    {
        #region Get All Rooms
        [RedisCache(120)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomReturnDTO>>> GetAllRoomsAsync([FromQuery] RoomSpceParams parmeters)
        {
            var rooms = await serviceManger.RoomServices.GetAllRoomsAsync(parmeters);

            return Ok(rooms);
        }
        #endregion

        #region Get Room by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomReturnDTO>> GetRoomByIdAsync(int id)
        {
            var room = await serviceManger.RoomServices.GetRoomByIdAsync(id);
            if (room is null)
                return NotFound($"Room with ID {id} not found.");
            return Ok(room);
        }
        #endregion

        #region Get All Hotels
        [RedisCache(120)]
        [HttpGet("Hotels")]
        public async Task<ActionResult<IEnumerable<HotelReturnDTO>>> GetAllHotelsAsync()
        {
            var hotels = await serviceManger.RoomServices.GetAllHotelsAsync();

            return Ok(hotels);
        }
        #endregion 

        #region Create Room
        [HttpPost("CreateRoom")]
        [Authorize]
        public async Task<ActionResult<RoomReturnDTO>> createRoomAsync([FromBody] CreatedRoomDTO createdRoomDTO)
        {
            if (createdRoomDTO == null)
                throw new ArgumentNullException(nameof(createdRoomDTO));

            var hotelRepository = unitOfWork.GetRepository<Hotel, int>();
            var hotel = await hotelRepository.GetByIdAsync(createdRoomDTO.HotelId);

            if (hotel == null)
                return BadRequest($"Hotel with Id {createdRoomDTO.HotelId} does not exist.");

            var roomRepository = unitOfWork.GetRepository<Room, int>();
            var room = mapper.Map<Room>(createdRoomDTO);

            room.IsDeleted = false;
            room.HotelId = createdRoomDTO.HotelId;

            if (createdRoomDTO.RoomPictures != null && createdRoomDTO.PictureUrl.Any())
            {
                var pictureRepository = unitOfWork.GetRepository<RoomPictures, int>();
                var selectedPictures = await pictureRepository.GetAllAsync();
                room.RoomPictures = selectedPictures.ToList();
            }

            await roomRepository.AddAsync(room);
            await unitOfWork.SaveChangesAsync();

            return Ok(mapper.Map<RoomReturnDTO>(room));
        }
        #endregion

        #region Update Room
        [HttpPut("updateRoom")]
        [Authorize]
        public async Task<ActionResult<RoomReturnDTO>> UpdateRoomAsync([FromBody] UpdatedRoomDTO updatedRoomDTO)
        {
            if (updatedRoomDTO == null)
                throw new ArgumentNullException(nameof(updatedRoomDTO));
            var roomRepository = unitOfWork.GetRepository<Room, int>();
            var room = await roomRepository.GetByIdAsync(updatedRoomDTO.Id);
            if (room == null)
                return NotFound($"Room with ID {updatedRoomDTO.Id} not found.");
            mapper.Map(updatedRoomDTO, room);
            room.IsDeleted = false;
            if (updatedRoomDTO.RoomPictures != null && updatedRoomDTO.PictureUrl.Any())
            {
                var pictureRepository = unitOfWork.GetRepository<RoomPictures, int>();
                var selectedPictures = await pictureRepository.GetAllAsync();
                room.RoomPictures = selectedPictures.ToList();
            }
            roomRepository.Update(room);
            await unitOfWork.SaveChangesAsync();
            return Ok(mapper.Map<RoomReturnDTO>(room));
        }
        #endregion

        #region Delete Room
        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRoomAsync(int id)
        {
            var roomRepository = unitOfWork.GetRepository<Room, int>();

            var room = await roomRepository.GetByIdAsync(id);
            if (room == null)
                return NotFound(new { Message = $"Room with ID {id} not found." });

            room.IsDeleted = true;
            roomRepository.Delete(room);

            await unitOfWork.SaveChangesAsync();

            return Ok(new { Message = $"Room with ID {id} has been deleted successfully." });
        }

        #endregion
    }
}
