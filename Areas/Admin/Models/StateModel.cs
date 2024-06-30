using System.ComponentModel.DataAnnotations;

namespace Trip2.Areas.Admin.Models
{
    public class StateModel
    {
        public int? StateID { get; set; }

        [Required(ErrorMessage = "The State Name is required.")]
        public string? StateName { get; set; }

        [Required(ErrorMessage = "The Country Name is required.")]
        public int? CountryID { get; set; }
        public string? CountryName { get; set; }
    }

   /* public class StateInsert
    {
        public int? StateID { get; set; }
        public string? StateName { get; set; }
        public int? CountryID { get; set; }
    }*/
}
