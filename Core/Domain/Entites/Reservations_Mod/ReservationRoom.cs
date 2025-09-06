namespace Domain.Entites.Reservations_Mod
{
    public class ReservationRoom
    {
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
