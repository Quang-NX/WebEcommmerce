using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Common;
using WebApp.Models.ViewDto;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        EcommerceDbContext db = new EcommerceDbContext();

        public ActionResult Index()
        {
            var productList = db.Products.OrderByDescending(p => p.CreatedDate).Take(5);
            ViewBag.ProductList = new List<Product>(db.Products.OrderBy(p => p.CreatedDate).Take(5));
            return View(productList);
        }

        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(CreateUserDto loginDto)
        {
            User user = new User();
            if (ModelState.IsValid)
            {
                if (loginDto.Password.Equals(loginDto.ConfirmPassword))
                {
                    user.UserName = loginDto.UserName;
                    user.Password = loginDto.Password;
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    return RedirectToAction("CreateUser", "Home", loginDto);
                }
            }

            return View(loginDto);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginDto loginDto)
        {
            var sTaikhoan = loginDto.UserName;
            var sMatkhau = loginDto.Password;
            User user = db.Users.FirstOrDefault(n => n.UserName.Equals(sTaikhoan) && n.Password.Equals(sMatkhau));
            if (user != null)
            {
                Session["username"] = user;
                if (sTaikhoan == "admin" && sMatkhau.Equals(user.Password))
                {
                    return RedirectToAction("Index", "Product");
                }
                return RedirectToAction("Index", "Home");

            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            Session["username"] = null;
            return RedirectToAction("Index", "Home");
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
        public ActionResult SearchPatialView()
        {
            var lstCategory = db.Categories;
            return PartialView(lstCategory);
        }
    }
}