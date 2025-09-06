namespace Domain.Exceptions
{
    public sealed class RoomNotFoundException : NotFoundException
    {
        public RoomNotFoundException(int id): base($"Room with id {id} not found.")
        {
            
        }
    }
}
