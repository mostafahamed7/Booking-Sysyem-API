using AutoMapper;
using Domain.Contracts;
using Domain.Entites.Enums;
using Domain.Entites.Reservations_Mod;
using Microsoft.EntityFrameworkCore;
using Services.Abstraction;
using Shared.DTOs.Reservation_DTOs;

namespace Services
{
    public class ReservationService(IUnitOfWork unitOfWork, IMapper mapper) : IReservationService
    {
        public async Task<ReservationReturnDTO> CreateReservationAsync(ReservationCreateDTO dto, string userId)
        {
            var reservationRepo = unitOfWork.GetRepository<Reservation, int>();

            var reservation = mapper.Map<Reservation>(dto);
            reservation.UserId = userId;
            reservation.Status = ReservationStatus.Pending;

            await reservationRepo.AddAsync(reservation);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<ReservationReturnDTO>(reservation);
        }

        public async Task<ReservationReturnDTO?> GetReservationByIdAsync(int id, string userId)
        {
            var reservationRepo = unitOfWork.GetRepository<Reservation, int>();

            var reservation = await reservationRepo.GetQueryable()
                .Include(r => r.Hotel)
                .Include(r => r.ReservationRooms).ThenInclude(rr => rr.Room)
                .FirstOrDefaultAsync(r => r.ID == id && r.UserId == userId);

            return reservation == null ? null : mapper.Map<ReservationReturnDTO>(reservation);
        }

        public async Task<IEnumerable<ReservationReturnDTO>> GetUserReservationsAsync(string userId)
        {
            var reservationRepo = unitOfWork.GetRepository<Reservation, int>();

            var reservations = await reservationRepo.GetQueryable()
                .Include(r => r.Hotel)
                .Include(r => r.ReservationRooms).ThenInclude(rr => rr.Room)
                .Where(r => r.UserId == userId)
                .ToListAsync();

            return mapper.Map<IEnumerable<ReservationReturnDTO>>(reservations);
        }

        public async Task<bool> CancelReservationAsync(int id, string userId)
        {
            var reservationRepo = unitOfWork.GetRepository<Reservation, int>();

            var reservation = await reservationRepo.GetQueryable()
                .FirstOrDefaultAsync(r => r.ID == id && r.UserId == userId);

            if (reservation == null)
                return false;

            reservation.Status = ReservationStatus.Cancelled;
            reservationRepo.Update(reservation);
            await unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
