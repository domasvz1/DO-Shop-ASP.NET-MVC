using System.ComponentModel;

namespace BusinessObjects.Orders
{
    public class PaymentInformation
    {
        [DisplayName("Order Status")]
        public OrderStatus OrderStatus { get; set; }

        [DisplayName("Payment Details")]
        public string PaymentDetails { get; set; }
    }
}