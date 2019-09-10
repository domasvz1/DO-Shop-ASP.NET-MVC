using System.Collections.Generic;
// Used modules and interfaces in the project
using BusinessObjects;

namespace BusinessLogic.Interfaces
{
    public interface IItemCategoryControl
    {
        // Category is getable by id and its name
        Category GetCategory(int id);
        Category GetCategory(string name);
        IEnumerable<Category> GetAllCategories();
        void CreateCategory(string name, List<int> ids);
        void UpdateCategory(int id, string Name, List<int> properties);
        void DeleteCategory(int id);
    }
}