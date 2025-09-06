namespace Shared.DTOs
{
    public record CreateHotelDTO
    {
        public string Name { get; init; }
        public string Address { get; init; }
        public string? Description { get; init; }
        public int RoomsCount { get; init; }
        public decimal Rating { get; init; }
    }
}
