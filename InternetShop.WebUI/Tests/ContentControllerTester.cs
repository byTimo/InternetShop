using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InternetShop.DataLayer.Abstract;
using InternetShop.DataLayer.Entities;
using InternetShop.WebUI.Controllers;
using InternetShop.WebUI.Models;
using Moq;
using NUnit.Framework;

namespace InternetShop.WebUI.Tests
{
    [TestFixture]
    public class ContentControllerTester
    {
        [Test]
        public void ListFirstPageTest()
        {
            var testData = Enumerable.Repeat(new Audio {ProductId = 1}, 20).ToList();
            var mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(testData);
            var controller = new ContentController(mock.Object);

            var pageModel = (ProductViewModel)controller.List().Model;

            Assert.That(pageModel.Products.Count(), Is.EqualTo(15));
            CollectionAssert.AreEqual(testData.Take(15), pageModel.Products);
            Assert.That(pageModel.PagingInfo.CurrentPage, Is.EqualTo(1));
        }

        [Test]
        public void ListSecondPageTest()
        {
            var testData = Enumerable.Repeat(new Audio(), 20).ToList();
            var mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(testData);
            var controller = new ContentController(mock.Object);

            var pageModel = ((ProductViewModel) controller.List(2).Model);

            Assert.That(pageModel.Products.Count(), Is.EqualTo(5));
            CollectionAssert.AreEqual(testData.Skip(15).ToList(), pageModel.Products);
            Assert.That(pageModel.PagingInfo.CurrentPage, Is.EqualTo(2));
        }
    }
}