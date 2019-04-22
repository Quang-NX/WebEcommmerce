using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApp.Models.ViewDto;

namespace WebApp.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        EcommerceDbContext db = new EcommerceDbContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            
            return View(db.Products.ToList());
        }
        //login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginDto user)
        {
            var username = user.UserName;
            var password = user.Password;
            User myuser = db.Users.SingleOrDefault(m => m.UserName.Equals(username) && m.Password.Equals(password));
            if(myuser!=null)
            {
                var role = myuser.Role.Name;
                AuthorizeUser(username, role);
                return RedirectToAction("Index");
            }
            ViewBag.ThongBao("Tài khoản hoặc mật khẩu không chính xác !");
            return View();
        }
        //logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();//hủy cookie và đăng xuất
            return RedirectToAction("Login");
        }
        public void AuthorizeUser(string accountName,string role)
        {
            FormsAuthentication.Initialize();//khởi tạo 
            var ticket = new FormsAuthenticationTicket(1,
                            accountName,
                            DateTime.Now,//thời gian bắt đầu
                            DateTime.Now.AddHours(2),//thời gian kết thúc
                            false,//ghi nhớ đăng nhập
                            role,//quyền
                            FormsAuthentication.FormsCookiePath//là tên của cookie,ở đây ta lấy chuỗi ngẫu nhiên(random)

                );
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));//mã hóa cookie khi đưa lên client
            if(ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            //thêm vào cookie
            Response.Cookies.Add(cookie);
        }

    }
}