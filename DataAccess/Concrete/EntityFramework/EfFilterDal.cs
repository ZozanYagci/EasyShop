using DataAccess.Abstract;
using DTOs.DTOs.FilterAttributes;
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
            dbContext=context;
        }
        public async Task<List<ColorListDto>> GetColorListAsync()
        {
            var colors = await dbContext.Attributes
                 .Where(a => a.Name == "Renk")
                 .Include(a=>a.AttributeValues)
                 .Select(a=> new ColorListDto
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
                     .Where(a => a.Name == "Price")
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
    }
}
