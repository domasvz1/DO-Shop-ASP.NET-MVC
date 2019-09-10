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
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        private DataBaseMigrator DataBaseMigrator
        {
            get
            {
                var dbContext = _ambientDbContextLocator.Get<DataBaseMigrator>();

                if (dbContext == null)
                    throw new InvalidOperationException("No ambient DbContext of type DOShop found");

                return dbContext;
            }
        }

        public PropertyRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            _ambientDbContextLocator = ambientDbContextLocator ?? throw new ArgumentNullException("ambientDbContextLocator");
        }

        public void Create(Property property)
        {
            DataBaseMigrator.Properties.Add(property);
        }

        public Property GetProperty(int id)
        {
            return DataBaseMigrator.Properties.Include("ItemProperties").SingleOrDefault(c => c.Id == id);
        }

        public Property GetProperty(string name)
        {
            return DataBaseMigrator.Properties.Include("ItemProperties").FirstOrDefault(c => c.Name == name);
        }

        public List<Property> GetAllProperties()
        {
            return DataBaseMigrator.Properties.Include("ItemProperties").ToList();
        }

        public void Edit(Property property)
        {
            DataBaseMigrator.Entry(property).State = EntityState.Modified;
        }

        public void Delete(Property property)
        {
            DataBaseMigrator.Properties.Remove(property);
        }

        public List<Property> GetAllPropertiesByIds(List<int> ids)
        {
            return DataBaseMigrator.Properties.Where(x=>ids.Contains(x.Id)).Include("ItemProperties").ToList();
        }
    }
}
