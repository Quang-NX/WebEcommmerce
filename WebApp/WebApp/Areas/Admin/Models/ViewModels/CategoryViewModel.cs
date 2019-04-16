using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApp.Areas.Admin.Models.ViewModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tên sản phẩm")]
        [DisplayName("Tên thể loại")]
        [MaxLength(250)]
        public string Name { get; set; }
        [StringLength(2048)]
        [DisplayName("Chi tiết")]
        public string Description { get; set; }
    }

}