using System.Web.Mvc;
using Antlr.Runtime.Misc;
using InternetShop.WebUI.HtmlHelpers;
using InternetShop.WebUI.Models;
using NUnit.Framework;

namespace InternetShop.WebUI.Tests
{
    [TestFixture]
    public class HelpersTester
    {
        [Test]
        public void PageLinksGenereteTest()
        {
            HtmlHelper helper = null;
            Func<int, string> pageUrl = i => "Page" + i;
            var pagingInfo = new PagingInfo(2, 10, 28);

            var result = helper.PageLinks(pagingInfo, pageUrl);

            Assert.That(result.ToString(), Is.EqualTo(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                                                      + @"<a class=""btn btn-primary selected"" href=""Page2"">2</a>"
                                                      + @"<a class=""btn btn-default"" href=""Page3"">3</a>"));
        }
    }
}