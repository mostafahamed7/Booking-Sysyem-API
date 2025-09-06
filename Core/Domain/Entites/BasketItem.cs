namespace Domain.Entites
{
    public class BasketItem
    {
        public int Id { get; set; }
        public Room Room { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int GuestsCount { get; set; }
        public decimal Price { get; set; }
    }
}
