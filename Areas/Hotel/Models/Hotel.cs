namespace Trip2.Areas.Hotel.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int CountryID { get; set; }
        public int CityID { get; set; }
        public int StateID { get; set; }
        public double PricePerNight { get; set; }
        public int Review { get; set; }
        public double Rating { get; set; }
        public string Photo1 { get; set; }
        public int AmenitiesCount { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
