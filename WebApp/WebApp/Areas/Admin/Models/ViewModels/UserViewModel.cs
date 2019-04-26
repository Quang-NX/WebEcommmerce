using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApp.Areas.Admin.Models.ViewModels
{
    public class UserViewModel
    {
        [DisplayName("Id")]
        public Guid Id { get; set; }
        [DisplayName("Nhân viên")]
        public string UserName { get; set; }
        [DisplayName("Ngày tham gia")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        [DisplayName("Tuổi")]
        public int Age { get; set; }
        [DisplayName("Số ĐT")]
        public string PhoneNumber { get; set; }
        [DisplayName("Chức vụ")]
        public Guid? RoleId { get; set; }
        [DisplayName("Chức vụ")]
        public string RoleName { get; set; }


    }
}