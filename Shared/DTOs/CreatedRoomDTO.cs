using Domain.Entites.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public record CreatedRoomDTO
    {
        public int Id { get; set; }
        public RoomType RoomType { get; set; }
        public decimal Price { get; set; }
        public int NumOfBed { get; set; }
        public int MaxOccupancy { get; set; }
        public double RoomSize { get; set; }
        public int RoomNumber { get; set; }
        public string? PictureUrl { get; set; }
        public RoomPicturesDTO? RoomPictures { get; set; }
        public HotelReturnDTO? Hotel { get; set; }
        public int HotelId { get; set; }
    }
}
