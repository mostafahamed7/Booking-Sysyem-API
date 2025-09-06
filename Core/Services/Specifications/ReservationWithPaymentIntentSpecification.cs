using Domain;
using Domain.Entites.Reservations_Mod;

namespace Services.Specifications
{
    internal class ReservationWithPaymentIntentSpecification : Specification<Reservation>
    {
        public ReservationWithPaymentIntentSpecification(string paymentIntentID) :base(r => r.PaymentIntentId == paymentIntentID)
        {
            AddInclude(r => r.ReservationRooms);
            AddInclude(r => r.Hotel);
            AddInclude(r => r.User);
        }
    }
}
