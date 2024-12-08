using HighLandCoffeeWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace HighLandCoffeeWebsite.Controllers
{
    public class StoreController : Controller
    {

        private CoffeeDataContext db = new CoffeeDataContext();
        public ActionResult Index()
        {
            var stores = db.Stores.ToList();
            return View(stores);
        }
    }
}