namespace Shared
{
    public enum SortingTypes
    {
        PriceAsc,
        PriceDesc,
        NumAsc,
        NumDesc
    }
    public class RoomSpceParams
    {
        public SortingTypes? Sort { get; set; }
        public int? HotelID { get; set; }

        private const int MaxPageSize = 10;
        private const int DefaultPageSize = 5;
        public int PageIndex { get; set; } = 1;
        private int pageSize = DefaultPageSize;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

    }
}
