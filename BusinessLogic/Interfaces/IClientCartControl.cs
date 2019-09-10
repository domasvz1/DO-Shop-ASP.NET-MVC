using System.Collections.Generic;
//Used modules and Interfacesfrom the project
using BusinessObjects;

namespace BusinessLogic.Interfaces
{
    public interface IClientCartControl
    {
        int CountItemsInCart(ICollection<ItemsInCart> ItemsInCart);
        void RemoveItemFromCart(ICollection<ItemsInCart> ItemsInCart, int? ItemsIds);
    }
}
