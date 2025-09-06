namespace Shared.DTOs
{
    public record BasketDTO
    {
        public string Id { get; init; }
        public IEnumerable<BasketItemDTO> Items { get; init; }
        public bool IsCheckedOut { get; init; } = false;
        public string? PaymentIntentId { get; init; }
        public string? ClientSecret { get; init; }
        public decimal? BookingPrice { get; init; }
    }
}
