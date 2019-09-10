using System;
using Mehdime.Entity;
using System.Collections.Generic;
// Used modules and interfaces in the project
using BusinessObjects;
using DataAccess.Interfaces;
using BusinessLogic.Interfaces;

namespace BusinessLogic
{
    public class AdminControl : IAdminControl
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IAdminRepository _adminRepository;

        public AdminControl(IDbContextScopeFactory dbContextScopeFactory, IAdminRepository adminRepository)
        {
            _dbContextScopeFactory = dbContextScopeFactory ?? throw new ArgumentNullException("w/e dbContextScopeFactory broke near AdminControl");
            _adminRepository = adminRepository ?? throw new ArgumentNullException("adminRepository broke near AdminControl");
        }

        public Admin GetAdmin(string login)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return _adminRepository.GetAdmin(login);
            }
        }

        public Admin GetAdmin(int id)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return _adminRepository.GetAdmin(id);
            }
        }

        public List<Admin> GetAllAdmins()
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return _adminRepository.GetAllAdmins();
            }
        }

        // Could benefit from more edge case's handling
        public Admin ConnectAdmin(Admin admin)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {   
                // Need to search the admin object by name
                var foundAdminObject = _adminRepository.GetAdmin(admin.Login);

                if (foundAdminObject == null)
                    return null;

                // If both fields (Login and Password) are not empty
                if (admin.Login != null && admin.Password != null)
                {
                    // If the password matches the hash in the databse
                    if (foundAdminObject.IsCorrectPassword(admin.Password) && foundAdminObject != null)
                        return foundAdminObject;
                    // Else returning null object
                    else
                        return null;
                }
                return null;
            }
        }
    }
}
