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

namespace UnitTestProject.ControllersTest
{
    [TestClass]
    public class AdminControllerUnitTest
    {
         //need adminController here 

        [TestMethod]
        public void IndexChecker()
        {
            ViewResult objViewResult = adminController.Index() as ViewResult;
            Assert.AreEqual("Index", objViewResult.ViewName);
        }

        [TestMethod]
        public void Login()
        {
            ViewResult objViewResult = adminController.Login() as ViewResult;
            Assert.AreEqual("", objViewResult.ViewName);
        }

        [TestMethod]
        public void LoginWithParams()
        {
            Admin admin = new Admin();
            admin.Id = 999;
            admin.Login = "testAdmin";
            admin.Password = "randomPassword";
            string emptyUrl = "";
            ViewResult objViewResult = adminController.Login(admin, emptyUrl) as ViewResult;
            Assert.AreEqual("Index", objViewResult.ViewName);
        }
    }
}
