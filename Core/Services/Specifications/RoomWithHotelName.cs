using Domain;
using Domain.Entites;
using Shared;

namespace Services.Specifications
{
    public class RoomWithHotelName : Specification<Room>
    {
        public RoomWithHotelName(int id) :base(R => R.ID == id)
        {
            AddInclude(R => R.Hotel);
        }

        public RoomWithHotelName(RoomSpceParams parameters) : base(
            Room => (!parameters.HotelID.HasValue || Room.HotelId == parameters.HotelID)
            )
        {
            if (parameters.Sort != null)
            {
                switch (parameters.Sort)
                {
                    case SortingTypes.PriceAsc:
                        SetOrderBy(p => p.Price);
                        break;
                    case SortingTypes.PriceDesc:
                        SetOrderByDescending(p => p.Price);
                        break;
                    case SortingTypes.NumAsc:
                        SetOrderBy(p => p.RoomNumber);
                        break;
                    case SortingTypes.NumDesc:
                        SetOrderByDescending(p => p.RoomNumber);
                        break;
                    default:
                        SetOrderBy(p => p.ID); // Default sorting by Id
                        break;
                }
                }

            AddInclude(R => R.Hotel);
        }
    }
}
