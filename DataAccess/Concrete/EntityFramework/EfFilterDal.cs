using DataAccess.Abstract;
using DTOs.DTOs.FilterDtos;
using DTOs.DTOs.ProductDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfFilterDal : IFilterDal
    {

        private readonly Context dbContext;

        public EfFilterDal(Context context)
        {
            dbContext = context;
        }

        public async Task<List<FilterValueDto>> GetAllFilterValuesAsync()
        {
            var filterValues = await dbContext.Attributes
                .Include(a => a.AttributeValues)
                .Select(a => new FilterValueDto
                {
                    FilterName = a.Name,
                    Values = a.AttributeValues.Select(av => av.Value).ToList()
                }).ToListAsync();

            return filterValues;
        }

        public async Task<List<ProductsDto>> GetFilteredProductsAsync(FilterRequestDto filterRequest)
        {


            var query = dbContext.Products
                .AsNoTracking()
                .Include(p => p.ProductPrices)
                .Include(p => p.ProductAttributes)
                     .ThenInclude(pa => pa.AttributeValue)
                     .ThenInclude(av => av.Attribute)
                .AsQueryable();


            //Renk filtreleme

            if (filterRequest.Colors?.Any() == true)
            {
                query = query.Where(p => p.ProductAttributes
                .Any(pa => pa.AttributeValue.Attribute.Name == "Renk" &&
                filterRequest.Colors.Contains(pa.AttributeValue.Value)));
            }

            //Materyal filtreleme

            if (filterRequest.Components?.Any() == true)
            {
                query = query.Where(p => p.ProductAttributes
                .Any(pa => pa.AttributeValue.Attribute.Name == "Materyal" &&
                filterRequest.Components.Contains(pa.AttributeValue.Value)));
            }

            //Beden filtreleme

            if (filterRequest.Sizes?.Any() == true)
            {
                query = query.Where(p => p.ProductAttributes
                .Any(pa => pa.AttributeValue.Attribute.Name == "Beden" &&
                filterRequest.Sizes.Contains(pa.AttributeValue.Value)));
            }

            //Fiyat aralığı filtreleme

            if (filterRequest.MinPrice.HasValue && filterRequest.MaxPrice.HasValue)
            {
                query = query.Where(p => p.ProductPrices
                //.Where(pp => pp.ProductId == p.Id)
                .Any(pp => pp.IsCurrent && pp.Price >= filterRequest.MinPrice && pp.Price <= filterRequest.MaxPrice));
            }


            //sorting
            switch (filterRequest.SortBy)
            {
                case "price_asc":
                    query = query.OrderBy(p => p.ProductPrices.FirstOrDefault(pp => pp.IsCurrent).Price);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(p => p.ProductPrices.FirstOrDefault(pp => pp.IsCurrent).Price);
                    break;
                case "newest":
                    query = query.OrderByDescending(p => p.CreatedAt);
                    break;
                default:
                    query = query.OrderBy(p => p.Name);
                    break;

            }


            //Filtre uygulanmışsa filtreli ürünleri, uygulanmamışsa tüm ürünleri getirecek.
            var products = await query.Select(p => new ProductsDto
            {
                Id = p.Id,
                Name = p.Name,

                Prices = p.ProductPrices.Select(pp => new ProductPriceDto
                {
                    Price = pp.Price,
                    IsCurrent = pp.IsCurrent,
                    EffectiveDate = pp.EffectiveDate,
                }).ToList(),

                Attributes = p.ProductAttributes.Select(pa => new ProductAttributeDto
                {
                    AttributeName = pa.AttributeValue.Attribute.Name,
                    Value = pa.AttributeValue.Value
                }).ToList()
            }).ToListAsync();

            return products;
        }
    }
}
