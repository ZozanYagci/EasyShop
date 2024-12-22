using Core.DataAccess.EntityFramework;
using DTOs.DTOs.ProductDtos;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductDal:IGenericRepository<Product>
    {
        Task<List<ProductStockDto>> GetProductStockAsync();
        Task<List<RecentProductDto>> GetRecentProductAsync();
        Task<List<ProductWithPricesDto>> GetProductWithPricesAsync();
    }
}
