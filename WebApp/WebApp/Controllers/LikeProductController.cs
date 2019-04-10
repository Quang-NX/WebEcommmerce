using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class LikeProductController : Controller
    {
        EcommerceDbContext db = new EcommerceDbContext();

        //get list product like
        public List<LikeProduct> GetListProductsLike()
        {
            List<LikeProduct> lstLikeProduct = Session["likeproduct"] as List<LikeProduct>;
            if(lstLikeProduct==null)
            {
                lstLikeProduct = new List<LikeProduct>();
                Session["likeproduct"] = lstLikeProduct;
                return lstLikeProduct;
            }
            return lstLikeProduct;
        }

        //Add item ProductLike
        public ActionResult AddProductLike(Guid productLikeId,string stringURL)
        {
            Product product = db.Products.SingleOrDefault(s => s.Id == productLikeId);
            if(product==null)
            {
                return HttpNotFound();
            }
            //get list
            List<LikeProduct> listLikeProduct = GetListProductsLike();
            //lấy ra item trong list product yêu thích.nếu có rồi thì gán 1 cái ViewBag đẻ thông báo không thì tăng like lên 1
            LikeProduct likeCheck = listLikeProduct.SingleOrDefault(s => s.ProductId == productLikeId);
            if(likeCheck!=null)
            {
                //trong ds yêu thích đã có vậy nên View
                return Redirect(stringURL);
            }
            LikeProduct likeProduct = new LikeProduct(productLikeId);
            listLikeProduct.Add(likeProduct);
            return Redirect(stringURL);
        }
        //calculate total like product
        public int TotalProductLike()
        {
            //get list product
            List<LikeProduct> likeProducts=Session["likeproduct"] as List<LikeProduct>;
            if(likeProducts==null)
            {
                return 0;
            }
            return likeProducts.Sum(s => s.QuantityLikeProduct);
        }

        // GET: LikeProduct
        public ActionResult ShowProductsLike()
        {
            return View();
        }
    }
}