using System.Collections.Generic;
using System.Linq;
using InternetShop.DataLayer;
using InternetShop.DataLayer.Entities;
using NUnit.Framework;

namespace InternetShop.WebUI.Tests
{
    [TestFixture]
    public class CartTester
    {
        private Cart cart;
        private IEnumerable<Product> testData;

        [SetUp]
        public void SetupTests()
        {
            cart = new Cart();
            testData = Enumerable.Range(1, 20).Select(i => new Audio {ProductId = i, Price = 10});
        }

        [Test]
        public void AddNewItemToCartTest()
        {
            foreach (var product in testData)
            {
                cart.AddItem(product, 1);
            }

            Assert.That(cart.ProductsInCart.Count, Is.EqualTo(20));
            CollectionAssert.AreEqual(cart.ProductsInCart.Select(t => t.Item1), testData);
        }

        [Test]
        public void AddExistingItemToCartTest()
        {
            cart.AddItem(testData.ToArray()[0], 4);

            cart.AddItem(testData.ToArray()[0], 4);

            Assert.That(cart.ProductsInCart.Count, Is.EqualTo(1));
            Assert.That(cart.ProductsInCart.ToList()[0].Item2, Is.EqualTo(8));
        }

        [Test]
        public void RemoveItemFromCartTest()
        {
            var removingProduct = testData.ToArray()[5];
            foreach (var product in testData)
            {
                cart.AddItem(product, 2);
            }

            cart.RemoveItem(removingProduct);

            Assert.That(cart.ProductsInCart.Count, Is.EqualTo(19));
            Assert.False(cart.ProductsInCart.Any(t => t.Item1.Equals(removingProduct)));
        }

        [Test]
        public void ComputeTotalCountTest()
        {
            foreach (var product in testData)
            {
                cart.AddItem(product, 2);
            }

            Assert.That(cart.ProductsInCart.Count, Is.EqualTo(20));
            Assert.That(cart.ComputeTotalValue(), Is.EqualTo(400));
        }

        [Test]
        public void ClearCartTest()
        {
            foreach (var product in testData)
            {
                cart.AddItem(product, 10);
            }

            cart.Clear();

            Assert.That(cart.ProductsInCart.Count, Is.EqualTo(0));
        }
    }
}