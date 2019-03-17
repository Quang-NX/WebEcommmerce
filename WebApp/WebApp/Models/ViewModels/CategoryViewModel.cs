using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebApp.Models.ViewModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [StringLength(2048)]
        [DisplayName("Chi tiết")]
        public string Description { get; set; }
    }

}