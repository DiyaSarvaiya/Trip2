using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Trip2.Areas.Users_Area.Models
{
    public class UsersModel
    {
        public int UserID {  get; set; }

        [Required]
        [DisplayName("User Name shouls")]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        public string? Email { get; set; }

        public string? PhoneNo { get; set; }

        public bool? IsAdmin { get; set; }
    }
}
