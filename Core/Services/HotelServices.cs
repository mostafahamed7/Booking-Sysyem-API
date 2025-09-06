using AutoMapper;
using Domain.Contracts;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Services.Abstraction;
using Shared.DTOs;
using System.Text.Json;

namespace Services
{
    public class HotelServices(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache cache) : IHotelServices
    {
        public async Task<IEnumerable<HotelReturnDTO>> GetAllHotelsAsync()
        {
            var hotels = await unitOfWork.GetRepository<Hotel, int>().GetAllAsync();

            var resultHotels = mapper.Map<IEnumerable<HotelReturnDTO>>(hotels);

            return resultHotels;
        }

        public async Task<HotelReturnDTO> GetHotelByIdAsync(int id)
        {
            if(id == null)
                throw new ArgumentNullException(nameof(id));

            if(id <= 0)
                throw new ArgumentException("Invalid hotel ID.");

            var hotel = await unitOfWork.GetRepository<Hotel, int>().GetByIdAsync(id);

            var resultHotel = mapper.Map<HotelReturnDTO>(hotel);

            return resultHotel;
        }
        public async Task<HotelReturnDTO> CreateHotelAsync(HotelReturnDTO hotelDTO)
        {
            var pictures = await unitOfWork.GetRepository<RoomPictures, int>().GetAllAsync();

            var hotel = mapper.Map<Hotel>(hotelDTO);

            hotel.HotelPictures = pictures.ToList();

            await unitOfWork.GetRepository<Hotel, int>().AddAsync(hotel);

            await unitOfWork.SaveChangesAsync();

            return mapper.Map<HotelReturnDTO>(hotel);
        }
        public bool UpdateHotel(UpdateHotelDTO hotelDTO)
        {
            if(hotelDTO.Id == null)
                throw new ArgumentNullException(nameof(hotelDTO));

            if(hotelDTO.Id <= 0)
                throw new ArgumentException("Invalid hotel ID.");

            var hotel = mapper.Map<Hotel>(hotelDTO);

            unitOfWork.GetRepository<Hotel, int>().Update(hotel);

            unitOfWork.SaveChangesAsync();

            return true;
        }
        public bool DeleteHotel(int id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            if (id <= 0)
                throw new ArgumentException("Invalid hotel ID.");

            var hotel = unitOfWork.GetRepository<Hotel, int>().GetByIdAsync(id).Result;

            if (hotel == null)
                throw new ArgumentException("Hotel not found.");

            unitOfWork.GetRepository<Hotel, int>().Delete(hotel);

            unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<RoomReturnDTO>> GetAvilabaleRoomsByHotelIdAsync(int hotelId)
        {
            var roomRepository = unitOfWork.GetRepository<Room, int>();

            var rooms = await roomRepository.GetAllAsync(
            filter: r => r.HotelId == hotelId && !r.IsDeleted,
            includeProperties: "RoomPictures",
            asNoTracking: true
            );
            return mapper.Map<IEnumerable<RoomReturnDTO>>(rooms);
        }

        public async Task<IEnumerable<HotelReturnDTO>> SearchHotelsAsync(HotelSearchCriteria criteria)
        {
            if (string.IsNullOrWhiteSpace(criteria.Address))
                throw new ArgumentException("Address must be provided.");

            // 1. Cache key (Redis)
            var cacheKey = $"hotel_search_{criteria.Address}";

            var cached = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cached))
                return JsonSerializer.Deserialize<IEnumerable<HotelReturnDTO>>(cached)!;

            // 2. Get hotels from DB including Rooms
            var hotelRepo = unitOfWork.GetRepository<Hotel, int>();
            var hotelsQuery = hotelRepo.GetQueryable()
                .Include(h => h.Rooms)
                .AsQueryable();

            // 3. Filter by address
            hotelsQuery = hotelsQuery.Where(h => h.Address.ToLower().Contains(criteria.Address.ToLower()));

            var hotels = await hotelsQuery.ToListAsync();

            var results = new List<HotelReturnDTO>();

            foreach (var hotel in hotels)
            {
                var hotelDto = mapper.Map<HotelReturnDTO>(hotel);

                results.Add(hotelDto);
            }

            var finalResults = results;

            // 6. Save in Redis
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };
            await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(finalResults), cacheOptions);

            return finalResults;
        }
    }
}
