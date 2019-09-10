using System.Collections.Generic;
// Used modules and interfaces in the project
using BusinessObjects;

namespace BusinessLogic.Interfaces
{
    public interface IImportControl
    {
        List<Item> ImportItemsFromFile(string fileRepository);
        string ExportItemsToFile(IEnumerable<Item> items, string savingRepository);
    }
}