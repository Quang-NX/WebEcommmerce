using AutoMapper;
using Domain;
using Domain.Entities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApp.Areas.Admin.Models.ViewModels;
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
                var role = db.Roles.Where(r => r.Id==myuser.RoleId).Select(s=>s.Name).FirstOrDefault();
                if(role == null)
                {
                    return RedirectToAction("NotAccessAuthorize");
                }
                AuthorizeUser(username, role);
                Session["UserName"] = username;
                return RedirectToAction("Index");
            }
            ViewBag.ThongBao="Tài khoản hoặc mật khẩu không chính xác !";
            return View();
        }
        public ActionResult NotAccessAuthorize()
        {
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
        //list name product
        public ActionResult ListName(string q)
        {
            var lstName = db.Products.Where(m => m.Name.Contains(q) && m.IsDeleted == false).Select(s => s.Name).ToList();
            return Json(new
            {
                data = lstName,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        //phân trang
        public ActionResult Search(string keyword)
        {
            Session["keyword"] = keyword;
            return View();
        }
        public ActionResult SearchList(string keyword, int? page)
        {
            var listProduct = db.Products.Where(m => m.Name.Contains(keyword) && m.IsDeleted == false).ToList();
            var lstProductViewModel = Mapper.Map<IEnumerable<ProductViewModel>>(listProduct);
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            ViewBag.KeyWord = keyword;
            return View("_SearchPartialView",lstProductViewModel.ToPagedList(pageNumber, pageSize));
        }
    }
}