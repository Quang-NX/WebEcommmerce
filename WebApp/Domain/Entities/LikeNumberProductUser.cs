using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LikeNumberProductUser:BaseEntity
    {
        public int LikeNumber { get; set; }

        //khai báo khóa ngoại bảng product
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }

        //khai báo khóa ngoại bảng user
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
