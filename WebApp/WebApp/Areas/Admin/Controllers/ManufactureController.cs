using AutoMapper;
using Domain;
using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Areas.Admin.Models.ViewModels;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,QuanLy")]
    public class ManufactureController : Controller
    {
        EcommerceDbContext db = new EcommerceDbContext();
        // GET: Manufacture
        public ActionResult Index()
        {
            var manufacture = db.Manufacturers.Where(w=>w.IsDeleted==false).ToList();
            var manufactureViewModels = Mapper.Map<IEnumerable<ManufactureViewModel>>(manufacture);
            return View(manufactureViewModels);
        }

        // GET: Manufacture/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var manufacture = db.Manufacturers.Find(id);
            if (manufacture == null)
            {
                return HttpNotFound();
            }

            var manufactureViewModel = Mapper.Map<ManufactureViewModel>(manufacture);
            return View(manufactureViewModel);
        }

        // GET: Manufacture/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Manufacture/Create
        [HttpPost]
        public ActionResult Create(ManufactureViewModel manufactureViewModel)
        {
            if (ModelState.IsValid)
            {
                var manufacturer = Mapper.Map<Manufacturer>(manufactureViewModel);
                manufacturer.Id = Guid.NewGuid();
                manufacturer.Status = Domain.Enum.CommonStatus.Active;
                db.Manufacturers.Add(manufacturer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Manufacture/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var manufacture = db.Manufacturers.Find(id);
            if (manufacture == null)
            {
                return HttpNotFound();
            }

            var manufactureViewModel = Mapper.Map<ManufactureViewModel>(manufacture);
            return View(manufactureViewModel);
        }

        // POST: Manufacture/Edit/5
        [HttpPost]
        public ActionResult Edit(ManufactureViewModel manufactureViewModel)
        {
            if (ModelState.IsValid)
            {
                Manufacturer manufacturer = db.Manufacturers.Find(manufactureViewModel.Id);
                if (manufacturer == null)
                {
                    return HttpNotFound();
                }

                Mapper.Map(manufactureViewModel, manufacturer);
                db.Entry(manufacturer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manufactureViewModel);
        }

        // GET: Manufacture/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var manufacture = db.Manufacturers.Find(id);
            if (manufacture == null)
            {
                return HttpNotFound();
            }

            var manufactureViewModel = Mapper.Map<ManufactureViewModel>(manufacture);
            return View(manufactureViewModel);
        }

        // POST: Manufacture/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                Manufacturer manufacturer = db.Manufacturers.Find(id);
                if (manufacturer == null)
                {
                    return HttpNotFound();
                }
                List<Product> product = db.Products.Where(p => p.ManufacturerId == id).ToList();
                foreach (var item in product)
                {
                    item.IsDeleted=true;
                }
                manufacturer.IsDeleted=true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();

        }
    }
}