using System.Collections.Generic;
//Used modules and Interfacesfrom the project
using BusinessObjects;

namespace BusinessLogic.Interfaces
{
    public interface IAdminControl
    {
        Admin ConnectAdmin(Admin adminObject);
        Admin GetAdmin(int id);
        Admin GetAdmin(string login);
        List<Admin> GetAllAdmins();
    }
}