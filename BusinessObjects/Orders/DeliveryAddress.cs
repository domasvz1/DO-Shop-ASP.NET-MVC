using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Orders
{
    public class DeliveryAddress
    {
        [Required(ErrorMessage = "Enter receivers country")]
        [DisplayName("Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Enter receivers city")]
        [DisplayName("City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter receivers street's name")]
        [DisplayName("Street Name")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Enter receivers street number")]
        [DisplayName("Streets Number")]
        public int StreetNumber { get; set; }

        [Required(ErrorMessage = "Enter receivers apartment/office number")]
        [DisplayName("Apartment/Office number")]
        public int ApartmentNumber { get; set; }
    }
}