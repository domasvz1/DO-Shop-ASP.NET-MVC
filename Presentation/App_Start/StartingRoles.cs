using System;
using System.Web.Security;
// Used modules and interfaces in the project
using BusinessLogic.Interfaces;

namespace Presentation
{
    public class StartingRoles : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }
        // The first char of the received role variable will indicate if the connected entity is user or admin
        public override string[] GetRolesForUser(string roleVariable) 
        {
            string role = "";

             if (roleVariable[0] == 'a')
            {
                var adminService = UnityConfig.Container.Resolve(typeof(IAdminControl), "") as IAdminControl;
                var admin = adminService.GetAdmin(roleVariable.Substring(1, roleVariable.Length - 1));
                role = admin != null ? "Admin" : "";
            }
            else if (roleVariable[0] == 'c')
            {
                var clientConfig = UnityConfig.Container.Resolve(typeof(IClientProfileControl), "") as IClientProfileControl;
                var client = clientConfig.Client(roleVariable.Substring(1, roleVariable.Length - 1));
                role = client != null ? "Client" : "";
            }

            string[] result = { role };
            return result;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}