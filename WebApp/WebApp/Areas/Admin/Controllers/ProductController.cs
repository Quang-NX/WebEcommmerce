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
using WebApp.Areas.Admin.Models.ViewModels;

namespace WebApp.Areas.Admin.Controllers
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
        public ActionResult SaveData(HttpPostedFileBase file,ProductViewModel productViewModel)
        {
            if (file!=null)
            {
                var product = Mapper.Map<Product>(productViewModel);
                product.Id = Guid.NewGuid();
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                fileName = fileName + extension;

                file.SaveAs(Path.Combine(Server.MapPath("~/AppFile/Images/"), fileName));
                product.UrlImage = "http://localhost:55666/AppFile/Images/" + fileName;
                db.Products.Add(product);
                db.SaveChanges();

                return RedirectToAction("Index");

            }
            return View();
        }
        // GET: Products/Edit/5
        public ActionResult Edit(Guid? id)
        {
            var categories = db.Categories.ToList();
            var suppliers = db.Suppliers.ToList();
            var manufacturers = db.Manufacturers.ToList();

            var product = db.Products.Find(id);

            ViewBag.CategoryId = new SelectList(categories, "Id", "Name",product.CategoryId);

            ViewBag.SupplierId = new SelectList(suppliers, "Id", "Name",product.SupplierId);//select("thể loại","giá trị option","Giá trị sẽ được hiển thị",tham số)

            ViewBag.ManufacturerId = new SelectList(manufacturers, "Id", "Name",product.ManufacturerId);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            
            if (product == null)
            {
                return HttpNotFound();
            }

            Session["img"] = product.UrlImage;

            var productViewModel = Mapper.Map<ProductViewModel>(product);

            return View(productViewModel);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file,ProductViewModel productViewModel)
        {
            var product = db.Products.Find(productViewModel.Id);
            if (file!=null)
            {
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                fileName = fileName + extension;

                file.SaveAs(Path.Combine(Server.MapPath("~/AppFile/Images/"), fileName));
                
                Mapper.Map(productViewModel, product);
                product.UrlImage = "http://localhost:55666/AppFile/Images/" + fileName;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                
                Mapper.Map(productViewModel, product);
                product.UrlImage = Session["img"].ToString();
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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