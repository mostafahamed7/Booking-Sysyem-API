using Shared.DTOs.Reservation_DTOs;

namespace Services.Abstraction
{
    public interface IReservationService
    {
        public Task<ReservationReturnDTO> CreateReservationAsync(ReservationCreateDTO dto, string userId);
        public Task<ReservationReturnDTO?> GetReservationByIdAsync(int id, string userId);
        public Task<IEnumerable<ReservationReturnDTO>> GetUserReservationsAsync(string userId);
        public Task<bool> CancelReservationAsync(int id, string userId);
    }
}
