using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Domain.Entities;
using WebApp.Areas.Admin.Models.ViewModels;
using WebApp.Models.ViewModels;

namespace WebApp.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMapFromViewModelToEntities();
            CreateMapFromEntitiesToViewModel();
        }
        private void CreateMapFromViewModelToEntities()
        {
            //câu lệnh cho phép bỏ qua các trường mà viewmodel không có
            CreateMap<SupplierViewModel, Supplier>()
                .ForMember(dest => dest.CreatedBy, src => src.Ignore())
                .ForMember(dest => dest.CreatedDate, src => src.Ignore())
                .ForMember(dest => dest.UpdatedDate, src => src.Ignore())
                .ForMember(dest=>dest.UpdatedBy,src=>src.Ignore())
                .ForMember(dest=>dest.Status,src=>src.Ignore())
                .ForMember(dest=>dest.IsDeleted,src=>src.Ignore())
                ;
            CreateMap<CategoryViewModel, Category>()
                .ForMember(dest => dest.CreatedBy, src => src.Ignore())
                .ForMember(dest => dest.CreatedDate, src => src.Ignore())
                .ForMember(dest => dest.UpdatedDate, src => src.Ignore())
                .ForMember(dest => dest.UpdatedBy, src => src.Ignore())
                .ForMember(dest => dest.Status, src => src.Ignore())
                .ForMember(dest => dest.IsDeleted, src => src.Ignore())
                .ForMember(dest => dest.Products, src => src.Ignore())
                ;
            CreateMap<ManufactureViewModel, Manufacturer>()
                .ForMember(dest => dest.CreatedBy, src => src.Ignore())
                .ForMember(dest => dest.CreatedDate, src => src.Ignore())
                .ForMember(dest => dest.UpdatedDate, src => src.Ignore())
                .ForMember(dest => dest.UpdatedBy, src => src.Ignore())
                .ForMember(dest => dest.Status, src => src.Ignore())
                .ForMember(dest => dest.IsDeleted, src => src.Ignore())
                ;
            CreateMap<ProductViewModel, Product>()
                .ForMember(dest => dest.CreatedBy, src => src.Ignore())
                .ForMember(dest => dest.CreatedDate, src => src.Ignore())
                .ForMember(dest => dest.UpdatedDate, src => src.Ignore())
                .ForMember(dest => dest.UpdatedBy, src => src.Ignore())
                .ForMember(dest => dest.Status, src => src.Ignore())
                .ForMember(dest => dest.IsDeleted, src => src.Ignore())
                ;
            CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.CreatedBy, src => src.Ignore())
                .ForMember(dest => dest.UpdatedDate, src => src.Ignore())
                .ForMember(dest => dest.UpdatedBy, src => src.Ignore())
                .ForMember(dest => dest.Status, src => src.Ignore())
                .ForMember(dest => dest.IsDeleted, src => src.Ignore())
                ;
            CreateMap<OrderDetailViewModel, OrderDetails>()
                .ForMember(dest => dest.CreatedBy, src => src.Ignore())
                .ForMember(dest => dest.UpdatedDate, src => src.Ignore())
                .ForMember(dest => dest.UpdatedBy, src => src.Ignore())
                .ForMember(dest => dest.Status, src => src.Ignore())
                .ForMember(dest => dest.IsDeleted, src => src.Ignore())
                ;



        }
        private void CreateMapFromEntitiesToViewModel()
        {
            CreateMap<Supplier, SupplierViewModel>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Manufacturer, ManufactureViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<User, UserViewModel>();
            CreateMap<OrderDetails, OrderDetailViewModel>();
        }

    }
}