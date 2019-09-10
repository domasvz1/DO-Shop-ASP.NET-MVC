using System.Collections.Generic;
// Used modules and interfaces in the project
using BusinessObjects.Orders;

namespace BusinessLogic.Interfaces
{
    public interface IOrderControl
    {
        ClientOrders GetClientOrder(int id);
        IEnumerable<ClientOrders> GetAllOrders();
        void UpdateOrderStatus(int id, OrderStatus status);
    }
}