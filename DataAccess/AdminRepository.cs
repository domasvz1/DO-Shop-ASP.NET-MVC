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
    public class AdminRepository : IAdminRepository
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        private DataBaseMigrator DBContext
        {
            get
            {
                var ambientDbContextLocator = _ambientDbContextLocator.Get<DataBaseMigrator>();

                if (ambientDbContextLocator == null)
                    throw new InvalidOperationException("No ambient DbContext of type DOShop found");

                return ambientDbContextLocator;
            }
        }

        public AdminRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            _ambientDbContextLocator = ambientDbContextLocator ?? throw new ArgumentNullException("ambientDbContextLocator");
        }

        public Admin GetAdmin(int id)
        {
            return DBContext.Admin.Where(c => c.Id == id).FirstOrDefault();
        }

        public Admin GetAdmin(string login)
        {
            return DBContext.Admin.Where(admin => admin.Login == login).FirstOrDefault();
        }

        public List<Admin> GetAllAdmins()
        {
            return DBContext.Admin.ToList();
        }

        public void CreateAdmin(Admin admin)
        {
            DBContext.Admin.Add(admin);
        }

        public void EditAdmin(Admin admin)
        {
            DBContext.Entry(admin).State = EntityState.Modified;
        }
    }
}
