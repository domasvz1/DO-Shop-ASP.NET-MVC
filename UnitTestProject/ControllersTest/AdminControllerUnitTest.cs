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

namespace UnitTestProject.ControllersTest
{
    [TestClass]
    public class AdminControllerUnitTest
    {
        private readonly IItemDistributionControl _itemDistributionControl;
        private readonly IItemControl _itemControl;
        private readonly IItemCategoryControl _itemCategoryControl;
        private readonly IPropertyControl _propertyControl;
        private readonly IClientProfileControl _clientProfileControl;
        private readonly IAdminControl _adminControl;
        private readonly IOrderControl _orderControl;

        [TestMethod]
        public void Index()
        {
            AdminController adminController = new AdminController(_itemDistributionControl, _itemControl,
               _itemCategoryControl, _propertyControl, _clientProfileControl, _adminControl, _orderControl);

            var sth = adminController.Index() as ViewResult;

            ViewResult objViewResult = adminController.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Index", objViewResult.ViewName);
        }
    }
}
