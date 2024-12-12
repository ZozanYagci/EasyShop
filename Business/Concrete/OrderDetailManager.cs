using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderDetailManager : IOrderDetailService
    {
        public Task AddAsync(OrderDetail entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderDetail>> GetAllAsync(bool noTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetail> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(OrderDetail entity)
        {
            throw new NotImplementedException();
        }
    }
}
