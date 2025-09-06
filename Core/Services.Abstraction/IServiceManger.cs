using Services.Abstractions;

namespace Services.Abstraction
{
    public interface IServiceManger
    {
        public IRoomServices RoomServices { get; }

        public IHotelServices HotelServices { get; }

        public IBasketServices BasketServices { get; }

        public IAuthenticationServices AuthenticationServices { get; }

        public IReservationService ReservationServices { get; }

        public IPaymentServices PaymentServices { get; }

        public  ICacheServices CacheServices { get; }
    }
}
