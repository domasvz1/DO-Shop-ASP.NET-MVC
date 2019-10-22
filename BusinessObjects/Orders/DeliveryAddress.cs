using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Orders
{
    public class DeliveryAddress
    {
        public string Country { get; set; }
        public string City { get; set; }
        [Required(ErrorMessage = "Enter receivers street's name")]
        [DisplayName("Street Name")]
        public string Street { get; set; } = "-";
        [Required(ErrorMessage = "Enter receivers street number")]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; } = "-";
        [Required(ErrorMessage = "Enter receivers apartment/office number")]
        [DisplayName("Apartment/Office number")]
        public string ApartmentNumber { get; set; } = "-";
    }
}