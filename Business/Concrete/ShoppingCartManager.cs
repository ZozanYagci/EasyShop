using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ShoppingCartManager : IShoppingCartService
    {
        public Task AddAsync(ShoppingCart entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ShoppingCart>> GetAllAsync(bool noTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingCart> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ShoppingCart entity)
        {
            throw new NotImplementedException();
        }
    }
}
