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
using OpenQA.Selenium.Support.UI;

namespace LibraryManagement.Tests.Controllers
{
    [TestClass]
    public class ManagersControllerTest
    {
        static IWebDriver driverGC = new ChromeDriver();
        static LibraryManagementEntities db = new LibraryManagementEntities();

        public static void enterManagersPage()
        {
            driverGC.Navigate().GoToUrl("http://localhost:50682/Accounts/LogOff");
            driverGC.FindElement(By.Id("newAccounts")).Click();

            var listInscription = driverGC.FindElement(By.Id("newAccounts"));
            //create select element object 
            SelectElement selectElement = new SelectElement(listInscription);

            //select by value
            selectElement.SelectByValue("Managers");
        }


        [TestMethod]
        public void insertGoodManagers()
        {
            enterManagersPage();
            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("123");
            driverGC.FindElement(By.Id("cooperative_Name")).SendKeys("toto");
            driverGC.FindElement(By.Id("cooperative_NoStreet")).SendKeys("22");
            driverGC.FindElement(By.Id("cooperative_Street")).SendKeys("de la montagne");
            driverGC.FindElement(By.Id("cooperative_City")).SendKeys("totoville");
            driverGC.FindElement(By.Id("cooperative_PostalCode")).SendKeys("J8H 2L9");

            driverGC.FindElement(By.Id("save")).Click();
            driverGC.SwitchTo().Alert().Accept();

            var obj = db.Manager.SingleOrDefault(x => x.FirstName == "Test");

            Assert.IsNotNull(obj);
            db.Manager.Remove(obj);
            db.SaveChanges();

            driverGC.Navigate().GoToUrl("http://localhost:50682/Accounts/LogOff");
        }


        [TestMethod]
        public void emptyFirstName()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test3@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("123");
            driverGC.FindElement(By.Id("cooperative_Name")).SendKeys("toto");
            driverGC.FindElement(By.Id("cooperative_NoStreet")).SendKeys("22");
            driverGC.FindElement(By.Id("cooperative_Street")).SendKeys("de la montagne");
            driverGC.FindElement(By.Id("cooperative_City")).SendKeys("totoville");
            driverGC.FindElement(By.Id("cooperative_PostalCode")).SendKeys("J8H 2L9");

            driverGC.FindElement(By.Id("save")).Click();
            driverGC.SwitchTo().Alert().Accept();

            var obj = db.Manager.SingleOrDefault(x => x.FirstName == "Test");

            Assert.IsNull(obj);

        }

        [TestMethod]
        public void emptyLastName()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test4@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("123");
            driverGC.FindElement(By.Id("cooperative_Name")).SendKeys("toto");
            driverGC.FindElement(By.Id("cooperative_NoStreet")).SendKeys("22");
            driverGC.FindElement(By.Id("cooperative_Street")).SendKeys("de la montagne");
            driverGC.FindElement(By.Id("cooperative_City")).SendKeys("totoville");
            driverGC.FindElement(By.Id("cooperative_PostalCode")).SendKeys("J8H 2L9");

            driverGC.FindElement(By.Id("save")).Click();
            driverGC.SwitchTo().Alert().Accept();

            var obj = db.Manager.SingleOrDefault(x => x.FirstName == "Test");

            Assert.IsNull(obj);

        }

        [TestMethod]
        public void invalidEmail()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("testtest.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("123");
            driverGC.FindElement(By.Id("cooperative_Name")).SendKeys("toto");
            driverGC.FindElement(By.Id("cooperative_NoStreet")).SendKeys("22");
            driverGC.FindElement(By.Id("cooperative_Street")).SendKeys("de la montagne");
            driverGC.FindElement(By.Id("cooperative_City")).SendKeys("totoville");
            driverGC.FindElement(By.Id("cooperative_PostalCode")).SendKeys("J8H 2L9");

            driverGC.FindElement(By.Id("save")).Click();
            driverGC.SwitchTo().Alert().Accept();

            var obj = db.Manager.SingleOrDefault(x => x.FirstName == "Test");

            Assert.IsNull(obj);

        }

        [TestMethod]
        public void emptyPass1()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test5@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("");
            driverGC.FindElement(By.Id("password2")).SendKeys("123");
            driverGC.FindElement(By.Id("cooperative_Name")).SendKeys("toto");
            driverGC.FindElement(By.Id("cooperative_NoStreet")).SendKeys("22");
            driverGC.FindElement(By.Id("cooperative_Street")).SendKeys("de la montagne");
            driverGC.FindElement(By.Id("cooperative_City")).SendKeys("totoville");
            driverGC.FindElement(By.Id("cooperative_PostalCode")).SendKeys("J8H 2L9");

            driverGC.FindElement(By.Id("save")).Click();
            driverGC.SwitchTo().Alert().Accept();

            var obj = db.Manager.SingleOrDefault(x => x.FirstName == "Test");

            Assert.IsNull(obj);

        }

        [TestMethod]
        public void emptyPass2()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test6@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("");
            driverGC.FindElement(By.Id("cooperative_Name")).SendKeys("toto");
            driverGC.FindElement(By.Id("cooperative_NoStreet")).SendKeys("22");
            driverGC.FindElement(By.Id("cooperative_Street")).SendKeys("de la montagne");
            driverGC.FindElement(By.Id("cooperative_City")).SendKeys("totoville");
            driverGC.FindElement(By.Id("cooperative_PostalCode")).SendKeys("J8H 2L9");

            driverGC.FindElement(By.Id("save")).Click();
            driverGC.SwitchTo().Alert().Accept();

            var obj = db.Manager.SingleOrDefault(x => x.FirstName == "Test");

            Assert.IsNull(obj);

        }

        [TestMethod]
        public void notSamePass()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test7@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("22");
            driverGC.FindElement(By.Id("cooperative_Name")).SendKeys("toto");
            driverGC.FindElement(By.Id("cooperative_NoStreet")).SendKeys("22");
            driverGC.FindElement(By.Id("cooperative_Street")).SendKeys("de la montagne");
            driverGC.FindElement(By.Id("cooperative_City")).SendKeys("totoville");
            driverGC.FindElement(By.Id("cooperative_PostalCode")).SendKeys("J8H 1T0");

            driverGC.FindElement(By.Id("save")).Click();
            driverGC.SwitchTo().Alert().Accept();

            var obj = db.Manager.SingleOrDefault(x => x.FirstName == "Test");

            Assert.IsNull(obj);

        }


        [TestMethod]
        public void emptyCoopName()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test8@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("123");
            driverGC.FindElement(By.Id("cooperative_Name")).SendKeys("");
            driverGC.FindElement(By.Id("cooperative_NoStreet")).SendKeys("22");
            driverGC.FindElement(By.Id("cooperative_Street")).SendKeys("de la montagne");
            driverGC.FindElement(By.Id("cooperative_City")).SendKeys("totoville");
            driverGC.FindElement(By.Id("cooperative_PostalCode")).SendKeys("J8H 1T0");

            driverGC.FindElement(By.Id("save")).Click();
            driverGC.SwitchTo().Alert().Accept();

            var obj = db.Manager.SingleOrDefault(x => x.FirstName == "Test");

            Assert.IsNull(obj);

        }

        [TestMethod]
        public void emptyNoStreet()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test9@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("123");
            driverGC.FindElement(By.Id("cooperative_Name")).SendKeys("toto");
            driverGC.FindElement(By.Id("cooperative_NoStreet")).SendKeys("");
            driverGC.FindElement(By.Id("cooperative_Street")).SendKeys("de la montagne");
            driverGC.FindElement(By.Id("cooperative_City")).SendKeys("totoville");
            driverGC.FindElement(By.Id("cooperative_PostalCode")).SendKeys("J8H 1T0");

            driverGC.FindElement(By.Id("save")).Click();
            driverGC.SwitchTo().Alert().Accept();

            var obj = db.Manager.SingleOrDefault(x => x.FirstName == "Test");

            Assert.IsNull(obj);

        }

        [TestMethod]
        public void emptyStreet()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test10@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("123");
            driverGC.FindElement(By.Id("cooperative_Name")).SendKeys("toto");
            driverGC.FindElement(By.Id("cooperative_NoStreet")).SendKeys("22");
            driverGC.FindElement(By.Id("cooperative_Street")).SendKeys("");
            driverGC.FindElement(By.Id("cooperative_City")).SendKeys("totoville");
            driverGC.FindElement(By.Id("cooperative_PostalCode")).SendKeys("J8H 1T0");

            driverGC.FindElement(By.Id("save")).Click();
            driverGC.SwitchTo().Alert().Accept();

            var obj = db.Manager.SingleOrDefault(x => x.FirstName == "Test");

            Assert.IsNull(obj);

        }

        [TestMethod]
        public void emptyCity()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test11@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("123");
            driverGC.FindElement(By.Id("cooperative_Name")).SendKeys("toto");
            driverGC.FindElement(By.Id("cooperative_NoStreet")).SendKeys("22");
            driverGC.FindElement(By.Id("cooperative_Street")).SendKeys("de la montagne");
            driverGC.FindElement(By.Id("cooperative_City")).SendKeys("");
            driverGC.FindElement(By.Id("cooperative_PostalCode")).SendKeys("J8H 1T0");

            driverGC.FindElement(By.Id("save")).Click();
            driverGC.SwitchTo().Alert().Accept();

            var obj = db.Manager.SingleOrDefault(x => x.FirstName == "Test");

            Assert.IsNull(obj);

        }

        [TestMethod]
        public void invalidPostalCode()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test12@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("22");
            driverGC.FindElement(By.Id("cooperative_Name")).SendKeys("toto");
            driverGC.FindElement(By.Id("cooperative_NoStreet")).SendKeys("22");
            driverGC.FindElement(By.Id("cooperative_Street")).SendKeys("de la montagne");
            driverGC.FindElement(By.Id("cooperative_City")).SendKeys("totoville");
            driverGC.FindElement(By.Id("cooperative_PostalCode")).SendKeys("sfdsfds");

            driverGC.FindElement(By.Id("save")).Click();
            driverGC.SwitchTo().Alert().Accept();

            var obj = db.Manager.SingleOrDefault(x => x.FirstName == "Test");

            Assert.IsNull(obj);

        }
    }
}
