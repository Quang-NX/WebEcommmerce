using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role:BaseEntity
    {
        [MaxLength(50)]
        [Column(TypeName ="varchar")]
        public string Name { get; set; }
        public string Description { get; set; }

        #region Relationship
        public virtual IEnumerable<User> Users { get; set; }
        #endregion
    }
}
