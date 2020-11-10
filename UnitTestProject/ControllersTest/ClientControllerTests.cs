using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Presentation;
using BusinessObjects;
using BusinessObjects.Orders;
using BusinessLogic.Interfaces;
using Presentation.Models;
using Presentation.Controllers;
using BusinessLogic;
using DataAccess;
using Mehdime.Entity;
using DataAccess.Interfaces;

namespace UnitTestProject.ControllersTest
{
    [TestClass]
    public class ClientControllerTests : Controller
    {
        // Creating mock data for controller
        // Database Context Locators
        IDbContextScopeFactory iDBScope = new DbContextScopeFactory();
        IAmbientDbContextLocator iDBLoc = new AmbientDbContextLocator();

        // Items mock data interfaces
        IItemRepository iItemRep;
        ICategoryRepository iCatRep;

        // Main classes
        IClientProfileControl clientProfileControl;
        IClientCartControl clientCartControl;
        IItemDistributionControl itemDistributionControl;
        IHttpPaymentControl httpPaymentControl;
        IClientPaymentDatabaseControl clientDatabaseControl;
        IClientRepository iCliRep;


        ClientController clientController;

        // Setup
        [TestInitialize]
        public void Setup()
        {
            // Items mock data interfaces
            iItemRep = new ItemRepository(iDBLoc);
            iCatRep = new CategoryRepository(iDBLoc);
            // Main Classes for the controller
            iCliRep = new ClientRepository(iDBLoc);
            clientProfileControl = new ClientProfileControl(iDBScope, iCliRep);
            clientCartControl = new ClientCartControl();
            itemDistributionControl = new ItemDistributionControl(iDBScope, iItemRep, iCatRep);
            httpPaymentControl = new HttpPaymentControl();
            clientDatabaseControl = new ClientPaymentDatabaseControl(iDBScope, iCliRep, itemDistributionControl, httpPaymentControl);
            
            // Controller's constructors
            clientController = new ClientController(
                    iCliRep,
                    clientProfileControl,
                    clientCartControl,
                    itemDistributionControl,
                    httpPaymentControl,
                    clientDatabaseControl
                    );
        }


        [TestMethod]
        public void IndexClientEmpty()
        {
            var result = clientController.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            //Assert.IsNotNull(result.Model); // Additional check for the model itself
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }

        public void TestEditClientProfile()
        {
            ViewResult objViewResult = clientController.EditProfile() as ViewResult;
            Assert.AreEqual("Index", objViewResult.ViewName);
        }


        public void TestEditClientProfileGet()
        {
            ClientModel cModel = new ClientModel
            {
                ClientVM = clientProfileControl.GetClient(1)
            };
           
            //clientController.ConfigureClientModel(cModel); // Filling Countries and City's data
            ViewResult objViewResult = clientController.EditProfile(cModel) as ViewResult;
            Assert.AreEqual("Index", objViewResult.ViewName);
        }


        //[TestMethod]
        //public void IndexAdminListType()
        //{

        //    var result = adminController.Index();
        //    var model = ((ViewResult)result).Model as IEnumerable<Item>;

        //    // Assert
        //    Assert.IsNotNull(model, "Model is not the type of List<IEnumerable<Item>>");
        //    // second check, does the same
        //    Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(IEnumerable<Item>));
        //}

        //[TestMethod]
        //public void LoginAdminEmpty()
        //{
        //    var result = adminController.Login() as ViewResult;

        //    Assert.IsNotNull(result);
        //    //Assert.IsNotNull(result.Model); // Additional check for the model itself
        //    Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Login");
        //}

        //[TestMethod]
        //public void AdminLoginWithParams()
        //{
        //    // Mock default admin user for testing
        //    Admin admin = new Admin
        //    {
        //        Id = 1,
        //        Login = "admin",
        //        Password = "1234*"
        //    };

        //    ViewResult objViewResult = adminController.Login(admin) as ViewResult;
        //    //If logged on in with params, should redirect to Index page
        //    Assert.AreEqual("Index", objViewResult.ViewName);
        //}

        //[TestMethod]
        //public void AdminLoginPassingNull()
        //{
        //    Admin adminObjectNull = null;
        //    //Check
        //    ViewResult objViewResult = adminController.Login(adminObjectNull) as ViewResult;
        //    //Assert
        //    Assert.AreEqual("Login", objViewResult.ViewName);
        //}

        //// Test For Empty Error Admin VIew
        //[TestMethod]
        //public void AdminErrorWindow()
        //{
        //    var result = adminController.SomethingWrongAdmin() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //    // At the moment there's no model passed so its null
        //    //Assert.IsNotNull(result.Model); // Additional check for the model itself
        //    Assert.IsTrue(string.IsNullOrEmpty(result.ViewName));
        //}

        //[TestMethod]
        //public void TestSearchUsersListEmptyQuery()
        //{
        //   string emptyQuery = "";
        //    // Checks if the result is not null and equal to what it should be 
        //    PartialViewResult result = adminController.UsersList(emptyQuery) as PartialViewResult;
        //    // If the type is not List<CLient> this will return null, this is the check as if 
        //    var model = ((PartialViewResult)result).Model as List<Client>;

        //    // Assert
        //    Assert.IsNotNull(model, "Model is not the type of List<List<Client>>");
        //    Assert.AreEqual("../Admin/_UsersList", result.ViewName);
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void TestSearchUsersRandomQuery()
        //{
        //    string rndmQuery = "saiudausdnsayod_randomAdminquery123*";
        //    // Checks if the result is not null and equal to what it should be 
        //    PartialViewResult result = adminController.UsersList(rndmQuery) as PartialViewResult;
        //    // If the type is not List<CLient> this will return null, this is the check as if 
        //    var model = ((PartialViewResult)result).Model as List<Client>;

        //    // Assert
        //    Assert.IsNotNull(model, "Model is not the type of List<List<Client>>");
        //    Assert.IsTrue(model.IsNullOrEmpty());  // List should  be null or empty
        //    Assert.AreEqual("../Admin/_UsersList", result.ViewName);
        //    Assert.IsNotNull(result);
        //}
    }
}
