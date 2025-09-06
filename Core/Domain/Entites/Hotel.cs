using Domain.Entites.Reservations_Mod;

namespace Domain.Entites
{
    public class Hotel : BaseEntity<int>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int RoomsCount { get; set; }
        public string? Address { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Stars { get; set; }



        // Navigation property with Pictures Meny2Many relationship
        public ICollection<RoomPictures> HotelPictures { get; set; } = new List<RoomPictures>();

        // Navigation property with Rooms One2Many relationship
        public ICollection<Room> Rooms { get; set; } = new List<Room>();

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
