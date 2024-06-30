using System.Data;

namespace Trip2.Areas.Package.Models
{
    public class UserHotel
    {
        public string? HotelName { get; set; }
        public string? HotelPhoto { get; set; }
        public string? RoomType { get; set; }
        public double? HotelPrice { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public int? HotelAdults { get; set; }
        public int? HotelChildren { get; set; }

        public decimal HotelTotalCost { get; set; }
        public int? UserID { get; set; }
        public int? HotelID { get; set; }
        public int? UserHotelID { get; set; }

     
    }
}
