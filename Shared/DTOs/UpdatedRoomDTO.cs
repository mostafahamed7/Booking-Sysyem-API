using Domain.Entites.Enums;

namespace Shared.DTOs
{
    public record UpdatedRoomDTO
    {
        public int Id { get; set; }
        public RoomType RoomType { get; set; }
        public decimal Price { get; set; }
        public int NumOfBed { get; set; }
        public int MaxOccupancy { get; set; }
        public double RoomSize { get; set; }
        public RoomType roomType { get; set; }
        public int RoomNumber { get; set; }
        public RoomPicturesDTO? RoomPictures { get; set; }
        public string? PictureUrl { get; set; }

        public int HotelId { get; set; }
    }
}
