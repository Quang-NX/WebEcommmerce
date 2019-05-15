using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Areas.Admin.Controllers
{
    public class StatisticController : Controller
    {
        private EcommerceDbContext db = new EcommerceDbContext();
        // GET: Admin/Statistic
        public ActionResult Index()
        {
            ViewBag.QuantityOrder = db.Orders.Count();
            ViewBag.QuantityVisited = HttpContext.Application["PageView"];//lấy số lượng người truy cập
            ViewBag.QuantityOnline = HttpContext.Application["Online"];
            ViewBag.QuantityTotalPrice = (db.OrderDetails.Sum(n => n.QuantityProduct * n.BuyPrice)).ToString();
            ViewBag.QuantityUser = db.Users.Count();
            return PartialView("_Statistic");
        }
    }
}