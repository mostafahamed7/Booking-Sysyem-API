using Domain.Entites.Enums;
using Domain.Entites.Reservations_Mod;

namespace Domain.Entites
{
    public class Room : BaseEntity<int>
    {
        public RoomType RoomType { get; set; }
        public decimal Price { get; set; }
        public int NumOfBed { get; set; }
        public int MaxOccupancy { get; set; }
        public double RoomSize { get; set; }
        public int RoomNumber { get; set; }
        public string? PictureUrl { get; set; }
        public bool IsAvailable { get; set; }

        // Navigation property with Facility One2Many relationship
        public Hotel Hotel { get; set; }
        public int HotelId { get; set; }

        // Navigation property with Pictures Meny2Many relationship
        public ICollection<RoomPictures> RoomPictures { get; set; } = new List<RoomPictures>();
        
        public ICollection<ReservationRoom> ReservationRooms { get; set; } = new List<ReservationRoom>();


    }

}
