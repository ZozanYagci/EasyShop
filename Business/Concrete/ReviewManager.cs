using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ReviewManager : IReviewService
    {
        public Task AddAsync(Review entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> GetAllAsync(bool noTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Review entity)
        {
            throw new NotImplementedException();
        }
    }
}
