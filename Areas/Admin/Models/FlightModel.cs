using System.ComponentModel.DataAnnotations;
using System.Timers;
using Trip2.Areas.Admin.CustomValidation;

namespace Trip2.Areas.Admin.Models
{
    public class FlightModel
    {
        public int FlightID { get; set; }
        [Required(ErrorMessage = "The Airline Name is required !")]
        public string? AirlineName { get; set; }
        [Required(ErrorMessage = "The FlightNo is required !")]
        [StringLength(6,ErrorMessage ="FlightNo should be 6 character !")]
        public string? FlightNo { get; set; }
        [Required(ErrorMessage = "The City Name is required !")]
        public int? CityID { get; set; }
        [Required(ErrorMessage = "The State Name is required !")]
        public int? StateID { get; set; }
        [Required(ErrorMessage = "The Country Name is required !")]
        public int? CountryID { get; set; }
        [Required(ErrorMessage = "The Departure City is required !")]
        public string? DepartureCity { get; set; }
        [Required(ErrorMessage = "The Destination City is required !")]
        [NotEqual("DepartureCity", ErrorMessage = "Destination city should not be equals to Departure city")]
        public string? DestinationCity { get; set; }
        [Required(ErrorMessage = "The Stops is required !")]
        public int? Stops { get; set; }
        [Required(ErrorMessage = "The Departure Time is required !")]
        public DateTime? DepartureTime { get; set; }
        [Required(ErrorMessage = "The Arrival Time is required !")]
        [NotEqual("DepartureTime", ErrorMessage = "Arrival Time should not be equals to Departure Time !")]
        public DateTime? ArrivalTime { get; set; }
        
        [Required(ErrorMessage = "The Type is required !")]
        public string? Type { get; set; }
        [Required(ErrorMessage = "The Class is required !")]
        public string? Class { get; set; }
        [Required(ErrorMessage = "The Duration is required !")]
        public DateTime? Duration { get; set; }
        [Required(ErrorMessage = "Price is required !")]
        public double? Price { get; set; }
        [Required(ErrorMessage = "The File is required !")]
        public IFormFile? File { get; set; }
        public string? Photo { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }

    public class PlaceModel
    {
        public int CityID { get; set; }
        public int StateID { get; set; }
        public int CountryID { get; set; }
    }

   
}
