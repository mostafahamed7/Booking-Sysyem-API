using Domain.Entites.Enums;

namespace Shared.DTOs
{
    public record RoomReturnDTO
    {
        public RoomType RoomType { get; set; }
        public decimal Price { get; set; }
        public int NumOfBed { get; set; }
        public int MaxOccupancy { get; set; }
        public double RoomSize { get; set; }
        public int RoomNumber { get; set; }
        public string? PictureUrl { get; set; }

        public string? HotelName { get; set; }
        public RoomPicturesDTO? Pictures { get; set; }
    }
}
