using AutoMapper;
using Domain;
using PagedList;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Areas.Admin.Models.ViewDto;
using WebApp.Areas.Admin.Models.ViewModels;

namespace WebApp.Areas.Admin.Controllers
{
    public class ManagerOrderController : Controller
    {
        private EcommerceDbContext db = new EcommerceDbContext();
        // GET: Admin/ManagerOrders
        public ActionResult Index()
        {
            return View();
            
        }
        public PartialViewResult GetPagsing(int? page)
        {
            var lstOrder = db.Orders.Join(db.Customers, o => o.CustomerId, c => c.Id, (o, c) => new
            {
                Id = o.Id,
                FirstName = c.FistName,
                LastName=c.LastName,
                Address = c.Address,
                Age = c.Age,
                PhoneNumber = c.PhoneNumber,
                OrderDate = o.OrderDate
            }).OrderByDescending(o=>o.OrderDate).ToList();
            List<ManagerOrderViewModel> managerOrders = new List<ManagerOrderViewModel>();
            foreach (var item in lstOrder)
            {
                ManagerOrderViewModel mana = new ManagerOrderViewModel();
                mana.Id = item.Id;
                mana.FistName = item.FirstName;
                mana.LastName = item.LastName;
                mana.Address = item.Address;
                mana.PhoneNumber = item.PhoneNumber;
                mana.OrderDate = item.OrderDate;
                managerOrders.Add(mana);
            }
            int pageSize = 5;
            int pageNumber = page ?? 1;
            return PartialView("_ManagerOrderPartialView",managerOrders.ToPagedList(pageNumber, pageSize));
        }
        public PartialViewResult GetOrderDetail(Guid id)
        {
            //get orderdetail from order by id
            var orderDetails = db.OrderDetails.Where(od => od.OrderId == id).OrderByDescending(o=>o.CreatedDate).ToList();
            var orderDetailsViewModel = Mapper.Map<IEnumerable<OrderDetailViewModel>>(orderDetails);
            return PartialView("_OrderDetailPartialView", orderDetailsViewModel);
        }
        //thiết kế giao diện hóa đơn
        public ActionResult ExportOrder(Guid? Id)
        {
            var lstOrder = db.OrderDetails.Where(w => w.OrderId == Id).ToList();

            var customer = db.Orders.Join(db.Customers, o => o.CustomerId, c => c.Id, (o, c) => new
            {
                FirstName = c.FistName,
                LastName=c.LastName,
                Address = c.Address,
                PhoneNumber = c.PhoneNumber,
                OrderDate = o.OrderDate,
                OrderId = o.Id
            }).ToList().Where(w=>w.OrderId==Id).FirstOrDefault();
            OrderDetailDto orderDetailDto = new OrderDetailDto();
            orderDetailDto.FirstName = customer.FirstName;
            orderDetailDto.LastName = customer.LastName;
            orderDetailDto.Address = customer.Address;
            orderDetailDto.PhoneNumber = customer.PhoneNumber;
            orderDetailDto.OrderDate = customer.OrderDate;
            orderDetailDto.OrderId = customer.OrderId;

            ViewBag.Customer = orderDetailDto;
            return View(lstOrder);
        }
        public ActionResult ExportFilePDF(Guid? Id)
        {
            TempData["OrderId"] = Id;
            ActionAsPdf result = new ActionAsPdf("ExportOrder", new { Id=Id})
            {
                FileName = Server.MapPath("~/Assets/Invoice.pdf")
            };
            return result;
        }
    }
}