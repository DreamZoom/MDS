using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDS.Web.Controllers
{
    public class HomeController : Controller
    {
        MDS.Blog.ArticleService articleService = new Blog.ArticleService();
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            ViewBag.Articles = articleService.GetModelList("ID >0","ID Desc");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

      
    }
}
