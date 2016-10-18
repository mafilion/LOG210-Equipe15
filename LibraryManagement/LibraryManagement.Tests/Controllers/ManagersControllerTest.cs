using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryManagement;
using LibraryManagement.Controllers;
using LibraryManagement.Models;

namespace LibraryManagement.Tests.Controllers
{
    [TestClass]
    public class ManagersControllerTest
    {
        ManagersController controller = new ManagersController();

        [TestMethod]
        public void NullEmail()
        {
            ManagersCooperativesViewModels vm = new ManagersCooperativesViewModels();
            vm.manager.FirstName = "toto";
            vm.manager.LastName = "toto";
            vm.cooperative.Name = "totoCoop";
            vm.cooperative.NoStreet = "22";
            vm.cooperative.PostalCode = "J8H 2L9";
            vm.cooperative.Street = "toto";

            // Assert
            Assert.AreEqual(false, controller.validForm(vm));
        }


        [TestMethod]
        public void NullEmail2()
        {
            ManagersCooperativesViewModels vm = new ManagersCooperativesViewModels();
            vm.manager.FirstName = "toto";
            vm.manager.LastName = "toto";
            vm.manager.Email = "";
            vm.cooperative.Name = "totoCoop";
            vm.cooperative.NoStreet = "22";
            vm.cooperative.PostalCode = "J8H 2L9";
            vm.cooperative.Street = "toto";


            // Assert
            Assert.AreEqual(false, controller.validForm(vm));
        }

        [TestMethod]
        public void WrongFormatEmail()
        {
            ManagersCooperativesViewModels vm = new ManagersCooperativesViewModels();
            vm.manager.FirstName = "toto";
            vm.manager.LastName = "toto";
            vm.manager.Email = "test";
            vm.cooperative.Name = "totoCoop";
            vm.cooperative.NoStreet = "22";
            vm.cooperative.PostalCode = "J8H 2L9";
            vm.cooperative.Street = "toto";


            // Assert
            Assert.AreEqual(false, controller.validForm(vm));
        }


        [TestMethod]
        public void AlreadyExistingEmail()
        {
            ManagersCooperativesViewModels vm = new ManagersCooperativesViewModels();
            vm.manager.FirstName = "toto";
            vm.manager.LastName = "toto";
            vm.manager.Email = "test@test.com";
            vm.cooperative.Name = "totoCoop";
            vm.cooperative.NoStreet = "22";
            vm.cooperative.PostalCode = "J8H 2L9";
            vm.cooperative.Street = "toto";


            // Assert
            Assert.AreEqual(false, controller.validForm(vm));
        }

        [TestMethod]
        public void ValidData()
        {
            ManagersCooperativesViewModels vm = new ManagersCooperativesViewModels();
            vm.manager.FirstName = "toto";
            vm.manager.LastName = "toto";
            vm.manager.Email = "gabriel.dery20@test.com";
            vm.cooperative.Name = "totoCoop";
            vm.cooperative.NoStreet = "22";
            vm.cooperative.PostalCode = "J8H 2L9";
            vm.cooperative.Street = "toto";


            // Assert
            Assert.AreEqual(true, controller.validForm(vm));
        }

    }
}
