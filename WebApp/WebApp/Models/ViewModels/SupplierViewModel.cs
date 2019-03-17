using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class SupplierViewModel
    {
        public Guid Id { get; set; } 

        [Required(ErrorMessage = "Vui lòng nhập tên nhà cung cấp")]
        [MaxLength(250, ErrorMessage = "Tên nhà cung cấp không được quá 250 ký tự")]
        [DisplayName("Tên nhà cc")]
        public string Name { get; set; }

		[Required(ErrorMessage = "Email không được để trống")]
		[DataType(DataType.EmailAddress, ErrorMessage = "E-mail không đúng")]
		[MaxLength(50)]
        [DisplayName("Email")]
        public string Email { get; set; }
        
        [MaxLength(20)]
		[DataType(DataType.PhoneNumber,ErrorMessage ="Định dạng số điện thoại sai")]
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }

    }
}