using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.DataAccess.GenericRepository;
using DataAccess.Abstract;
using DTOs.DTOs.ProductDtos;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<ProductWithPricesDto>> GetProductWithPricesAsync()
        {

            var productWithPrices = await dbContext.Products
                .Select(p => new ProductWithPricesDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,

                    Prices = p.ProductPrices.Select(pp => new ProductPriceDto
                    {
                        Price = pp.Price,
                        IsCurrent = pp.IsCurrent,
                        EffectiveDate = pp.EffectiveDate,
                    }).ToList()
                }).ToListAsync();

            return productWithPrices;
        }
        public async Task<List<RecentProductDto>> GetRecentProductAsync()
        {
            var recentProducts = await dbContext.Products
                .Include(p => p.ProductPrices)
                .OrderByDescending(p => p.CreatedAt)
                .Take(10)

                .ToListAsync();

            return mapper.Map<List<RecentProductDto>>(recentProducts);   //AutoMapper
        }
    }
}
