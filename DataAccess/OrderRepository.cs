using System;
using System.Linq;
using Mehdime.Entity;
using System.Data.Entity;
using System.Collections.Generic;
//Used modules and interfaces in the project
using DataAccess.Interfaces;
using BusinessObjects.Orders;


namespace DataAccess
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        private DataBaseMigrator DbContext
        {
            get
            {
                var dbContext = _ambientDbContextLocator.Get<DataBaseMigrator>();

                if (dbContext == null)
                    throw new InvalidOperationException("No ambient DbContext of type DOShop found");

                return dbContext;
            }
        }

        public OrderRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            _ambientDbContextLocator = ambientDbContextLocator ?? throw new ArgumentNullException("ambientDbContextLocator");
        }

        public ClientOrders GetOrder(int? id)
        {
            return DbContext.Orders.Include("Cart").SingleOrDefault(o => o.Id == id);
        }

        public List<ClientOrders> GetAllOrders()
        {
            return DbContext.Orders.Include("Cart").ToList();
        }

        public void EditOrder(ClientOrders clientOrder)
        {
            DbContext.Entry(clientOrder).State = EntityState.Modified;
        }
    }
}
