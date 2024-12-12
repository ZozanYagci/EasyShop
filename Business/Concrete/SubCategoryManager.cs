using Business.Abstract;
using DataAccess.Abstract;
using DTOs.DTOs.SubCategoryDtos;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SubCategoryManager : ISubCategoryService
    {
        private readonly ISubCategoryDal subCategoryDal;

        public SubCategoryManager(ISubCategoryDal subCategoryDal)
        {
            this.subCategoryDal = subCategoryDal;
        }

        public Task AddAsync(SubCategory entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SubCategory>> GetAllAsync(bool noTracking = true)
        {
            return await subCategoryDal.GetAll(noTracking);
        }

        public Task<SubCategory> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetSubCategoryByCategoryIdDto>> GetSubCategoryByCategoryIdAsync()
        {
            return await subCategoryDal.GetSubCategoryByCategoryIdAsync();  
        }

        public Task UpdateAsync(SubCategory entity)
        {
            throw new NotImplementedException();
        }
    }
}
