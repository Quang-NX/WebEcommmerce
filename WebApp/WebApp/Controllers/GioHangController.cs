using Domain;
using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class GioHangController : Controller
    {
        EcommerceDbContext db = new EcommerceDbContext();
        //lấy giỏ hàng và lưu nó vào sesstion
        //sesstion ở đây sẽ lưu 1 list item giỏ hàng
        public List<ItemGioHang> LayGioHang()
        {
            //TH giỏ hàng đã tồn tại
            List<ItemGioHang> listGioHang = Session["GioHang"] as List<ItemGioHang>;//ép kiểu về itemGioHang
            //Check list giỏ hàng tồn tại hay chưa
            if (listGioHang == null)
            {
                //nếu session giỏ hàng chưa tồn tại tiến hành khởi tạo
                listGioHang = new List<ItemGioHang>();
                Session["GioHang"] = listGioHang;
                return listGioHang;
            }
            //ngược lại thì trả về listGioHang
            return listGioHang;
        }

        //Thêm giỏ hàng thông thường load lại trang
        //public ActionResult ThemGioHang(Guid productId, string strURL)//mã sản phẩm,đường link dẫn đến khi redirect
        //{
        //    //kiểm tra sản phẩm thông qua mã sp tồn tại trong csdl hay không
        //    Product sp = db.Products.SingleOrDefault(n => n.Id == productId);
        //    if (sp == null)
        //    {
                
        //        //trả về trang 404 đường dẫn không hợp lệ

        //        Response.StatusCode = 404;
        //        return null;
        //    }
        //    //lấy list giỏ hàng ra
        //    List<ItemGioHang> lstGioHang = LayGioHang();
        //    //TH1:Nếu đã có sản phẩm đó tồn tại trong giỏ hàng
        //    //thì lấy ra sản phẩm đó ra và tăng số lượng lên tính lại thành tiền của sản phẩm đó
        //    ItemGioHang spCheck = lstGioHang.SingleOrDefault(s => s.ProductCode == productId);
        //    if (spCheck != null)
        //    {
        //        //kiểm tra xem trong số lượng tồn còn đủ số sản phẩm khách hàng muốn mua hay không
        //        //nếu không đủ sẽ in ra thông báo cho khách hàng biết
        //        //chứ không nói là hết hàng
        //        //kiểm tra số lượng tồn trước khi cho khách hàng đặt hàng
        //        //nếu số sản phẩm tồn nhỏ hơn số sản phẩm đặt thì view ra thông báo
        //        if ( sp.productInStock < spCheck.QuantityProduct )
        //        {
        //            return View("ThongBao");
        //        }
        //        spCheck.QuantityProduct++;
        //        spCheck.TotalPrice = spCheck.ProductPrice * spCheck.QuantityProduct;
        //        return Redirect(strURL);
        //    }
        //    //còn không thì tạo lại một sản phẩm mới và add vào
        //    //tạo ra 1 item để thêm vào giỏ hàng
        //    ItemGioHang itemGioHang = new ItemGioHang(productId);
        //    if (sp.productInStock < itemGioHang.QuantityProduct)
        //    {
        //        return View("ThongBao");
        //    }
        //    lstGioHang.Add(itemGioHang);
        //    return Redirect(strURL);
        //}

        //Tinhs tổng số lượng
        public int TinhTongSoLuong()
        {
            //Lấy giỏ hàng
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if(lstGioHang==null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.QuantityProduct);
        }
        
        //Tinsh tổng tiền
        public double TinhTongTien()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if(lstGioHang==null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.TotalPrice);
        }

        // GET: GioHang
        public ActionResult XemGioHang()
        {
            //Lấy giỏ hàng
            List<ItemGioHang> itemGioHangs = LayGioHang();
            ViewBag.TotalPrice = TinhTongTien();
            return View(itemGioHangs);
        }

        public ActionResult GioHangPartial()
        {
            if(TinhTongSoLuong()==0)
            {
                TempData["TongSoLuong"] = 0;
                TempData["TongTien"] = 0;
                return PartialView();
            }
            TempData["TongSoLuong"] = TinhTongSoLuong();
            TempData["TongTien"] = TinhTongTien();
            return PartialView();
        }
        //chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang(Guid productId)
        {
            if(productId==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<ItemGioHang> lstItemGioHang = LayGioHang();
            //gán lst giỏ hàng vào viewbag để truy xuất và hiển thị trong view
            ViewBag.GioHang = lstItemGioHang;
            //check sản phẩm có trong giỏ hàng hay không
            ItemGioHang spCheck = lstItemGioHang.SingleOrDefault(n => n.ProductCode == productId);
            if(spCheck==null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(spCheck);
        }
        //xử lý cập nhật giỏ hàng
        public ActionResult CapNhatGioHang(ItemGioHang itemGioHang)
        {
            //kiểm tra số lượng tồn
            //nếu khách hàng đặt thêm mà số lượng trong kho hết thì xuất
            //ra thông báo sản phẩm đã hết hàng
            Product spCheck = db.Products.Single(n => n.Id == itemGioHang.ProductCode);
            //spCheck.productInstock:sản phẩm còn trong kho
            //item.QuantityProduct:sản phẩm khách hàng đặt
            if (spCheck.productInStock<itemGioHang.QuantityProduct)
            {
                return View("ThongBao");
            }
            //ngược lại thì cập nhật số lại số lượng trong giỏ hàng của khách hàng
            //cập nhật vào session
            
            List<ItemGioHang> lstGioHang = LayGioHang();//lấy giỏ hàng ra
            //lấy sản phâm trong giở hàng ra để cập nhật lại giá trị
            ItemGioHang itemGHUpdate = lstGioHang.Find(n=>n.ProductCode==itemGioHang.ProductCode);
            //cập nhật số lượng
            itemGHUpdate.QuantityProduct = itemGioHang.QuantityProduct;
            //cập nhật thành tiền
            itemGHUpdate.TotalPrice = itemGHUpdate.QuantityProduct * itemGHUpdate.ProductPrice;
            return RedirectToAction("XemGioHang","GioHang");
        }
        //Xử lý xóa giỏ hàng
        public ActionResult XoaGioHang(Guid id)
        {
            //kiểm tra sản phẩm thông qua mã sp tồn tại trong csdl hay không
            Product sp = db.Products.SingleOrDefault(n => n.Id == id);
            if (sp == null)
            {

                //trả về trang 404 đường dẫn không hợp lệ

                Response.StatusCode = 404;
                return null;
            }
            //lấy list giỏ hàng ra
            List<ItemGioHang> lstGioHang = LayGioHang();
            //TH1:Nếu đã có sản phẩm đó tồn tại trong giỏ hàng
            //thì lấy ra sản phẩm đó ra và tăng số lượng lên tính lại thành tiền của sản phẩm đó
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(s => s.ProductCode == id);
            if(spCheck==null)
            {
                return RedirectToAction("Index","Home");
            }
            //xóa item trong giỏ hàng
            lstGioHang.Remove(spCheck);
            return RedirectToAction("XemGioHang","GioHang");
        }
        //Xây dưng chức năng đặt hàng
        [HttpPost]
        public ActionResult DatHang(Customer cus)
        {
            //Tạo đơn đặt hàng
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //thêm user vào db
            Customer customer = new Customer();
            if (Session["username"] == null)
            {
                //Thêm khách hàng vào bảng khách hàng đối vs khách hàng chưa có tài khoản
                customer = cus;
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            else
            {
                User user = Session["username"] as User;
                cus.CustomerName = user.UserName;
                cus.Address = user.Address;
                cus.Email = user.Email;
                cus.PhoneNumber = user.PhoneNumber;
                db.Customers.Add(cus);
                db.SaveChanges();
            }

            //thêm đơn hàng
            Orders orders = new Orders();
            orders.OrderDate = DateTime.Now;
            orders.StatusPayment = CommonStatus.InActive;
            orders.Cancelled = CommonStatus.InActive;
            orders.Deleted = CommonStatus.InActive;
            orders.CustomerId = cus.Id;
            db.Orders.Add(orders);
            db.SaveChanges();//lưu lại để trong db phát sinh mã đơn đặt hàng
            //thêm chi tiết đơn đặt hàng
            List<ItemGioHang> lstGioHang = LayGioHang();
            foreach(var item in lstGioHang)
            {
                OrderDetails orderDetails = new OrderDetails();
                orderDetails.OrderId = orders.Id;
                orderDetails.ProductId = item.ProductCode;
                orderDetails.QuantityProduct = item.QuantityProduct;
                orderDetails.BuyPrice = item.ProductPrice;
                db.OrderDetails.Add(orderDetails);
                
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XemGioHang");
        }
        //Thêm giỏ hàng sử dụng Ajax
        public ActionResult ThemGioHangAjax(Guid productId)//mã sản phẩm,đường link dẫn đến khi redirect
        {
            //kiểm tra sản phẩm thông qua mã sp tồn tại trong csdl hay không
            Product sp = db.Products.SingleOrDefault(n => n.Id == productId);
            if (sp == null)
            {

                //trả về trang 404 đường dẫn không hợp lệ

                Response.StatusCode = 404;
                return null;
            }
            //lấy list giỏ hàng ra
            List<ItemGioHang> lstGioHang = LayGioHang();
            //TH1:Nếu đã có sản phẩm đó tồn tại trong giỏ hàng
            //thì lấy ra sản phẩm đó ra và tăng số lượng lên tính lại thành tiền của sản phẩm đó
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(s => s.ProductCode == productId);
            if (spCheck != null)
            {
                //kiểm tra xem trong số lượng tồn còn đủ số sản phẩm khách hàng muốn mua hay không
                //nếu không đủ sẽ in ra thông báo cho khách hàng biết
                //chứ không nói là hết hàng
                //kiểm tra số lượng tồn trước khi cho khách hàng đặt hàng
                //nếu số sản phẩm tồn nhỏ hơn số sản phẩm đặt thì view ra thông báo
                if (sp.productInStock < spCheck.QuantityProduct)
                {
                    return Content("<script> alert(\"Sản phẩm hết hàng !\")</script>");
                }
                spCheck.QuantityProduct++;
                TempData["TongSoLuong"] = TinhTongSoLuong();
                return PartialView("GioHangPartial");
            }
            //còn không thì tạo lại một sản phẩm mới và add vào
            //tạo ra 1 item để thêm vào giỏ hàng
            ItemGioHang itemGioHang = new ItemGioHang(productId);
            if (sp.productInStock < itemGioHang.QuantityProduct)
            {
                return Content("<script> alert(\"Sản phẩm hết hàng !\")</script>");
            }
            lstGioHang.Add(itemGioHang);
            TempData["TongSoLuong"] = TinhTongSoLuong();
            return PartialView("GioHangPartial");
        }
    }
}