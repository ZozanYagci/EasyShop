using AutoMapper;
using Core.Entities.Concrete;
using DTOs.DTOs.ProductDtos;
using DTOs.DTOs.ProductImageDtos;
using DTOs.DTOs.SubCategoryDtos;
using DTOs.DTOs.UserDtos;
using EasyShop.DTOs.DTOs.CategoryDtos;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category, CategoryListDto>().ReverseMap();

            //CreateMap<SubCategory, GetSubCategoryByCategoryIdDto>()
            //    .ForMember(x=>x.SubCategoryName, y=>y.MapFrom(z=>z.Name))
            //    .ForMember(x=>x.CategoryName, y=>y.MapFrom(z=> z.Category.Name));

            CreateMap<Product, ProductListDto>().ReverseMap();

            CreateMap<Product, ProductStockDto>()
                 .ForMember(c => c.CategoryName, y => y.MapFrom(z => z.SubCategory.Category.Name))
                 //.ForMember(p => p.StockQuantity, y => y.MapFrom(z => z.StockQuantity))
                 .ForMember(s => s.SubCategoryId, y => y.MapFrom(z => z.SubCategoryId));


            CreateMap<Product, RecentProductDto>()
                .ForMember(x => x.Price, y => y.MapFrom(z => z.ProductPrices.FirstOrDefault(p => p.IsCurrent).Price))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                .ForMember(x => x.ImageUrl, y => y.MapFrom(z => z.ImageUrl));

            CreateMap<ProductDetail, ProductDetailsDto>().ReverseMap();

            CreateMap<UserProfileUpdateDto, AuthUser>().ReverseMap();

            CreateMap<ProductImage, ProductImageDto>().ReverseMap();
            CreateMap<ProductImage, ProductImageCreateDto>().ReverseMap();  
        }
    }
}
