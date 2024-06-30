using System.ComponentModel.DataAnnotations;

namespace Trip2.Areas.Admin.Models
{
    public class CityModel
    {
        public int? CityID { get; set; }

        [Required(ErrorMessage = "The City Name is required !")]
        public string? CityName { get; set; }

        [Required(ErrorMessage = "The State Name is required !")]
        public int? StateID { get; set; }
        public string? StateName { get; set; }
        public string? CountryName { get; set; }

        [Required(ErrorMessage = "The Country Name is required !")]
       
        public int? CountryID { get; set; }
    }

   
}
