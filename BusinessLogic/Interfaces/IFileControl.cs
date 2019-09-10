using System.Web;

namespace BusinessLogic.Interfaces
{
    public interface IFileControl
    {
        string LoadShopItemsFile(string fileDestination, HttpPostedFileBase file);
    }
}