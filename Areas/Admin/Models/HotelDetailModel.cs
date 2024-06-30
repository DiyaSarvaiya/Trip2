namespace Trip2.Areas.Admin.Models
{
    public class HotelDetailModel
    {
        public int HotelDetailID { get; set; }
        public int? HotelID { get; set; }
        public string? Name { get; set; }
        public int? UserID { get; set; }
        public int? CountryID { get; set; }
        public int? StateID { get; set; }
        public int? CityID { get; set; }
        public string? Type { get; set; }
        public string? AvailableRooms { get; set; }
        public string? Photo1 { get; set; }
        public int? RoomID { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
