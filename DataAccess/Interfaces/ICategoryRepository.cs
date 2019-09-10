using System.Collections.Generic;
//Used modules and interfaces in the project
using BusinessObjects;

namespace DataAccess.Interfaces
{
    public interface ICategoryRepository
    {
        Category GetCategory(int? id);
        Category GetCategory(string name);
        List<Category> GetAllCategories();
        void Create(Category category);
        void Edit(Category category);
        void Delete(Category category);
    }
}