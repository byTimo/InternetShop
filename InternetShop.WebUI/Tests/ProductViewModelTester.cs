using InternetShop.DataLayer.Entities;
using InternetShop.WebUI.Models;
using NUnit.Framework;

namespace InternetShop.WebUI.Tests
{
    [TestFixture]
    public class ProductViewModelTester
    {
        private Audio audioTestProduct;
        private Video videoTestProduct;

        [SetUp]
        public void SetupTests()
        {
            audioTestProduct = new Audio
            {
                ProductId = 4,
                Name = "Audio product",
                Description = "This is audio product",
                Year = 2016,
                Price = 30000,
                MusicalDirection = "Hip-hop",
                Perfomer = "byTimo"
            };
            videoTestProduct = new Video
            {
                ProductId = 16,
                Name = "Video product",
                Description = "This is video product",
                Year = 1990,
                Price = 100,
                Director = "byTimo",
                Genre = "Action"
            };
        }

        [Test]
        public void ProductViewModelCreateTest()
        {
            var audioViewModel = new ProductViewModel(audioTestProduct);
            var videoViewModel = new ProductViewModel(videoTestProduct);

            AssertThatViewModelFromAudio(audioTestProduct, audioViewModel);
            AssertThatViewModelFromVideo(videoTestProduct, videoViewModel);
        }

        private void AssertThatViewModelFromAudio(Audio audio, ProductViewModel viewModel)
        {
            Assert.That(viewModel.Type, Is.EqualTo(ProductType.Audio));
            AssertThatEqualCommonFields(audio, viewModel);
            Assert.AreEqual(audio.MusicalDirection, viewModel.MusicalDirection);
            Assert.AreEqual(audio.Perfomer, viewModel.Perfomer);
        }

        private void AssertThatEqualCommonFields(Product product, ProductViewModel viewModel)
        {
            Assert.AreEqual(product.ProductId, viewModel.ProductId);
            Assert.AreEqual(product.Name, viewModel.Name);
            Assert.AreEqual(product.Description, viewModel.Description);
            Assert.AreEqual(product.Year, viewModel.Year);
            Assert.AreEqual(product.Price, viewModel.Price);
        }

        private void AssertThatViewModelFromVideo(Video video, ProductViewModel viewModel)
        {
            Assert.That(viewModel.Type, Is.EqualTo(ProductType.Video));
            AssertThatEqualCommonFields(video, viewModel);
            Assert.AreEqual(video.Director, viewModel.Director);
            Assert.AreEqual(video.Genre, viewModel.Genre);
        }

        [Test]
        public void ProductViewModelEditTest()
        {
            var audioViewModel = new ProductViewModel(audioTestProduct);
            var videoViewModel = new ProductViewModel(videoTestProduct);

            audioViewModel.Perfomer = "BYtIMO";
            audioViewModel.Price = 300;
            videoViewModel.Director = "BYtIMO";
            videoViewModel.Description = "This is changed model";

            AssertThatViewModelFromAudio(audioViewModel.ToProduct() as Audio, audioViewModel);
            AssertThatViewModelFromVideo(videoViewModel.ToProduct() as Video, videoViewModel);
        }
    }
}