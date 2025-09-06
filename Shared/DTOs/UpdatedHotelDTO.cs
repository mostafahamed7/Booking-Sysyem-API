using Domain.Entites.Enums;

namespace Shared.DTOs
{
    public record UpdatedHotelDTO
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public string? Address { get; init; }
        public string? Description { get; init; }
        public int? RoomsCount { get; init; }
        public decimal? Rating { get; init; }
        public RoomPicturesDTO? RoomPictures { get; init; }
        public string? PictureUrl { get; init; }
    }
}
