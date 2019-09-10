using System;
using System.Linq;
using Mehdime.Entity;
using System.Data.Entity;
using System.Collections.Generic;
// Used modules and interfaces in the project
using BusinessObjects;
using DataAccess.Interfaces;

namespace DataAccess
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        private DataBaseMigrator DBContext
        {
            get
            {
                var dbContext = _ambientDbContextLocator.Get<DataBaseMigrator>();

                if (dbContext == null)
                    throw new InvalidOperationException("No ambient DbContext of type DOShop found");

                return dbContext;
            }
        }

        public CategoryRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            _ambientDbContextLocator = ambientDbContextLocator ?? throw new ArgumentNullException("ambientDbContextLocator");
        }

        public void Create(Category category)
        {
            DBContext.Categories.Add(category);
        }

        public Category GetCategory(int? id)
        {
            return DBContext.Categories.Include("Items").Include("Properties").SingleOrDefault(c => c.Id == id);
        }

        public List<Category> GetAllCategories()
        {
            return DBContext.Categories.Include("Items").Include("Properties").ToList();
        }

        public void Edit(Category itemCategory)
        {
            DBContext.Entry(itemCategory).State = EntityState.Modified;
        }

        public void Delete(Category itemCategory)
        {
            DBContext.Categories.Remove(itemCategory);
        }

        public Category GetCategory(string itemCategoryName)
        {
            return DBContext.Categories.Include("Items").Where(c => c.Name == itemCategoryName).SingleOrDefault();
        }
    }
}
