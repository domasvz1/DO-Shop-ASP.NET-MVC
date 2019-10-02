using System.Collections.Generic;
// Used modules and interfaces in the project
using BusinessObjects;
using BusinessObjects.Orders;

namespace DataAccess.Interfaces
{
    public interface IClientRepository
    {
        Client GetClient(int? id);
        Client GetClient(string email);
        List<Client> GetAllClients();
        List<Client> GettAllClients(string quarry);
        void CreateClient(Client client);
        void EditClient(Client client);
        void UpdateClientOrder(Client client);
        void DeleteClient(Client client);
        List<City> FetchCities();
        List<Locality> FetchLocalities();
    }
}