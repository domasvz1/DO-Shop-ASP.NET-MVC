using System;
using Mehdime.Entity;
using System.Collections.Generic;
// Used modules and interfaces in the project
using BusinessObjects;
using DataAccess.Interfaces;
using BusinessLogic.Interfaces;

namespace BusinessLogic
{
    public class ItemCategoryControl : IItemCategoryControl
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPropertyControl _propertyControl;

        public ItemCategoryControl(IDbContextScopeFactory dbContextScopeFactory, ICategoryRepository kategorijosRepositorija, IPropertyControl savybesValdymas)
        {
            _dbContextScopeFactory = dbContextScopeFactory ?? throw new ArgumentNullException("ItemCategoryControl broke");
            _categoryRepository = kategorijosRepositorija ?? throw new ArgumentNullException("ItemCategoryControl broke");
            _propertyControl = savybesValdymas ?? throw new ArgumentNullException("ItemCategoryControl broke");
        }

        public Category GetCategory(int id)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                Category category = _categoryRepository.GetCategory(id);

                if (category == null)
                    throw new ArgumentException(String.Format("No category with such id", id));

                return category;
            }
        }

        public Category GetCategory(string name)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                Category category = _categoryRepository.GetCategory(name);

                if (category == null)
                    throw new ArgumentException(String.Format("The following category was not found", name));

                return category;
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                return _categoryRepository.GetAllCategories();
            }
        }

        public void CreateCategory(string name, List<int> propertisIds)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var foundCategoryObject = _categoryRepository.GetCategory(name);
                if (foundCategoryObject != null)
                {
                    throw new Exception("That category already exists");
                }
                var properties = _propertyControl.GetAllProperties(propertisIds);
                var category = new Category { Name = name, Properties = properties};
                _categoryRepository.Create(category);
                dbContextScope.SaveChanges();
            }
        }

        public void UpdateCategory(int id, string name, List<int> properties)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var foundCategoryObject = _categoryRepository.GetCategory(id);
                if (foundCategoryObject == null)
                    throw new Exception("No category with such id found");

                // before updating category's name we need to check if it doesnt match the existing one
                var wantedCategoryName = _categoryRepository.GetCategory(name);
                if (wantedCategoryName != null && wantedCategoryName.Id != foundCategoryObject.Id)
                    throw new Exception("The wanted category name already exists");

                foundCategoryObject.Name = name;
                foundCategoryObject.Properties = _propertyControl.GetAllProperties(properties);

                _categoryRepository.Edit(foundCategoryObject);
                dbContextScope.SaveChanges();
            }
        }

        public void DeleteCategory(int id)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {

                var foundCategoryObject = _categoryRepository.GetCategory(id);
                if (foundCategoryObject == null)
                    throw new Exception($"When deleting category with id {id}, it was not found");

                _categoryRepository.Delete(foundCategoryObject);
                dbContextScope.SaveChanges();
            }
        }
    }
}
