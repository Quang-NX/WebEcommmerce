using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CatalogCoupon:BaseEntity
    {
        #region Filed
        public string Name { get; set; }
        public string CodeName { get; set; }
        public string Decription { get; set; }
        public int? Amount { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        #endregion
        #region Relationship
        [ForeignKey("Category")]
        public Guid? ApplyForCategory { get; set; }
        public virtual Category Category { get; set; }
        [ForeignKey("Product")]
        public Guid? ApplyForProduct { get; set; }
        public virtual Product Product { get; set; }
        #endregion
    }
}
