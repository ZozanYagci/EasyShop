
using DTOs.DTOs.FilterDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFilterService
    {
        Task<List<FilterValueDto>> GetFilterValueAsync();
        Task<List<ProductsDto>> GetFilteredProductsListAsync(FilterRequestDto filterRequest);
    }
}
