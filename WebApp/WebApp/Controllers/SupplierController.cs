using AutoMapper;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class SupplierController : Controller
    {
        EcommerceDbContext db = new EcommerceDbContext();
        // GET: Supplier
        #region Index
        public ActionResult Index()
        {
            var supplier = db.Suppliers.ToList();
            var supplierViewModel = Mapper.Map<IEnumerable<SupplierViewModel>>(supplier);
            return View(supplierViewModel);
        }
        #endregion

        #region Details
        // GET: Supplier/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var supplier = db.Suppliers.Find(id.Value);
            if (supplier == null)
            {
                return HttpNotFound();
            }

            var supplierViewModel = Mapper.Map<SupplierViewModel>(supplier);
            ViewBag.Name = supplierViewModel.Name;
            return View(supplierViewModel);
        }
        #endregion

        #region Create

        // GET: Supplier/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(SupplierViewModel supplierViewModel)
        {
            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                var supplier = Mapper.Map<Supplier>(supplierViewModel);
                supplier.Id = Guid.NewGuid();
                supplier.Status = Domain.Enum.CommonStatus.Active;
                db.Suppliers.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion

        #region Edit
        // GET: Supplier/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }

            var supplierViewModel = Mapper.Map<SupplierViewModel>(supplier);
            return View(supplierViewModel);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(SupplierViewModel supplierViewModel)
        {

            //check box space
            if (ModelState.IsValid)
            {
                Supplier supplier = db.Suppliers.Find(supplierViewModel.Id);
                if (supplier == null)
                {
                    return HttpNotFound();

                }

                Mapper.Map(supplierViewModel, supplier);

                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplierViewModel);
        }
        #endregion

        #region Delete
        // GET: Supplier/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }

            var supplierViewModel = Mapper.Map<SupplierViewModel>(supplier);
            return View(supplierViewModel);
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            db.Suppliers.Remove(supplier);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
    }
}