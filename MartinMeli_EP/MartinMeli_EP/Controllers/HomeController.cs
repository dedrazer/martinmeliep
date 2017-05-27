using Logic;
using MartinMeli_EP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MartinMeli_EP.Controllers
{
    public class HomeController : Controller
    {
        ArticleLogic al = new ArticleLogic();

        public ActionResult Index()
        {
            var a = al.GetBreakingNews();
            ArticleModel am = new ArticleModel() { Headline = a.headline, CategoryID = a.categoryId, Description = a.description, AuthorID = a.authorId, ImageURL = a.imageURL };
            ViewData["breakingNews"] = am;

            ViewBag.Recent = al.Get5MostRecentArticles();
            ViewBag.RecentNational = al.Get5MostRecentArticles(0);
            ViewBag.RecentOverseas = al.Get5MostRecentArticles(1);
            ViewBag.RecentSports = al.Get5MostRecentArticles(2);
            ViewBag.RecentOpinion = al.Get5MostRecentArticles(3);
            ViewBag.RecentTravel = al.Get5MostRecentArticles(4);
            ViewBag.RecentOdd = al.Get5MostRecentArticles(5);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
