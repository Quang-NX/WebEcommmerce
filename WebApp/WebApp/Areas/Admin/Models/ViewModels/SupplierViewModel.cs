using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Areas.Admin.Models.ViewModels
{
    public class SupplierViewModel
    {
        public Guid Id { get; set; } 

        [Required(ErrorMessage = "Vui lòng nhập tên nhà cung cấp")]
        [MaxLength(250, ErrorMessage = "Tên nhà cung cấp không được quá 250 ký tự")]
        [DisplayName("Tên nhà cc")]
        public string Name { get; set; }

		[Required(ErrorMessage = "Email không được để trống")]
		[DataType(DataType.EmailAddress, ErrorMessage = "E-mail phải chứa kí tự @")]
		[MaxLength(50)]
        [DisplayName("Email")]
        public string Email { get; set; }
        
        [MaxLength(20)]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }

    }
}