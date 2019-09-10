using System.Collections.Generic;
// Used modules and interfaces in the project
using BusinessObjects;

namespace BusinessLogic.Interfaces
{
    public interface IPropertyControl
    {
        Property GetProperty(int id);
        Property GetProperty(string name);
        List<Property> GetAllProperties();
        List<Property> GetAllProperties(List<int> ids);
        void CreateProperty(string name);
        void UpdateProperty(Property property);
        void DeleteProperty(int propertyId);
    }
}