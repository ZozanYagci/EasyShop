using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.DataAccess.GenericRepository;
using DataAccess.Abstract;
using DTOs.DTOs.SubCategoryDtos;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSubCategoryDal : GenericRepository<Entities.Concrete.SubCategory, Context>, ISubCategoryDal
    {
        private readonly Context dbContext;
     


        public EfSubCategoryDal(Context context) : base(context)
        {
        
            dbContext = context;
        }

        public async Task<List<GetSubCategoryByCategoryIdDto>> GetSubCategoryByCategoryIdAsync()
        {
            var subCategory = dbContext.Categories
                .Join(dbContext.SubCategories, c => c.Id, s => s.CategoryId, (c, s) => new GetSubCategoryByCategoryIdDto
                {
                    CategoryName = c.Name,
                    SubCategoryName = s.Name,
                    CategoryId = s.CategoryId

                });
                         
            return subCategory.ToList();
            //return await subCategory.ProjectTo<GetSubCategoryByCategoryIdDto>(mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
