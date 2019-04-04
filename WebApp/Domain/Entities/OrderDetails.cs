using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderDetails:BaseEntity
    {
        public int QuantityProduct { get; set; }
        public double BuyPrice { get; set; }

        [ForeignKey("Orders")]
        public Guid? OrderId { get; set; }
        public virtual Orders Orders{ get; set; }
        [ForeignKey("Product")]
        public Guid? ProductId{ get; set; }
        public virtual Product Product { get; set; }
    }
}
