using System.ComponentModel.DataAnnotations;

namespace Trip2.Areas.Admin.Models
{
    public class RoomModel
    {
        public int RoomID { get; set; }
        [Required(ErrorMessage = "The Hotel Name is required !")]
        public int? HotelID { get; set; }
        public string? HotelName {  get; set; }
        [Required(ErrorMessage = "The Type is required !")]
        public string? Type { get; set; }
        [Required(ErrorMessage = "The Rating is required !")]
        [Range(0.0, 5.0, ErrorMessage = "Rating should be between 0.0 to 5.0 !")]
        public double? Rating { get; set; }
        [Required(ErrorMessage = "The Price is required !")]
        [Range(2000, 50000, ErrorMessage = "Price should be between 2000 and 50000 !")]
        public double? Price { get; set; }
        public IFormFile? File { get; set; }
        [Required(ErrorMessage = "The Photo is required !")]
        public string? Photo { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
