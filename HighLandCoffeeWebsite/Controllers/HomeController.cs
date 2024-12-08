using HighLandCoffeeWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HighLandCoffeeWebsite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        CoffeeDataContext db = new CoffeeDataContext();

        public ActionResult Index()
        {
            var product = db.Products.ToList();

            return View(product);
        }
    }
}