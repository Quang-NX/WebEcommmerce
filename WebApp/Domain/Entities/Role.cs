using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        #region Relationship
        public virtual IEnumerable<User> Users { get; set; }
        #endregion
    }
}
