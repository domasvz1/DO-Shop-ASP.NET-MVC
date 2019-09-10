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

        public PaymentInformation ConfirmPayment(int clientId, Cart cart, string cardNumber, int expirationYear, int expirationMonth, string cardHolder, string cvv)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {

                var foundClientObject = _clientRepository.GetClient(clientId);
                if (foundClientObject == null)
                {
                    throw new Exception($"There was no client found with the {clientId} id");
                }

                // Assigning the values to the required fields of the card
                foundClientObject.Card.CardNumber = cardNumber;
                foundClientObject.Card.CardExpirationYear = expirationYear;
                foundClientObject.Card.CardExpirationMonth = expirationMonth;
                foundClientObject.Card.CardHolder = cardHolder;
                foundClientObject.Card.CVV = cvv;


                var paymentInformation = _httpPaymentControl.InitializePayment(foundClientObject.Card, cart.CartsPrice);

                cart.ItemsInCart.ToList().ForEach(x => x.Item = _itemDistributionControl.GetItem(x.Item.Id));

                var order = new ClientOrders { Cart = cart, OrderDate = DateTime.Now, OrderStatus = paymentInformation.OrderStatus };
                foundClientObject.ClientOrders.Add(order);
                _clientRepository.EditClient(foundClientObject);

                // Making cards information default or blank (for now)
                foundClientObject.Card.CardNumber = "4111111111111111";
                foundClientObject.Card.CardExpirationYear = 2018;
                foundClientObject.Card.CardExpirationMonth = 1;
                foundClientObject.Card.CardHolder = "Vardenis Pavardenis";
                foundClientObject.Card.CVV = "111";
                dbContextScope.SaveChanges();
                return paymentInformation;
            }
        }

        public PaymentInformation ConfirmedPresetOrder(
            int clientId,
            Cart cart,
            string cardNumber,
            int expirationYear,
            int expirationMonth,
            string cardHolder,
            string cvv)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {

                var foundClientObject = _clientRepository.GetClient(clientId);
                if (foundClientObject == null)
                {
                    throw new Exception($"There was no client found with the {clientId} id");
                }

                // Cia irgi reiks prideti likusius duomenis
                foundClientObject.Card.CardNumber = cardNumber;
                foundClientObject.Card.CardExpirationYear = expirationYear;
                foundClientObject.Card.CardExpirationMonth = expirationMonth;
                foundClientObject.Card.CardHolder = cardHolder;
                foundClientObject.Card.CVV = cvv;

                var paymentInformation = _httpPaymentControl.InitializePayment(foundClientObject.Card, cart.CartsPrice);

                ClientOrders order = foundClientObject.ClientOrders.FirstOrDefault(o => o.Cart.Id == cart.Id);
                order.OrderStatus = paymentInformation.OrderStatus;
                _clientRepository.EditClient(foundClientObject);

                // Making card object data default (for now)
                foundClientObject.Card.CardNumber = "4111111111111111";
                foundClientObject.Card.CardExpirationYear = 2018;
                foundClientObject.Card.CardExpirationMonth = 1;
                foundClientObject.Card.CardHolder = "Vardenis Pavardenis";
                foundClientObject.Card.CVV = "111";
                dbContextScope.SaveChanges();
                return paymentInformation;
            }
        }
    }
}
