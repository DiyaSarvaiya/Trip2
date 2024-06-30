using System.ComponentModel.DataAnnotations;

namespace Trip2.Areas.Admin.Models
{
    public class HotelModel
    {
        public int HotelID { get; set; }
        [Required(ErrorMessage ="The Hotel Name is required !")]
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
        [Required(ErrorMessage = "The Price Per Night is required !")]
        [Range(2000,50000,ErrorMessage ="Price should be between 2000 and 50000 !")]
        public double? PricePerNight { get; set; }
        [Required(ErrorMessage = "The Review Count is required !")]
        [Range(0,100000,ErrorMessage ="Review Count should be between 0 and 100000")]
        public int? Review { get; set; }
        [Required(ErrorMessage = "The Rating is required !")]
        [Range(0.0,5.0,ErrorMessage ="Rating should be between 0.0 to 5.0 !")]
        public double? Rating { get; set; }
        [Required(ErrorMessage = "The Image is required !")]
        public IFormFile? File { get; set; }
        public string? Photo1 { get; set; }
        [Required(ErrorMessage = "The Amenities Count is required !")]
        [Range(0,10,ErrorMessage ="Amenities Count should be between 0 to 10 !")]
        public int? AmenitiesCount { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }


    }
}
