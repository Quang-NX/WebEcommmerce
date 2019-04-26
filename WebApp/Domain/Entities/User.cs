using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Serializable]
    public class User : BaseEntity
    {
        #region Filed
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        
        #endregion


        #region Relationship
        public Guid? RoleId { get; set; }
        public virtual Role Role { get; set; }
        public ICollection<LikeNumberProductUser> likeNumberProductUsers { get; set; }
        #endregion
    }
}
