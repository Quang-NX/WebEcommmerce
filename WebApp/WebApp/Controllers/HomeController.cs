﻿using AutoMapper;
using Domain;
using Domain.Entities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApp.Common;
using WebApp.Models.ViewDto;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        EcommerceDbContext db = new EcommerceDbContext();

        public ActionResult Index()
        {
            var productList = db.Products.OrderByDescending(p => p.CreatedDate).Select(s => new ProductViewModel {
                Id = s.Id, Name = s.Name, Price = s.Price, UrlImage = s.UrlImage, CategoryName = s.Category.Name }).Take(5);

            ViewBag.ProductList = new List<ProductViewModel>(db.Products.OrderBy(p => p.CreatedDate).Select(s => new ProductViewModel {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price,
                UrlImage = s.UrlImage,
                CategoryName = s.Category.Name
            }).Take(5));
            ViewBag.Gamming = db.Products.Where(s => s.Category.Name.Equals("Gamming")).Select(s => s.UrlImage).FirstOrDefault();
            ViewBag.DoanhNhan = db.Products.Where(s => s.Category.Name.Equals("Doanh nhân")).Select(s => s.UrlImage).FirstOrDefault();
            ViewBag.DoHoa = db.Products.Where(s => s.Category.Name.Equals("Đồ họa")).Select(s => s.UrlImage).FirstOrDefault();

            TempData["GammingId"] = db.Products.Where(s => s.Category.Name.Equals("Gamming")).Select(s => new ProductViewModel { CategoryId = s.CategoryId }).FirstOrDefault().CategoryId;
            TempData["DoanhNhanId"] = db.Products.Where(s => s.Category.Name.Equals("Doanh nhân")).Select(s => new ProductViewModel { CategoryId = s.Category.Id}).FirstOrDefault().CategoryId;
            TempData["DoHoaId"] = db.Products.Where(s => s.Category.Name.Equals("Đồ họa")).Select(s => new ProductViewModel { CategoryId = s.Category.Id }).FirstOrDefault().CategoryId;
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
            return RedirectToAction("Login", "Home");
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

        public ActionResult ProductDetail(Guid? id,string sp)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products.Find(id);
            if(product==null)
            {
                return HttpNotFound();
            }
            var productViewModel = Mapper.Map<ProductViewModel>(product);
            return View(productViewModel);
        }
        public ActionResult PageProductDetail(int? page,Guid Id)
        {
            //ViewBag.ListManu = new List<ManufactureViewModel>(db.Manufacturers.Select(s => new ManufactureViewModel { Name = s.Name }));

            ViewBag.QuantityManu = new List<ManufactureDto>(db.Products.Join(db.Manufacturers, p => p.ManufacturerId, m => m.Id, (p, m) => new { p = p.productInStock, m = m.Name })
                .ToList().GroupBy(model => model.m, (i, models) => new ManufactureDto { Name = i, QuantityManu = models.Sum(model => model.p) }));


            ViewBag.ListCate = new List<CategoryDto>(db.Products.Join(db.Categories, p => p.CategoryId, c => c.Id, (p, c) => new { p = p.productInStock, c = c.Name }).ToList()
                .GroupBy(model => model.c, (i, models) => new CategoryDto { Name = i, QuantityCate = models.Sum(model => model.p) }));

            TempData["GammingId"] = db.Products.Where(s => s.Category.Name.Equals("Gamming")).Select(s => new ProductViewModel { CategoryId = s.CategoryId }).FirstOrDefault().CategoryId;
            TempData["DoanhNhanId"] = db.Products.Where(s => s.Category.Name.Equals("Doanh nhân")).Select(s => new ProductViewModel { CategoryId = s.Category.Id }).FirstOrDefault().CategoryId;
            TempData["DoHoaId"] = db.Products.Where(s => s.Category.Name.Equals("Đồ họa")).Select(s => new ProductViewModel { CategoryId = s.Category.Id }).FirstOrDefault().CategoryId;

            var listProduct = db.Products.Where(p => p.Category.Id.Equals(Id)).ToList();
            var lstProductViewModel = Mapper.Map<IEnumerable<ProductViewModel>>(listProduct);
            int pageSize = 2;
            //toán tử tương đương với nếu page!=null thì pageNumber =1 ngược lại =page
            int pageNumber = (page ?? 1);
            //get Id Category
            ViewBag.CategoryId = Id;
            return View(lstProductViewModel.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult GetFormEmail(FormCollection form)
        {
            string email = form["email"].ToString();
            string emailAddress = email;
            /*Cảm ơn bạn đã để lại thông tin gmail.Chúng tôi sẽ cập nhật cho bạn những thông tin mới nhất từ trang web.*/
            string content = "<h1>Thư xác nhận Email từ Quang Bếu</h1></br>";
            content += "<p>Chúc tìn yêu một ngày tốt lành nha !</p>";
            content += "<a href=" + "http://localhost:55666/Home/Index" + ">Quay lại trang chủ</a>";
            GuiEmail("Email từ tình iu <3 !", emailAddress, "adquang199x@gmail.com",
                "Quangtrang99xx", content);
            return RedirectToAction("Index");
        }
        //gửi email
        public void GuiEmail(string Title, string ToEmail, string FromEmail, string PassWord, string Content)
        {
            // goi email
            MailMessage mail = new MailMessage();
            mail.To.Add(ToEmail); // Địa chỉ nhận
            mail.From = new MailAddress(ToEmail); // Địa chửi gửi
            mail.Subject = Title; // tiêu đề gửi
            mail.Body = Content; // Nội dung
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; // host gửi của Gmail
            smtp.Port = 587; //port của Gmail
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            (FromEmail, PassWord);//Tài khoản password người gửi
            smtp.EnableSsl = true; //kích hoạt giao tiếp an toàn SSL
            smtp.Send(mail); //Gửi mail đi
        }
    }
}