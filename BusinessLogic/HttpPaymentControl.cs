using System;
using System.IO;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
// Used modules and interfaces in the project
using BusinessObjects.Orders;
using BusinessLogic.Interfaces;

namespace BusinessLogic
{
    // Http Side of client payment information
    public class HttpPaymentControl : IHttpPaymentControl
    {
        private readonly HttpClient _clientObject = new HttpClient();

        private Task<HttpResponseMessage> SendPaymentInformation(Card card, int cost)
        {

            // We take the credit cards information and send it to the payment api
            // To be honest it should be encrypted, the payment should be only entered in the last page
            string json = "{" +
                "\"amount\":" + cost + ","
                + "\"number\":\"" + card.CardNumber + "\","
                + "\"holder\":\"" + card.CardHolder + "\","
                + "\"exp_year\":" + card.CardExpirationYear + ","
                + "\"exp_month\":" + card.CardExpirationMonth + ","
                + "\"cvv\":\"" + card.CVV + "\""
                + "}";

            _clientObject.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            // Sending the request to the technologines:platformos 
            _clientObject.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "dGVjaG5vbG9naW5lczpwbGF0Zm9ybW9z");


            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://mock-payment-processor.appspot.com/v1/payment")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
 
            return _clientObject.SendAsync(request).ContinueWith(responseTask =>
            {
                try
                {
                    return responseTask.Result;
                }
                catch (AggregateException)
                {
                    var response = new HttpResponseMessage(System.Net.HttpStatusCode.RequestTimeout);
                    return response;
                }
            });
        }


        public PaymentInformation InitializePayment(Card card, int paymentAmount)
        {
            HttpResponseMessage paymentResponse = SendPaymentInformation(card, paymentAmount).Result;
            
            OrderStatus orderStatus;
            string returnMessage = "";

            if (paymentResponse.IsSuccessStatusCode)
                orderStatus = OrderStatus.Approved;

            else
            {
                orderStatus = OrderStatus.Waiting;

                switch (paymentResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.BadRequest: // 400 bad request
                        returnMessage = "Nekorektiški užklausos įvesti duomenys";
                        break;

                    case System.Net.HttpStatusCode.Unauthorized: //401 Failed to autheticated API service
                        returnMessage = "nepavyko autentifikuoti serviso vartotojo";
                        break;

                    case System.Net.HttpStatusCode.PaymentRequired:

                        var receiveStream = paymentResponse.Content.ReadAsStreamAsync().Result;
                        var readStream = new StreamReader(receiveStream, Encoding.UTF8);
                        JObject responseContent = JObject.Parse(readStream.ReadToEnd());

                        string error = responseContent.Property("error").Value.ToString();
                        if (error == "OutOfFunds")
                            returnMessage = "Neužtenka pinigų kortelėje";

                        else if (error == "CardExpired")
                            returnMessage = "Kortelės galiojimo data yra praeityje";

                        break;

                    case System.Net.HttpStatusCode.NotFound:
                        returnMessage = "Operacija nerasta";
                        break;

                    case System.Net.HttpStatusCode.RequestTimeout:
                        returnMessage = "Apmokėjimas nevyko, bandykite dar karta";
                        break;
                }
            }
            return new PaymentInformation() { OrderStatus = orderStatus, PaymentDetails = returnMessage };
        }
    }
}
