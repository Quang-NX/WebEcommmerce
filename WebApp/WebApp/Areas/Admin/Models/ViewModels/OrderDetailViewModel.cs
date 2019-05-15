using Domain.Entities;
using System;

namespace WebApp.Areas.Admin.Models.ViewModels
{
    public class OrderDetailViewModel
    {
        public Guid Id { get; set; }
        public int QuantityProduct { get; set; }
        public double BuyPrice { get; set; }

        public Guid? OrderId { get; set; }
        public virtual Orders Orders { get; set; }
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}