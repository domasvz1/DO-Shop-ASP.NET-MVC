﻿using BusinessObjects.Orders;
//Used modules and Interfacesfrom the project
namespace BusinessLogic.Interfaces
{
    public interface IHttpPaymentControl
    {
        PaymentInformation InitializePayment(Card card, int amount);
    }
}