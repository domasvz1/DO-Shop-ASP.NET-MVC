using System.Collections.Generic;
// Used modules and interfaces in the project
using BusinessObjects;

namespace BusinessLogic.Interfaces
{
    public interface IItemDistributionControl
    {
        Item GetItem(int id);
        // This probably can be deleted
        IEnumerable<Item> GetItemsByCategory(int categoryId);
        IEnumerable<Item> GetAllItems();
    }
}
