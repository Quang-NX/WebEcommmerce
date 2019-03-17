using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Supplier:BaseEntity
    {
        [Required]
        [MaxLength(250)]
        [Column(Order = 1)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(Order = 3)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        [Column(Order = 4)]
        public string Phone { get; set; }
    }
}
