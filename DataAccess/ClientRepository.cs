using System;
using System.Linq;
using Mehdime.Entity;
using System.Data.Entity;
using System.Collections.Generic;
using System.Text.RegularExpressions;
// Used modules and interfaces in the project
using BusinessObjects;
using DataAccess.Interfaces;

namespace DataAccess
{
    public class ClientRepository : IClientRepository
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        private DataBaseMigrator DbContext
        {
            get
            {
                var dbContext = _ambientDbContextLocator.Get<DataBaseMigrator>();

                if (dbContext == null)
                    throw new InvalidOperationException("No ambient DbContext of type DOShop found.");

                return dbContext;
            }
        }

        public ClientRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            _ambientDbContextLocator = ambientDbContextLocator ?? throw new ArgumentNullException("ambientDbContextLocator");
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
            Regex tinkamasKlientas = new Regex(@"" + request + "", RegexOptions.IgnoreCase);
            return DbContext.Client.Where((x => tinkamasKlientas.IsMatch(x.FirstName) || tinkamasKlientas.IsMatch(x.LastName) || tinkamasKlientas.IsMatch(x.Email))).Distinct().ToList();
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
    }
}
