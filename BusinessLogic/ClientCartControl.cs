using System.Linq;
using System.Collections.Generic;
// Used modules and interfaces in the project
using BusinessObjects;
using BusinessLogic.Interfaces;

namespace BusinessLogic
{
    public class ClientCartControl : IClientCartControl
    {
        public int CountItemsInCart(ICollection<ItemsInCart> ItemsInCart)
        {
            if (ItemsInCart == null || ItemsInCart.Count == 0)
                return 0;

            int amountOfItems = 0;
            foreach (var cartItem in ItemsInCart)
            {
                amountOfItems += cartItem.Quantity;
            }
            return amountOfItems;
        }

        public void RemoveItemFromCart(ICollection<ItemsInCart> ItemsInCart, int? ItemsIds)
        {
            var itemsToRemove = ItemsInCart.FirstOrDefault(i => i.Id == ItemsIds);

            if (itemsToRemove != null)
                ItemsInCart.Remove(itemsToRemove);
            return; // Needs some work, fixing this or return something
        }
    }
}