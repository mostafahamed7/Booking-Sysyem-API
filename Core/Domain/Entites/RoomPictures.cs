namespace Domain.Entites
{
    public class RoomPictures : BaseEntity<int>
    {
        public string? PictureUrl { get; set; }

        // Navigation property with Room Meny2Many relationship
        public ICollection<Room> Rooms { get; set; } = new List<Room>();

        // Navigation property with Hotel Meny2Many relationship
        public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
    }
}
