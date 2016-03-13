using System.Collections.Generic;
using System.Web.Mvc;
using InternetShop.DataLayer.Entities;

namespace InternetShop.WebUI.Controllers
{
    public class ContentController : Controller
    {
        public int pageSize = 15;

        public ContentController()
        {
        }

        public ActionResult List(int page = 1)
        {
            return View();
        }
    }
}