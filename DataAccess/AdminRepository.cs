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
        public readonly IAmbientDbContextLocator _ambientDbContextLocator;

        private DataBaseMigrator DBContext
        {
            get
            {
                var ambientDbContextLocator = _ambientDbContextLocator.Get<DataBaseMigrator>();

                if (ambientDbContextLocator == null)
                {
                    throw new InvalidOperationException("No ambient DbContext of type UserManagementDbContext found." +
                        "This means that this repository method has been called outside of the scope of a DbContextScope." +
                        "A repository must only be accessed within the scope of a DbContextScope," +
                        "which takes care of creating the DbContext instances that the repositories need and making them available" +
                        "as ambient contexts. This is what ensures that, for any given DbContext-derived type," +
                        "the same instance is used throughout the duration of a business transaction." +
                        "To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create" +
                        "a DbContextScope that wraps the entire business transaction that your service method implements." +
                        "Then access this repository within that scope.");
                }


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
