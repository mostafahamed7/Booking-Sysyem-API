namespace Shared.DTOs
{
    public record BasketItemDTO
    {
        public int Id { get; set; }
        public RoomReturnDTO Room { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int GuestsCount { get; set; }
        public decimal Price { get; set; }
    }
}
