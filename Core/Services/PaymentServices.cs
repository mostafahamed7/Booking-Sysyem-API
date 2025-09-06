using AutoMapper;
using Domain.Contracts;
using Domain.Entites.Order_Entites;
using Domain.Entites.Reservations_Mod;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Abstractions;
using Services.Specifications;
using Shared.DTOs.Reservation_DTOs;
using Stripe;

namespace Services
{
    internal class PaymentServices(
    IConfiguration configuration,
    IUnitOfWork unitOfWork,
    IMapper mapper
    )
    : IPaymentServices
    {
        public async Task<ReservationReturnDTO> CreateOrUpdatePaymentIntentAsync(int reservationId)
        {
            StripeConfiguration.ApiKey = configuration.GetSection("StripeSettings")["SecretKey"];

            var reservationRepo = unitOfWork.GetRepository<Reservation, int>();
            var reservation = await reservationRepo.GetByIdAsync(reservationId)
                               ?? throw new ReservationNotFoundException(reservationId);

            // Nights
            var nights = (reservation.CheckOutDate - reservation.CheckInDate).Days;
            if (nights <= 0) nights = 1;

            // Calculate total
            var amount = (long)(
                (reservation.ReservationRooms.Sum(rr => rr.Room.Price) * nights) * 100
            );

            var service = new PaymentIntentService();

            if (string.IsNullOrWhiteSpace(reservation.PaymentIntentId))
            {
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                var paymentIntent = await service.CreateAsync(createOptions);

                reservation.PaymentIntentId = paymentIntent.Id;
                reservation.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };

                await service.UpdateAsync(reservation.PaymentIntentId, updateOptions);
            }

            reservation.TotalPrice = amount / 100m;

            reservationRepo.Update(reservation);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<ReservationReturnDTO>(reservation);
        }

        public async Task UpdateOrderPaymentStatus(string request, string header)
        {
            var endpointSecret = configuration.GetSection("StripeSettings")["EndPointSecret"];
            var stripeEvent = EventUtility.ConstructEvent(request, header, endpointSecret, throwOnApiVersionMismatch: false);

            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentPaymentFailed:
                    await UpdatePaymentFailed(paymentIntent!.Id);
                    break;

                case EventTypes.PaymentIntentSucceeded:
                    await UpdatePaymentSucceeded(paymentIntent!.Id);
                    break;

                default:
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                    break;
            }
        }

        private async Task UpdatePaymentSucceeded(string paymentIntentId)
        {
            var repo = unitOfWork.GetRepository<Reservation, int>();
            var reservation = await repo.GetByIdAsync(new ReservationWithPaymentIntentSpecification(paymentIntentId))
                               ?? throw new Exception("Reservation not found with PaymentIntentId");

            reservation.PaymentStatus = ReservationPaymentStatus.PymentReceived;

            repo.Update(reservation);
            await unitOfWork.SaveChangesAsync();
        }

        private async Task UpdatePaymentFailed(string paymentIntentId)
        {
            var repo = unitOfWork.GetRepository<Reservation, int>();
            var reservation = await repo.GetByIdAsync(new ReservationWithPaymentIntentSpecification(paymentIntentId))
                               ?? throw new Exception("Reservation not found with PaymentIntentId");

            reservation.PaymentStatus = ReservationPaymentStatus.PaymentFailed;

            repo.Update(reservation);
            await unitOfWork.SaveChangesAsync();
        }
    }


}
