using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Orders
{
    //[R04] TAG REMOVE
    public class CardNeedsRemoving
    {
        ////All the card data should be hashed
        //[Required(ErrorMessage = "need 16 digits numeric, fix later")]
        //[StringLength(16, MinimumLength = 16, ErrorMessage = "Wrong number")]
        //[DisplayName("Kortelės numeris")]
        //public string CardNumber { get; set; } = "4111111111111111";

        //[Required(ErrorMessage = "Need the card holder")]
        //[DisplayName("Card Holder")]
        //public string CardHolder { get; set; } = "Not selected card holder";

        //[Required(ErrorMessage = "Enter expiration year")]
        //[Range(2020, 2200, ErrorMessage = "wrong expiration year")]
        //[DisplayName("Card Expiration Year")]
        //public int CardExpirationYear { get; set; } = 2020;

        //[Range(1, 12, ErrorMessage = "Enter expiration month")]
        //[Required(ErrorMessage = "wrong expiration month")]
        //[DisplayName("Card Expiration Month")]
        //public int CardExpirationMonth { get; set; } = 11;

        //[NotMapped]
        //public string CVV { get; set; } = "Not selected yet";
    }
}