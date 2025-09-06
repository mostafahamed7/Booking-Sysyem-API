using Domain.Entites.Enums;

namespace Shared.DTOs.Reservation_DTOs
{
    public record ReservationReturnDTO
    {
        public int Id { get; init; }
        public string UserId { get; init; }
        public int HotelId { get; init; }
        public string HotelName { get; init; }
        public List<string> RoomNumbers { get; init; }
        public DateTime CheckInDate { get; init; }
        public DateTime CheckOutDate { get; init; }
        public ReservationStatus Status { get; init; }
        public decimal TotalPrice { get; init; }
        public List<RoomReturnDTO> Rooms { get; init; } = new();
        public string? ClientSecret { get; set; }

    }
}
