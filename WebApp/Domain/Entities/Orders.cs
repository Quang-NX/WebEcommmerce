using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Orders:BaseEntity
    {
        #region Field
        public DateTime? OrderDate{ get; set; }
        public DateTime? ShippedDate { get; set; }
        public CommonStatus StatusPayment { get; set; }

        #endregion
        #region Relationship
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        #endregion
    }
}
