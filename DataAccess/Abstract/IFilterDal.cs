
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
        Task<List<FilterValueDto>> GetAllFilterValuesAsync();
        Task<List<ProductsDto>> GetFilteredProductsAsync(FilterRequestDto filterRequest);
    }
}
