﻿using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewModels
{
    public class GioHang
    {
        public string ProductName { get; set; }
        public Guid? ProductCode { get; set; }
        public int QuantityProduct { get; set; }
        public double? ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public double TotalPrice { get; set; }
        public GioHang(Guid productCode)
        {
            using (EcommerceDbContext db = new EcommerceDbContext())
            {
                this.ProductCode = productCode;
                Product product = db.Products.Single(s => s.Id == productCode);
                this.ProductName = product.Name;
                this.ProductPrice = product.Price;
                this.ProductImage = product.UrlImage;
                this.TotalPrice = product.productInStock * product.Price;

            }
        }
        public GioHang(Guid productCode,int quantityProduct)
        {
            using (EcommerceDbContext db = new EcommerceDbContext())
            {
                this.ProductCode = productCode;
                Product product = db.Products.Single(s => s.Id == productCode);
                this.ProductName = product.Name;
                this.QuantityProduct = quantityProduct;
                this.ProductPrice = product.Price;
                this.ProductImage = product.UrlImage;
                this.TotalPrice = product.productInStock * product.Price;

            }
        }
        public GioHang()
        {

        }
    }
}