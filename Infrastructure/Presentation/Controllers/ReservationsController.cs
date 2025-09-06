using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.DTOs.Reservation_DTOs;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class ReservationsController(IServiceManger _serviceManager) : ApiController
    {
        // Create Reservation
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReservationReturnDTO>> Create([FromBody] ReservationCreateDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _serviceManager.ReservationServices.CreateReservationAsync(dto, userId);
            return Ok(result);
        }

        // Get Reservation by Id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ReservationReturnDTO>> GetById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reservation = await _serviceManager.ReservationServices.GetReservationByIdAsync(id, userId);

            if (reservation == null)
                return NotFound();

            return Ok(reservation);
        }

        // Get all reservations for the current user
        [HttpGet("my")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ReservationReturnDTO>>> GetMyReservations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reservations = await _serviceManager.ReservationServices.GetUserReservationsAsync(userId);
            return Ok(reservations);
        }

        // Cancel reservation
        [HttpPut("{id}/cancel")]
        [Authorize]
        public async Task<ActionResult> Cancel(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cancelled = await _serviceManager.ReservationServices.CancelReservationAsync(id, userId);

            if (!cancelled)
                return NotFound();

            return NoContent();
        }
    }
}
