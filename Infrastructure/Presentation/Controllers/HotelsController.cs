using AutoMapper;
using Domain.Contracts;
using Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Abstraction;
using Shared.DTOs;

namespace Presentation.Controllers
{
    public class HotelsController(IServiceManger serviceManger, IUnitOfWork unitOfWork, IMapper mapper) : ApiController
    {
        #region Get All Hotels
        [RedisCache(120)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelReturnDTO>>> GetAllHotelsAsync()
        {
            var hotels = await serviceManger.RoomServices.GetAllHotelsAsync();

            return Ok(hotels);
        }
        #endregion

        #region Get Hotel By ID
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelReturnDTO>> GetHotelByIdAsync(int id)
        {
            var hotel = await serviceManger.HotelServices.GetHotelByIdAsync(id);

            if (hotel is null)
                return NotFound($"Hotel with ID {id} not found.");

            return Ok(hotel);
        }
        #endregion

        #region Get Avilabale Rooms By HotelId
        [HttpGet("{hotelId}/rooms/available")]
        public async Task<ActionResult<IEnumerable<RoomReturnDTO>>> GetAvilabaleRoomsByHotelId(int hotelId)
        {
            var rooms = await serviceManger.HotelServices.GetAvilabaleRoomsByHotelIdAsync(hotelId);

            if (rooms == null || !rooms.Any())
                return NotFound($"No available rooms found for hotel with Id {hotelId}.");

            return Ok(rooms);
        }
        #endregion

        #region Create Hotel
        [HttpPost("createHotel")]
        [Authorize]
        public async Task<ActionResult<HotelReturnDTO>> createHotelAsync([FromBody] CreateHotelDTO createHotelDTO)
        {
            if (createHotelDTO == null)
                throw new ArgumentNullException(nameof(createHotelDTO));

            var hotelRepository = unitOfWork.GetRepository<Hotel, int>();
            var hotel = mapper.Map<Hotel>(createHotelDTO);

            hotel.IsDeleted = false;

            await hotelRepository.AddAsync(hotel);
            await unitOfWork.SaveChangesAsync();

            return Ok(mapper.Map<HotelReturnDTO>(hotel));
        }
        #endregion

        #region Update Hotel
        [HttpPut("updateHotel")]
        [Authorize]
        public async Task<ActionResult<HotelReturnDTO>> UpdateRoomAsync([FromBody] UpdatedHotelDTO updatedHotelDTO)
        {
            if (updatedHotelDTO == null)
                throw new ArgumentNullException(nameof(updatedHotelDTO));
            var hotelRepository = unitOfWork.GetRepository<Hotel, int>();
            var hotel = await hotelRepository.GetByIdAsync(updatedHotelDTO.Id);
            if (hotel == null)
                return NotFound($"Hotel with ID {updatedHotelDTO.Id} not found.");
            mapper.Map(updatedHotelDTO, hotel);
            hotel.IsDeleted = false;
            if (updatedHotelDTO.RoomPictures != null && updatedHotelDTO.PictureUrl.Any())
            {
                var pictureRepository = unitOfWork.GetRepository<RoomPictures, int>();
                var selectedPictures = await pictureRepository.GetAllAsync();
                hotel.HotelPictures = selectedPictures.ToList();
            }
            hotelRepository.Update(hotel);
            await unitOfWork.SaveChangesAsync();
            return Ok(mapper.Map<HotelReturnDTO>(hotel));
        }
        #endregion

        #region Delete Hotel
        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteHotelAsync(int id)
        {
            var hotelRepository = unitOfWork.GetRepository<Hotel, int>();

            var hotel = await hotelRepository.GetByIdAsync(id);
            if (hotel == null)
                return NotFound(new { Message = $"Room with ID {id} not found." });

            hotel.IsDeleted = true;
            hotelRepository.Delete(hotel);

            await unitOfWork.SaveChangesAsync();

            return Ok(new { Message = $"Room with ID {id} has been deleted successfully." });
        }

        #endregion

        #region Search
        [RedisCache(120)]
        [HttpPost("search")]
        public async Task<ActionResult<IEnumerable<HotelReturnDTO>>> SearchHotels([FromBody] HotelSearchCriteria criteria)
        {
            if (criteria == null)
                return BadRequest("Search criteria is required.");

            var results = await serviceManger.HotelServices.SearchHotelsAsync(criteria);

            if (!results.Any())
                return NotFound("No hotels found for the given criteria.");

            return Ok(results);
        }
        #endregion
    }
}
