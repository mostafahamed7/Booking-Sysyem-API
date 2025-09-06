using Domain.Entites.Enums;

namespace Shared.DTOs
{
    public record HotelSearchResultDTO
    {
        public int HotelId { get; init; }
        public string HotelName { get; init; }
        public string Address { get; init; }
        public double Rating { get; init; }
        public string PictureUrl { get; init; }
            
        public RoomType RoomType { get; init; }
        public decimal Price { get; init; }

        public string Provider { get; init; }
    }
}
