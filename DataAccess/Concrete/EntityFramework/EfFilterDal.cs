using DataAccess.Abstract;
using DTOs.DTOs.FilterAttributes;
using DTOs.DTOs.FilterDtos;
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
        public async Task<List<ColorListDto>> GetColorListAsync()
        {
            var colors = await dbContext.Attributes
                 .Where(a => a.Name == "Renk")
                 .Include(a => a.AttributeValues)
                 .Select(a => new ColorListDto
                 {
                     ColorValue = a.AttributeValues.Select(av => av.Value).ToList()

                 }).ToListAsync();

            return colors;
        }

        public async Task<List<ComponentListDto>> GetComponentListAsync()
        {
            var components = await dbContext.Attributes
                 .Where(a => a.Name == "Materyal")
                 .Include(a => a.AttributeValues)
                 .Select(a => new ComponentListDto
                 {
                     ComponentValue = a.AttributeValues.Select(av => av.Value).ToList()

                 }).ToListAsync();

            return components;
        }

        public async Task<List<PriceRangeListDto>> GetPriceRangeListAsync()
        {
            var priceRange = await dbContext.Attributes
                     .Where(a => a.Name == "Fiyat")
                     .Include(a => a.AttributeValues)
                     .Select(a => new PriceRangeListDto
                     {
                         PriceRangeValue = a.AttributeValues.Select(av => av.Value).ToList()

                     }).ToListAsync();

            return priceRange;
        }

        public async Task<List<SizeListDto>> GetSizeListAsync()
        {
            var sizes = await dbContext.Attributes
               .Where(a => a.Name == "Beden")
               .GroupJoin(dbContext.AttributeValues, a => a.Id, av => av.AttributeId, (a, avs) => new SizeListDto()
               {
                   SizeValue = avs.Select(av => av.Value).ToList()

               }).ToListAsync();

            return sizes;
        }


        public async Task<List<ProductsDto>> GetFilteredProductsAsync(FilterRequestDto filterRequest)
        {
            //lazy loading

            var query = dbContext.Products
                .Include(p => p.ProductAttributes)
                .ThenInclude(pa => pa.AttributeValue)
                .ThenInclude(av => av.Attribute)
                .AsQueryable();


            //Renk filtreleme

            if (filterRequest.Colors != null && filterRequest.Colors.Any())
            {
                query = query.Where(p => p.ProductAttributes
                .Any(pa => pa.AttributeValue.Attribute.Name == "Renk" &&
                filterRequest.Colors.Contains(pa.AttributeValue.Value)));
            }

            //Materyal filtreleme

            if (filterRequest.Components != null && filterRequest.Components.Any())
            {
                query = query.Where(p => p.ProductAttributes
                .Any(pa => pa.AttributeValue.Attribute.Name == "Materyal" &&
                filterRequest.Components.Contains(pa.AttributeValue.Value)));
            }

            //Beden filtreleme

            if (filterRequest.Sizes != null && filterRequest.Sizes.Any())
            {
                query = query.Where(p => p.ProductAttributes
                .Any(pa => pa.AttributeValue.Attribute.Name == "Beden" &&
                filterRequest.Sizes.Contains(pa.AttributeValue.Value)));
            }

            //Fiyat aralığı filtreleme

            if (filterRequest.MinPrice.HasValue && filterRequest.MaxPrice.HasValue)
            {
                query = query.Where(p => dbContext.ProductPrices
                .Where(pp => pp.ProductId == p.Id)
                .Any(pp => pp.Price >= filterRequest.MinPrice && pp.Price <= filterRequest.MaxPrice));
            }



            //Filtre uygulanmışsa filtreli ürünleri, uygulanmamışsa tüm ürünleri getirecek.
            var products = await query.Select(p => new ProductsDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = dbContext.ProductPrices
                .Where(pp => pp.ProductId == p.Id && pp.IsCurrent == true)
                .Select(pp => pp.Price).FirstOrDefault(),

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
