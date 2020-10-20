using System;
using Mehdime.Entity;
using System.Collections.Generic;
// Used modules and interfaces in the project
using BusinessObjects;
using DataAccess.Interfaces;
using BusinessLogic.Interfaces;

namespace BusinessLogic
{
    public class ItemDistributionControl : IItemDistributionControl
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ItemDistributionControl(
            IDbContextScopeFactory dbContextScopeFactory,
            IItemRepository itemRepository,
            ICategoryRepository categoryRepository)
        {
            _dbContextScopeFactory = dbContextScopeFactory ?? throw new ArgumentNullException("dbContextScopeFactory");
            _itemRepository = itemRepository ?? throw new ArgumentNullException("itemRepository");
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException("categoryRepository");
        }

        public IEnumerable<Item> GetAllItems()
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                return _itemRepository.GetAllItems();
            }
        }

        public Item GetItem(int id)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                Item item = _itemRepository.GetItem(id);

                if (item == null)
                    throw new ArgumentException("Item with such id was not found"); // handle this expection

                return item;
            }
        }

        public IEnumerable<Item> GetItemsByCategory(int categoryId)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                Category category = _categoryRepository.GetCategory(categoryId);

                if (category == null)
                    throw new ArgumentException(String.Format("Item with such category was not found"));

                return category.Items;
            }
        }
    }
}
