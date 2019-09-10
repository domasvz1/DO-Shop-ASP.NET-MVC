using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Orders
{
    public class Card
    {
        //All the card data should be hashed
        [Required(ErrorMessage = "need 16 digits numeric, fix later")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Wrong number")]
        [DisplayName("Kortelės numeris")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Need the card holder")]
        [DisplayName("Card Holder")]
        public string CardHolder { get; set; }

        [Required(ErrorMessage = "Enter expiration year")]
        [Range(2020, 2200, ErrorMessage = "wrong expiration year")]
        [DisplayName("Card Expiration Year")]
        public int CardExpirationYear { get; set; }

        [Range(1, 12, ErrorMessage = "Enter expiration month")]
        [Required(ErrorMessage = "wrong expiration month")]
        [DisplayName("Card Expiration Month")]
        public int CardExpirationMonth { get; set; }

        [NotMapped]
        public string CVV { get; set; }
    }
}