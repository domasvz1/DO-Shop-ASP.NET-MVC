using System.Web;
using System.Threading.Tasks;
using System.Collections.Generic;
// Used modules and interfaces in the project
using BusinessObjects;

namespace BusinessLogic.Interfaces
{
    public interface IItemControl
    {
        void CreateItem(Item item);
        void CreateItemWithPicture(Item item, string pictureRepository);
        void UpdateItem(Item item);
        void UpdateItemsPicture(Item item, string photoRepository);
        void DeleteItem(int id);

        // Properties
        void AddPropertyToItem(int itemId, int propertyId, string propertyInfo);
        void RemovePropertyFromItem(int itemId, int propertyId);

        // Tasks with files
        Task StartImportingItems(string itemFolderRepository, HttpPostedFileBase file, string imageRepository);
        Task ExportItemsTask(IEnumerable<Item> items, string itemsRepository);
    }
}