using System.Collections.Generic;
//Used modules and interfaces in the project
using BusinessObjects.Orders;

namespace DataAccess.Interfaces
{
    public interface IOrderRepository
    {
        ClientOrders GetOrder(int? id);
        List<ClientOrders> GetAllOrders();
        void EditOrder(ClientOrders order);
    }
}
