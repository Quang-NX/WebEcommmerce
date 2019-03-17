using AutoMapper;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        EcommerceDbContext db = new EcommerceDbContext();
        // GET: Products
        public ActionResult Index()
        {
            var product = db.Products.ToList();

            var productViewModel = Mapper.Map<IEnumerable<ProductViewModel>>(product);
            return View(productViewModel);
        }

        // GET: Products/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var productViewModel = Mapper.Map<ProductViewModel>(product);

            return View(productViewModel);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            var categories = db.Categories.ToList();
            var suppliers = db.Suppliers.ToList();
            var manufacturers = db.Manufacturers.ToList();

            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");

            ViewBag.SupplierId = new SelectList(suppliers, "Id", "Name");//select("thể loại","giá trị option","Giá trị sẽ được hiển thị")

            ViewBag.ManufacturerId = new SelectList(manufacturers, "Id", "Name");

            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult SaveData(ProductViewModel productViewModel)
        {
            if (productViewModel.UploadImage!=null)
            {
                var product = Mapper.Map<Product>(productViewModel);
                product.Id = Guid.NewGuid();
                string fileName = Path.GetFileNameWithoutExtension(product.UploadImage.FileName);
                string extension = Path.GetExtension(product.UploadImage.FileName);
                fileName = fileName + extension;
                product.UrlImage = fileName;
                product.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/AppFile/Images/"), fileName));
                db.Products.Add(product);
                db.SaveChanges();

                return Json("Sussess !", JsonRequestBehavior.AllowGet);

            }
            return View();
        }
        // GET: Products/Edit/5
        public ActionResult Edit(Guid? id)
        {
            var categories = db.Categories.ToList();
            var suppliers = db.Suppliers.ToList();
            var manufacturers = db.Manufacturers.ToList();

            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");

            ViewBag.SupplierId = new SelectList(suppliers, "Id", "Name");//select("thể loại","giá trị option","Giá trị sẽ được hiển thị")

            ViewBag.ManufacturerId = new SelectList(manufacturers, "Id", "Name");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var productViewModel = Mapper.Map<ProductViewModel>(product);

            return View(productViewModel);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductViewModel productViewModel)
        {
            if (productViewModel!=null)
            {
                var product = db.Products.Find(productViewModel.Id);
                if (product == null)
                {
                    return HttpNotFound();
                }

                Mapper.Map(productViewModel, product);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productViewModel);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(Guid? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var productViewModel = Mapper.Map<ProductViewModel>(product);

            return View(productViewModel);
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                var product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }

                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}