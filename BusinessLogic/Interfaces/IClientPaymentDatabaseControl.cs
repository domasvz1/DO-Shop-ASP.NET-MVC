//Used modules and Interfacesfrom the project
using BusinessObjects;
using BusinessObjects.Orders;

namespace BusinessLogic.Interfaces
{
    public interface IClientPaymentDatabaseControl
    {
        PaymentInformation ConfirmPayment(int clientId, Cart cart);
        PaymentInformation ConfirmedPresetOrder(int clientId, Cart cart);
    }
}