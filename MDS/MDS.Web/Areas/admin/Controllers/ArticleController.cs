using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MDS.Web.Areas.admin.Controllers
{
    public class ArticleController : Controller
    {
        //
        // GET: /admin/Article/

        Blog.ArticleService articleService = new Blog.ArticleService();

        public ActionResult List(int pageIndex=1)
        {
            var list = articleService.GetModelList("", "", pageIndex, 20);
            return View(list);
        }

        public ActionResult Add()
        {
            ViewData.ModelMetadata=ModelMetadataProviders.Current.GetMetadataForType(null,typeof(Blog.Article));
            return View();
        }

        [HttpPost]
        public ActionResult Add(Blog.Article article)
        {
            try
            {
                 articleService.Add(article);
                 return RedirectToAction("List");
            }
            catch (Exception err)
            {
                ModelState.AddModelError("",err.Message);
            }
            return View(article);
        }

        public ActionResult Edit(int Id)
        {
            var art = articleService.GetModel("ID="+Id);
            if (art == null) return RedirectToAction("Error");
            return View(art);
        }

        [HttpPost]
        public ActionResult Edit(Blog.Article article)
        {
            try
            {
                articleService.Update(article);
                return RedirectToAction("List");
            }
            catch (Exception err)
            {
                ModelState.AddModelError("", err.Message);
            }
            return View(article);
        }


        public ActionResult Delete(int Id)
        {
            var art = articleService.GetModel("ID=" + Id);
            if (art == null) return RedirectToAction("Error");
            return View(art);
        }

        [HttpPost]
        public ActionResult Delete(Blog.Article article)
        {
            var art = articleService.GetModel("ID=" + article.ID);
            if (art == null) return RedirectToAction("Error");
            try
            {
                articleService.Delete(art);
                return RedirectToAction("List");
            }
            catch (Exception err)
            {
                ModelState.AddModelError("", err.Message);
            }
            return View(art);
        }
        

    }
}
