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
            driverGC = new FirefoxDriver();
        }


        [TestMethod]
        public void TestChromeDriver()
        {
            driverGC.Navigate().GoToUrl("http://www.google.com");
            driverGC.FindElement(By.Id("lst-ib")).SendKeys("Selenium");
            driverGC.FindElement(By.Id("lst-ib")).SendKeys(Keys.Enter);
        }
    }
}
