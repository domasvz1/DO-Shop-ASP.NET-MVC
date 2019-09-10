using System;
using Mehdime.Entity;
using System.Collections.Generic;
//Used modules and Interfacesfrom the project
using BusinessObjects;
using DataAccess.Interfaces;
using BusinessLogic.Interfaces;

namespace BusinessLogic
{
    public class PropertyControl : IPropertyControl
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IPropertyRepository _propertyRepository;

        public PropertyControl(IDbContextScopeFactory dbContextScopeFactory, IPropertyRepository propertyRepository)
        {
            _dbContextScopeFactory = dbContextScopeFactory ?? throw new ArgumentNullException("dbContextScopeFactory near propertyControl broke");
            _propertyRepository = propertyRepository ?? throw new ArgumentNullException("property repository near propertyControl broke");
        }

        public Property GetProperty(int id)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var foundPropertyObject = _propertyRepository.GetProperty(id);

                if (foundPropertyObject == null)
                    throw new ArgumentException($"Property with such id was not found");

                return foundPropertyObject;
            }
        }

        public Property GetProperty(string name)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var foundPropertyObject = _propertyRepository.GetProperty(name);

                return foundPropertyObject;
            }
        }

        public List<Property> GetAllProperties()
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                return _propertyRepository.GetAllProperties();
            }
        }

        public List<Property> GetAllProperties(List<int> propertiesIds)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                return _propertyRepository.GetAllPropertiesByIds(propertiesIds);
            }
        }

        public void CreateProperty(string name)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var property = new Property
                {
                    Name = name
                };

                _propertyRepository.Create(property);
                dbContextScope.SaveChanges();
            }
        }

        public void UpdateProperty(Property property)
        {
            if (property == null)
                throw new ArgumentNullException($"Property with id {property} not found");

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var foundPropertyObject = _propertyRepository.GetProperty(property.Id);
                if (foundPropertyObject == null)
                    throw new Exception($"Property with id {property} not found");

                foundPropertyObject.Name = property.Name;
                _propertyRepository.Edit(foundPropertyObject);
                dbContextScope.SaveChanges();
            }
        }

        public void DeleteProperty(int id)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var property = _propertyRepository.GetProperty(id);

                if (property == null)
                    throw new ArgumentException($"Property with id {id} not found");

                _propertyRepository.Delete(property);
                dbContextScope.SaveChanges();
            }
        }
    }
}
