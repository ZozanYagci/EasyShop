﻿using DTOs.DTOs.FilterAttributes;
using DTOs.DTOs.FilterDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IFilterDal
    {
        Task<List<ColorListDto>> GetColorListAsync();
        Task<List<SizeListDto>> GetSizeListAsync();
        Task<List<ComponentListDto>> GetComponentListAsync();
        Task<List<PriceRangeListDto>> GetPriceRangeListAsync();
        Task<List<ProductsDto>> GetFilteredProductsAsync(FilterRequestDto filterRequest);
    }
}
