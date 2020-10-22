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
using System.ComponentModel.Design;
using BusinessLogic;
using DataAccess;
using System.ComponentModel;
using Moq;
using System.Dynamic;
using Castle.Core.Internal;

namespace UnitTestProject.ControllersTest
{
    [TestClass]
    public class AdminControllerUnitTest : Controller
    {
        private readonly Mock<IItemDistributionControl> _itemDistributionControl = new Mock<IItemDistributionControl>();
        private readonly Mock<IItemControl> _itemControl = new Mock<IItemControl>();
        private readonly Mock<IItemCategoryControl> _itemCategoryControl = new Mock<IItemCategoryControl>();
        private readonly Mock<IPropertyControl> _propertyControl = new Mock<IPropertyControl>();
        private readonly Mock<IClientProfileControl> _clientProfileControl = new Mock<IClientProfileControl>();
        private readonly Mock<IAdminControl> _adminControl = new Mock<IAdminControl>();
        private readonly Mock<IOrderControl> _orderControl = new Mock<IOrderControl>();
        // admin data
        private readonly AdminController adminController;

        
        public AdminControllerUnitTest()
        {
            // mock the admin controller
            adminController = new AdminController(_itemDistributionControl.Object,
                _itemControl.Object,
                _itemCategoryControl.Object,
                _propertyControl.Object,
                _clientProfileControl.Object,
                _adminControl.Object,
                _orderControl.Object);
        }


        [TestMethod]
        public void Index_Admin_Empty()
        {
            var result = adminController.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model); // Additional check for the model itself
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }

        [TestMethod]
        public void Index_Admin_List_Type()
        {
            var result = adminController.Index();
            var model = ((ViewResult)result).Model as IEnumerable<Item>;

            // Assert
            Assert.IsNotNull(model, "Model is not the type of List<IEnumerable<Item>>");
            // second check, does the same
            Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(IEnumerable<Item>));
            
        }

        [TestMethod]
        public void Login_Admin_Empty()
        {
            var result = adminController.Login() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model); // Additional check for the model itself
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Login");
        }

        //[TestMethod]
        //public void LoginWithParams()
        //{
        //    Admin admin = new Admin();
        //    admin.Id = 999;
        //    admin.Login = "testAdmin";
        //    admin.Password = "randomPassword";
        //    string emptyUrl = "";
        //    ViewResult objViewResult = adminController.Login(admin, emptyUrl) as ViewResult;
        //    Assert.AreEqual("Index", objViewResult.ViewName);
        //}
    }
}
