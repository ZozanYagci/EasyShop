using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        public Task AddAsync(Payment entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Payment>> GetAllAsync(bool noTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<Payment> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Payment entity)
        {
            throw new NotImplementedException();
        }
    }
}
