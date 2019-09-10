using System;
using System.Linq;
using Mehdime.Entity;
using System.Data.Entity;
using System.Collections.Generic;
//Used modules and interfaces in the project
using BusinessObjects;
using DataAccess.Interfaces;

namespace DataAccess
{
    public class ItemRepository : IItemRepository
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        private DataBaseMigrator DbContext
        {
            get
            {
                var dbContext = _ambientDbContextLocator.Get<DataBaseMigrator>();

                if (dbContext == null)
                    throw new InvalidOperationException("No ambient DbContext of type DOShop found");

                return dbContext;
            }
        }

        public ItemRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            _ambientDbContextLocator = ambientDbContextLocator ?? throw new ArgumentNullException("ambientDbContextLocator");
        }

        public void CreateItem(Item item)
        {
            DbContext.Items.Add(item);
        }

        public Item GetItem(int? id)
        {
            return DbContext.Items.Include("Category").Include("ItemProperties.Property") .SingleOrDefault(c => c.Id == id);
        }

        public List<Item> GetAllItems()
        {
            return DbContext.Items.Include("Category").Include("ItemProperties.Property").ToList();
        }

        public void EditItem(Item item)
        {
            DbContext.Entry(item).State = EntityState.Modified;
        }

        public void DeleteItem(Item item)
        {
            DbContext.ItemsInCart.Where(x => x.Item != null && x.Item.Id == item.Id).ToList().ForEach(y => y.Item = null);
            DbContext.Items.Remove(item);
        }

        public List<Item> GetAllItems(IEnumerable<int> ids)
        {
            return DbContext.Items.Include("Category").Include("ItemProperties.Property").Where(t => ids.Contains(t.Id)).ToList();
        }
    }
}
