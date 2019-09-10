using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class ItemsInCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Kiekis")]
        public int Quantity { get; set; }
        [DisplayName("Krepšelio kaina")]
        public int CartsPrice { get; set; }
        public Cart Cart { get; set; }
        public Item Item { get; set; }
    }
}