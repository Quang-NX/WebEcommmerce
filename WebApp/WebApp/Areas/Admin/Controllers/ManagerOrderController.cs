using AutoMapper;
using Domain;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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
                CustomerName = c.CustomerName,
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
                mana.CustomerName = item.CustomerName;
                mana.Address = item.Address;
                mana.Age = item.Age;
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
    }
}