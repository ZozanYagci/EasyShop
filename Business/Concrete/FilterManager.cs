﻿using Business.Abstract;
using DataAccess.Abstract;

using DTOs.DTOs.FilterDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FilterManager : IFilterService
    {
        private readonly IFilterDal filterDal;

        public FilterManager(IFilterDal filterDal)
        {
            this.filterDal = filterDal;
        }
       
        

        public async Task<List<ProductsDto>> GetFilteredProductsListAsync(FilterRequestDto filterRequest)
        {
            return await filterDal.GetFilteredProductsAsync(filterRequest);
        }

        public async Task<List<FilterValueDto>> GetFilterValueAsync()
        {
            return await filterDal.GetAllFilterValuesAsync();
        }
    }
}
