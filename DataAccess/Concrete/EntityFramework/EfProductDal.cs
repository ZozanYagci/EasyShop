using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.DataAccess.GenericRepository;
using DataAccess.Abstract;
using DTOs.DTOs.ProductDtos;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : GenericRepository<Product, Context>, IProductDal

    {
        private readonly Context dbContext;
        private readonly IMapper mapper;

        public EfProductDal(Context context, IMapper mapper) : base(context)
        {
            dbContext = context;
            this.mapper = mapper;
        }

        public async Task<List<ProductStockDto>> GetProductStockAsync()
        {
            //var productStocks = await dbContext.Products
            //    .Where(p => dbContext.SubCategories.Any(sc => sc.Id == p.SubCategoryId))
            //    .ProjectTo<ProductStockDto>(mapper.ConfigurationProvider)
            //    .ToListAsync();


            var productStocks = dbContext.Categories
                .Select(c => new ProductStockDto
                {
                    CategoryName = c.Name,
                    SubCategoryId = c.Id,
                    StockQuantity = c.SubCategories
                    .SelectMany(sc => sc.Products)
                    .Sum(p => p.StockQuantity)
                }).ToList();

            return productStocks;
        }

        public async Task<List<RecentProductDto>> GetRecentProductAsync()
        {
            var recentProducts = await dbContext.Products
                .OrderByDescending(p => p.CreatedAt)
                .Take(10)
                //.Select(p => new RecentProductDto
                //{
                //    Name= p.Name,
                //    Price= p.Price,
                //    ImageUrl= p.ImageUrl

                //})
                .ToListAsync();

            return mapper.Map<List<RecentProductDto>>(recentProducts);   //AutoMapper
        }
    }
}
