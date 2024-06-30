namespace Trip2.Areas.Admin.Models
{
    public class PackageModel
    {
        public int PackageID { get; set; }
        public int? DestinationID { get; set; }
        public int? UserID { get; set; }
        public DateTime? StartDate { get; set; }
        public int? Duration { get; set; }
        public int? Nights { get; set; }
        public string? Adults { get; set; }
        public int? UserHotelID { get; set; }
        public int? UserFlightID { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
