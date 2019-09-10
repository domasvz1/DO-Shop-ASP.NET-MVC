using System.IO;
using System.Web;
// Used modules and interfaces in the project
using BusinessLogic.Interfaces;

namespace BusinessLogic
{
    public class FileControl : IFileControl
    {
        public string LoadShopItemsFile(string fileFolder, HttpPostedFileBase file)
        {
            if(file == null || file.ContentLength <= 0)
            {
                return null;
            }
            string fileName = Path.GetFileName(file.FileName);
            string pathToFile = Path.Combine(fileFolder, fileName);
            file.SaveAs(pathToFile);
            return fileName;
        }
    }
}