using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MartinMeli_EP.Controllers
{
    public class CategoryController : Controller
    {
        public static string category = "n/a";

        Logic.ArticleLogic al = new Logic.ArticleLogic();
        //
        // GET: /Category/

        public ActionResult National()
        {
            category = "National";
            ViewBag.articles = al.Get5MostRecentArticles(1);
            return View();
        }

        public ActionResult Overseas()
        {
            category = "Overseas";
            ViewBag.articles = al.Get5MostRecentArticles(2);
            return View();
        }

        public ActionResult Sports()
        {
            category = "Sports";
            ViewBag.articles = al.Get5MostRecentArticles(3);
            return View();
        }

        public ActionResult Odd()
        {
            category = "Odd";
            ViewBag.articles = al.Get5MostRecentArticles(4);
            return View();
        }
        public ActionResult Travel()
        {
            category = "Travel";
            ViewBag.articles = al.Get5MostRecentArticles(5);
            return View();
        }

        public ActionResult Opinion()
        {
            category = "Opinion";
            ViewBag.articles = al.Get5MostRecentArticles(6);
            return View();
        }
    }
}
