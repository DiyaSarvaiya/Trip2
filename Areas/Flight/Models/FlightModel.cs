using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.ComponentModel.DataAnnotations;

namespace Trip2.Areas.Flight.Models
{
    public class FlightModel
    {
        public int FlightID { get; set; }
        public string? AirlineName { get; set; }
        public string? FlightNo { get; set; }
        public int? CityID { get; set; }
        public int? StateID { get; set; }
        public int? CountryID { get; set; }
        public string? DepartureCity { get; set; }
        public int? Stops { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public string? DestinationCity { get; set; }
        public string? Type { get; set; }
        public string? Class { get; set; }
        public DateTime? Duration { get; set; }
        public double? Price { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }

    public class SearchFlight
    {
        public string? DepartureCity { get; set; }
        public string? DestinationCity { get; set; }
        public string? Type { get; set; }
        public string? Class { get; set; }
        public int? Adults {  get; set; }
        public int? Children { get; set; }


    }
}
