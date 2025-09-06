namespace Shared.DTOs.Reservation_DTOs
{
    public record ReservationCreateDTO
    {
        public int HotelId { get; init; }
        public List<int> RoomIds { get; init; }
        public DateTime CheckInDate { get; init; }
        public DateTime CheckOutDate { get; init; }
    }
}
