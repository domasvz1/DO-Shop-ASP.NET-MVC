using System.Collections.Generic;
//Used modules and interfaces in the project
using BusinessObjects;

namespace DataAccess.Interfaces
{
    public interface IPropertyRepository
    {
        Property GetProperty(int id);
        Property GetProperty(string name);
        List<Property> GetAllProperties();
        List<Property> GetAllPropertiesByIds(List<int> ids);
        void Create(Property property);
        void Edit(Property property);
        void Delete(Property property);
    }
}