using System.ComponentModel.DataAnnotations;

namespace Trip2.Areas.Admin.Models
{
    public class DestinationModel
    {
        public int DestinationID { get; set; }

        [Required(ErrorMessage = "The Destination Name is required !")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The City Name is required !")]
        public int? CityID { get; set; }
        public string? CityName { get; set; }
        [Required(ErrorMessage = "The State Name is required !")]
        public int? StateID { get; set; }
        public string? StateName { get; set; }
        [Required(ErrorMessage = "The Country Name is required !")]
        public int? CountryID { get; set; }
        public string? CountryName { get; set; }
        public int? UserID{ get; set;}
        [Required(ErrorMessage = "Detail is required !")]
        [StringLength(200,ErrorMessage ="Detail max length is 200 !")]
        public string? Detail { get; set; }
        [Required(ErrorMessage = "Climate is required !")]
        public string? Climate { get; set; }
        [Required(ErrorMessage = "Best Time To Visit is required !")]
        public string? BestTimeToVisit { get; set; }
        [Required(ErrorMessage = "Currency is required !")]
        public string? Currency { get; set; }
        [Required(ErrorMessage = "Language is required !")]
        public string? Language { get; set; }
        [Required(ErrorMessage = "Travel Tips is required !")]
        [StringLength(200, ErrorMessage = "Travel Tip max length is 200 !")]
        public string? TravelTips { get; set; }
        [Required(ErrorMessage = "Photo is required !")]
        public IFormFile? File { get; set; }
        [Required(ErrorMessage = "Photo1 is required !")]
        public string? Photo1 { get; set; }
        [Required(ErrorMessage = "Rating is required !")]
        [Range(0.0,5.0,ErrorMessage ="Rating is only allowed from 0.0 to 5.0 !")]
        public double? Rating { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

    }
}
