using System.Collections.Generic;
// Used modules and interfaces in the project
using BusinessObjects;

namespace BusinessLogic.Interfaces
{
    public interface IClientProfileControl
    {
        Client ConnectClient(Client clientObject);
        Client GetClient(int id);
        Client Client(string email);
        List<Client> GetClientsList();
        void CreateClient(Client clientObject);
        void EditProfile(Client clientObject);
        void EditPassword(int id, string newPassword);
        void EditStatus(Client clientObject);
    }
}