using System.Collections.Generic;
//Used modules and interfaces in the project
using BusinessObjects;

namespace DataAccess.Interfaces
{
    public interface IItemRepository
    {
        Item GetItem(int? id);
        List<Item> GetAllItems();
        List<Item> GetAllItems(IEnumerable<int> ids);
        void CreateItem(Item item);
        void EditItem(Item item);
        void DeleteItem(Item item);
    }
}