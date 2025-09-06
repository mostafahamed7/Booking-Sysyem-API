namespace Domain.Exceptions
{
    public class ReservationNotFoundException : NotFoundException
    {
        public ReservationNotFoundException(int id) :base($"Reservation with id {id} not found.")
        {
            
        }
    }
}
