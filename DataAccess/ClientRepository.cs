using System;
using System.Linq;
using Mehdime.Entity;
using System.Data.Entity;
using System.Collections.Generic;
using System.Text.RegularExpressions;
// Used modules and interfaces in the project
using BusinessObjects;
using DataAccess.Interfaces;
using BusinessObjects.Orders;

namespace DataAccess
{
    public class ClientRepository : IClientRepository
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        public ClientRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            _ambientDbContextLocator = ambientDbContextLocator ?? throw new ArgumentNullException("ambientDbContextLocator");
        }

        private DataBaseMigrator DbContext
        {
            get
            {
                var dbContext = _ambientDbContextLocator.Get<DataBaseMigrator>();

                if (dbContext == null)
                {
                    throw new InvalidOperationException("No ambient DbContext of type DOShop found." +
                        "This means that this repository method has been called outside of the scope of a DbContextScope." +
                        "A repository must only be accessed within the scope of a DbContextScope," +
                        "which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts." +
                        "This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction." +
                        "To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope" +
                        "that wraps the entire business transaction that your service method implements. Then access this repository within that scope." +
                        "Refer to the comments in the IDbContextScope.cs file for more details.");
                }

                return dbContext;
            }
        }


        public Client GetClient(int? clientsId)
        {
            if (clientsId == null)
                return null;

            return DbContext.Client.Include("ClientOrders.Cart.ItemsInCart.Item.Category").Where(client => client.Id == clientsId).FirstOrDefault();
        }

        public Client GetClient(string clientsEmail)
        {
            return DbContext.Client.Include("ClientOrders.Cart.ItemsInCart.Item.Category").Where(client => client.Email== clientsEmail).FirstOrDefault();
        }

        public List<Client> GetAllClients()
        {
            return DbContext.Client.Include("ClientOrders.Cart.ItemsInCart.Item.Category").ToList();
        }

        public List<Client> GettAllClients(string request)
        {
            if (request == null)
            {
                return new List<Client>();
            }
            Regex regex = new Regex(@"" + request + "", RegexOptions.IgnoreCase);
            return DbContext.Client.Where((x => regex.IsMatch(x.FirstName) || regex.IsMatch(x.LastName) || regex.IsMatch(x.Email))).Distinct().ToList();
        }

        public void CreateClient(Client client)
        {
            DbContext.Client.Add(client);
        }

        public void EditClient(Client client)
        {
            DbContext.Entry(client).State = EntityState.Modified;
        }

        public void DeleteClient(Client client)
        {
            DbContext.Client.Remove(client);
        }

        public void UpdateClientOrder(Client client)
        {
            DbContext.Entry(client).State = EntityState.Modified;
        }

        public List<Country> FetchCountries()
        {
            // Data here needs to be exported from an excel file to show which Cities belong to a certain country
            List<Country> countries = new List<Country>
            {
                new Country() { ID = 0, Name = "-- (Unselected) Select Delivery Country--" },
                new Country() { ID = 1, Name = "Lithuania" },
                new Country() { ID = 2, Name = "Latvia" }
            };
            return countries;
        }
        public List<City> FetchCities()
        {
            // If you cahnge values here, this might corralate with databe and cause error in Client Controller
            List<City> cities = new List<City>
            {
                new City() { ID = 0, CountryID = 0, Name = "- First Select Delivery Country -" },
                new City() { ID = 1, CountryID = 1, Name = "Vilnius" },
                new City() { ID = 2, CountryID = 1, Name = "Kaunas" },
                new City() { ID = 3, CountryID = 1, Name = "Klaipeda" },
                new City() { ID = 4, CountryID = 2, Name = "Ryga" },
                new City() { ID = 5, CountryID = 2, Name = "Jurmala" },
                new City() { ID = 6, CountryID = 2, Name = "Liepaja" },
                new City() { ID = 7, CountryID = 1, Name = "Alytus" },
                new City() { ID = 8, CountryID = 1, Name = "Panevezys" },
            };
            return cities;
        }
    }
}
