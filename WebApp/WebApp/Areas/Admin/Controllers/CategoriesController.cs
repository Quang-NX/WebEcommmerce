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
    public class CategoriesController : Controller
    {
        EcommerceDbContext db = new EcommerceDbContext();
        // GET: Categories
        public ActionResult Index()
        {
            var category = db.Categories.Where(w => w.IsDeleted == false).ToList();
            var categoryViewModel = Mapper.Map<IEnumerable<CategoryViewModel>>(category);
            return View(categoryViewModel);
        }

        // GET: Categories/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            var categoryViewModel = Mapper.Map<CategoryViewModel>(category);
            return View(categoryViewModel);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        public ActionResult Create(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var category = Mapper.Map<Category>(categoryViewModel);
                category.Id = Guid.NewGuid();
                category.Status = Domain.Enum.CommonStatus.Active;
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            var categoryViewModel = Mapper.Map<CategoryViewModel>(category);
            return View(categoryViewModel);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        public ActionResult Edit(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                Category category = db.Categories.Find(categoryViewModel.Id);
                if (category == null)
                {
                    return HttpNotFound();
                }

                Mapper.Map(categoryViewModel, category);
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryViewModel);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            var categoryViewModel = Mapper.Map<CategoryViewModel>(category);
            return View(categoryViewModel);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            
            if (ModelState.IsValid) //check format data
            {
                Category category = db.Categories.Find(id);
                if (category == null)
                {
                    return HttpNotFound();
                }
                List<Product> product = db.Products.Where(p => p.CategoryId == id).ToList();
                foreach (var item in product)
                {
                    item.IsDeleted = true;
                }
                category.IsDeleted = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();

        }
    }
}