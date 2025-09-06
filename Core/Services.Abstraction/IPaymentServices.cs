using Shared.DTOs;
using Shared.DTOs.Reservation_DTOs;

namespace Services.Abstractions
{
    public interface IPaymentServices
    {
        public Task<ReservationReturnDTO> CreateOrUpdatePaymentIntentAsync(int reservationId);

        public Task UpdateOrderPaymentStatus(string request, string header);
    }
}
