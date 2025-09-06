using Domain.Entites.Enums;
using Domain.Entites.Identity;
using Domain.Entites.Order_Entites;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entites.Reservations_Mod
{
    public class Reservation : BaseEntity<int>
    {

        // User
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        // Hotel
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        // Dates
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string? ClientSecret { get; set; }


        // Status
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

        // Total Price
        public decimal TotalPrice { get; set; }

        // Rooms (Many-to-Many)
        public ICollection<ReservationRoom> ReservationRooms { get; set; }

        public string PaymentIntentId { get; set; }

        public ReservationPaymentStatus PaymentStatus { get; set; } = ReservationPaymentStatus.Pending;
    }
}
