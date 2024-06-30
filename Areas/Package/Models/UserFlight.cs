using System.Data;

namespace Trip2.Areas.Package.Models
{
    public class UserFlight
    {
        public string? AirlineName { get; set; }
        public string? FlightNo { get; set; }
        public string? FlightPhoto { get; set; }
        public string? DepartureCity { get; set; }
        public string? DestinationCity { get; set; }
        public DateTime? DepartureTime {  get; set; }
        public DateTime? ArrivalTime { get; set; }
        public string? FlightType { get; set; }
        public string? Class { get; set; }
        public double? FlightPrice { get; set; }
        public int? FlightAdults { get; set; }
        public int? FlightChildren { get; set; }

        public decimal FlightTotalCost { get; set; }

        public int? UserID {  get; set; }
        public int? FlightID { get; set; }
        public int? UserFlightID { get; set; }

    }
}
