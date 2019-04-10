using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModels
{
    public class LikeProduct
    {
        public string ProductName { get; set; }
        public bool Like { get; set; }
        public int QuantityLikeProduct { get; set; }
        public Guid ProductId { get; set; }
        public double ProductPrice { get; set; }
        public string ProductImage { get; set; }

        public LikeProduct(Guid productId)
        {
            using (EcommerceDbContext db = new EcommerceDbContext())
            {
                this.ProductId = productId;
                Product product = db.Products.SingleOrDefault(s => s.Id == productId);
                this.ProductImage = product.UrlImage;
                this.ProductPrice = product.Price;
                this.QuantityLikeProduct = 1;
                this.Like = true;
            }
        }
    }
}