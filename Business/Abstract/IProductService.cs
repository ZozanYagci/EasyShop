using DTOs.DTOs.ProductDtos;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService : IGenericService<Product>
    {
        Task<List<ProductStockDto>> GetProductStockAsync();
        Task<List<RecentProductDto>> GetRecentProductAsync();
    }
}
