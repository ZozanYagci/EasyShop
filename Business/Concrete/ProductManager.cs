using Business.Abstract;
using DataAccess.Abstract;
using DTOs.DTOs.ProductDtos;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {

        private readonly IProductDal productDal;

        public ProductManager(IProductDal productDal)
        {
            this.productDal = productDal;
        }

        public Task AddAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetAllAsync(bool noTracking = true)
        {

           return await productDal.GetAll(noTracking);
        }

        public Task<Product> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductStockDto>> GetProductStockAsync()
        {
            return await productDal.GetProductStockAsync();
        }

        public async Task<List<ProductWithPricesDto>> GetProductWithPricesAsync()
        {
            return await productDal.GetProductWithPricesAsync();
        }

        public async Task<List<RecentProductDto>> GetRecentProductAsync()
        {
            return await productDal.GetRecentProductAsync();
        }

        public Task UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
