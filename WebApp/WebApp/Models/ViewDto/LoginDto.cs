using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models.ViewDto
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Nhập tên đăng nhập !")]
        [DisplayName("Tên đăng nhập")]
        public string UserName { get; set; }
        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Nhập mật khẩu !")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}