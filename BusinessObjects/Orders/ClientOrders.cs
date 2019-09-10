using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Orders
{
    public class ClientOrders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Current status of the order")]
        public OrderStatus OrderStatus { get; set; }

        [DisplayName("The date of the order")]
        public DateTime OrderDate { get; set; }
        public Cart Cart { get; set; }
    }
}
