using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public ICollection<ItemsInCart> ItemsInCart { get; set; }

        [DisplayName("Carts price")]
        public int CartsPrice { get; set; }

        public int CountCartsPrice(ICollection<ItemsInCart> ItemsInCart)
        {
            if (ItemsInCart == null)
                return 0;

            int cartsPrice = 0;
            foreach (var itemInCart in ItemsInCart)
                cartsPrice += itemInCart.Item.Price * itemInCart.Quantity;

            // return the carts full price
            return cartsPrice;
        }
    }
}