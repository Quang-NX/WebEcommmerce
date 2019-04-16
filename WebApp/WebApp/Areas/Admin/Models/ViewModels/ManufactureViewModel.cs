using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Areas.Admin.Models.ViewModels
{
    public class ManufactureViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên nhà sản xuất")]
        [MaxLength(250)]
        public string Name { get; set; }
        [DisplayName("Chi tiết")]
        public string Description { get; set; }
        [MaxLength(250)]
        [DisplayName("Trang Web")]
        public string Website { get; set; }
        [DisplayName("Logo")]
        public string Logo { get; set; }
    }
}