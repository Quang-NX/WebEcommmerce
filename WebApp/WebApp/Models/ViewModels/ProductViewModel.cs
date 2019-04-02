using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebApp.Models.ViewDto;

namespace WebApp.Models.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        [StringLength(256)]
        [Required(ErrorMessage ="Vui lòng nhập tên sản phẩm.")]
        [DisplayName("Tên sp")]
        public string Name { get; set; }

        
        [DisplayName("Ngày hết hạn")]
        [DataType(DataType.DateTime), Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PublicationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0} VNĐ")]
        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm.")]
        [DisplayName("Mệnh giá")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ảnh sản phẩm.")]
        [DisplayName("Chi tiết")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ảnh sản phẩm.")]
        [DisplayName("Cân nặng")]
        public double? Weight { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ảnh sản phẩm.")]
        [DisplayName("Chiều cao")]
        public double? Height { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ảnh sản phẩm.")]
        [DisplayName("Chiều rộng")]
        public double? Width { get; set; }

		[DisplayName("Hình ảnh")]
		public string UrlImage { get; set; }

		[DisplayName("Thể loại")]
		public Guid? CategoryId { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ảnh sản phẩm.")]
        [DisplayName("Thể loại")]
		public string CategoryName { get; set; }

		[DisplayName("Nhà cung cấp")]
		public Guid? SupplierId { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ảnh sản phẩm.")]
        [DisplayName("Nhà cung cấp")]
		public string SupplierName { get; set; }
		

		[DisplayName("Hãng sản xuất")]
        public Guid? ManufacturerId { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ảnh sản phẩm.")]

        [DisplayName("Hãng sản xuất")]
		public string ManufacturerName { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ảnh sản phẩm.")]
        [NotMapped]
		public HttpPostedFileBase UploadImage { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn chọn số lượng còn lại.")]
        [DisplayName("Số lượng")]
        public int productInStock { get; set; }

    }
}