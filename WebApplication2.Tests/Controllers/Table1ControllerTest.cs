using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// required references
using System.Web.Mvc;
using WebApplication2.Controllers;
using Moq;
using WebApplication2.Models;
using System.Linq;

namespace WebApplication2.Tests.Controllers
{

    [TestClass]
    public class Table1ControllerTest
    {
        //globals
        Table1Controller controller;
        Mock<Table1Repository> mock;
        List<Table1> table1;

        [TestInitialize]
        public void TestInitialize()
        {
            // arrange
            mock = new Mock<Table1Repository>();

            // mock table1 data
            table1 = new List<Table1>
            {
                new Table1 {Price = 1, Songs = "Hello", Album = "Adrene", Movie ="Super"}
            };


            //add Table1 data to the mock object
            mock.Setup(m => m.Table1).Returns(table1.AsQueryable());
            // pass the mock to the controller
            controller = new Table1Controller(mock.Object);
        }
        [TestMethod]
        public void IndexLoadsValid()
        {


            //act
            ViewResult result = controller.Index() as ViewResult;

            //assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexShowsValidTable1()
        {
            //act
            var actual = (List<Table1>)controller.Index().Model;

            //assert
            CollectionAssert.AreEqual(table1, actual);
        }

        [TestMethod]
        public void DetailsValidTable1()
        {
            //act 
            var actual = (Table1)controller.Details(1).Model;

            //assert
            Assert.AreEqual(table1.ToList()[0], actual);

        }

        [TestMethod]
        public void DetailsInvalidId()
        {
            //act 
            ViewResult actual = controller.Details(4);

            //assert
            Assert.AreEqual("Error", actual.ViewName);

        }
        [TestMethod]
        public void DetailsInvalidNoId()
        {
            //act 
            ViewResult actual = controller.Details(null);

            //assert
            Assert.AreEqual("Error", actual.ViewName);

        }
        [TestMethod]
        public void DeleteConfirmedNoId()
        {
            //act
            ViewResult actual = controller.DeleteConfirmed(null);

            //assert
            Assert.AreEqual("Error", actual.ViewName);

        }
        [TestMethod]
        public void DeleteConfirmedInvalidId()
        {

            //act
            ViewResult actual = controller.DeleteConfirmed(4);

            //assert
            Assert.AreEqual("Error", actual.ViewName);


        }
        [TestMethod]
        public void DeleteConfirmedValidId()
        {

            //act
            ViewResult actual = controller.DeleteConfirmed(1);

            //assert
            Assert.AreEqual("Index", actual.ViewName);


        }
    }
}
