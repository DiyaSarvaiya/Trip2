using System.ComponentModel.DataAnnotations;

namespace Trip2.Areas.Package.Models
{
    public class PaymentModel
    {
        [Required(ErrorMessage ="Enter Card Number.")]
        public string cardNumber { get; set; }
        [Required(ErrorMessage = "Enter Card Holder Name.")]
        public string cardHolderName { get; set; }
        [Required(ErrorMessage = "Enter Expiration Date.")]
        public DateTime expiration {  get; set; }
        [Required(ErrorMessage = "Enter cvv")]
        public string cvv { get; set; }
    }
}
