using System.ComponentModel.DataAnnotations;
//
using BusinessObjects;
using BusinessObjects.Orders;

namespace Presentation.Models
{
    public class PaymentModel
    {
        public PaymentInformation PaymentInformation { get; set; }
        public Cart Cart { get; set; }
        public Client Client { get; set; }
        public bool FullOrder { get; set; }
    }
}