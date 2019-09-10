//Used modules and Interfacesfrom the project
using BusinessObjects;
using BusinessObjects.Orders;

namespace BusinessLogic.Interfaces
{
    public interface IClientPaymentDatabaseControl
    {
        PaymentInformation ConfirmPayment(int clientId, Cart cart, string cardNumber, int expirationYear, int expirationMonth , string cardHolder, string cvv );
        PaymentInformation ConfirmedPresetOrder(int clientId, Cart cart, string cardNumber, int expirationYear, int expirationMonth, string cardHolder, string cvv);
    }
}