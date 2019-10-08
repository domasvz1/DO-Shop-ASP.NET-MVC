using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Collections.Generic;
// Used modules and interfaces in the project
using BusinessObjects;
using BusinessObjects.Orders;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Presentation.Models;
using System.Web.UI.WebControls;

namespace Presentation.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientProfileControl _clientProfileControl;
        private readonly IClientCartControl clientCartControl;
        private readonly IItemDistributionControl _itemDistributionControl;
        private readonly IHttpPaymentControl _httpPaymentControl;
        private readonly IClientPaymentDatabaseControl _clientPaymentDatabseControl;
        private readonly IClientRepository _IClientRepository;

        public ClientController(IClientProfileControl clientProfileControl, IClientCartControl cartControl, IItemDistributionControl itemDistributionControl, IHttpPaymentControl httpPaymentControl, IClientPaymentDatabaseControl clientPaymentDatabseControl, IClientRepository iClientRepository)
        {
            _clientProfileControl = clientProfileControl;
            clientCartControl = cartControl;
            _itemDistributionControl = itemDistributionControl;
            _httpPaymentControl = httpPaymentControl;
            _clientPaymentDatabseControl = clientPaymentDatabseControl;
            _IClientRepository = iClientRepository; 
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [UserAuthorization(ConnectionPage = "~/Client/Login", Roles = "Client")]
        public ActionResult EditProfile()
        {
            // A check here should be implemented in the never versions, to see f any cleints are left from last time log in
            ClientModel model = new ClientModel
            {
                ClientVM = _clientProfileControl.GetClient((int)Session["AccountId"])
            };

            // Since in Country and City list a value with index of 0 will be as " -- Not selected --"
            ConfigureClientModel(model); // Filling Countries and City's data
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorization(ConnectionPage = "~/Client/Login", Roles = "Client")]
        public ActionResult EditProfile(ClientModel model)
        {
            // Since the selected Country and City is never null after editing anymore the null checking is not necessary
            // List values assigned to the Client Object's Delivery Address class
            model.ClientVM.DeliveryAddress.Country = _IClientRepository.FetchCountries().ElementAt(model.SelectedCountry.Value).Name;
            model.ClientVM.DeliveryAddress.City = _IClientRepository.FetchCities().ElementAt(model.SelectedCity.Value).Name;

            if (ModelState.IsValid)
            {
                try
                {
                    _clientProfileControl.EditProfile(model.ClientVM);
                    return RedirectToAction("Index", "Main");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Failino ?", ex.Message);
                }
            }
            ConfigureClientModel(model);
            return View(model);
        }

        // Ajax calls this
        [HttpGet]
        public JsonResult FetchCities(int ID)
        {
            var data = _IClientRepository.FetchCities()
                .Where(l => l.CountryID == ID)
                .Select(l => new { Value = l.ID, Text = l.Name });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private void ConfigureClientModel(ClientModel model)
        {
            List<Country> countries = _IClientRepository.FetchCountries();
            model.CountryList = new SelectList(countries, "ID", "Name");
            IEnumerable<City> cities = _IClientRepository.FetchCities().Where(l => l.CountryID == model.SelectedCountry.Value);
            model.CityList = new SelectList(cities, "ID", "Name");
            if(!String.IsNullOrEmpty(model.ClientVM.DeliveryAddress.Country)) {
                model.SelectedCountry = countries.Find(obj => obj.Name == model.ClientVM.DeliveryAddress.Country).ID;
                model.SelectedCity = cities.FirstOrDefault(obj => obj.Name == model.ClientVM.DeliveryAddress.City).ID;
            }
            else // If its newly created user, the ID = 0 is the first option in Countris and Cities list, which always counts as not selected
            {
                int? notselectedID = 0;
                model.SelectedCountry = notselectedID;
                model.SelectedCity = notselectedID;
            }
        }

        //GET: Client/Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Client ClientObject)
        {
            // Creating Card and DeliveryAddress objects with default data to fill up for the user
            // He will be available to edit them in other windows
            ClientObject.MobilePhone = "";
            DeliveryAddress delivery = new DeliveryAddress();
            Card card = new Card();

            // Passing the client data to the created object
            ClientObject.DeliveryAddress = delivery;
            ClientObject.Card = card;

            if (ModelState.IsValid) // Tells if any model errors have been added to ModelState.
            {
                try
                {
                    _clientProfileControl.CreateClient(ClientObject);
                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    string exMessage = ex.Message;
                    ModelState.AddModelError("", exMessage);
                }
            }
            // Should print whats wrong with the launch
            var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
            return View(ClientObject);
        }

        //GET: Client/Checkout
        public ActionResult Checkout()
        {
            return View();
        }

        //GET: Client/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Client ClientObject, string returnUrl)
        {
            // Login model should have model if false exception

            // We pass the client's onject and then we search if there's a client object saved with the given data
            var foundClientObject = _clientProfileControl.ConnectClient(ClientObject);
            if (foundClientObject != null)
            {
                FormsAuthentication.SetAuthCookie("c" + foundClientObject.Email, false);
                Session["AccountId"] = foundClientObject.Id;
                Session["AccountEmail"] = foundClientObject.Email;
                Session["IsAdminAccount"] = false;
                if (returnUrl == null || returnUrl == string.Empty)
                {
                    return RedirectToAction("Index", "Main");
                }
                return Redirect(returnUrl);
            }

            if(!ClientObject.IsBlocked)
                ModelState.AddModelError("", "We are sorry but you are blocked in our system");
            else
                ModelState.AddModelError("", "You have entered the wrong password or the email");

            return View(foundClientObject);
        }

        [UserAuthorization(ConnectionPage= "~/Client/Login", Roles = "Client")]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["AccountId"] = null;
            Session["AccountEmail"] = null;
            Session["IsAdminAccount"] = null;
            return RedirectToAction("Login");
        }

        [UserAuthorization(ConnectionPage = "~/Client/Login", Roles = "Client")]
        public ActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        [UserAuthorization(ConnectionPage = "~/Client/Login", Roles = "Client")]
        public ActionResult PasswordChange(PasswordModel model)
        {
            Client Client = _clientProfileControl.GetClient((int)Session["AccountId"]);
            if (ModelState.IsValid)
            {
                if (!Client.DoEncyptedPassowrdsMacth(model.OldPassword))
                {
                    var errMessage = "Entered password is wrong";

                    ModelState.AddModelError("", errMessage);
                    return View(model);
                }
                if (!model.DoPasswordsMatch())
                {
                    var errMessage = "The entered password matches the current one";

                    ModelState.AddModelError("", errMessage + "! Pick a new password");
                    return View(model);
                }

                model.ExcryptThePassword();

                _clientProfileControl.EditPassword(Client.Id, model.NewPassword);

            }
            return RedirectToAction("EditProfile", new { @client = Client });
        }

        // GET: Cart
        public ActionResult Cart()
        {
            //if (Session["Cart"] == null || (int?)Session["Count"] == 0)
            //    return RedirectToAction("EmptyCart");

            return View((Cart)Session["Cart"]);
        }

        public ActionResult EmptyCart()
        {
            return View();
        }

        public ActionResult RemoveFromCart(int? cartItemId)
        {
            Cart cart = (Cart)Session["Cart"];


            clientCartControl.RemoveItemFromCart(cart.ItemsInCart, cartItemId);
            Session["Count"] = clientCartControl.CountItemsInCart(cart.ItemsInCart);
            cart.CartsPrice = cart.CountCartsPrice(cart.ItemsInCart);

            return RedirectToAction("Cart");
        }

        [HttpPost]
        public ActionResult AddItemToCart(FormCollection fc)
        {
            int id = Convert.ToInt32(fc["itemId"]), quantityOfItems = 0;

            try
            {
                quantityOfItems = Convert.ToInt32(fc["quantity"]);
            }
            catch (ArgumentOutOfRangeException)
            {
                quantityOfItems = 1;
            }

            var clientsCart = (Cart)Session["Cart"];
            Item item = _itemDistributionControl.GetItem(id);


            if (item == null)
                return RedirectToAction("Cart");

            if (clientsCart == null)
            {
                clientsCart = new Cart()
                {
                    ItemsInCart = new List<ItemsInCart>()
                };
                Session["Cart"] = clientsCart;
            }

            if ((clientsCart.ItemsInCart.FirstOrDefault(i => i.Item.Id == id) != null))
            {
                clientsCart.ItemsInCart.Where(x => x.Item.Id == id).ToList()
                    .ForEach(y => y.Quantity += quantityOfItems);
            }
            else
            {
                ItemsInCart cartItem = new ItemsInCart()
                {
                    Cart = (Cart)Session["Cart"],
                    Item = item,
                    Quantity = quantityOfItems
                };
                clientsCart.ItemsInCart.Add(cartItem);
            }

            clientsCart.CartsPrice = clientsCart.CountCartsPrice(clientsCart.ItemsInCart);
            Session["Count"] = clientCartControl.CountItemsInCart(clientsCart.ItemsInCart);
            return Json(new { message = item.Name + "(" + quantityOfItems + ")" + " buvo prideta i krepseli ", itemCount = Session["Count"] });
        }

        [HttpPost]
        public ActionResult ChangeItemsQuantity(FormCollection fc)
        {
            if ((Cart)Session["Cart"] == null)
                return RedirectToAction("EmptyCart");

            int cartItemId = 0;
            int cartItemQuantity = 0;

            try
            {
                cartItemId = Convert.ToInt32(fc["itemId"]);
                cartItemQuantity = Convert.ToInt32(fc["quantity"]);
                if (cartItemQuantity < 1)
                    return RedirectToAction("Cart");
            }
            catch (OverflowException)
            {
                return RedirectToAction("Cart");
            }
            catch (FormatException)
            {
                return RedirectToAction("Cart");
            }

            Cart cart = (Cart)Session["Cart"];
            ItemsInCart preke = cart.ItemsInCart.FirstOrDefault(x => x.Item.Id == cartItemId);

            if (preke != null)
                preke.Quantity = cartItemQuantity;
            else
                return RedirectToAction("Cart");

            cart.CartsPrice = cart.CountCartsPrice(cart.ItemsInCart);
            Session["Count"] = clientCartControl.CountItemsInCart(cart.ItemsInCart);
            return Json(new
            {
                cartCost = (cart.CartsPrice / 100.0m),
                itemCount = Session["Count"],
                itemCost = (preke.Item.Price * cartItemQuantity) / 100.0m,
            });
        }

        // Payment
        [UserAuthorization(ConnectionPage = "~/Client/Login", Roles = "Client")]
        public ActionResult Payment()
        {
            ActionResult actionResult = GetSessionProperties(out Client customer, out Cart cart);
            if (actionResult != null)
            {
                return actionResult;
            }

            RecalculatePrices(cart);
            return View(new PaymentModel() { Client = customer, Cart = cart, FullOrder = false });
        }

        [UserAuthorization(ConnectionPage = "~/Client/Login", Roles = "Client")]
        public ActionResult ApmoketiSuformuotaUzsakyma(FormCollection fc)
        {
            string cardNumber = fc["Client.Card.cardNumber"];
            string cardHolder = fc["Client.Card.cardOwner"];
            int cardExpirationYear = Int32.Parse(fc["Client.Card.cardExpirationYear"]);
            int cardExpirationMonth = Int32.Parse(fc["Client.Card.cardExpirationMonth"]);
            string cardCVV = fc["Client.Card.CVV"];
            int cartId = Int32.Parse(fc["cartId"]);

            GetSessionCustomer(out Client klientas);
            var order = klientas.ClientOrders.FirstOrDefault(o => o.Cart.Id == cartId);

            var cart = order.Cart;
            var paymentInformation = _clientPaymentDatabseControl.ConfirmedPresetOrder(klientas.Id, cart, cardNumber, cardExpirationYear, cardExpirationMonth, cardHolder, cardCVV);
            return View("NotPaidOrders", new PaymentModel() { PaymentInformation = paymentInformation, Cart = cart });
        }

        public ActionResult PayUnpaidOrder(int orderId)
        {
            GetSessionCustomer(out Client customer);

            var order = customer.ClientOrders.FirstOrDefault(o => o.Id == orderId);
            var cart = order.Cart;

            // If the order is null this sould be added to favro
            if (order == null)
            {

            }
            return View("Payment", new PaymentModel() { Client = customer, Cart = cart, FullOrder = true });
        }

        [UserAuthorization(ConnectionPage = "~/Client/Login", Roles = "Client")]
        public ActionResult NotPaidOrders(FormCollection fc)
        {
            string cardNumber = fc["Client.Card.cardNumber"];
            string cardHolder = fc["Client.Card.cardOwner"];
            int cardExpirationYear = Int32.Parse(fc["Client.Card.cardExpirationYear"]);
            int cardExpirationMonth = Int32.Parse(fc["Client.Card.cardExpirationMonth"]);
            string cardCVV = fc["Client.Card.CVV"];

            ActionResult actionResult = GetSessionProperties(out Client customer, out Cart cart);

            if (actionResult != null)
            {
                return actionResult;
            }

            var apmokejimoInformacija = _clientPaymentDatabseControl.ConfirmPayment( customer.Id, cart, cardNumber, cardExpirationYear, cardExpirationMonth, cardHolder, cardCVV);

            Session["Cart"] = null;
            Session["Count"] = 0;
            return View(new PaymentModel() { PaymentInformation = apmokejimoInformacija, Cart = cart });
        }

        [UserAuthorization(ConnectionPage = "~/Client/Login", Roles = "Client")]
        private ActionResult GetSessionProperties(out Client customer, out Cart cart)
        {
            GetSessionCustomer(out customer);

            if (Session["Cart"] == null)
            {
                cart = null;
                return RedirectToAction("EmptyCart", "Client");
            }

            cart = (Cart)Session["Cart"];
            return null;
        }

        [UserAuthorization(ConnectionPage = "~/Client/Login", Roles = "Client")]
        private void GetSessionCustomer(out Client customer)
        {
            int? customerId = (int?)Session["AccountId"];
            customer = _clientProfileControl.GetClient((int)customerId);
        }

        [UserAuthorization(ConnectionPage = "~/Client/Login", Roles = "Client")]
        private void RecalculatePrices(Cart cart)
        {
            cart.CartsPrice = 0;

            foreach (var item in cart.ItemsInCart)
            {
               item.CartsPrice = item.Item.Price;
               cart.CartsPrice += item.Quantity * item.CartsPrice;
            }
        }


        // Here starts Order History Views

        // GET: OrderHistory
        [UserAuthorization(ConnectionPage = "~/Client/Login", Roles = "Client")]
        public ActionResult OrderHistory()
        {
            Client currentCustomer = _clientProfileControl.GetClient((int)Session["AccountId"]);

            if (currentCustomer.ClientOrders.Count == 0)
                return RedirectToAction("NoOrders");
            return View(currentCustomer.ClientOrders);
        }

        [UserAuthorization(ConnectionPage = "~/Client/Login", Roles = "Client")]
        public ActionResult NoOrders()
        {
            return View();
        }

        [HttpPost]
        [UserAuthorization(ConnectionPage = "~/Client/Login", Roles = "Client")]
        public ActionResult DisplayOrders(FormCollection fc)
        {
            int orderID = 0;
            try
            {
                orderID = Convert.ToInt32(fc["OrderId"]);
            }
            catch (FormatException)
            {
                return Content("<html></html>");
            }

            int? currentCustomerId = (int)Session["AccountId"];
            if (currentCustomerId == null)
                return Content("<html></html>");

            Client currentCustomer = _clientProfileControl.GetClient((int)currentCustomerId);
            ClientOrders order = currentCustomer.ClientOrders.FirstOrDefault(o => o.Id == orderID);
            ViewBag.OrderId = order.Id;
            OrderModel um = new OrderModel { ClientOrders = order };
            return View(um);
        }


        [UserAuthorization(ConnectionPage = "~/Client/Login", Roles = "Client")]
        public ActionResult RepeatTheOrder(int orderId)
        {

            var customer = _clientProfileControl.GetClient((int)Session["AccountId"]);
            ClientOrders cleintOrders = customer.ClientOrders.FirstOrDefault(o => o.Id == orderId);

            if (cleintOrders == null)
            {
                return View("Cart", (Cart)Session["Cart"]);
            }

            Item newItem;
            foreach (var item in cleintOrders.Cart.ItemsInCart)
            {
                newItem = _itemDistributionControl.GetItem(item.Item.Id);
                if (newItem != null)
                {
                    FormCollection fc = new FormCollection
                    {
                        ["itemId"] = Convert.ToString(newItem.Id),
                        ["quantity"] = Convert.ToString(item.Quantity)
                    };
                    AddItemToCart(fc);
                }
            }

            return View("Cart", (Cart)Session["Cart"]);
        }
    }
}
