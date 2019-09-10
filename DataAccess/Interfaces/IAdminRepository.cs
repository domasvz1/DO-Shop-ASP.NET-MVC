using System.Collections.Generic;
//Used modules and interfaces in the project
using BusinessObjects;

namespace DataAccess.Interfaces
{
    public interface IAdminRepository
    {
        Admin GetAdmin(int id);
        Admin GetAdmin(string login);
        List<Admin> GetAllAdmins();
        void CreateAdmin(Admin admin);
        void EditAdmin(Admin admin);
    }
}