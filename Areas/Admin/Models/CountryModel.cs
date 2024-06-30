using System.ComponentModel.DataAnnotations;

namespace Trip2.Areas.Admin.Models
{
    public class CountryModel
    {
        public int? CountryID { get; set; }

        [Required(ErrorMessage = "The Country Name is required.")]
        public string? CountryName { get; set; }
    }
}
