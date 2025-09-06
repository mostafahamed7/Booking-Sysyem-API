using AutoMapper;
using Domain.Contracts;
using Domain.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstraction;
using Services.Abstractions;
using Shared;

namespace Services
{
    public class ServiceManger : IServiceManger
    {
        private readonly Lazy<IRoomServices> _roomServices;
        private readonly Lazy<IHotelServices> _hotelServices;
        private readonly Lazy<IBasketServices> _basketServices;
        private readonly Lazy<IAuthenticationServices> _authenticationServices;
        private readonly Lazy<IReservationService> _reservationService;
        private readonly Lazy<IPaymentServices> _paymentServices;
        private readonly Lazy<ICacheServices> _cacheServices;


        public ServiceManger(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IDistributedCache cache,
            IBasketRepository basketRepository,
            UserManager<User> userManager,
            IOptions<JwtOptions> options,
            IConfiguration configuration,
            ICacheRepository cacheRepository
            )
        {
            _roomServices = new Lazy<IRoomServices>(() => new RoomServices(unitOfWork, mapper));

            _hotelServices = new Lazy<IHotelServices>(() => new HotelServices(unitOfWork, mapper, cache));

            _basketServices = new Lazy<IBasketServices>(() => new BasketServices(basketRepository, mapper));

            _authenticationServices = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(userManager, options, mapper));

            _reservationService = new Lazy<IReservationService>(() => new ReservationService(unitOfWork, mapper));

            _paymentServices = new Lazy<IPaymentServices>(() => new PaymentServices(configuration, unitOfWork, mapper));

            _cacheServices = new Lazy<ICacheServices>(() => new CacheServices(cacheRepository));
        }

        public IRoomServices RoomServices => _roomServices.Value;

        public IHotelServices HotelServices => _hotelServices.Value;

        public IBasketServices BasketServices => _basketServices.Value;

        public IAuthenticationServices AuthenticationServices => _authenticationServices.Value;

        public IReservationService ReservationServices => _reservationService.Value;

        public IPaymentServices PaymentServices => _paymentServices.Value;

        public ICacheServices CacheServices => _cacheServices.Value;
    }
}
