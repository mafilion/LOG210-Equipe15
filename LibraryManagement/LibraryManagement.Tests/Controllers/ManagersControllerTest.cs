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
        static IWebDriver driverGC;
        ManagersController controller = new ManagersController();
        static LibraryManagementEntities db = new LibraryManagementEntities();

        [AssemblyInitialize]
        public static void SetUp(TestContext context)
        {
            driverGC = new ChromeDriver();
            enterManagersPage();
        }

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
            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("Toto");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("123");
            driverGC.FindElement(By.Id("cooperative_Name")).SendKeys("toto");
            driverGC.FindElement(By.Id("cooperative_NoStreet")).SendKeys("22");
            driverGC.FindElement(By.Id("cooperative_Street")).SendKeys("de la montagne");
            driverGC.FindElement(By.Id("cooperative_City")).SendKeys("totoville");
            driverGC.FindElement(By.Id("cooperative_PostalCode")).SendKeys("J8H 1T0");

            driverGC.FindElement(By.Id("save")).Click();
            driverGC.SwitchTo().Alert().Accept();

            var obj = db.Manager.SingleOrDefault(x => x.FirstName == "Test");

            Assert.IsNotNull(obj);

            driverGC.Navigate().GoToUrl("http://localhost:50682/Accounts/LogOff");
        }
        [TestMethod]
        public void sameEmailError()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("toto");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("123");
            driverGC.FindElement(By.Id("cooperative_Name")).SendKeys("toto");
            driverGC.FindElement(By.Id("cooperative_NoStreet")).SendKeys("22");
            driverGC.FindElement(By.Id("cooperative_Street")).SendKeys("de la montagne");
            driverGC.FindElement(By.Id("cooperative_City")).SendKeys("totoville");
            driverGC.FindElement(By.Id("cooperative_PostalCode")).SendKeys("J8H 1T0");

            driverGC.FindElement(By.Id("save")).Click();
            driverGC.SwitchTo().Alert().Accept();

            var obj2 = db.Manager.SingleOrDefault(x => x.FirstName == "Test2");
            var obj = db.Manager.SingleOrDefault(x => x.FirstName == "Test");
            Assert.IsNull(obj2);

            db.Manager.Remove(obj);
            db.SaveChanges();

        }

        [TestMethod]
        public void emptyFirstName()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("Toto");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("123");
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
        public void emptyLastName()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("123");
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
        public void invalidEmail()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("toto");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("testtest.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("123");
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
        public void emptyPass1()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("toto");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("");
            driverGC.FindElement(By.Id("password2")).SendKeys("123");
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
        public void emptyPass2()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("toto");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("");
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
        public void notSamePass()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("toto");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test@test.com");
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
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("toto");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test@test.com");
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
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("toto");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("tes@ttest.com");
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
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("toto");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test@test.com");
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
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("toto");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test@test.com");
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
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("toto");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("123");
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

        [TestMethod]
        public void newCoop()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("toto");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("123");
            driverGC.FindElement(By.Id("cooperative_Name")).SendKeys("totoCoop");
            driverGC.FindElement(By.Id("cooperative_NoStreet")).SendKeys("22");
            driverGC.FindElement(By.Id("cooperative_Street")).SendKeys("de la montagne");
            driverGC.FindElement(By.Id("cooperative_City")).SendKeys("totoville");
            driverGC.FindElement(By.Id("cooperative_PostalCode")).SendKeys("J8H 1T0");

            driverGC.FindElement(By.Id("save")).Click();
            driverGC.SwitchTo().Alert().Accept();

            var obj = db.Cooperative.SingleOrDefault(x => x.Name == "totoCoop");

            Assert.IsNotNull(obj);

            var obj1 = db.Manager.SingleOrDefault(x => x.FirstName == "Test");
            db.Manager.Remove(obj1);
            db.SaveChanges();
        }

        [TestMethod]
        public void updateCoop()
        {
            enterManagersPage();

            driverGC.FindElement(By.Id("manager_FirstName")).SendKeys("Test");
            driverGC.FindElement(By.Id("manager_LastName")).SendKeys("toto");
            driverGC.FindElement(By.Id("manager_Email")).SendKeys("test@test.com");
            driverGC.FindElement(By.Id("password1")).SendKeys("123");
            driverGC.FindElement(By.Id("password2")).SendKeys("123");
            driverGC.FindElement(By.Id("cooperative_Name")).SendKeys("totoCoop");
            driverGC.FindElement(By.Id("cooperative_NoStreet")).SendKeys("22");
            driverGC.FindElement(By.Id("cooperative_Street")).SendKeys("de la montagne");
            driverGC.FindElement(By.Id("cooperative_City")).SendKeys("totovilleTest");
            driverGC.FindElement(By.Id("cooperative_PostalCode")).SendKeys("J8H 1T0");

            driverGC.FindElement(By.Id("save")).Click();
            driverGC.SwitchTo().Alert().Accept();

            var obj = db.Cooperative.SingleOrDefault(x => x.Name == "totoCoop" && x.City == "totovilleTest");

            Assert.IsNotNull(obj);
            var obj1 = db.Manager.SingleOrDefault(x => x.FirstName == "Test");
            db.Manager.Remove(obj1);
            db.Cooperative.Remove(obj);
            db.SaveChanges();


        }

    }
}
