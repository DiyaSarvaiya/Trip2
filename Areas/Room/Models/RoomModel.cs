namespace Trip2.Areas.Room.Models
{
    public class RoomModel
    {
        public int RoomID { get; set; }
        public int? HotelID { get; set; }
        public string? Type { get; set; }
        public DateTime? CheckinDate { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public double? Rating { get; set; }
        public double? Price { get; set; }
        public string? Photo { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
