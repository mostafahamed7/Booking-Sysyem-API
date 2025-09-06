namespace Shared.DTOs
{
    public record HotelReturnDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public int RoomsCount { get; init; }
        public bool IsAvailable { get; init; }
        public string? PictureUrl { get; init; }
        public string? Address { get; init; }
        public int Stars { get; init; }

        public RoomPicturesDTO? Pictures { get; init; }
        public List<RoomReturnDTO> Rooms { get; set; } = new List<RoomReturnDTO>();
    }
}
