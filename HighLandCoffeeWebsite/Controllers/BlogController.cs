using HighLandCoffeeWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HighLandCoffeeWebsite.Controllers
{
    public class BlogController : Controller
    {
        CoffeeDataContext db = new CoffeeDataContext();
        // GET: Blog
        public ActionResult ViewBlog()
        {
            var n = db.News.ToList();

            return View(n);
        }
    }
}