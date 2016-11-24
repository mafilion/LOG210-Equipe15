using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryManagement;
using LibraryManagement.Controllers;
using LibraryManagement.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace LibraryManagement.Tests.Controllers
{
    [TestClass]
    public class ManagersControllerTest
    {
        static IWebDriver driverGC;
        ManagersController controller = new ManagersController();

        [AssemblyInitialize]
        public static void SetUp(TestContext context)
        {
            driverGC = new ChromeDriver();
            driverGC.Navigate().GoToUrl("http://localhost:50682/");
        }

        [TestMethod]
        public void openInscriptionGestionnaire()
        {
            driverGC.FindElement(By.Id("newAccounts")).Click();

            var listInscription = driverGC.FindElement(By.Id("newAccounts"));
            //create select element object 
            SelectElement selectElement = new SelectElement(listInscription);

            //select by value
            selectElement.SelectByValue("Managers");
            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Toto");
        }
    }
}
