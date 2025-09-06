namespace Domain.Entites
{
    public class Basket
    {
        public string Id { get; set; }
        public IEnumerable<BasketItem> Items { get; set; }
        public bool IsCheckedOut { get; set; } = false;
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public decimal? BookingPrice { get; set; }

    }
}
