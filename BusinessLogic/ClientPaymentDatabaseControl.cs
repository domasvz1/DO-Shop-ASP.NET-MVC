using System;
using System.Linq;
using Mehdime.Entity;
// Used modules and interfaces in the project
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using BusinessObjects.Orders;
using BusinessObjects;

namespace BusinessLogic
{
    // Database side of the payment information (connected with DataAcces layer)
    public class ClientPaymentDatabaseControl : IClientPaymentDatabaseControl
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IClientRepository _clientRepository;
        private readonly IItemDistributionControl _itemDistributionControl;
        private readonly IHttpPaymentControl _httpPaymentControl;

        public ClientPaymentDatabaseControl(
            IDbContextScopeFactory dbContextScopeFactory,
            IClientRepository cklientRepository,
            IItemDistributionControl itemDistributionControl,
            IHttpPaymentControl httpPaymentControl)
        {
            _dbContextScopeFactory = dbContextScopeFactory ?? throw new ArgumentNullException("dbContextScopeFactory broke near ClientPaymentDatabaseControl");
            _clientRepository = cklientRepository ?? throw new ArgumentNullException("client repository broke near ClientPaymentDatabaseControl");
            _itemDistributionControl = itemDistributionControl ?? throw new ArgumentNullException("item distribution control broke near ClientPaymentDatabaseControl");
            _httpPaymentControl = httpPaymentControl ?? throw new ArgumentNullException("http payment control broke near ClientPaymentDatabaseControl");
        }

        // [R04] TAG Changing Clients Data
        public PaymentInformation ConfirmPayment(int clientId, Cart cart)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {

                var foundClientObject = _clientRepository.GetClient(clientId);
                if (foundClientObject == null)
                {
                    throw new Exception($"There was no client found with the {clientId} id");
                }


                var orderInformation = _httpPaymentControl.InitializePayment(cart.CartsPrice);
                cart.ItemsInCart.ToList().ForEach(x => x.Item = _itemDistributionControl.GetItem(x.Item.Id));

                var order = new ClientOrders { Cart = cart, OrderDate = DateTime.Now, OrderStatus = orderInformation.OrderStatus };
                // Adding order to clinet orders
                foundClientObject.ClientOrders.Add(order);

                // [R04] TAG we do not need to update client information anymore
                //_clientRepository.EditClient(foundClientObject);

                // Saving database contex changes
                dbContextScope.SaveChanges();
                return orderInformation;
            }
        }

        // [R04] TAG Changing Clients Data
        public PaymentInformation ConfirmedPresetOrder(int clientId, Cart cart)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {

                var foundClientObject = _clientRepository.GetClient(clientId);
                if (foundClientObject == null)
                {
                    throw new Exception($"There was no client found with the {clientId} id");
                }

                var paymentInformation = _httpPaymentControl.InitializePayment(cart.CartsPrice);

                ClientOrders order = foundClientObject.ClientOrders.FirstOrDefault(o => o.Cart.Id == cart.Id);
                order.OrderStatus = paymentInformation.OrderStatus;

                _clientRepository.EditClient(foundClientObject);

              
                dbContextScope.SaveChanges();
                return paymentInformation;
            }
        }
    }
}
