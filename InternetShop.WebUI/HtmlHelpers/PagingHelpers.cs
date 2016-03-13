using System.Text;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using InternetShop.WebUI.Models;

namespace InternetShop.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper helper, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            var resultBuilder = new StringBuilder();
            for (var i = 1; i <= pagingInfo.TotalPage; i++)
            {
                var tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn btn-primary");
                }
                else
                {
                    tag.AddCssClass("btn btn-default");
                }
                resultBuilder.Append(tag);
            }
            return MvcHtmlString.Create(resultBuilder.ToString());
        }
    }
}