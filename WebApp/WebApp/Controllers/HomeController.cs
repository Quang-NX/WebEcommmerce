using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        EcommerceDbContext db = new EcommerceDbContext();
        public ActionResult Index()
        {
            var productList = db.Products.OrderByDescending(p=>p.CreatedDate).Take(5);
            ViewBag.ProductList = new List<Product>(db.Products.OrderBy(p => p.CreatedDate).Take(5));
            return View(productList);
        }
        [HttpGet]
        public JsonResult GetProduct()
        {
            var productList = db.Products.OrderBy(p => p.CreatedDate).Take(5);
            return Json(productList,JsonRequestBehavior.AllowGet);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}